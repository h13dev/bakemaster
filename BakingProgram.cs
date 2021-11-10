using System;

namespace Tritronix.BakingOven
{
    public class BakingProgram
    {
        ////////////////////////
        //FIELDS
        // Grundeinstellungen
        byte vorheiztemp;      //40 - 230
        byte abkuehltemp;      //40 - 230
        byte grundmenge;       //0 - 10

        // Name Backware
        string backware_name;  //Besteht aus 16 Buchstaben

        // Backzeit Phase 1-4
        ushort[] p_zeit;       //0:00 - 59:30 in 30-Sekunden-Schritten

        // Backtemperatur Phase 1-4
        byte[] p_temp;         //40 - 230

        // Beschwadungsmenge Phase 1-4 
        byte[] p_menge;        //0 - 5

        // Beschwadungsdauer Phase 1-4  
        ushort[] p_dauer;      //0:00 - 59:30 in 30-Sekunden-Schritten

        // Ventilatorleistung Phase 1-4
        byte[] p_prozent;      //50 / 100

        // Klappenstellung Phase 1-4
        byte[] p_klappe;       //0 / 1

        // Ventilatorstopp Phase 1
        ushort p_stop;         //0:00 - 2:00 in 30-Sekunden-Schritten

        // Soft-Heat Phase 1
        byte p_softheat;       //30 - 100

        ////////////////////////
        //CONSTRUCTOR
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="stream">Zeichenkette welche alle gültigen Parameter eines  
        /// BakingProgram Objektes enthält. Die Parameter müssen durch einen 
        /// ',' getrennt werden</param>
        /// <param name="check">1..Empfangene Daten von Steurung, Für sonnstige Fälle eine wählen sie eine beliebige Zahl</param>
        public BakingProgram(string stream, byte check)
        {
            //Falls die Backdaten direkt von der Steuerung übertragen werde, sollen sie nicht überprüft werden

            string[] cache = stream.Split(',');

            #region Felder initialisieren
            if (check == 1)
            {
                this.vorheiztemp = Convert.ToByte(cache[0]);
                this.abkuehltemp = Convert.ToByte(cache[1]);
                this.grundmenge = Convert.ToByte(cache[2]);

                this.backware_name = cache[3];

                this.p_zeit = new ushort[]{Convert.ToUInt16(cache[4]),
                                       Convert.ToUInt16(cache[5]),
                                       Convert.ToUInt16(cache[6]),
                                       Convert.ToUInt16(cache[7])};

                this.p_temp = new byte[]{Convert.ToByte(cache[8]),
                                       Convert.ToByte(cache[9]),
                                       Convert.ToByte(cache[10]),
                                       Convert.ToByte(cache[11])};

                this.p_menge = new byte[] {Convert.ToByte(cache[12]),
                                       Convert.ToByte(cache[13]),
                                       Convert.ToByte(cache[14]),
                                       Convert.ToByte(cache[15])};

                this.p_dauer = new ushort[]{Convert.ToUInt16(cache[16]),
                                        Convert.ToUInt16(cache[17]),
                                        Convert.ToUInt16(cache[18]),
                                        Convert.ToUInt16(cache[19])};

                this.p_prozent = new byte[]{Convert.ToByte(cache[20]),
                                        Convert.ToByte(cache[21]),
                                        Convert.ToByte(cache[22]),
                                        Convert.ToByte(cache[23])};

                this.p_klappe = new byte[] {Convert.ToByte(cache[24]),
                                        Convert.ToByte(cache[25]),
                                        Convert.ToByte(cache[26]),
                                        Convert.ToByte(cache[27])};

                this.p_stop = Convert.ToUInt16(cache[28]);

                this.p_softheat = Convert.ToByte(cache[29]);
            }
            else
            {
                this.vorheiztemp = CheckTemp(Convert.ToByte(cache[0]));
                this.abkuehltemp = CheckTemp(Convert.ToByte(cache[1]));
                this.grundmenge = CheckGrundmenge(Convert.ToByte(cache[2]));

                this.backware_name = CheckName(cache[3]);

                this.p_zeit = new ushort[]{CheckTime(Convert.ToUInt16(cache[4])),
                                       CheckTime(Convert.ToUInt16(cache[5])),
                                       CheckTime(Convert.ToUInt16(cache[6])),
                                       CheckTime(Convert.ToUInt16(cache[7]))};

                this.p_temp = new byte[]  {CheckTemp(Convert.ToByte(cache[8])),
                                       CheckTemp(Convert.ToByte(cache[9])),
                                       CheckTemp(Convert.ToByte(cache[10])),
                                       CheckTemp(Convert.ToByte(cache[11]))};

                this.p_menge = new byte[] {CheckBeschwadung(Convert.ToByte(cache[12])),
                                       CheckBeschwadung(Convert.ToByte(cache[13])),
                                       CheckBeschwadung(Convert.ToByte(cache[14])),
                                       CheckBeschwadung(Convert.ToByte(cache[15]))};

                this.p_dauer = new ushort[]{CheckTime(Convert.ToUInt16(cache[16])),
                                        CheckTime(Convert.ToUInt16(cache[17])),
                                        CheckTime(Convert.ToUInt16(cache[18])),
                                        CheckTime(Convert.ToUInt16(cache[19]))};

                this.p_prozent = new byte[]{CheckVentilatorpower(Convert.ToByte(cache[20])),
                                        CheckVentilatorpower(Convert.ToByte(cache[21])),
                                        CheckVentilatorpower(Convert.ToByte(cache[22])),
                                        CheckVentilatorpower(Convert.ToByte(cache[23]))};

                this.p_klappe = new byte[] {CheckKlappe(Convert.ToByte(cache[24])),
                                        CheckKlappe(Convert.ToByte(cache[25])),
                                        CheckKlappe(Convert.ToByte(cache[26])),
                                        CheckKlappe(Convert.ToByte(cache[27]))};

                this.p_stop = CheckVentStop(Convert.ToUInt16(cache[28]));

                this.p_softheat = CheckSoftHeat(Convert.ToByte(cache[29]));
            }
          #endregion
        }


