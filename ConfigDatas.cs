using System;
using System.Collections.Generic;
using System.Text;

namespace Tritronix.BakingOven
{
    public class ConfigDatas
    {
        ////////////////////////
        //FIELDS

        //Unbenutzte Daten
        byte[] unusedDatas;

        //READONLY (nur anzeigen)
        //Anzahl Backvorgänge (3 Byte)
        uint numberOfBakingActs;

        //Betriebsstundenzähle 
        byte betrieb_Sek;
        byte betrieb_Min;
        ushort betrieb_Std;        //High+Lowbyte
        byte betrieb_Overflow;

        //Filterwarnung
        byte betrieb_Warn_Cnt; 
        

        //Fehlercodeszähler für Menü
        byte code_Error_Cnt;
        byte code_Error_Wait;

        //Zustandsspeicher Backofen
        byte letzter_State;
        byte letzte_Backware;
        byte[] restzeit_Min;       //1-5

        //Einstellungsversion
        byte setup_Code_1;
        byte setup_Code_2;



        //WRITE AND READ
        //Auswahl Ofentyp
        byte company;                // 0/1,     0...Gassner,     1...Rein & Co
        byte flaps;                  // 0/1,     0...no Flaps,    1...Flaps
        byte big_Type;               // 0/1,     0...normal,      1...big

        //Ofeneinstellung
        byte kontrast;               // 0...40
        byte summer;                 // 0...15
        byte ende_Beep;              // 0...100  (100ms)
        byte beschwandung_Pause;     // 1...60   (Sekunden)
        byte beschwandung_Menge;     // 1...20   
        byte coolDown_180;           // 0...100  (Grad °C)  
        byte coolDown_200;           // 0...100  (Grad °C)  
        byte zeit_Klappe;            // 0...60   (Sekunden)  
        byte ext_Summer;             // 0...2     0...intern,   1...intern & extern,    2...extern   
        byte wassersensor;           // 0/1       0...aus,      1...ein
        byte autostart;              // 0/1       0...aus,      1...ein
        byte door_Active;            // 0/1       0...aus,      1...ein
        byte zusatzheizung;          // 0/1       0...aus,      1...ein
        byte energiesaving_Min;      // 1...30   (Minuten)
        byte energiesaving_Temp;     // 80...150 (Grad °C) 
        byte temp_Korr;              // 0...40   (Grad °C) 
        byte anzahl_Backwaren;       // 1...30  
        byte power_Reduction;        // 0...10 

        ////////////////////////
        //CONSTRUCTOR
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="stream">Zeichenkette welche alle gültigen Parameter eines  
        /// BakingProgram Objektes enthält. Die Parameter müssen durch einen 
        /// ',' getrennt werden</param>
        /// <param name="check">1..Empfangene Daten von Steurung, Für sonnstige Fälle wählen sie eine beliebige Zahl</param>
        public ConfigDatas(string stream, byte check)
        {
            string[] temp = stream.Split(',');

            #region Felder initialisieren
            if (check == 1)
            {
                this.numberOfBakingActs = Convert.ToUInt32(temp[0]);

                this.betrieb_Sek = Convert.ToByte(temp[1]);
                this.betrieb_Min = Convert.ToByte(temp[2]);
                this.betrieb_Std = Convert.ToUInt16(temp[3]);
                this.betrieb_Overflow = Convert.ToByte(temp[4]);

                this.betrieb_Warn_Cnt = Convert.ToByte(temp[5]);

                this.code_Error_Cnt = Convert.ToByte(temp[6]);
                this.code_Error_Wait = Convert.ToByte(temp[7]);

                this.letzter_State = Convert.ToByte(temp[8]);
                this.letzte_Backware = Convert.ToByte(temp[9]);
                this.restzeit_Min = new byte[] {Convert.ToByte(temp[10]),
                                                   Convert.ToByte(temp[11]),
                                                   Convert.ToByte(temp[12]),
                                                   Convert.ToByte(temp[13]),
                                                   Convert.ToByte(temp[14])};

                this.company = Convert.ToByte(temp[15]);
                this.flaps = Convert.ToByte(temp[16]);
                this.big_Type = Convert.ToByte(temp[17]);

                this.kontrast = Convert.ToByte(temp[18]);
                this.summer = Convert.ToByte(temp[19]);
                this.ende_Beep = Convert.ToByte(temp[20]);
                this.beschwandung_Pause = Convert.ToByte(temp[21]);
                this.beschwandung_Menge = Convert.ToByte(temp[22]);
                this.coolDown_180 = Convert.ToByte(temp[23]);
                this.coolDown_200 = Convert.ToByte(temp[24]);
                this.zeit_Klappe = Convert.ToByte(temp[25]);
                this.ext_Summer = Convert.ToByte(temp[26]);
                this.wassersensor = Convert.ToByte(temp[27]);
                this.autostart = Convert.ToByte(temp[28]);
                this.door_Active = Convert.ToByte(temp[29]);
                this.zusatzheizung = Convert.ToByte(temp[30]);
                this.energiesaving_Min = Convert.ToByte(temp[31]);
                this.energiesaving_Temp = Convert.ToByte(temp[32]);
                this.temp_Korr = Convert.ToByte(temp[33]);
                this.anzahl_Backwaren = Convert.ToByte(temp[34]);
                this.power_Reduction = Convert.ToByte(temp[35]);


                this.setup_Code_1 = Convert.ToByte(temp[36]);
                this.setup_Code_2 = Convert.ToByte(temp[37]);

                //Nicht verwendeten Daten
                this.unusedDatas = new byte[215];
                for (int i = 38; i < 253; i++) { this.unusedDatas[i - 38] = Convert.ToByte(temp[i]);}
            }
            #endregion
        }

