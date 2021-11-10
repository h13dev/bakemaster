using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Tritronix.BakingOven
{
    /// <summary>
    /// Diese Klasse beinhaltet wichtige Methoden zur Kommunikation 
    /// mit der Backofensteuerung.
    /// Sie wartet auf Daten im Puffer und konvertiert empfangene bzw 
    /// zu sendende Daten. 
    /// </summary>
    public class ControlAdapter 
    {
        ////////////////////////
        //FIELDS
        SerialPort comPort;

        Thread thread;    //Extrathread für das Empfangen bzw. Senden von Daten

        BakingProgram[] bakingProgArray;  //Speichert die Backprogramme 
        ConfigDatas configDatas;          //Speichert die Konfigurationen
        string[] languageDatas;           //Speichert die empfangenen Sprachdaten

        #region Delegates + Eventhandler
        public delegate void ReceivedAllDataEvent();
        public delegate void TransmittedAllDataEvent();
        public delegate void TransmitOneBackingDataEvent();
        public delegate void ReceiveWaitForBackingDatasEvent();
        public delegate void TransmissionErrorEvent(string message);

        /// <summary>
        /// Wird ausgelöst, sobald die Daten vollständig empfangen wurden (Backdaten+Sprachdatei)
        /// </summary>
        public event ReceivedAllDataEvent ReceivedAllData;


        /// <summary>
        /// Wird ausgelöst, sobald die Daten vollständig gesendet wurden (Backdaten+Sprachdatei)
        /// </summary>
        public event TransmittedAllDataEvent TransmittedAllData;

        /// <summary>
        /// Wird bei der Übertragung ausgelöst (ControlAdapter.Transmit), pro übertragenes Backprogramm bzw. Sprachdatei einmal
        /// </summary>
        public event TransmitOneBackingDataEvent TransmitOneBackingData;

        /// <summary>
        /// Wird bei der Übertragung ausgelöst (ControlAdapter.Receive), pro Zeiteinheit(100 ms) einmal
        /// </summary>
        public event ReceiveWaitForBackingDatasEvent ReceiveWaitForBackingDatas;

        /// <summary>
        /// Wird ausgelöst sobald ein Fehler während der Übertragung auftritt
        /// </summary>
        public event TransmissionErrorEvent TransmissionError;
        #endregion

        ////////////////////////
        //CONSTRUCTOR
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="portName">Port welches verwendet werden soll(zB.: COM1)</param>
        /// <param name="baudRate">Anzahl der Bits welche pro Sekunde übertragen werden</param>
        /// <param name="parity">Kontrollroutine ein/aus</param>
        /// <param name="dataBits">Anzahl der Datenbits</param>
        /// <param name="stopBits">Anzahl der Stopbits</param>
        public ControlAdapter(string portName, int baudRate, Parity parity
                              , int dataBits, StopBits stopBits)
        {
            try
            {
                this.comPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// Verbindung beenden, wenn gerade keine Daten empfangen bzw übertragen werden
        /// </summary>
        public void DisConnect()
        {
            //Falls der Thread noch läuft
            try
            {
                if (this.thread != null)
                {
                    if (this.thread.IsAlive == true) throw new OwnExceptions("Es werden zur Zeit noch Daten übertragen!");
                }
                if(this.comPort.IsOpen) this.comPort.Close();
            }
            catch (Exception ex) { throw ex; }          
        }

        /// <summary>
        /// Verbindung beenden auch wenn gerade Daten empfangen bzw übertragen werden
        /// </summary>
        public void Exit()
        {
            if (this.thread != null)
            {
                if (this.thread.IsAlive == true) this.thread.Abort();
            }
            if (this.comPort.IsOpen) this.comPort.Close();
        }

        /// <summary>
        /// Fordert den Microcontroller auf, die aktuellen Daten zu übertragen 
        /// </summary>
        public void CallForDatas()
        {
            //Falls der Thread noch läuft
            if (this.thread != null)
            {
                if (this.thread.IsAlive) throw new OwnExceptions("Es werden zur Zeit noch Daten übertragen!");
            }

            try
            {
                //Comport schließen
                if (this.comPort.IsOpen) this.comPort.Close();
                Thread.Sleep(100);

                //Übertragung Starten
                this.comPort.Open();
                this.thread = new Thread(new ThreadStart(Receive));
                this.thread.Start();
                this.comPort.Write("#");
            }
            catch (Exception ex)
            {
                if (this.comPort.IsOpen) this.comPort.Close();

                //Übertragungserrorevent auslösen
                if (TransmissionError != null) TransmissionError(ex.Message);
            }      
        }



        /// <summary>
        /// Überträgt alle Daten an die Steuerung
        /// </summary>
        public void TransmitDatas(BakingProgram[] objArray, string[] _languageDatas, ConfigDatas _configDatas)
        {

            //Falls der Thread noch läuft
            if (this.thread != null)
            {
                if (this.thread.IsAlive) throw new OwnExceptions("Es werden zur Zeit noch Daten übertragen!");
            }

            try
            {
                //Comport schließen
                if (this.comPort.IsOpen) this.comPort.Close();
                Thread.Sleep(100);

                //Übertragung Starten
                this.comPort.Open();

                //Daten an den Adapter übergeben
                this.bakingProgArray = objArray;
                this.languageDatas = _languageDatas;
                this.configDatas = _configDatas;

                this.thread = new Thread(new ThreadStart(Transmit));
                this.thread.Start();
            }
            catch (Exception ex) 
            {
                if (this.comPort.IsOpen) this.comPort.Close();

                //Übertragungserrorevent auslösen
                if (TransmissionError != null) TransmissionError(ex.Message);
            }
        }


        /// <summary>
        /// Alle empfangenen Backprogramme auslesen
        /// </summary>
        /// <returns></returns>
        public BakingProgram[] GetBakingProgramms()
        {
            if (this.bakingProgArray == null) throw new OwnExceptions("Es sind keine Backdaten vorhanden!");
            return this.bakingProgArray;
        }

        /// <summary>
        /// Alle empfangenen Konfigurationsdaten auslesen
        /// </summary>
        /// <returns></returns>
        public ConfigDatas GetConfigDatas()
        {
            if (this.configDatas == null) throw new OwnExceptions("Es sind keine Konfigurationsdaten vorhanden!");
            return this.configDatas;
        }


        /// <summary>
        /// Alle empfangenen Sprachdaten auslesen
        /// </summary>
        /// <returns></returns>
        public string[] GetLanguageDatas()
        {
            if (this.languageDatas == null) throw new OwnExceptions("Es sind keine Backdaten vorhanden!");
            return this.languageDatas;
        }


        /// <summary>
        /// Überträgt alle Daten an die Steuerung
        /// </summary>
        /// <param name="objArray"></param>
        private void Transmit()
        {
            byte[] stream;                //Im stream werden die zu sendenden Daten gespeichert
            int streamCounter;            //Hilfsvariable

            try
            {
                //Input-Puffer leeren
                this.comPort.DiscardInBuffer();

                //Kontrolliern ob die Steuerung bereit ist Daten zu empfangen
                this.comPort.Write("%");
                CheckTransmission();

                #region Konfigdaten übertragen

                stream = configDatas.GetByteStream();
                /*for (int i = 0; i < 256; i++)
                {
                    comPort.Write(stream, i, i+1);
                    Thread.Sleep(1);
                    
                }*/
                foreach (byte b in stream)
                {
                    comPort.Write(new byte[] {b}, 0, 1);
                    Thread.Sleep(1);
                    Console.WriteLine("Config");
                    Console.WriteLine(b);
                }
                //comPort.Write(stream, 0, stream.Length);

                CheckTransmission();

                #endregion

                #region BACKPROGRAMME ÜBERTRAGEN
                foreach (BakingProgram obj in this.bakingProgArray)
                {
                    //BakingProgram in einen für die Steuerung gültigen Stream konvertieren
                    stream = obj.GetByteStream();

                    //1 Backprogramm übertragen + auf Kontrollzeichen der Steuerung warten
                    /*for(int i = 0;i<1920;i++)
                    {
                        comPort.Write(stream,i,i+1);
                        Thread.Sleep(1);
                    }*/
                    foreach (byte b in stream)
                    {
                        comPort.Write(new byte[] { b }, 0, 1);
                        Thread.Sleep(1);
                        Console.WriteLine("Programme");
                        Console.WriteLine(b);
                    }
                    //comPort.Write(stream, 0, stream.Length);
                    CheckTransmission();

                    //Event auslösen, ein Backprogramm wurde übertragen
                    if (TransmitOneBackingData != null) TransmitOneBackingData();
                }
                #endregion

                #region SPRACHDATEI ÜBERTRAGEN
                int count = 1;
                foreach (string str in this.languageDatas)
                {
                    stream = new byte[17]; //In stream werden die zu sendenden Daten gespeicher
                    streamCounter = 0;     //Zähler für jedes einzelne Zeichen pro Wort

                    //1 Wort vom Typ "string" in ein byteArray konvertieren
                    foreach (byte objByte in str)
                    {
                        stream[streamCounter++] = objByte;

                        //Maximal 16 Zeichen
                        if (streamCounter == 16) break;
                    }

                    //Leerzeichen am Ende des Wortes anhängen bis die geforderte Zeichenanzahl(16 ohne TermZeichen) erreicht wurde
                    if (str.Length < 16)
                    {
                        for (int i = 0; i < str.Length - 16; i++)
                        {
                            stream[streamCounter++] = 32;
                        }
                    }

                    //Terminierungszeichen anhängen
                    if(str.Length == 16) stream[streamCounter++] = 0;
                    if (str.Length < 17 || str.Length > 17) Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");


                    //Wort übertragen und auf eine Antwort der Steuerung warten
                    /*for (int i = 0; i < 17; i++)
                    {
                        comPort.Write(stream, i, i + 1);
                        Thread.Sleep(1);
                    }*/
 
                    foreach (byte b in stream)
                    {
                        comPort.Write(new byte[] { b }, 0, 1);
                        Thread.Sleep(1);
                        Console.WriteLine("Sprachdatei");
                        Console.WriteLine(b);
                        Console.WriteLine(count);
                        count++;
                    }
                    //comPort.Write(stream, 0, stream.Length);
                    CheckTransmission();

                    //Event auslösen, eine Sprachdatei wurde übertragen
                    if (TransmitOneBackingData != null) TransmitOneBackingData();
                }
                #endregion

                //Event auslösen
                if (TransmittedAllData != null) TransmittedAllData();
            }
            catch (Exception ex)
            {
                //Übertragungserrorevent auslösen
                if (TransmissionError != null) TransmissionError(ex.Message);
                if (this.comPort.IsOpen) this.comPort.Close();
                if (this.thread.IsAlive) this.thread.Abort();
            }
        }


        /// <summary>
        /// Dient zum Empfangen aller Daten 
        /// </summary>
        private void Receive()
        {
            try
            {
                //Warten bis alle Daten im Puffer angekommen sind
                for (int i = 0; i < 30; i++)
                {
                    Thread.Sleep(100);

                    //Event auslösen, ein Backprogramm wurde empfangen
                    if (ReceiveWaitForBackingDatas != null) ReceiveWaitForBackingDatas();

                    //Falls nach 2 Sekunden noch keine Daten vorhanden sind, wird der Vorgang beended
                    if (i == 20 && this.comPort.BytesToRead == 0) throw new OwnExceptions("Übertragung Fehlgeschlagen, Die Steuerung reagiert nicht!");
                }


                //Puffer auslesen + ausgelesene Daten Konvertieren

                //Array für die Backprogramme initialisieren (30 Backprogramme)
                this.bakingProgArray = new BakingProgram[30];
                //StringArray für die Sprachdaten initialisieren
                this.languageDatas = new string[57];

                StringBuilder cache = new StringBuilder();
                StringBuilder unusedTemp = new StringBuilder(); //Wird verwendet um einen Teil der Konfiguarationsdaten zu speichern

                int counter = 0; //Dient als Unterscheider(zwischen int,char,string) der auszulesenden Pufferdaten
                int pos = 0;     //Hilfsvariable für sämtliche Arraypositionen            


                //Kontrolle ob wirklich alle Bytes angekommen sind
                if (this.comPort.BytesToRead != 3145) throw new OwnExceptions("Übertragung Fehlgeschlagen, es wurden nicht alle Daten empfangen!");

                #region Konfigurationsdatei empfangen

                //Bytes welche nicht verwendet werden, in unusedTemp speichern
                for (int i = 0; i < 16; i++)
                {
                    unusedTemp.Append(',');  //unusedTemp wirdn anschließend an cache angehängt
                    unusedTemp.Append(this.comPort.ReadByte());
                }

                //Backvorgang auslesen
                cache.Append(this.comPort.ReadByte() + this.comPort.ReadByte() * 256 + this.comPort.ReadByte()* 65536);
                cache.Append(',');


                //Bytes welche nicht verwendet werden, in unusedTemp speichern
                for (int i = 0; i < 13; i++)
                {
                    unusedTemp.Append(',');
                    unusedTemp.Append(this.comPort.ReadByte());
                }

                //Betriebsstundenzähler
                for (int i = 0; i < 2; i++)
                {
                    cache.Append(this.comPort.ReadByte());
                    cache.Append(',');
                }

                cache.Append(this.comPort.ReadByte() + this.comPort.ReadByte() * 256);
                cache.Append(',');

                cache.Append(this.comPort.ReadByte());
                cache.Append(',');

                //Filterwarnung
                cache.Append(this.comPort.ReadByte());
                cache.Append(',');

                //Bytes welche nicht verwendet werden, in unusedTemp speichern
                for (int i = 0; i < 2; i++)
                {
                    unusedTemp.Append(',');
                    unusedTemp.Append(this.comPort.ReadByte());
                }

                //Fehlercodeszähler 
                for (int i = 0; i < 2; i++)
                {
                    cache.Append(this.comPort.ReadByte());
                    cache.Append(',');
                }

                //Bytes welche nicht verwendet werden, in unusedTemp speichern
                for (int i = 0; i < 6; i++)
                {
                    unusedTemp.Append(',');
                    unusedTemp.Append(this.comPort.ReadByte());
                }

                //Zustandsspeicher Backofen
                for (int i = 0; i < 7; i++)
                {
                    cache.Append(this.comPort.ReadByte());
                    cache.Append(',');
                }


                //Bytes welche nicht verwendet werden, in unusedTemp speichern
                for (int i = 0; i < 3; i++)
                {
                    unusedTemp.Append(',');
                    unusedTemp.Append(this.comPort.ReadByte());
                }


                //Auswahl Ofentyp
                for (int i = 0; i < 3; i++)
                {
                    cache.Append(this.comPort.ReadByte());
                    cache.Append(',');
                }

                //Bytes welche nicht verwendet werden, in unusedTemp speichern
                for (int i = 0; i < 3; i++)
                {
                    unusedTemp.Append(',');
                    unusedTemp.Append(this.comPort.ReadByte());
                }


                //Ofeneinstellung
                for (int i = 0; i < 17; i++)
                {
                    cache.Append(this.comPort.ReadByte());
                    cache.Append(',');
                }

                //Bytes welche nicht verwendet werden, in unusedTemp speichern
                unusedTemp.Append(',');
                unusedTemp.Append(this.comPort.ReadByte());

                cache.Append(this.comPort.ReadByte());
                cache.Append(',');


                //Bytes welche nicht verwendet werden, in unusedTemp speichern
                for (int i = 0; i < 170; i++)
                {
                    unusedTemp.Append(',');
                    unusedTemp.Append(this.comPort.ReadByte());
                }

                //Einstellungsversion
                cache.Append(this.comPort.ReadByte());
                cache.Append(',');
                cache.Append(this.comPort.ReadByte());

                //Bytes welche nicht verwendet werden, in unusedTemp speichern
                unusedTemp.Append(',');
                unusedTemp.Append(this.comPort.ReadByte());

                //Nicht verwendeten Daten anhängen..
                cache.Append(unusedTemp);

                //ConfigDataObject instanzieren
                this.configDatas = new ConfigDatas(Convert.ToString(cache), 1);
                #endregion

                cache = new StringBuilder();

                #region Backdaten empfangen
                while (this.comPort.BytesToRead > 969)
                {
                    if (counter > 2 && counter < 19)
                    {
                        cache.Append(Convert.ToChar(this.comPort.ReadByte()));
                        if (counter == 18) //Zeichenkette fertig eingelesen
                        {
                            cache.Append(',');
                            //Terminierungszeichen überspringen
                            this.comPort.ReadByte();
                            counter++;
                        }
                    }
                    else
                    {
                        if (counter % 16 > 3 && counter % 16 < 12 && counter != 54)
                        {
                            //Einlesen von Integerwerten(2 Byte)
                            //High- und Low-Byte werden zusammengefügt
                            cache.Append(this.comPort.ReadByte() + this.comPort.ReadByte() * 256);
                            cache.Append(',');
                            counter++;
                        }
                        else
                        {
                            //Einlesen von Character
                            cache.Append(this.comPort.ReadByte());
                            cache.Append(',');
                        }
                    }
                    if (counter++ > 53)
                    {
                        //FillUp-Zeichenkette überspringen
                        counter = 0;
                        for (int i = 0; i < 9; i++) this.comPort.ReadByte();
                        //Zwischenspeicher in den Puffer speichern
                        this.bakingProgArray[pos++] = new BakingProgram(Convert.ToString(cache), 1);

                        //Leerzeicehn trimmen
                        this.bakingProgArray[pos - 1].Backware_Name = this.bakingProgArray[pos - 1].Backware_Name.Trim();

                        //Zwischenspeicher leeren
                        cache.Remove(0, cache.Length);
                    }

                }
                #endregion

                pos = 0;

                #region Sprachdatei empfangen
                while (this.comPort.BytesToRead > 0)
                {
                    //Ein Wort lesen..
                    for (int i = 0; i < 17; i++) cache.Append(Convert.ToChar(this.comPort.ReadByte()));

                    //Zwischenspeicher in den Puffer speichern
                    this.languageDatas[pos++] = Convert.ToString(cache);

                    //Zwischenspeicher leeren
                    cache.Remove(0, cache.Length);
                }
                #endregion
            }
            catch (Exception ex)
            {
                //Übertragungserrorevent auslösen
                if (TransmissionError != null) TransmissionError(ex.Message);
                if (this.comPort.IsOpen) this.comPort.Close();
                if (this.thread.IsAlive) this.thread.Abort();
            }

            //Event auslösen, alle Daten wurden empfangen
            if (ReceivedAllData != null) ReceivedAllData();
        }

        /// <summary>
        /// Verarbeitet sämtliche Kontrollzeichen der Steuerung
        /// </summary>
        private void CheckTransmission()
        {
            int timeout = 0; //Dient zum Methodenabbruch falls ein Lesevorgang nicht beendet wird

            while (this.comPort.BytesToRead == 0)
            {
                Thread.Sleep(200);
                //Nach 10 Sekunden erfolgt der Abbruch
                if (timeout++ >= 20) throw new OwnExceptions("Timeout Error, die Steuerung reagiert nicht");
            }

            Thread.Sleep(100);//Falls mehrere Zeichen zu empfangen sind
            this.comPort.DiscardInBuffer(); //Input-Puffer leeren
        }

        #region PROPERTIES
        //PROPERTIES
        /// <summary>
        /// Liest oder setzt die Portbezeichnung (zB.: COM1)
        /// </summary>
        public string PortName
        {
            set
            {
                if (this.comPort.IsOpen) throw new OwnExceptions("Der Portname konnte nicht verändert werden!");
                this.comPort.PortName = value;
            }
            get { return this.comPort.PortName; }
        }

        /// <summary>
        /// Liest oder setzt die Baudrate
        /// </summary>
        public int BaudRate
        {
            set
            {
                if (this.comPort.IsOpen) throw new OwnExceptions("Die Baudrate konnte nicht verändert werden!");
                this.comPort.BaudRate = value;
            }
            get { return this.comPort.BaudRate; }
        }

        /// <summary>
        /// Liest oder setzt die Paritätseigenschaften
        /// </summary>
        public Parity Parity
        {
            set
            {
                if (this.comPort.IsOpen) throw new OwnExceptions("Parität konnte nicht verändert werden!");
                this.comPort.Parity = value;
            }
            get { return this.comPort.Parity; }
        }

        /// <summary>
        /// Liest oder setzt die Anzahl der Datenbits
        /// </summary>
        public int DataBits
        {
            set
            {
                if (this.comPort.IsOpen) throw new OwnExceptions("Die Anzahl der Datenbits konnte nicht verändert werden!");
                this.comPort.DataBits = value;
            }
            get { return this.comPort.DataBits; }
        }

        /// <summary>
        /// Liest oder setzt die Anzahl der Stopbits
        /// </summary>
        public StopBits StopBits
        {
            set
            {
                if (this.comPort.IsOpen) throw new OwnExceptions("Die Anzahl der Stopbits konnte nicht verändert werden!");
                this.comPort.StopBits = value;
            }
            get { return this.comPort.StopBits; }
        }

        /// <summary>
        /// Gibt den Verbindungsstatus zurück
        /// </summary>
        public bool IsConnected
        {
            get { return this.comPort.IsOpen; }
        }
        #endregion
    }
}