        ////////////////////////
        //METHODS

        /// <summary>
        /// Konvertiert ein BakingProgram in einen für die Steuerung passenden Stream
        /// </summary>
        /// <returns></returns>
        public byte[] GetByteStream()
        {
            //Konvertierungsrichtline laut Programmcode der Steuerung...

            byte[] stream = new byte[64]; //Im stream werden die zu sendenden Daten gespeichert
            int streamCounter = 0;

            stream[streamCounter++] = this.Vorheiztemp;
            stream[streamCounter++] = this.Abkuehltemp;
            stream[streamCounter++] = this.Grundmenge;

            foreach (byte objByte in this.Backware_Name) stream[streamCounter++] = objByte;

            //Leerzeichen anhängen
            while (streamCounter < 19) stream[streamCounter++] = 0x20;

            //Terminierungszeichen anhängen
            stream[streamCounter++] = 0;

            stream[streamCounter++] = (byte)(this.P_Zeit_Phase1 % 256); //Low Byte
            stream[streamCounter++] = (byte)(this.P_Zeit_Phase1 / 256); //High Byte
            stream[streamCounter++] = (byte)(this.P_Zeit_Phase2 % 256);
            stream[streamCounter++] = (byte)(this.P_Zeit_Phase2 / 256);
            stream[streamCounter++] = (byte)(this.P_Zeit_Phase3 % 256);
            stream[streamCounter++] = (byte)(this.P_Zeit_Phase3 / 256);
            stream[streamCounter++] = (byte)(this.P_Zeit_Phase4 % 256);
            stream[streamCounter++] = (byte)(this.P_Zeit_Phase4 / 256);

            stream[streamCounter++] = this.P_Temp_Phase1;
            stream[streamCounter++] = this.P_Temp_Phase2;
            stream[streamCounter++] = this.P_Temp_Phase3;
            stream[streamCounter++] = this.P_Temp_Phase4;

            stream[streamCounter++] = this.P_Menge_Phase1;
            stream[streamCounter++] = this.P_Menge_Phase2;
            stream[streamCounter++] = this.P_Menge_Phase3;
            stream[streamCounter++] = this.P_Menge_Phase4;

            stream[streamCounter++] = (byte)(this.P_Dauer_Phase1 % 256);
            stream[streamCounter++] = (byte)(this.P_Dauer_Phase1 / 256);
            stream[streamCounter++] = (byte)(this.P_Dauer_Phase2 % 256);
            stream[streamCounter++] = (byte)(this.P_Dauer_Phase2 / 256);
            stream[streamCounter++] = (byte)(this.P_Dauer_Phase3 % 256);
            stream[streamCounter++] = (byte)(this.P_Dauer_Phase3 / 256);
            stream[streamCounter++] = (byte)(this.P_Dauer_Phase4 % 256);
            stream[streamCounter++] = (byte)(this.P_Dauer_Phase4 / 256);

            stream[streamCounter++] = this.P_Prozent_Phase1;
            stream[streamCounter++] = this.P_Prozent_Phase2;
            stream[streamCounter++] = this.P_Prozent_Phase3;
            stream[streamCounter++] = this.P_Prozent_Phase4;

            stream[streamCounter++] = this.P_Klappe_Phase1;
            stream[streamCounter++] = this.P_Klappe_Phase2;
            stream[streamCounter++] = this.P_Klappe_Phase3;
            stream[streamCounter++] = this.P_Klappe_Phase4;

            stream[streamCounter++] = (byte)(this.P_Stop % 256);
            stream[streamCounter++] = (byte)(this.P_Stop / 256);

            stream[streamCounter++] = this.P_Softheat;

            //Fill Up to 64 Byte
            for (int i = 0; i < 9; i++) stream[streamCounter++] = 0;

            return stream;
        }