        ////////////////////////
        //METHODS

        /// <summary>
        /// Konvertiert die ConfigDatas in einen für die Steuerung passenden Stream
        /// </summary>
        /// <returns></returns>
        public byte[] GetByteStream()
        {
            //Konvertierungsrichtline laut Programmcode der Steuerung...

            byte[] stream = new byte[256]; //Im stream werden die zu sendenden Daten gespeichert
            int streamCounter = 0;
            int unusedCounter = 0;

            //Unused Datas
            for (int i = 0; i < 16; i++) stream[streamCounter++] = unusedDatas[unusedCounter++];

            //Backvorgänge
            stream[streamCounter++] = (byte)(this.numberOfBakingActs % 256);           //Low     Byte
            stream[streamCounter++] = (byte)((this.numberOfBakingActs / 256) % 256);   //High    Byte
            stream[streamCounter++] = (byte)(this.numberOfBakingActs / 65536);         //Highest Byte

            //Unused Datas
            for (int i = 0; i < 13; i++) stream[streamCounter++] = unusedDatas[unusedCounter++];

            //Betriebsstundenzähler
            stream[streamCounter++] = this.betrieb_Sek;
            stream[streamCounter++] = this.betrieb_Min;
            stream[streamCounter++] = (byte)(this.betrieb_Std % 256);           //Low     Byte
            stream[streamCounter++] = (byte)(this.betrieb_Std / 256);           //High    Byte
            stream[streamCounter++] = this.betrieb_Overflow;

            //Filterwarnung
            stream[streamCounter++] = this.betrieb_Warn_Cnt;

            //Unused Datas
            for (int i = 0; i < 2; i++) stream[streamCounter++] = unusedDatas[unusedCounter++];

            //Fehlercodezähler
            stream[streamCounter++] = this.code_Error_Cnt;
            stream[streamCounter++] = this.code_Error_Wait;

            //Unused Datas 
            for (int i = 0; i < 6; i++) stream[streamCounter++] = unusedDatas[unusedCounter++];


            //Zustandsspeicher Backofen
            stream[streamCounter++] = this.letzter_State;
            stream[streamCounter++] = this.letzte_Backware;
            for (int i = 0; i < 5; i++) stream[streamCounter++] = this.restzeit_Min[i];

            //Unused Datas
            for (int i = 0; i < 3; i++) stream[streamCounter++] = unusedDatas[unusedCounter++];


            //Auswahl Ofentyp
            stream[streamCounter++] = this.company;
            stream[streamCounter++] = this.flaps;
            stream[streamCounter++] = this.big_Type;

            //Unused Datas
            for (int i = 0; i < 3; i++) stream[streamCounter++] = unusedDatas[unusedCounter++];


            //Ofeneinstellungen
            stream[streamCounter++] = this.kontrast;
            stream[streamCounter++] = this.summer;
            stream[streamCounter++] = this.ende_Beep;
            stream[streamCounter++] = this.beschwandung_Pause;
            stream[streamCounter++] = this.beschwandung_Menge;
            stream[streamCounter++] = this.coolDown_180;
            stream[streamCounter++] = this.coolDown_200;
            stream[streamCounter++] = this.zeit_Klappe;
            stream[streamCounter++] = this.ext_Summer;
            stream[streamCounter++] = this.wassersensor;
            stream[streamCounter++] = this.autostart;
            stream[streamCounter++] = this.door_Active;
            stream[streamCounter++] = this.zusatzheizung;
            stream[streamCounter++] = this.energiesaving_Min;
            stream[streamCounter++] = this.energiesaving_Temp;
            stream[streamCounter++] = this.temp_Korr;
            stream[streamCounter++] = this.anzahl_Backwaren;

            //Unused Datas
            stream[streamCounter++] = unusedDatas[unusedCounter++];

            stream[streamCounter++] = this.power_Reduction;

            //Unused Datas
            for (int i = 0; i < 170; i++) stream[streamCounter++] = unusedDatas[unusedCounter++];

            //Einstellungsversion
            stream[streamCounter++] = this.setup_Code_1;
            stream[streamCounter++] = this.setup_Code_2;


            //Unused Datas
            stream[streamCounter++] = unusedDatas[unusedCounter++];

            return stream;
        }

        #region Checkroutinen

        private byte CheckCompany(byte b)
        {
            if (b != 0 && b != 1) throw new OwnExceptions("Ungültiger Company-Konfiguration!");
            return b;
        }

        private byte CheckFlaps(byte b)
        {
            if (b != 0 && b != 1) throw new OwnExceptions("Ungültiger Flaps-Konfiguration!");
            return b;
        }

        private byte CheckBig_Type(byte b)
        {
            //if (b != 0 && b != 1) throw new OwnExceptions("Ungültiger Big_Type-Konfiguration!");
            return b;
        }

        private byte CheckKontrast(byte b)
        {
            if (b < 0  || b > 40) throw new OwnExceptions("Ungültiger Kontrast-Konfiguration!");
            return b;
        }

        private byte CheckSummer(byte b)
        {
            if (b < 0 || b > 15) throw new OwnExceptions("Ungültiger Summer-Konfiguration!");
            return b;
        }

        private byte CheckEnde_Beep(byte b)
        {
            if (b < 0 || b > 100) throw new OwnExceptions("Ungültiger Ende_Beep-Konfiguration!");
            return b;
        }

        private byte CheckBeschwandung_Pause(byte b)
        {
            if (b < 1 || b > 60) throw new OwnExceptions("Ungültiger Beschwandung_Pause-Konfiguration!");
            return b;
        }

        private byte CheckBeschwandung_Menge(byte b)
        {
            if (b < 1 || b > 20) throw new OwnExceptions("Ungültiger Beschwandung_Menge-Konfiguration!");
            return b;
        }

        private byte CheckCoolDown_180(byte b)
        {
            if (b < 0 || b > 100) throw new OwnExceptions("Ungültiger CoolDown_180-Konfiguration!");
            return b;
        }

        private byte CheckCoolDown_200(byte b)
        {
            if (b < 0 || b > 100) throw new OwnExceptions("Ungültiger CoolDown_200-Konfiguration!");
            return b;
        }

        private byte CheckZeit_Klappe(byte b)
        {
            if (b < 0 || b > 60) throw new OwnExceptions("Ungültiger Zeit_Klappe-Konfiguration!");
            return b;
        }

        private byte CheckExt_Summer(byte b)
        {
            if (b < 0 || b > 2) throw new OwnExceptions("Ungültiger Ext_Summer-Konfiguration!");
            return b;
        }

        private byte CheckWassersensor(byte b)
        {
            if (b != 0 && b != 1) throw new OwnExceptions("Ungültiger Wassersensor-Konfiguration!");
            return b;
        }

        private byte CheckAutostart(byte b)
        {
            if (b != 0 && b != 1) throw new OwnExceptions("Ungültiger Autostart-Konfiguration!");
            return b;
        }

        private byte CheckDoor_Active(byte b)
        {
            if (b != 0 && b != 1) throw new OwnExceptions("Ungültiger Door_Active-Konfiguration!");
            return b;
        }

        private byte CheckZusatzheizung(byte b)
        {
            if (b != 0 && b != 1) throw new OwnExceptions("Ungültiger Zusatzheizung-Konfiguration!");
            return b;
        }

        private byte CheckEnergiesaving_Min(byte b)
        {
            if (b < 1 || b > 30) throw new OwnExceptions("Ungültiger Energiesaving_Min-Konfiguration!");
            return b;
        }

        private byte CheckEnergiesaving_Temp(byte b)
        {
            if (b < 50 || b > 100) throw new OwnExceptions("Ungültiger Energiesaving_Temp-Konfiguration!");
            return b;
        }

        private byte CheckTemp_Korr(byte b)
        {
            if (b < 0 || b > 40) throw new OwnExceptions("Ungültiger Temp_Korr-Konfiguration!");
            return b;
        }

        private byte CheckAnzahl_Backwaren(byte b)
        {
            if (b < 1 || b > 30) throw new OwnExceptions("Ungültiger Anzahl_Backwaren-Konfiguration!");
            return b;
        }