        #region Checkroutinen 
        private string CheckName(string name)
        {
            if (name.Length > 16) name.Remove(16);
            return name;
        }

        private byte CheckSoftHeat(byte heat)
        {
            if (heat < 30 || heat > 100) throw new OwnExceptions("Ungültiger Soft Heat Wert!");
            return heat;
        }

        private ushort CheckTime(ushort time)
        {
            if (time > 5930) throw new OwnExceptions("Ungültige Zeitangabe!");

            byte min = (byte)(time / 100);
            byte sek = (byte)(time - min * 100);

            //Runden Fall1:
            if (sek >= 45) time = (ushort)((min + 1) * 100);

            //Runden Fall2:
            if (sek < 45 && sek >= 15) time = (ushort)(min * 100 + 30);

            //Runden Fall3:
            if (sek < 15) time = (ushort)(min * 100);
            return time;
        }

        private byte CheckTemp(byte temp)
        {
            if (temp < 40 || temp > 230) throw new OwnExceptions("Ungültige Temperatur!");
            return temp;
        }

        private byte CheckGrundmenge(byte grund)
        {
            if (grund > 10) throw new OwnExceptions("Ungültige Grundmenge!");
            return grund;
        }

        private byte CheckBeschwadung(byte besch)
        {
            if (besch > 5) throw new OwnExceptions("Ungültige Beschwadungsmenge!");
            return besch;
        }

        private byte CheckVentilatorpower(byte vent)
        {
            if (vent != 50 && vent != 100) throw new OwnExceptions("Ungültige Ventilatorleistung!");
            return vent;
        }

        private byte CheckKlappe(byte klap)
        {
            if (klap != 0 && klap != 1) throw new OwnExceptions("Ungültige Klappeneinstellung!");
            return klap;
        }

        private ushort CheckVentStop(ushort time)
        {
            if (time > 200) throw new OwnExceptions("Ungültige Ventilatorstopp Zeitangabe!");

            byte min = (byte)(time / 100);
            byte sek = (byte)(time - min * 100);

            //Runden Fall1:
            if (sek >= 45) time = (ushort)((min + 1) * 100);

            //Runden Fall2:
            if (sek < 45 && sek >= 15) time = (ushort)(min * 100 + 30);

            //Runden Fall3:
            if (sek < 15) time = (ushort)(min * 100);

            return time;
        }
        #endregion


        #region PROPERTIES
        public byte Vorheiztemp
        {
            set { this.vorheiztemp = CheckTemp(value); }
            get { return this.vorheiztemp; }
        }
        public byte Abkuehltemp
        {
            set { this.abkuehltemp = CheckTemp(value); }
            get { return this.abkuehltemp; }
        }
        public byte Grundmenge
        {
            set { this.grundmenge = CheckGrundmenge(value); }
            get { return this.grundmenge; }
        }