        private byte CheckPower_Reduction(byte b)
        {
            if (b < 0 || b > 10) throw new OwnExceptions("Ungültiger Power_Reduction-Konfiguration!");
            return b;
        }
        #endregion

        #region PROPERTIES

        //READONLY
        public uint NumberOfBakingActs
        {
            get { return this.numberOfBakingActs; }
        }

        public byte Betrieb_Sek
        {
            get { return this.betrieb_Sek; }
        }

        public byte Betrieb_Min
        {
            get { return this.betrieb_Min; }
        }

        public ushort Betrieb_Std
        {
            get { return this.betrieb_Std; }
        }

        public byte Betrieb_Overflow
        {
            get { return this.betrieb_Overflow; }
        }

        public byte Betrieb_Warn_Cnt
        {
            get { return this.betrieb_Warn_Cnt; }
        }

        public byte Code_Error_Wait
        {
            get { return this.code_Error_Wait; }
        }

        public byte Code_Error_Cnt
        {
            get { return this.code_Error_Cnt; }
        }

        public byte Letzter_State
        {
            get { return this.letzter_State; }
        }

        public byte Letzte_Backware
        {
            get { return this.letzte_Backware; }
        }

        public byte[] Restzeit_Min
        {
            get { return this.restzeit_Min; }
        }

        public byte Setup_Code_1
        {
            get { return this.setup_Code_1; }
        }

        public byte Setup_Code_2
        {
            get { return this.setup_Code_2; }
        }



        //READ AND WRITE
        public byte Company
        {
            get { return this.company; }
            set { this.company = CheckCompany(value); }
        }

        public byte Flaps
        {
            get { return this.flaps; }
            set { this.flaps = CheckFlaps(value); }
        }

        public byte Big_Type
        {
            get { return this.big_Type; }
            set { this.big_Type = CheckBig_Type(value); }
        }

        public byte Kontrast
        {
            get { return this.kontrast; }
            set { this.kontrast = CheckKontrast(value); }
        }

        public byte Summer
        {
            get { return this.summer; }
            set { this.summer = CheckSummer(value); }
        }

        public byte Ende_Beep
        {
            get { return this.ende_Beep; }
            set { this.ende_Beep = CheckEnde_Beep(value); }
        }

        public byte Beschwandung_Pause
        {
            get { return this.beschwandung_Pause; }
            set { this.beschwandung_Pause = CheckBeschwandung_Pause(value); }
        }

        public byte Beschwandung_Menge
        {
            get { return this.beschwandung_Menge; }
            set { this.beschwandung_Menge = CheckBeschwandung_Menge(value); }
        }

        public byte CoolDown_180
        {
            get { return this.coolDown_180; }
            set { this.coolDown_180 = CheckCoolDown_180(value); }
        }

        public byte CoolDown_200
        {
            get { return this.coolDown_200; }
            set { this.coolDown_200 = CheckCoolDown_200(value); }
        }

        public byte Zeit_Klappe
        {
            get { return this.zeit_Klappe; }
            set { this.zeit_Klappe = CheckZeit_Klappe(value); }
        }

        public byte Ext_Summer
        {
            get { return this.ext_Summer; }
            set { this.ext_Summer = CheckExt_Summer(value); }
        }

        public byte Wassersensor
        {
            get { return this.wassersensor; }
            set { this.wassersensor = CheckWassersensor(value); }
        }

        public byte Autostart
        {
            get { return this.autostart; }
            set { this.autostart = CheckAutostart(value); }
        }

        public byte Door_Active
        {
            get { return this.door_Active; }
            set { this.door_Active = CheckDoor_Active(value); }
        }

        public byte Zusatzheizung
        {
            get { return this.zusatzheizung; }
            set { this.zusatzheizung = CheckZusatzheizung(value); }
        }

        public byte Energiesaving_Min
        {
            get { return this.energiesaving_Min; }
            set { this.energiesaving_Min = CheckEnergiesaving_Min(value); }
        }

        public byte Energiesaving_Temp
        {
            get { return this.energiesaving_Temp; }
            set { this.energiesaving_Temp = CheckEnergiesaving_Temp(value); }
        }

        public byte Temp_Korr
        {
            get { return this.temp_Korr; }
            set { this.temp_Korr = CheckTemp_Korr(value); }
        }

        public byte Anzahl_Backwaren
        {
            get { return this.anzahl_Backwaren; }
            set { this.anzahl_Backwaren = CheckAnzahl_Backwaren(value); }
        }

        public byte Power_Reduction
        {
            get { return this.power_Reduction; }
            set { this.power_Reduction = CheckPower_Reduction(value); }
        }
        #endregion
    }
}