        public string Backware_Name
        {
            set { this.backware_name = CheckName(value); }
            get { return this.backware_name; }
        }



        public ushort P_Zeit_Phase1
        {
            set { this.p_zeit[0] = CheckTime(value); }
            get { return this.p_zeit[0]; }
        }
        public ushort P_Zeit_Phase2
        {
            set { this.p_zeit[1] = CheckTime(value); }
            get { return this.p_zeit[1]; }
        }
        public ushort P_Zeit_Phase3
        {
            set { this.p_zeit[2] = CheckTime(value); }
            get { return this.p_zeit[2]; }
        }
        public ushort P_Zeit_Phase4
        {
            set { this.p_zeit[3] = CheckTime(value); }
            get { return this.p_zeit[3]; }
        }



        public byte P_Temp_Phase1
        {
            set { this.p_temp[0] = CheckTemp(value); }
            get { return this.p_temp[0]; }
        }
        public byte P_Temp_Phase2
        {
            set { this.p_temp[1] = CheckTemp(value); }
            get { return this.p_temp[1]; }
        }
        public byte P_Temp_Phase3
        {
            set { this.p_temp[2] = CheckTemp(value); }
            get { return this.p_temp[2]; }
        }
        public byte P_Temp_Phase4
        {
            set { this.p_temp[3] = CheckTemp(value); }
            get { return this.p_temp[3]; }
        }



        public byte P_Menge_Phase1
        {
            set { this.p_menge[0] = CheckBeschwadung(value); }
            get { return this.p_menge[0]; }
        }
        public byte P_Menge_Phase2
        {
            set { this.p_menge[1] = CheckBeschwadung(value); }
            get { return this.p_menge[1]; }
        }
        public byte P_Menge_Phase3
        {
            set { this.p_menge[2] = CheckBeschwadung(value); }
            get { return this.p_menge[2]; }
        }
        public byte P_Menge_Phase4
        {
            set { this.p_menge[3] = CheckBeschwadung(value); }
            get { return this.p_menge[3]; }
        }



        public ushort P_Dauer_Phase1
        {
            set { this.p_dauer[0] = CheckTime(value); }
            get { return this.p_dauer[0]; }
        }
        public ushort P_Dauer_Phase2
        {
            set { this.p_dauer[1] = CheckTime(value); }
            get { return this.p_dauer[1]; }
        }
        public ushort P_Dauer_Phase3
        {
            set { this.p_dauer[2] = CheckTime(value); }
            get { return this.p_dauer[2]; }
        }
        public ushort P_Dauer_Phase4
        {
            set { this.p_dauer[3] = CheckTime(value); }
            get { return this.p_dauer[3]; }
        }



        public byte P_Prozent_Phase1
        {
            set { this.p_prozent[0] = CheckVentilatorpower(value); }
            get { return this.p_prozent[0]; }
        }
        public byte P_Prozent_Phase2
        {
            set { this.p_prozent[1] = CheckVentilatorpower(value); }
            get { return this.p_prozent[1]; }
        }
        public byte P_Prozent_Phase3
        {
            set { this.p_prozent[2] = CheckVentilatorpower(value); }
            get { return this.p_prozent[2]; }
        }
        public byte P_Prozent_Phase4
        {
            set { this.p_prozent[3] = CheckVentilatorpower(value); }
            get { return this.p_prozent[3]; }
        }




        public byte P_Klappe_Phase1
        {
            set { this.p_klappe[0] = CheckKlappe(value); }
            get { return this.p_klappe[0]; }
        }
        public byte P_Klappe_Phase2
        {
            set { this.p_klappe[1] = CheckKlappe(value); }
            get { return this.p_klappe[1]; }
        }
        public byte P_Klappe_Phase3
        {
            set { this.p_klappe[2] = CheckKlappe(value); }
            get { return this.p_klappe[2]; }
        }
        public byte P_Klappe_Phase4
        {
            set { this.p_klappe[3] = CheckKlappe(value); }
            get { return this.p_klappe[3]; }
        }




        public ushort P_Stop
        {
            set { this.p_stop = CheckVentStop(value); }
            get { return this.p_stop; }
        }

        public byte P_Softheat
        {
            set { this.p_softheat = CheckSoftHeat(value); }
            get { return this.p_softheat; }
        }
        #endregion
    }
}
