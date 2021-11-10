using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Collections;
using Microsoft.Win32;
using System.Management;
using System.Drawing.Printing;

namespace Tritronix.BakingOven
{
    public partial class Form1 : Form
    {      
        //////////////////
        //FIELDS
        ControlAdapter connection;

        //Für Progressbar beim Senden/Empfangen
        ProgressbarForm transForm;

        //Array in welchen die Backprogramme gespeichert werden
        BakingProgram[] bakingPorgArr;

        //Speicher für die Konfigurationsdaten
        ConfigDatas configDatas;

        //Beinhaltet alle backingProgArr Positionen, die beim Öffnen einer Backprogrammdatei (.bak) Warning Exception auslösen
        //Erst wenn die Arraylist leer ist können Daten an die Steurung geschickt werden
        ArrayList bakingPorgWarningList;

        //Arrayposition des aktuell angezeigten Backprogrammes
        int indexTreeView = -1; 

        //Arrayposition des ausgewählten Wortes von der Sprachdatei
        int indexLanguage = -1;

        int curpage = 0;

        string[] seclanguage;

        //////////////////
        //CONSTRUCTOR
        public Form1(string security)
        {
            InitializeComponent();

            if (security == "1")
            {
                txtName.Visible = false;
                txtAbkühltemp.Visible = false;
                txtBacktemp1.Visible = false;
                txtBacktemp2.Visible = false;
                txtBacktemp3.Visible = false;
                txtBacktemp4.Visible = false;
                txtBackzeit1.Visible = false;
                txtBackzeit2.Visible = false;
                txtBackzeit3.Visible = false;
                txtBackzeit4.Visible = false;
                txtBeschwdauer1.Visible = false;
                txtBeschwdauer2.Visible = false;
                txtBeschwdauer3.Visible = false;
                txtBeschwdauer4.Visible = false;
                txtBeschwmenge1.Visible = false;
                txtBeschwmenge2.Visible = false;
                txtBeschwmenge3.Visible = false;
                txtBeschwmenge4.Visible = false;
                txtKlappenstellung1.Visible = false;
                txtKlappenstellung2.Visible = false;
                txtKlappenstellung3.Visible = false;
                txtKlappenstellung4.Visible = false;
                txtVentilatorleistung1.Visible = false;
                txtVentilatorleistung2.Visible = false;
                txtVentilatorleistung3.Visible = false;
                txtVentilatorleistung4.Visible = false;
                txtVorheiztemp.Visible = false;
                txtGrundmenge.Visible = false;
                txtSoftHeat.Visible = false;
                txtVentilatorStop.Visible = false;
                label1.Visible = false;
                label41.Visible = false;
                label3.Visible = false;
                label34.Visible = false;
                label2.Visible = false;
                label35.Visible = false;
                label4.Visible = false;
                label9.Visible = false;
                label14.Visible = false;
                label33.Visible = false;
                label28.Visible = false;
                label23.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                toolStripMenuItem1.Enabled = false;
                toolStripMenuItem2.Enabled = false;
                öffnenToolStripMenuItem.Enabled = false;
                speichernUnterToolStripMenuItem.Enabled = false;
                sprachdatenÖffnenToolStripMenuItem.Enabled = false;
                sprachdatenSpeichernUnterToolStripMenuItem.Enabled = false;
                configDatenOeffnen.Enabled = false;
                configDatenspeichern.Enabled = false;
            }
        }


        //////////////////
        //METHODES
        private void Form1_Load(object sender, EventArgs e)
        {
            #region COMBOBOXEN

            //COM_Ports auslesen..
            string[] portnames = SerialPort.GetPortNames();

            if (portnames != null)
            {
                //Ports sortieren (BubbleSort)..
                int n = portnames.Length; 
                bool check = true;
                string temp;

                while (check != false && n >= 1)
                {
                    check = false;
                    for (int i = 0; i < n - 1; i++)
                    {
                        if (Convert.ToInt32(portnames[i].Substring(3)) > Convert.ToInt32(portnames[i+1].Substring(3)))
                        {
                            temp = portnames[i];
                            portnames[i] = portnames[i + 1];
                            portnames[i + 1] = temp;
                            check = true;
                        }
                    }
                    n--;
                }

                if (portnames.Length > 0)
                {
                    cmBCOM_Ports.Text = portnames[0];
                    for (int i = 0; i < portnames.Length; i++)
                    {
                        cmBCOM_Ports.Items.Add(portnames[i]);
                    }
                }
            }

            cmBBaudrate.Items.Add("110");
            cmBBaudrate.Items.Add("300");
            cmBBaudrate.Items.Add("1200");
            cmBBaudrate.Items.Add("2400");
            cmBBaudrate.Items.Add("4800");
            cmBBaudrate.Items.Add("9600");
            cmBBaudrate.Items.Add("19200");
            cmBBaudrate.Items.Add("38400");
            cmBBaudrate.Items.Add("57600");
            cmBBaudrate.Items.Add("115200");
            cmBBaudrate.Items.Add("230400");
            cmBBaudrate.Items.Add("460800");
            cmBBaudrate.Items.Add("921600");
            cmBBaudrate.SelectedItem = cmBBaudrate.Items[6];

            cmBParity.Items.Add("Even");
            cmBParity.Items.Add("Mark");
            cmBParity.Items.Add("None");
            cmBParity.Items.Add("Odd");
            cmBParity.Items.Add("Space");
            cmBParity.SelectedItem = cmBParity.Items[2];

            cmBStopbit.Items.Add("1");
            cmBStopbit.Items.Add("2");
            cmBStopbit.SelectedItem = cmBStopbit.Items[0];

            cmBDatabits.Items.Add("5");
            cmBDatabits.Items.Add("6");
            cmBDatabits.Items.Add("7");
            cmBDatabits.Items.Add("8");
            cmBDatabits.SelectedItem = cmBDatabits.Items[3];
            #endregion

            #region REGISTRY

            //Name des Standartportes aus der Registry ermitteln
            RegistryKey Key = Registry.CurrentUser.OpenSubKey("Software\\Tritronix\\Bakemaster", true);

            try
            {
                for (int i = 0; i < cmBCOM_Ports.Items.Count; i++)
                {
                    if (portnames[i] == Key.GetValue("ComPort").ToString())
                    {
                        cmBCOM_Ports.SelectedIndex = i;
                    }
                }
                if (cmBCOM_Ports.Text == "") cmBCOM_Ports.SelectedIndex++;
            }
            catch
            {
                try
                {
                    cmBCOM_Ports.SelectedIndex = 0;
                    if (cmBCOM_Ports.Text == "") cmBCOM_Ports.SelectedIndex++;
                }
                catch
                {
                    MessageBox.Show("Fehler bei der Portbestimmung!\n Es sind keine Ports vorhanden.", "ERROR!", MessageBoxButtons.OK,
MessageBoxIcon.Error);
                    
                    Application.Exit();
                }
            }
            #endregion

            //connection-Object wird zwecks Eventhandler initialisiert
            this.connection = new ControlAdapter("COM1", 19200, Parity.Even, 8, StopBits.One);

            //Eventhandler initialisieren

            //Wird ausgelöst sobald alle Daten empfangen wurden
            this.connection.ReceivedAllData += new ControlAdapter.ReceivedAllDataEvent(DataReceived);

            //Wird ausgelöst sobald alle Daten übertragen wurden
            this.connection.TransmittedAllData += new ControlAdapter.TransmittedAllDataEvent(DataTransmitted);

            //Wird ausgelöst wenn während der Übertragung ein Fehler auftritt
            this.connection.TransmissionError += new ControlAdapter.TransmissionErrorEvent(TransmissionErrorCatch);

            //FÜR PROGRESSBAR
            //Wird ausgelöst sobald ein Backprogram bzw. ein Wort der Sprachdatei übertragen wurde
            this.connection.TransmitOneBackingData += new ControlAdapter.TransmitOneBackingDataEvent(TransmitBackingData);

            //Wird ausgelöst sobald ein Backprogram bzw. ein Wort der Sprachdatei empfangen wurde
            this.connection.ReceiveWaitForBackingDatas += new ControlAdapter.ReceiveWaitForBackingDatasEvent(ReceiveBackingData);
        }

        /// <summary>
        /// Bereitet die Übertragung vor indem das ControlAdapter Object seine Werte erhält bzw. eine 
        /// mögliche Verbindung mit der Steuerung geschlossen wird
        /// </summary>
        private void Connect()
        {
            //Falls gerade noch Daten übertragen werden...
            try
            {
                this.connection.DisConnect();
            }
            catch (OwnExceptions oex)
            {
                //Wird ausgelöst wenn noch Daten übertragen bzw. empfangen werden
                throw oex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #region ComboBoxwerte Konvertieren + Verbindung herstellen
            switch (this.cmBParity.Text)
            {
                case "Even":
                    {
                        switch (this.cmBStopbit.Text)
                        {
                            case "1":
                                {
                                    this.connection.BaudRate = Convert.ToInt32(this.cmBBaudrate.Text);
                                    this.connection.DataBits = Convert.ToInt32(this.cmBDatabits.Text);
                                    this.connection.PortName = this.cmBCOM_Ports.Text;
                                    this.connection.Parity = Parity.Even;
                                    this.connection.StopBits = StopBits.One;
                                    break;
                                }
                            case "2":
                                {
                                    this.connection.BaudRate = Convert.ToInt32(this.cmBBaudrate.Text);
                                    this.connection.DataBits = Convert.ToInt32(this.cmBDatabits.Text);
                                    this.connection.PortName = this.cmBCOM_Ports.Text;
                                    this.connection.Parity = Parity.Even;
                                    this.connection.StopBits = StopBits.Two;
                                    break;
                                }
                        }
                        break;
                    }
                case "Mark":
                    {
                        switch (cmBStopbit.Text)
                        {
                            case "1":
                                {
                                    this.connection.BaudRate = Convert.ToInt32(this.cmBBaudrate.Text);
                                    this.connection.DataBits = Convert.ToInt32(this.cmBDatabits.Text);
                                    this.connection.PortName = this.cmBCOM_Ports.Text;
                                    this.connection.Parity = Parity.Mark;
                                    this.connection.StopBits = StopBits.One;
                                    break;
                                }
                            case "2":
                                {
                                    this.connection.BaudRate = Convert.ToInt32(this.cmBBaudrate.Text);
                                    this.connection.DataBits = Convert.ToInt32(this.cmBDatabits.Text);
                                    this.connection.PortName = this.cmBCOM_Ports.Text;
                                    this.connection.Parity = Parity.Mark;
                                    this.connection.StopBits = StopBits.Two;
                                    break;
                                }
                        }
                        break;
                    }
                case "None":
                    {
                        switch (cmBStopbit.Text)
                        {
                            case "1":
                                {
                                    this.connection.BaudRate = Convert.ToInt32(this.cmBBaudrate.Text);
                                    this.connection.DataBits = Convert.ToInt32(this.cmBDatabits.Text);
                                    this.connection.PortName = this.cmBCOM_Ports.Text;
                                    this.connection.Parity = Parity.None;
                                    this.connection.StopBits = StopBits.One;
                                    break;
                                }
                            case "2":
                                {
                                    this.connection.BaudRate = Convert.ToInt32(this.cmBBaudrate.Text);
                                    this.connection.DataBits = Convert.ToInt32(this.cmBDatabits.Text);
                                    this.connection.PortName = this.cmBCOM_Ports.Text;
                                    this.connection.Parity = Parity.None;
                                    this.connection.StopBits = StopBits.Two;
                                    break;
                                }
                        }
                        break;
                    }
                case "Odd":
                    {
                        switch (cmBStopbit.Text)
                        {
                            case "1":
                                {
                                    this.connection.BaudRate = Convert.ToInt32(this.cmBBaudrate.Text);
                                    this.connection.DataBits = Convert.ToInt32(this.cmBDatabits.Text);
                                    this.connection.PortName = this.cmBCOM_Ports.Text;
                                    this.connection.Parity = Parity.Odd;
                                    this.connection.StopBits = StopBits.One;
                                    break;
                                }
                            case "2":
                                {
                                    this.connection.BaudRate = Convert.ToInt32(this.cmBBaudrate.Text);
                                    this.connection.DataBits = Convert.ToInt32(this.cmBDatabits.Text);
                                    this.connection.PortName = this.cmBCOM_Ports.Text;
                                    this.connection.Parity = Parity.Odd;
                                    this.connection.StopBits = StopBits.Two;
                                    break;
                                }
                        }
                        break;
                    }
                case "Space":
                    {
                        switch (cmBStopbit.Text)
                        {
                            case "1":
                                {
                                    this.connection.BaudRate = Convert.ToInt32(this.cmBBaudrate.Text);
                                    this.connection.DataBits = Convert.ToInt32(this.cmBDatabits.Text);
                                    this.connection.PortName = this.cmBCOM_Ports.Text;
                                    this.connection.Parity = Parity.Space;
                                    this.connection.StopBits = StopBits.One;
                                    break;
                                }
                            case "2":
                                {
                                    this.connection.BaudRate = Convert.ToInt32(this.cmBBaudrate.Text);
                                    this.connection.DataBits = Convert.ToInt32(this.cmBDatabits.Text);
                                    this.connection.PortName = this.cmBCOM_Ports.Text;
                                    this.connection.Parity = Parity.Space;
                                    this.connection.StopBits = StopBits.Two;
                                    break;
                                }
                        }
                        break;
                    }
            }
            #endregion
        }


        /// <summary>
        /// MenuItems welche für die Übertragung wichtig sind Enablen
        /// </summary>
        private void EnableMenuItems()
        {
            backdatenAbrufenToolStripMenuItem1.Enabled = true;
            backdatenSendenToolStripMenuItem1.Enabled = true;
            //öffnenToolStripMenuItem.Enabled = true;
            //speichernUnterToolStripMenuItem.Enabled = true;
            sprachdatenÖffnenToolStripMenuItem.Enabled = true;
            sprachdatenSpeichernUnterToolStripMenuItem.Enabled = true;
            neuToolStripMenuItem.Enabled = true;
            configDatenOeffnen.Enabled = true;
            configDatenspeichern.Enabled = true;
        }

        /// <summary>
        /// MenuItems welche für die Übertragung wichtig sind Disablen
        /// </summary>
        private void DisableMenuItems()
        {
            backdatenAbrufenToolStripMenuItem1.Enabled = false;
            backdatenSendenToolStripMenuItem1.Enabled = false;
            //öffnenToolStripMenuItem.Enabled = false;
            //speichernUnterToolStripMenuItem.Enabled = false;
            sprachdatenÖffnenToolStripMenuItem.Enabled = false;
            sprachdatenSpeichernUnterToolStripMenuItem.Enabled = false;
            neuToolStripMenuItem.Enabled = false;
            configDatenOeffnen.Enabled = false;
            configDatenspeichern.Enabled = false;
        }


        /// <summary>
        /// Alle Comboboxen der Konfiguration enablen
        /// </summary>
        private void EnableComboBoxes()
        {
            this.cmbAutostart.Enabled = true;
            //this.cmbBig_Type.Enabled = true;
            this.cmbCompany.Enabled = true;
            this.cmbDoorActive.Enabled = true;
            this.cmbExt_Summer.Enabled = true;
            this.cmbFlaps.Enabled = true;
            this.cmbWasserSensor.Enabled = true;
            this.cmbZusatzheizung.Enabled = true;
        }

        /// <summary>
        /// Alle Comboboxen der Konfiguration disablen
        /// </summary>
        private void DisableComboBoxes()
        {
            this.cmbAutostart.Enabled = false;
            //this.cmbBig_Type.Enabled = false;
            this.cmbCompany.Enabled = false;
            this.cmbDoorActive.Enabled = false;
            this.cmbExt_Summer.Enabled = false;
            this.cmbFlaps.Enabled = false;
            this.cmbWasserSensor.Enabled = false;
            this.cmbZusatzheizung.Enabled = false;
        }

        /// <summary>
        /// Alle Textboxen für das Backprogramm enablen
        /// </summary>
        private void EnableTextBoxes()
        {
            if (this.bakingPorgArr != null)
            {
                this.txtAbkühltemp.Enabled = true;

                this.txtBacktemp1.Enabled = true;
                this.txtBacktemp2.Enabled = true;
                this.txtBacktemp3.Enabled = true;
                this.txtBacktemp4.Enabled = true;

                this.txtBackzeit1.Enabled = true;
                this.txtBackzeit2.Enabled = true;
                this.txtBackzeit3.Enabled = true;
                this.txtBackzeit4.Enabled = true;

                this.txtBeschwdauer1.Enabled = true;
                this.txtBeschwdauer2.Enabled = true;
                this.txtBeschwdauer3.Enabled = true;
                this.txtBeschwdauer4.Enabled = true;

                this.txtBeschwmenge1.Enabled = true;
                this.txtBeschwmenge2.Enabled = true;
                this.txtBeschwmenge3.Enabled = true;
                this.txtBeschwmenge4.Enabled = true;

                this.txtGrundmenge.Enabled = true;

                this.txtKlappenstellung1.Enabled = true;
                this.txtKlappenstellung2.Enabled = true;
                this.txtKlappenstellung3.Enabled = true;
                this.txtKlappenstellung4.Enabled = true;

                this.txtName.Enabled = true;

                this.txtSoftHeat.Enabled = true;

                this.txtVentilatorleistung1.Enabled = true;
                this.txtVentilatorleistung2.Enabled = true;
                this.txtVentilatorleistung3.Enabled = true;
                this.txtVentilatorleistung4.Enabled = true;

                this.txtVentilatorStop.Enabled = true;

                this.txtVorheiztemp.Enabled = true;
            }

            //Konfiguration
            if (this.configDatas != null)
            {
                this.txtKontrast.Enabled = true;
                this.txtSummer.Enabled = true;
                this.txtEnde_Beep.Enabled = true;
                this.txtBeschwandungs_Pause.Enabled = true;
                this.txtBeschwandungs_Menge.Enabled = true;
                this.txtCoolDown_200.Enabled = true;
                this.txtCoolDown_180.Enabled = true;
                this.txtZeit_Klappe.Enabled = true;
                this.txtEnergieSaving_Min.Enabled = true;
                this.txtEnergiesaving_temp.Enabled = true;
                this.txtTemp_Korr.Enabled = true;
                this.txtAnzahl_Backwaren.Enabled = true;
                this.txtPowerReduction.Enabled = true;
            }
        }

        /// <summary>
        /// Alle Textboxen für das Backprogramm disablen
        /// </summary>
        private void DisableTextBoxes()
        {
            this.txtAbkühltemp.Enabled = false;

            this.txtBacktemp1.Enabled = false;
            this.txtBacktemp2.Enabled = false;
            this.txtBacktemp3.Enabled = false;
            this.txtBacktemp4.Enabled = false;

            this.txtBackzeit1.Enabled = false;
            this.txtBackzeit2.Enabled = false;
            this.txtBackzeit3.Enabled = false;
            this.txtBackzeit4.Enabled = false;

            this.txtBeschwdauer1.Enabled = false;
            this.txtBeschwdauer2.Enabled = false;
            this.txtBeschwdauer3.Enabled = false;
            this.txtBeschwdauer4.Enabled = false;

            this.txtBeschwmenge1.Enabled = false;
            this.txtBeschwmenge2.Enabled = false;
            this.txtBeschwmenge3.Enabled = false;
            this.txtBeschwmenge4.Enabled = false;

            this.txtGrundmenge.Enabled = false;

            this.txtKlappenstellung1.Enabled = false;
            this.txtKlappenstellung2.Enabled = false;
            this.txtKlappenstellung3.Enabled = false;
            this.txtKlappenstellung4.Enabled = false;

            this.txtName.Enabled = false;

            this.txtSoftHeat.Enabled = false;

            this.txtVentilatorleistung1.Enabled = false;
            this.txtVentilatorleistung2.Enabled = false;
            this.txtVentilatorleistung3.Enabled = false;
            this.txtVentilatorleistung4.Enabled = false;

            this.txtVentilatorStop.Enabled = false;

            this.txtVorheiztemp.Enabled = false;

            //Konfiguration
            this.txtKontrast.Enabled = false;
            this.txtSummer.Enabled = false;
            this.txtEnde_Beep.Enabled = false;
            this.txtBeschwandungs_Pause.Enabled = false;
            this.txtBeschwandungs_Menge.Enabled = false;
            this.txtCoolDown_200.Enabled = false;
            this.txtCoolDown_180.Enabled = false;
            this.txtZeit_Klappe.Enabled = false;
            this.txtEnergieSaving_Min.Enabled = false;
            this.txtEnergiesaving_temp.Enabled = false;
            this.txtTemp_Korr.Enabled = false;
            this.txtAnzahl_Backwaren.Enabled = false;
            this.txtPowerReduction.Enabled = false;
        }


        private void DeleteDesign()
        {
            #region Reset Textboxes

            //Backprogramm
            txtVorheiztemp.Text = "";
            txtAbkühltemp.Text = "";
            txtGrundmenge.Text = "";

            txtName.Text = "";

            txtBackzeit1.Text = "";
            txtBackzeit2.Text = "";
            txtBackzeit3.Text = "";
            txtBackzeit4.Text = "";

            txtBacktemp1.Text = "";
            txtBacktemp2.Text = "";
            txtBacktemp4.Text = "";
            txtBacktemp3.Text = "";

            txtBeschwmenge1.Text = "";
            txtBeschwmenge2.Text = "";
            txtBeschwmenge3.Text = "";
            txtBeschwmenge4.Text = "";

            txtBeschwdauer1.Text = "";
            txtBeschwdauer2.Text = "";
            txtBeschwdauer3.Text = "";
            txtBeschwdauer4.Text = "";

            txtVentilatorleistung1.Text = "";
            txtVentilatorleistung2.Text = "";
            txtVentilatorleistung3.Text = "";
            txtVentilatorleistung4.Text = "";

            txtKlappenstellung1.Text = "";
            txtKlappenstellung2.Text = "";
            txtKlappenstellung3.Text = "";
            txtKlappenstellung4.Text = "";

            txtVentilatorStop.Text = "";
            txtSoftHeat.Text = "";

            //Information
            txtNumberOfBakingActs.Text = "";
            txtBetrieb_Std.Text = "";
            txtBetrieb_Min.Text = "";
            txtBetrieb_Sek.Text = "";
            txtBetrieb_Overflow.Text = "";
            txtBetrieb_Warn_Cnt.Text = "";
            txtError_Cnt.Text = "";
            txtError_Wait.Text = "";
            txtLetzter_State.Text = "";
            txtLetzte_Backware.Text = ""; 
            txtRestzeit_Min1.Text = "";
            txtRestzeit_Min2.Text = "";
            txtRestzeit_Min3.Text = "";
            txtRestzeit_Min4.Text = "";
            txtRestzeit_Min5.Text = "";
            txtSetup_Code1.Text = "";
            txtSetup_Code2.Text = "";

            //Konfiguration
            txtKontrast.Text = "";
            txtSummer.Text = "";
            txtEnde_Beep.Text = "";
            txtBeschwandungs_Pause.Text = "";
            txtBeschwandungs_Menge.Text = "";
            txtCoolDown_200.Text = "";
            txtCoolDown_180.Text = "";
            txtZeit_Klappe.Text = "";
            txtEnergieSaving_Min.Text = "";
            txtEnergiesaving_temp.Text = "";
            txtTemp_Korr.Text = "";
            txtAnzahl_Backwaren.Text = "";
            txtPowerReduction.Text = "";

            #endregion

            //Reset TreeView..
            this.trVBackprogramme.Nodes[0].Nodes.Clear();

            //Reset Comboboxen von Konfiguration..
            this.cmbAutostart.Items.Clear();
            //this.cmbBig_Type.Items.Clear();
            this.cmbCompany.Items.Clear();
            this.cmbDoorActive.Items.Clear();
            this.cmbExt_Summer.Items.Clear();
            this.cmbFlaps.Items.Clear();
            this.cmbWasserSensor.Items.Clear();
            this.cmbZusatzheizung.Items.Clear();

            this.cmbAutostart.Text = "";
            //this.cmbBig_Type.Text = "";
            this.cmbCompany.Text = "";
            this.cmbDoorActive.Text = "";
            this.cmbExt_Summer.Text = "";
            this.cmbFlaps.Text = "";
            this.cmbWasserSensor.Text = "";
            this.cmbZusatzheizung.Text = "";

            //Reset LanguageListbox + Textbox
            this.listBox1.Items.Clear();
            this.txb_lang.Text = "";
        }


        /// <summary>
        /// Dient zum Refreshen der Textboxen
        /// </summary>
        private void RefreshTxt()
        {
            if (this.bakingPorgArr != null)
            {
                #region RefreshTextboxen
                txtVorheiztemp.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].Vorheiztemp);
                txtAbkühltemp.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].Abkuehltemp);
                txtGrundmenge.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].Grundmenge);

                txtName.Text = this.bakingPorgArr[indexTreeView].Backware_Name;

                txtBackzeit1.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Zeit_Phase1);
                txtBackzeit2.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Zeit_Phase2);
                txtBackzeit3.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Zeit_Phase3);
                txtBackzeit4.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Zeit_Phase4);

                txtBacktemp1.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Temp_Phase1);
                txtBacktemp2.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Temp_Phase2);
                txtBacktemp3.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Temp_Phase3);
                txtBacktemp4.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Temp_Phase4);

                txtBeschwmenge1.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Menge_Phase1);
                txtBeschwmenge2.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Menge_Phase2);
                txtBeschwmenge3.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Menge_Phase3);
                txtBeschwmenge4.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Menge_Phase4);

                txtBeschwdauer1.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Dauer_Phase1);
                txtBeschwdauer2.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Dauer_Phase2);
                txtBeschwdauer3.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Dauer_Phase3);
                txtBeschwdauer4.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Dauer_Phase4);

                txtVentilatorleistung1.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Prozent_Phase1);
                txtVentilatorleistung2.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Prozent_Phase2);
                txtVentilatorleistung3.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Prozent_Phase3);
                txtVentilatorleistung4.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Prozent_Phase4);

                txtKlappenstellung1.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Klappe_Phase1);
                txtKlappenstellung2.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Klappe_Phase2);
                txtKlappenstellung3.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Klappe_Phase3);
                txtKlappenstellung4.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Klappe_Phase4);

                txtVentilatorStop.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Stop);
                txtSoftHeat.Text = Convert.ToString(this.bakingPorgArr[this.indexTreeView].P_Softheat);
                #endregion
            }

            if (this.configDatas != null)
            {
                #region RefreshTextboxen

                //READONLY
                txtNumberOfBakingActs.Text = Convert.ToString(this.configDatas.NumberOfBakingActs);
                txtBetrieb_Std.Text = Convert.ToString(this.configDatas.Betrieb_Std);
                txtBetrieb_Min.Text = Convert.ToString(this.configDatas.Betrieb_Min);
                txtBetrieb_Sek.Text = Convert.ToString(this.configDatas.Betrieb_Sek);
                txtBetrieb_Overflow.Text = Convert.ToString(this.configDatas.Betrieb_Overflow);
                txtBetrieb_Warn_Cnt.Text = Convert.ToString(this.configDatas.Betrieb_Warn_Cnt);
                txtError_Cnt.Text = Convert.ToString(this.configDatas.Code_Error_Cnt);
                txtError_Wait.Text = Convert.ToString(this.configDatas.Code_Error_Wait);
                txtLetzter_State.Text = Convert.ToString(this.configDatas.Letzter_State);
                txtLetzte_Backware.Text = Convert.ToString(this.configDatas.Letzte_Backware);
                txtRestzeit_Min1.Text = Convert.ToString(this.configDatas.Restzeit_Min[0]);
                txtRestzeit_Min2.Text = Convert.ToString(this.configDatas.Restzeit_Min[1]);
                txtRestzeit_Min3.Text = Convert.ToString(this.configDatas.Restzeit_Min[2]);
                txtRestzeit_Min4.Text = Convert.ToString(this.configDatas.Restzeit_Min[3]);
                txtRestzeit_Min5.Text = Convert.ToString(this.configDatas.Restzeit_Min[4]);
                txtSetup_Code1.Text = Convert.ToString(this.configDatas.Setup_Code_1);
                txtSetup_Code2.Text = Convert.ToString(this.configDatas.Setup_Code_2);

                //READ AND WRITE
                txtKontrast.Text = Convert.ToString(this.configDatas.Kontrast);
                txtSummer.Text = Convert.ToString(this.configDatas.Summer);
                txtEnde_Beep.Text = Convert.ToString(this.configDatas.Ende_Beep);
                txtBeschwandungs_Pause.Text = Convert.ToString(this.configDatas.Beschwandung_Pause);
                txtBeschwandungs_Menge.Text = Convert.ToString(this.configDatas.Beschwandung_Menge);
                txtCoolDown_200.Text = Convert.ToString(this.configDatas.CoolDown_200);
                txtCoolDown_180.Text = Convert.ToString(this.configDatas.CoolDown_180);
                txtZeit_Klappe.Text = Convert.ToString(this.configDatas.Zeit_Klappe);
                txtEnergieSaving_Min.Text = Convert.ToString(this.configDatas.Energiesaving_Min);
                txtEnergiesaving_temp.Text = Convert.ToString(this.configDatas.Energiesaving_Temp);
                txtTemp_Korr.Text = Convert.ToString(Convert.ToInt32(this.configDatas.Temp_Korr) - 20);
                txtAnzahl_Backwaren.Text = Convert.ToString(this.configDatas.Anzahl_Backwaren);
                txtPowerReduction.Text = Convert.ToString(this.configDatas.Power_Reduction);
                #endregion
            }
        }

        /// <summary>
        /// Form-Elemente Refreshen
        /// </summary>
        private void RefreshDesign()
        {
            RefreshTxt();

            //TreeViewelemente löschen
            this.trVBackprogramme.Nodes[0].Nodes.Clear();

            if (this.bakingPorgArr != null)
            {              
                //Treeview mit Daten füllen
                foreach (BakingProgram obj in this.bakingPorgArr)
                {
                    this.trVBackprogramme.Nodes[0].Nodes.Add(obj.Backware_Name);
                }
            }

            //Sprachdatei anzeigen...
            this.listBox1.Items.Clear();           

            if (this.seclanguage != null)
            {
                foreach (string obj in this.seclanguage)
                {
                    this.listBox1.Items.Add(obj);
                }

                //this.listBox1.SelectedItem = this.listBox1.Items[0];
                this.txb_lang.Text = this.listBox1.Items[0].ToString();
                this.listBox1.SelectedIndex = 0;
            }


            #region Konfigurationsdaten (Combobox anzeigen) anzeigen..
            this.cmbAutostart.Items.Clear();
            //this.cmbBig_Type.Items.Clear();
            this.cmbCompany.Items.Clear();
            this.cmbDoorActive.Items.Clear();
            this.cmbExt_Summer.Items.Clear();
            this.cmbFlaps.Items.Clear();
            this.cmbWasserSensor.Items.Clear();
            this.cmbZusatzheizung.Items.Clear();

            this.cmbAutostart.Items.Add("AUS");
            this.cmbAutostart.Items.Add("EIN");

            //this.cmbBig_Type.Items.Add("NORMAL");
            //this.cmbBig_Type.Items.Add("BIG");

            this.cmbCompany.Items.Add("GASSNER");
            this.cmbCompany.Items.Add("PAN & CO");

            this.cmbDoorActive.Items.Add("AUS");
            this.cmbDoorActive.Items.Add("EIN");

            this.cmbExt_Summer.Items.Add("INTERN");
            this.cmbExt_Summer.Items.Add("INTERN & EXTERN");
            this.cmbExt_Summer.Items.Add("EXTERN");

            this.cmbFlaps.Items.Add("NO FLAPS");
            this.cmbFlaps.Items.Add("FLAPS");

            this.cmbWasserSensor.Items.Add("AUS");
            this.cmbWasserSensor.Items.Add("EIN");

            this.cmbZusatzheizung.Items.Add("AUS");
            this.cmbZusatzheizung.Items.Add("EIN");

            try
            {
                if (this.configDatas != null)
                {
                    this.cmbExt_Summer.SelectedIndex = this.configDatas.Ext_Summer;
                    this.cmbAutostart.SelectedIndex = this.configDatas.Autostart;
                    //this.cmbBig_Type.SelectedIndex = this.configDatas.Big_Type;
                    this.cmbDoorActive.SelectedIndex = this.configDatas.Door_Active;
                    this.cmbWasserSensor.SelectedIndex = this.configDatas.Wassersensor;
                    this.cmbZusatzheizung.SelectedIndex = this.configDatas.Zusatzheizung;
                    this.cmbCompany.SelectedIndex = this.configDatas.Company;
                    this.cmbFlaps.SelectedIndex = this.configDatas.Flaps;
                }
            }
            catch(Exception)
            {
                this.listBox1.Items.Clear();
                this.trVBackprogramme.Nodes[0].Nodes.Clear();
                this.cmbAutostart.Items.Clear();
                //this.cmbBig_Type.Items.Clear();
                this.cmbCompany.Items.Clear();
                this.cmbDoorActive.Items.Clear();
                this.cmbExt_Summer.Items.Clear();
                this.cmbFlaps.Items.Clear();
                this.cmbWasserSensor.Items.Clear();
                this.cmbZusatzheizung.Items.Clear();
                txb_lang.Clear();
                txtAbkühltemp.Clear();
                txtAnzahl_Backwaren.Clear();
                txtBacktemp1.Clear();
                txtBacktemp2.Clear();
                txtBacktemp3.Clear();
                txtBacktemp4.Clear();
                txtBackzeit1.Clear();
                txtBackzeit2.Clear();
                txtBackzeit3.Clear();
                txtBackzeit4.Clear();
                txtBeschwandungs_Menge.Clear();
                txtBeschwandungs_Pause.Clear();
                txtBeschwdauer1.Clear();
                txtBeschwdauer2.Clear();
                txtBeschwdauer3.Clear();
                txtBeschwdauer4.Clear();
                txtBeschwmenge1.Clear();
                txtBeschwmenge2.Clear();
                txtBeschwmenge3.Clear();
                txtBeschwmenge4.Clear();
                txtBetrieb_Min.Clear();
                txtBetrieb_Overflow.Clear();
                txtBetrieb_Sek.Clear();
                txtBetrieb_Std.Clear();
                txtBetrieb_Warn_Cnt.Clear();
                txtCoolDown_180.Clear();
                txtCoolDown_200.Clear();
                txtEnde_Beep.Clear();
                txtEnergieSaving_Min.Clear();
                txtEnergiesaving_temp.Clear();
                txtError_Cnt.Clear();
                txtError_Wait.Clear();
                txtGrundmenge.Clear();
                txtKlappenstellung1.Clear();
                txtKlappenstellung2.Clear();
                txtKlappenstellung3.Clear();
                txtKlappenstellung4.Clear();
                txtKontrast.Clear();
                txtLetzte_Backware.Clear();
                txtLetzter_State.Clear();
                txtName.Clear();
                txtNumberOfBakingActs.Clear();
                txtPowerReduction.Clear();
                txtRestzeit_Min1.Clear();
                txtRestzeit_Min2.Clear();
                txtRestzeit_Min3.Clear();
                txtRestzeit_Min4.Clear();
                txtRestzeit_Min5.Clear();
                txtSetup_Code1.Clear();
                txtSetup_Code2.Clear();
                txtSoftHeat.Clear();
                txtSummer.Clear();
                txtTemp_Korr.Clear();
                txtVentilatorleistung1.Clear();
                txtVentilatorleistung2.Clear();
                txtVentilatorleistung3.Clear();
                txtVentilatorleistung4.Clear();
                txtVentilatorStop.Clear();
                txtVorheiztemp.Clear();
                txtZeit_Klappe.Clear();

           
                    

                MessageBox.Show("Daten Fehlerhaft", "ERROR!", MessageBoxButtons.OK,
                MessageBoxIcon.Error);

                Application.Exit();
            }
            #endregion

        }


        /// <summary>
        /// EVENTHANDLER 
        /// Falls während der Übertragung ein Fehler auftritt 
        /// (ControlAdapter.DataTransmit() bzw. ControlAdapter.CallForDatas()),
        /// wird er hier behandelt
        /// </summary>
        /// <param name="_message"></param>
        private void TransmissionErrorCatch(string _message)
        {
            if (this.InvokeRequired)
            {
                //Methodenaufruf per GUI-Thread 
                this.Invoke(new ControlAdapter.TransmissionErrorEvent(TransmissionErrorCatch), new object[] { _message });
            }
            else
            {
                //FORM-Steuerelemente wieder enablen
                EnableMenuItems();


                //Textboxen und listbox enablen
                EnableTextBoxes();
                EnableComboBoxes();

                this.txb_lang.Enabled = true;
                this.listBox1.Enabled = true;

                //Progressbar schließen
                if (transForm != null) this.transForm.Close();

                //Fehlermeludung ausgeben
                MessageBox.Show(_message, "ERROR!", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// EVENTHANDLER, für Progressbar (Auslöser in der Methode ControlAdapter.Transmit)
        /// </summary>
        private void TransmitBackingData()
        {
            if (this.InvokeRequired)
            {
                //Methodenaufruf per GUI-Thread 
                this.Invoke(new ControlAdapter.TransmitOneBackingDataEvent(TransmitBackingData));
            }
            else
            {
                //Nach STEPSIZE=300, werden kleiner Daten übertragen => Nicht mehr so große Schritte ausführen
                if (transForm.ProgressbarValueSetGet == 300) transForm.ProgressBarStepSizeSetGet = 5;
                transForm.ProgressbarPerformStep();
            }
        }

        /// <summary>
        /// EVENTHANDLER, für Progressbar (Auslöser in der Methode ControlAdapter.Receive)
        /// </summary>
        private void ReceiveBackingData()
        {
            if (this.InvokeRequired)
            {
                //Methodenaufruf per GUI-Thread 
                this.Invoke(new ControlAdapter.ReceiveWaitForBackingDatasEvent(ReceiveBackingData));
            }
            else
            {
                transForm.ProgressbarPerformStep();
            }
        }


        /// <summary>
        /// EVENTHANDLER (Auslöser aus der Methode ControlAdapter.Receiver) 
        /// Wird ausgelöst sobald alle Daten vollständig empfangen worden sind
        /// </summary>
        private void DataReceived()
        {
            if (this.InvokeRequired)
            {
                //Methodenaufruf per GUI-Thread 
                this.Invoke(new ControlAdapter.ReceivedAllDataEvent(DataReceived));
            }
            else
            {
                //Daten vom Puffer auslesen
                try
                {
                    //FORM-Steuerelemente wieder enablen
                    EnableMenuItems();

                    //Progressbar schließen
                    this.transForm.Close();

                    //Backprogramme auslesen...
                    this.bakingPorgArr = this.connection.GetBakingProgramms();                  

                    //Sprachdaten auslesen..
                    this.seclanguage = this.connection.GetLanguageDatas();

                    //Konfigurationsdaten auslesen
                    this.configDatas = this.connection.GetConfigDatas();

                    //Anzeige refreshen, erstes Backprogramm anzeigen 
                    indexTreeView = 0;
                    RefreshDesign();

                    //Textboxen und comboboxen enablen
                    EnableTextBoxes();
                    EnableComboBoxes();
                    backprogrammeDruckenToolStripMenuItem.Enabled = true;
                    konfigurationenDruckenToolStripMenuItem.Enabled = true;

                    this.txb_lang.Enabled = true;
                    this.listBox1.Enabled = true;

                    this.tabControl2.SelectedTab = this.tabControl2.TabPages[0];
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[1];

                    //ComPort Name in die Registry eintragen
                    RegistryKey Key = Registry.CurrentUser.OpenSubKey("Software\\Tritronix\\Bakemaster", true);
                    if (Key == null) Key = Registry.CurrentUser.CreateSubKey("Software\\Tritronix\\Bakemaster");
                    Key.SetValue("ComPort", this.connection.PortName);
                }
                catch (OwnExceptions oex)
                {
                    MessageBox.Show(oex.Message, "ERROR!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
                catch (Exception)
                {
                    MessageBox.Show("Fehler bei der Datenübertragung!\nVersuchen sie es erneut", "ERROR!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }


        /// <summary>
        /// EVENTHANDLER (Auslöser aus der Methode ControlAdapter.Transmit) 
        /// Wird ausgelöst sobald alle Daten vollständig übertragen worden sind
        /// </summary>
        private void DataTransmitted()
        {
            if (this.InvokeRequired)
            {
                //Methodenaufruf per GUI-Thread 
                this.Invoke(new ControlAdapter.TransmittedAllDataEvent(DataTransmitted));
            }
            else
            {
                MessageBox.Show("Übertragung erfolgreich abgeschlossen", "INFO", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                //FORM-Steuerelemente wieder enablen
                EnableMenuItems();
                EnableComboBoxes();

                //Textboxen und listbox enablen
                EnableTextBoxes();
                this.txb_lang.Enabled = true;
                this.listBox1.Enabled = true;

                //ComPort Name in die Registry eintragen
                RegistryKey Key = Registry.CurrentUser.OpenSubKey("Software\\Tritronix\\Bakemaster", true);
                if (Key == null) Key = Registry.CurrentUser.CreateSubKey("Software\\Tritronix\\Bakemaster");
                Key.SetValue("ComPort", this.connection.PortName);
            }
        }



        /// <summary>
        /// Daten von der Steuerung abrufen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backdatenAbrufenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                //Form-Elemente-Disablen
                DisableMenuItems();
                DisableComboBoxes();
                DisableTextBoxes();

                this.txb_lang.Enabled = false;
                this.listBox1.Enabled = false;

                //connection Object initialisieren
                Connect();

                //ProgressbarForm initilasieren
                transForm = new ProgressbarForm("Backdaten abrufen", "Bitte warten...", 300, 10);
                transForm.Show();

                //Daten von der Steuerung anfordern, Exeption von dieser Methode werden im Eventhandler 
                //TransmissionErrorCatch abgefangen
                this.connection.CallForDatas();
            }
            catch (OwnExceptions oex)
            {
                MessageBox.Show(oex.Message, "ERROR!", MessageBoxButtons.OK,
                MessageBoxIcon.Error);

                //FORM-Steuerelemente wieder enablen
                EnableMenuItems();

                //Progressbar schließen
                if (transForm != null) this.transForm.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Fehler bei der Datenübertragung!\nEventuell haben sie den Falschen COM-Port ausgewählt", "ERROR!", MessageBoxButtons.OK,
                MessageBoxIcon.Error);

                //FORM-Steuerelemente wieder enablen
                EnableMenuItems();

                //Progressbar schließen
                if (transForm != null) this.transForm.Close();
            }
        }

        private void backdatenSendenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                //Falls noch nicht alle Werte vorhanden sind wird eine Exception ausgelöst
                if (this.bakingPorgArr == null || this.seclanguage == null || this.configDatas == null)
                {
                    throw new OwnExceptions("Es sind nicht alle benötigten Backdaten vorhanden");
                }

                //Form-Elemente-Disablen
                DisableMenuItems();

                DisableTextBoxes();
                DisableComboBoxes();
                this.txb_lang.Enabled = false;
                this.listBox1.Enabled = false;

                //connection Object initialisieren
                Connect();

                //ProgressbarForm initilasieren
                transForm = new ProgressbarForm("Backdaten senden", "Bitte warten...", 565, 10);
                transForm.Show();


                //Daten senden, Exeption von dieser Methode werden im Eventhandler 
                //TransmissionErrorCatch abgefangen
                TransmitDatas();
            }
            catch (OwnExceptions oex)
            {
                //FORM-Steuerelemente wieder enablen
                EnableMenuItems();
                EnableComboBoxes();
                EnableTextBoxes();

                //Progressbar schließen
                if (transForm != null) this.transForm.Close();

                MessageBox.Show(oex.Message, "ERROR!", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                //FORM-Steuerelemente wieder enablen
                EnableMenuItems();
                EnableComboBoxes();
                EnableTextBoxes();

                //Progressbar schließen
                if (transForm != null) this.transForm.Close();

                MessageBox.Show("Fehler bei der Datenübertragung!\nEventuell haben sie den Falschen COM-Port ausgewählt", "ERROR!", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Wird ausgeführt nachdem auf Senden gedrückt worden ist(Sendet die Backdaten an die Steuerung)
        /// </summary>
        public void TransmitDatas()
        {
            try
            {
                //Alle Backprogramme welche ungültige Werte besitzen ausgeben
                string temp = null;

                //Aktuell geöffnetes Programm abspeichern
                SaveBakingProgram();

                //Konfigdaten speichern
                SaveConfigDatas();

                //Aktuell geöffnetes Wort der Sprachdatei abspeichern
                this.seclanguage[this.listBox1.SelectedIndex] = txb_lang.Text;
                this.listBox1.Items[this.listBox1.SelectedIndex] = txb_lang.Text;

                //Kontrolle ob die Backdaten von einer Backdatendatei (.bak) geladen wurden
                if (this.bakingPorgWarningList != null)
                {
                    //Falls eine oder mehrere Warnigs vorliegen, alle betroffenen Backprogramme ausgeben
                    if (this.bakingPorgWarningList.Count != 0)
                    {
                        for (int i = 0; i < this.bakingPorgWarningList.Count; i++)
                        {
                            temp += ((BakingProgram)this.bakingPorgArr[(int)this.bakingPorgWarningList[i]]).Backware_Name.Trim();
                            if (i != this.bakingPorgWarningList.Count - 1) temp += ", ";
                        }
                        if (this.bakingPorgWarningList.Count > 1)
                        {
                            throw new OwnExceptions("Folgende Backprogramme enthalten fehlerhafte Werte: " + temp);
                        }
                        else throw new OwnExceptions("Folgendes Backprogramm enthält fehlerhafte Werte: " + temp);
                    }
                }


                //Sind die Daten korrekt, kann die Übertragung beginnen
                if (temp == null && seclanguage != null)
                {
                    this.connection.TransmitDatas(this.bakingPorgArr, this.seclanguage, this.configDatas);
                }
            }
            catch (OwnExceptions oex) { throw oex; }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Speichert das Aktuell in der Form angezeigt Backprogramm in das bakingProgArr
        /// </summary>
        private void SaveBakingProgram()
        {
            if (this.bakingPorgArr != null)
            {
                string save = txtVorheiztemp.Text + "," + txtAbkühltemp.Text + "," + txtGrundmenge.Text + "," + txtName.Text + "," +
                        txtBackzeit1.Text + "," + txtBackzeit2.Text + "," + txtBackzeit3.Text + "," + txtBackzeit4.Text
                        + "," + txtBacktemp1.Text + "," + txtBacktemp2.Text + "," + txtBacktemp3.Text + "," + txtBacktemp4.Text
                        + "," + txtBeschwmenge1.Text + "," + txtBeschwmenge2.Text + "," + txtBeschwmenge3.Text + "," + txtBeschwmenge4.Text
                        + "," + txtBeschwdauer1.Text + "," + txtBeschwdauer2.Text + "," + txtBeschwdauer3.Text + "," + txtBeschwdauer4.Text
                        + "," + txtVentilatorleistung1.Text + "," + txtVentilatorleistung2.Text + "," + txtVentilatorleistung3.Text + "," + txtVentilatorleistung4.Text
                        + "," + txtKlappenstellung1.Text + "," + txtKlappenstellung2.Text + "," + txtKlappenstellung3.Text + "," + txtKlappenstellung4.Text
                        + "," + txtVentilatorStop.Text + "," + txtSoftHeat.Text;
                try
                {
                    BakingProgram obj = new BakingProgram(save, 2);

                    //Falls die Daten gültig sind mögliche Warnings ausstreichen
                    if (this.bakingPorgWarningList != null)
                    {
                        //Warning aus der bakingPorgArrlistWarning streichen
                        for (int i = 0; i < this.bakingPorgWarningList.Count; i++)
                        {
                            if ((int)bakingPorgWarningList[i] == this.indexTreeView)
                            {
                                this.bakingPorgWarningList.RemoveAt(i);
                            }
                        }
                    }

                    //bakingProgArr aktualisieren
                    this.bakingPorgArr[this.indexTreeView] = obj;
                }

                //Falls ungültige Daten vorhanden sind wird eine Exception ausgelöst
                catch (OwnExceptions oex)
                {
                    throw oex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        //Sichert die aktuellen Konfigdaten
        private void SaveConfigDatas()
        {
            if (this.configDatas != null)
            {
                this.configDatas.Company = Convert.ToByte(this.cmbCompany.SelectedIndex);
                this.configDatas.Flaps = Convert.ToByte(this.cmbFlaps.SelectedIndex);
                //this.configDatas.Big_Type = Convert.ToByte(this.cmbBig_Type.SelectedIndex);
                this.configDatas.Ext_Summer = Convert.ToByte(this.cmbExt_Summer.SelectedIndex);
                this.configDatas.Wassersensor = Convert.ToByte(this.cmbWasserSensor.SelectedIndex);
                this.configDatas.Autostart = Convert.ToByte(this.cmbAutostart.SelectedIndex);
                this.configDatas.Door_Active = Convert.ToByte(this.cmbDoorActive.SelectedIndex);
                this.configDatas.Zusatzheizung = Convert.ToByte(this.cmbZusatzheizung.SelectedIndex);

                this.configDatas.Kontrast = Convert.ToByte(this.txtKontrast.Text);
                this.configDatas.Summer = Convert.ToByte(this.txtSummer.Text);
                this.configDatas.Ende_Beep= Convert.ToByte(this.txtEnde_Beep.Text);
                this.configDatas.Beschwandung_Menge = Convert.ToByte(this.txtBeschwandungs_Menge.Text);
                this.configDatas.Beschwandung_Pause = Convert.ToByte(this.txtBeschwandungs_Pause.Text);
                this.configDatas.CoolDown_180 = Convert.ToByte(this.txtCoolDown_180.Text);
                this.configDatas.CoolDown_200 = Convert.ToByte(this.txtCoolDown_200.Text);
                this.configDatas.Zeit_Klappe = Convert.ToByte(this.txtZeit_Klappe.Text);
                this.configDatas.Energiesaving_Min = Convert.ToByte(this.txtEnergieSaving_Min.Text);
                this.configDatas.Energiesaving_Temp = Convert.ToByte(this.txtEnergiesaving_temp.Text);
                this.configDatas.Temp_Korr = Convert.ToByte(Convert.ToInt32(this.txtTemp_Korr.Text) + 20);
                this.configDatas.Anzahl_Backwaren = Convert.ToByte(this.txtAnzahl_Backwaren.Text);
                this.configDatas.Power_Reduction = Convert.ToByte(this.txtPowerReduction.Text);
            }
        }


        //TreeView Element auswählen
        private void trVBackprogramme_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //Nur wenn eines der ChildNodes ausgewählt wird, soll reagiert werden
            if (e.Node.Parent != null)
            {
                try
                {
                    //Aktuelles Backprogramm speichern
                    SaveBakingProgram();

                    //Name in Treeview aktualisieren
                    this.trVBackprogramme.Nodes[0].Nodes[this.indexTreeView].Remove();
                    this.trVBackprogramme.Nodes[0].Nodes.Insert(this.indexTreeView, this.bakingPorgArr[this.indexTreeView].Backware_Name);

                    //Treeview aktualisiert
                    this.trVBackprogramme.SelectedNode = this.trVBackprogramme.Nodes[0].Nodes[e.Node.Index];

                    //index Ändern
                    indexTreeView = e.Node.Index;

                    //Design refreshen
                    RefreshTxt();

                    this.tabControl2.SelectedTab = this.tabControl2.TabPages[0];
                }
                catch (OwnExceptions oex)
                {
                    MessageBox.Show(oex.Message, "WARNING!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }


        //Listbox Element auswählen
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.seclanguage != null)
            {
                //Geänderten Wert speichern
                if (this.indexLanguage != -1)
                {
                    this.seclanguage[this.indexLanguage] = this.txb_lang.Text;

                    //Listbox aktualisieren, abfrage um endlosschleife zu verhindern
                    if (this.listBox1.Items[this.indexLanguage].ToString() != this.txb_lang.Text)
                    {
                        this.listBox1.Items[this.indexLanguage] = this.txb_lang.Text;
                    }
                }

                //Name des aktuell ausgewälten Items in Textbox schreiben
                this.indexLanguage = this.listBox1.SelectedIndex;


                //Bei einen ungültigen Click, wird listBox1.SelectedIndex = -1 
                if (this.listBox1.SelectedIndex != -1) this.txb_lang.Text = this.seclanguage[this.indexLanguage];                         
            }
        }

        /// <summary>
        /// Application Beenden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                                  "Wollen Sie die Anwendung beenden?",
                                  "Beenden",
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question,
                                  MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                this.connection.Exit();
                Application.Exit();
            }
        }

        #region Speichern und Öffnen von Files


        private void sprachdatenSpeichernUnterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.seclanguage != null)
            {
                SaveFileDialog dlgFileSave = new SaveFileDialog();
                dlgFileSave.AddExtension = true;
                dlgFileSave.DefaultExt = "lng";
                dlgFileSave.Filter = "Languagedatei (*.lng)|*.lng";
                dlgFileSave.OverwritePrompt = true;
                dlgFileSave.InitialDirectory = Application.StartupPath;
                if (dlgFileSave.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(dlgFileSave.FileName);

                    try
                    {
                        for(int i=0;i<57;i++)
                        {
                            if (i < 56) sw.WriteLine(seclanguage[i]);
                            else sw.Write(seclanguage[i]);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR!", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                        return;
                    }
                    finally
                    {
                        sw.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Keine zu speichernden Daten vorhanden", "Warning", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            }
        }


        /// <summary>
        /// Sprachdaten von File laden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sprachdatenÖffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Flag zur Kontrolle ob ein schwerwiegender Fehler vorliegt oder nicht
            bool bigError = false;

            //OpenFile Dialog erstellen
            OpenFileDialog dlgFileOpen = new OpenFileDialog();
            dlgFileOpen.InitialDirectory = Application.StartupPath;
            dlgFileOpen.Filter = "Languagedatei (*.lng)|*.lng";

            if (dlgFileOpen.ShowDialog() == DialogResult.OK)
            {
                //Streamreader definieren
                StreamReader sr = new StreamReader(dlgFileOpen.FileName);

                //Counter für die Position im bakingProgArr definieren
                int count = 0;

                //Kontrolle ob die Anzahl der Zeilen(Wörter) gültig ist            
                while(!sr.EndOfStream)
                {
                    sr.ReadLine();
                    count++;
                }

                if (count != 57) bigError = true;
                sr.Close();

                if (!bigError)
                {
                    //Datei auslesen bis ein schwer wiegender Fehler bzw das EndofFile Zeichen erreicht wird
                    sr = new StreamReader(dlgFileOpen.FileName);

                    this.seclanguage = new string[count];
                    count = 0;

                    //Sprachdatei, Zeilenweise auslesen
                    while (!sr.EndOfStream)
                    {
                        seclanguage[count++] = sr.ReadLine().TrimEnd(' ');
                        if (this.seclanguage[count-1].Length > 16) this.seclanguage[count-1].Remove(16);
                    }

                    //GUI aktualisieren
                    RefreshDesign();
                    listBox1.Enabled = true;
                    txb_lang.Enabled = true;

                    this.tabControl2.SelectedTab = this.tabControl2.TabPages[1];

                    sr.Close();

                    Form1.ActiveForm.Text = "Bakemaster " + dlgFileOpen.FileName;
                }
                else
                {
                    MessageBox.Show("Fehlerhafte Daten!", "ERROR!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }              
            }
        }


        /// <summary>
        /// Backdaten in File abspeichern
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void speichernUnterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.bakingPorgArr != null)
            {
                SaveFileDialog dlgFileSave = new SaveFileDialog();
                dlgFileSave.AddExtension = true;
                dlgFileSave.DefaultExt = "bak";
                dlgFileSave.Filter = "Backprogrammdatei (*.bpg)|*.bpg";
                dlgFileSave.OverwritePrompt = true;
                dlgFileSave.InitialDirectory = Application.StartupPath;
                if (dlgFileSave.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(dlgFileSave.FileName);

                    try
                    {
                        SaveBakingProgram();
                        StringBuilder cache = new StringBuilder();
                        #region Cache Initialisieren
                        foreach (BakingProgram obj in bakingPorgArr)
                        {
                            cache.Append(obj.Vorheiztemp);
                            cache.Append(",");
                            cache.Append(obj.Abkuehltemp);
                            cache.Append(",");
                            cache.Append(obj.Grundmenge);
                            cache.Append(",");

                            cache.Append(obj.Backware_Name);
                            cache.Append(",");

                            cache.Append(obj.P_Zeit_Phase1);
                            cache.Append(",");
                            cache.Append(obj.P_Zeit_Phase2);
                            cache.Append(",");
                            cache.Append(obj.P_Zeit_Phase3);
                            cache.Append(",");
                            cache.Append(obj.P_Zeit_Phase4);
                            cache.Append(",");

                            cache.Append(obj.P_Temp_Phase1);
                            cache.Append(",");
                            cache.Append(obj.P_Temp_Phase2);
                            cache.Append(",");
                            cache.Append(obj.P_Temp_Phase3);
                            cache.Append(",");
                            cache.Append(obj.P_Temp_Phase4);
                            cache.Append(",");

                            cache.Append(obj.P_Menge_Phase1);
                            cache.Append(",");
                            cache.Append(obj.P_Menge_Phase2);
                            cache.Append(",");
                            cache.Append(obj.P_Menge_Phase3);
                            cache.Append(",");
                            cache.Append(obj.P_Menge_Phase4);
                            cache.Append(",");

                            cache.Append(obj.P_Dauer_Phase1);
                            cache.Append(",");
                            cache.Append(obj.P_Dauer_Phase2);
                            cache.Append(",");
                            cache.Append(obj.P_Dauer_Phase3);
                            cache.Append(",");
                            cache.Append(obj.P_Dauer_Phase4);
                            cache.Append(",");

                            cache.Append(obj.P_Prozent_Phase1);
                            cache.Append(",");
                            cache.Append(obj.P_Prozent_Phase2);
                            cache.Append(",");
                            cache.Append(obj.P_Prozent_Phase3);
                            cache.Append(",");
                            cache.Append(obj.P_Prozent_Phase4);
                            cache.Append(",");

                            cache.Append(obj.P_Klappe_Phase1);
                            cache.Append(",");
                            cache.Append(obj.P_Klappe_Phase2);
                            cache.Append(",");
                            cache.Append(obj.P_Klappe_Phase3);
                            cache.Append(",");
                            cache.Append(obj.P_Klappe_Phase4);
                            cache.Append(",");

                            cache.Append(obj.P_Stop);
                            cache.Append(",");
                            cache.Append(obj.P_Softheat);
                            cache.Append("\r\n");
                        }
                        #endregion
                        sw.Write(Convert.ToString(cache));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR!", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                        return;
                    }
                    finally
                    {
                        sw.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Keine zu speichernden Daten vorhanden", "Warning", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Backdaten von File laden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Flag zur Kontrolle ob ein schwerwiegender Fehler vorliegt oder nicht
            bool bigError = false;

            //Speichert die Waring-Message
            string errorBackprog = "";

            //WarningListspeicher räumen
            this.bakingPorgWarningList = null;

            //OpenFile Dialog erstellen
            OpenFileDialog dlgFileOpen = new OpenFileDialog();
            dlgFileOpen.InitialDirectory = Application.StartupPath;
            dlgFileOpen.Filter = "Backprogrammdatei (*.bpg)|*.bpg";

            if (dlgFileOpen.ShowDialog() == DialogResult.OK)
            {
                //Streamreader definieren
                StreamReader sr = new StreamReader(dlgFileOpen.FileName);

                //Counter für die Position im bakingProgArr definieren
                int counter = 0; 

                //Kontrolle ob die Anzahl der Zeilen(Backprogramme) gültig ist
                while (!sr.EndOfStream)
                {
                    sr.ReadLine();
                    counter++;
                }
                if (counter != 30) bigError = true;
                sr.Close();

                //Zwischenspeicher welcher immer die Aktuell eingelesene Zeile enthält
                string strcache = null;

                if (!bigError)
                {
                    sr = new StreamReader(dlgFileOpen.FileName);
                    counter = 0;

                    //BackingProgramarray instanzieren
                    if (bakingPorgArr == null) bakingPorgArr = new BakingProgram[30];
                }

                //Datei auslesen bis ein schwer wiegender Fehler bzw das EndofFile Zeichen erreicht wird
                while (!sr.EndOfStream && !bigError)
                {
                    try
                    {
                        while (!sr.EndOfStream)
                        {
                            //Zeile auslesen
                            strcache = sr.ReadLine();

                            //String in ein Bakprogramm konvertieren, mit spezieller Datenüberprüfung
                            //Bei einen Fehler wird eine OwnExections ausgelöst
                            this.bakingPorgArr[counter] = new BakingProgram(strcache, 2);

                            //Leerzeichen trimmen
                            this.bakingPorgArr[counter].Backware_Name = this.bakingPorgArr[counter].Backware_Name.Trim();
                            counter++;
                        }

                        Form1.ActiveForm.Text = "Bakemaster " + dlgFileOpen.FileName;
                    }
                    catch (OwnExceptions)
                    {
                        //Ein für die Steuerung ungültiger Wert ist in einem Datensatz vorhanden

                        //String in ein Bakprogramm konvertieren, ohne spezieller Datenüberprüfung
                        this.bakingPorgArr[counter++] = new BakingProgram(strcache, 1);

                        //Leerzeichen trimmen
                        this.bakingPorgArr[counter].Backware_Name = this.bakingPorgArr[counter].Backware_Name.TrimEnd(' ');

                        //WarningList Instanzieren und die Stelle des ungültigen Backprogramms speichern
                        if (this.bakingPorgWarningList == null) this.bakingPorgWarningList = new ArrayList();
                        this.bakingPorgWarningList.Add(counter-1);

                        //Backprogrammname konvertieren
                        errorBackprog += this.bakingPorgArr[counter-1].Backware_Name.Trim() + ", ";
                    }
                    catch (Exception)
                    {
                        //Datei Fehlerhaft(zb FormatException)
                        MessageBox.Show("Fehlerhafte Daten!", "ERROR!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                        bigError = true;
                    }
                }

                //Stream schließen
                sr.Close();

                if (!bigError)//Falls keine ungültige Datei vorliegt
                {
                    //Falls Warnings vorliegen
                    if (this.bakingPorgWarningList != null)
                    {
                        //Wariningsmessage konvertieren und ausgeben
                        errorBackprog = errorBackprog.TrimEnd(' ');
                        errorBackprog = errorBackprog.TrimEnd(',');
                        if (this.bakingPorgWarningList.Count > 1)
                        {
                            MessageBox.Show("Folgende Backprogramme enthalten fehlerhafte Werte: " + errorBackprog, "Warning", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Folgendes Backprogramm enthält fehlerhafte Werte: " + errorBackprog, "Warning", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        }
                    }

                    //Design resetten und neu refreshen
                    DeleteDesign();

                    this.tabControl2.SelectedTab = this.tabControl2.TabPages[0];
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[1];

                    this.indexTreeView = 0;
                    RefreshDesign();
                    EnableTextBoxes();
                    backprogrammeDruckenToolStripMenuItem.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Fehlerhafte Daten!", "ERROR!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Backdaten auf die Festplatte speichern
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void configDatenspeichern_Click(object sender, EventArgs e)
        {
            if (this.configDatas != null)
            {
                SaveFileDialog dlgFileSave = new SaveFileDialog();
                dlgFileSave.AddExtension = true;
                dlgFileSave.DefaultExt = "cof";
                dlgFileSave.Filter = "Konfigdatei (*.cfg)|*.cfg";
                dlgFileSave.OverwritePrompt = true;
                dlgFileSave.InitialDirectory = Application.StartupPath;
                if (dlgFileSave.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(dlgFileSave.FileName);
                    char[] cache = new char[configDatas.GetByteStream().Length];

                    SaveConfigDatas();

                    //Byte Array in ein Char Array konvertieren
                    for (int i = 0; i < configDatas.GetByteStream().Length; i++)
                    {
                        cache[i] = Convert.ToChar(configDatas.GetByteStream()[i]);
                    }

                    try
                    {
                        sw.Write(cache, 0, cache.Length);    
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                    finally
                    {
                        sw.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Keine zu speichernden Daten vorhanden", "Warning", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            }
        }

        private void configDatenOeffnen_Click(object sender, EventArgs e)
        {
            //OpenFile Dialog erstellen
            OpenFileDialog dlgFileOpen = new OpenFileDialog();
            dlgFileOpen.InitialDirectory = Application.StartupPath;
            dlgFileOpen.Filter = "Konfigdatei (*.cfg)|*.cfg";

            if (dlgFileOpen.ShowDialog() == DialogResult.OK)
            {
                //Streamreader definieren
                StreamReader sr = new StreamReader(dlgFileOpen.FileName);

                //Kontrolle ob die Anzahl der Bytes(256) gültig ist
                int counter = 0;
                while (!sr.EndOfStream)
                {
                    sr.Read();
                    counter++;
                }
                sr.Close();

                if (counter != 256)
                {
                    MessageBox.Show("Fehlerhafte Daten!", "ERROR!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
                else
                {
                    sr = new StreamReader(dlgFileOpen.FileName);

                    #region Bytes auslesen
                    try
                    {
                        StringBuilder cache = new StringBuilder();
                        StringBuilder unusedTemp = new StringBuilder(); //Wird verwendet um einen Teil der Konfiguarationsdaten zu speichern

                        //Bytes welche nicht verwendet werden, in unusedTemp speichern
                        for (int i = 0; i < 16; i++)
                        {
                            unusedTemp.Append(','); //unusedTemp wird anschließend an cache angehängt
                            unusedTemp.Append(sr.Read());
                        }

                        //Backvorgang auslesen
                        cache.Append(sr.Read() + sr.Read() * 256 + sr.Read() * 65536);
                        cache.Append(',');


                        //Bytes welche nicht verwendet werden, in unusedTemp speichern
                        for (int i = 0; i < 13; i++)
                        {
                            unusedTemp.Append(',');
                            unusedTemp.Append(sr.Read());
                        }

                        //Betriebsstundenzähler
                        for (int i = 0; i < 2; i++)
                        {
                            cache.Append(sr.Read());
                            cache.Append(',');
                        }

                        cache.Append(sr.Read() + sr.Read() * 256);
                        cache.Append(',');

                        cache.Append(sr.Read());
                        cache.Append(',');

                        //Filterwarnung
                        cache.Append(sr.Read());
                        cache.Append(',');

                        //Bytes welche nicht verwendet werden, in unusedTemp speichern
                        for (int i = 0; i < 2; i++)
                        {
                            unusedTemp.Append(',');
                            unusedTemp.Append(sr.Read());
                        }

                        //Fehlercodeszähler 
                        for (int i = 0; i < 2; i++)
                        {
                            cache.Append(sr.Read());
                            cache.Append(',');
                        }

                        //Bytes welche nicht verwendet werden, in unusedTemp speichern
                        for (int i = 0; i < 6; i++)
                        {
                            unusedTemp.Append(',');
                            unusedTemp.Append(sr.Read());
                        }

                        //Zustandsspeicher Backofen
                        for (int i = 0; i < 7; i++)
                        {
                            cache.Append(sr.Read());
                            cache.Append(',');
                        }


                        //Bytes welche nicht verwendet werden, in unusedTemp speichern
                        for (int i = 0; i < 3; i++)
                        {
                            unusedTemp.Append(',');
                            unusedTemp.Append(sr.Read());
                        }


                        //Auswahl Ofentyp
                        for (int i = 0; i < 3; i++)
                        {
                            cache.Append(sr.Read());
                            cache.Append(',');
                        }

                        //Bytes welche nicht verwendet werden, in unusedTemp speichern
                        for (int i = 0; i < 3; i++)
                        {
                            unusedTemp.Append(',');
                            unusedTemp.Append(sr.Read());
                        }


                        //Ofeneinstellung
                        for (int i = 0; i < 17; i++)
                        {
                            cache.Append(sr.Read());
                            cache.Append(',');
                        }

                        //Bytes welche nicht verwendet werden, in unusedTemp speichern
                        unusedTemp.Append(',');
                        unusedTemp.Append(sr.Read());

                        cache.Append(sr.Read());
                        cache.Append(',');


                        //Bytes welche nicht verwendet werden, in unusedTemp speichern
                        for (int i = 0; i < 170; i++)
                        {
                            unusedTemp.Append(',');
                            unusedTemp.Append(sr.Read());
                        }

                        //Einstellungsversion
                        cache.Append(sr.Read());
                        cache.Append(',');
                        cache.Append(sr.Read());

                        //Bytes welche nicht verwendet werden, in unusedTemp speichern
                        unusedTemp.Append(',');
                        unusedTemp.Append(sr.Read());

                        //Nicht verwendeten Daten anhängen..
                        cache.Append(unusedTemp);

                        //ConfigDataObject instanzieren
                        this.configDatas = new ConfigDatas(Convert.ToString(cache), 1);

                        RefreshDesign();
                        EnableTextBoxes();
                        konfigurationenDruckenToolStripMenuItem.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR!", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                    finally
                    {
                        sr.Close();
                    }
                    #endregion

                    Form1.ActiveForm.Text = "Bakemaster " + dlgFileOpen.FileName;
                }          
            }
        }
    
        /// <summary>
        /// Sämtliche Daten löschen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
                DialogResult result = MessageBox.Show(
                                      "Wollen Sie die Aktuellen Daten wirklich löschen?",
                                      "Neu",
                                      MessageBoxButtons.YesNoCancel,
                                      MessageBoxIcon.Question,
                                      MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    DeleteDesign();
                    DisableTextBoxes();
                    this.listBox1.Enabled = false;
                    this.txb_lang.Enabled = false;

                    this.bakingPorgArr = null;
                    this.seclanguage = null;
                    this.configDatas = null;
                    this.indexTreeView = -1;
                    this.indexLanguage = -1;
                }
        }

        #endregion

        #region txts Runden-Eventhandler
        private void txtBeschwdauer1_Validated(object sender, EventArgs e)
        {
            Rounder((TextBox)sender);
        }
        private void txtBeschwdauer2_Validated(object sender, EventArgs e)
        {
            Rounder((TextBox)sender);
        }
        private void txtBeschwdauer3_Validated(object sender, EventArgs e)
        {
            Rounder((TextBox)sender);
        }
        private void txtBeschwdauer4_Validated(object sender, EventArgs e)
        {
            Rounder((TextBox)sender);
        }


        private void txtBackzeit1_Validated(object sender, EventArgs e)
        {
            Rounder((TextBox)sender);
        }
        private void txtBackzeit2_Validated(object sender, EventArgs e)
        {
            Rounder((TextBox)sender);
        }
        private void txtBackzeit3_Validated(object sender, EventArgs e)
        {
            Rounder((TextBox)sender);
        }
        private void txtBackzeit4_Validated(object sender, EventArgs e)
        {
            Rounder((TextBox)sender);
        }

        private void txtSoftHeat_Validated(object sender, EventArgs e)
        {
            Rounder((TextBox)sender);
        }

        private void Rounder(TextBox textbox)
        {
            if (textbox.Text == "") textbox.Text = "0";
            try
            {
                ushort time = Convert.ToUInt16(textbox.Text);
                if (time > 5930)
                {
                    textbox.Text = "";
                    MessageBox.Show("Ungültige Zeitangabe", "ERROR!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
                else
                {
                    byte min = (byte)(time / 100);
                    byte sek = (byte)(time - min * 100);

                    //Runden Fall1:
                    if (sek >= 45) time = (ushort)((min + 1) * 100);

                    //Runden Fall2:
                    if (sek < 45 && sek >= 15) time = (ushort)(min * 100 + 30);

                    //Runden Fall3:
                    if (sek < 15) time = (ushort)(min * 100);
                    textbox.Text = Convert.ToString(time);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ungültige Zeitangabe", "ERROR!", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                textbox.Text = "";
            }
        }
        #endregion

        #region TextboxenLeave Eventhandler

        private void txtName_Leave(object sender, EventArgs e)
        {
            if (txtName.Text.Length != 16)
            {
                if (txtName.Text.Length > 16) txtName.Text = txtName.Text.Remove(16);
            }
        }

        private void txtSoftHeat_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtSoftHeat.Text,out number);

            if (!result)
            {
                MessageBox.Show("Ungültiger Soft Heat Wert!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoftHeat.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Softheat.ToString();
            }
            else if (Convert.ToInt32(txtSoftHeat.Text) < 30 || Convert.ToInt32(txtSoftHeat.Text) > 100)
            {
                MessageBox.Show("Ungültiger Soft Heat Wert!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoftHeat.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Softheat.ToString();
            }
        }

        private void txtVorheiztemp_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtVorheiztemp.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Vorheiztemperatur!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVorheiztemp.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).Vorheiztemp.ToString();
            }
            else if (Convert.ToInt32(txtVorheiztemp.Text) < 40 || Convert.ToInt32(txtVorheiztemp.Text) > 230)
            {
                MessageBox.Show("Ungültige Vorheiztemperatur!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVorheiztemp.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).Vorheiztemp.ToString();
            }
        }

        private void txtGrundmenge_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtGrundmenge.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Grundmenge!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGrundmenge.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).Grundmenge.ToString();
            }
            else if (Convert.ToInt32(txtGrundmenge.Text) > 10 || Convert.ToInt32(txtGrundmenge.Text) < 0)
            {
                MessageBox.Show("Ungültige Grundmenge!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGrundmenge.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).Grundmenge.ToString();
            }
        }

        private void txtAbkühltemp_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtAbkühltemp.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Abkühltemperatur!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAbkühltemp.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).Abkuehltemp.ToString();
            }
            else if (Convert.ToInt32(txtAbkühltemp.Text) < 40 || Convert.ToInt32(txtAbkühltemp.Text) > 230)
            {
                MessageBox.Show("Ungültige Abkühltemperatur!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAbkühltemp.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).Abkuehltemp.ToString();
            }
        }

        private void txtVentilatorStop_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtVentilatorStop.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültiger Ventilator Stopwert!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVentilatorStop.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Stop.ToString();
            }
            else if (Convert.ToInt32(txtVentilatorStop.Text) > 200 || Convert.ToInt32(txtVentilatorStop.Text) < 0)
            {
                MessageBox.Show("Ungültiger Ventilator Stopwert!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVentilatorStop.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Stop.ToString();
            }
        }

        private void txtBackzeit1_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtBackzeit1.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Backzeit 1!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBackzeit1.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Zeit_Phase1.ToString();
            }
            else if (Convert.ToInt32(txtBackzeit1.Text) > 5930 || Convert.ToInt32(txtBackzeit1.Text) < 0)
            {
                MessageBox.Show("Ungültige Backzeit 1!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBackzeit1.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Zeit_Phase1.ToString();
            }
        }

        private void txtBackzeit2_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtBackzeit2.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Backzeit 2!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBackzeit2.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Zeit_Phase2.ToString();
            }
            else if (Convert.ToInt32(txtBackzeit2.Text) > 5930 || Convert.ToInt32(txtBackzeit2.Text) < 0)
            {
                MessageBox.Show("Ungültige Backzeit 2!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBackzeit2.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Zeit_Phase2.ToString();
            }
        }

        private void txtBackzeit3_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtBackzeit3.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Backzeit 3!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBackzeit3.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Zeit_Phase3.ToString();
            }
            else if (Convert.ToInt32(txtBackzeit3.Text) > 5930 || Convert.ToInt32(txtBackzeit3.Text) < 0)
            {
                MessageBox.Show("Ungültige Backzeit 3!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBackzeit3.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Zeit_Phase3.ToString();
            }
        }

        private void txtBackzeit4_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtBackzeit4.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Backzeit 4!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBackzeit4.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Zeit_Phase4.ToString();
            }
            else if (Convert.ToInt32(txtBackzeit4.Text) > 5930 || Convert.ToInt32(txtBackzeit4.Text) < 0)
            {
                MessageBox.Show("Ungültige Backzeit 4!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBackzeit4.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Zeit_Phase4.ToString();
            }
        }

        private void txtBacktemp1_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtBacktemp1.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Backtemperatur 1!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBacktemp1.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Temp_Phase1.ToString();
            }
            else if (Convert.ToInt32(txtBacktemp1.Text) < 40 || Convert.ToInt32(txtBacktemp1.Text) > 230)
            {
                MessageBox.Show("Ungültige Backtemperatur 1!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBacktemp1.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Temp_Phase1.ToString();
            }
        }

        private void txtBacktemp2_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtBacktemp2.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Backtemperatur 2!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBacktemp2.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Temp_Phase2.ToString();
            }
            else if (Convert.ToInt32(txtBacktemp2.Text) < 40 || Convert.ToInt32(txtBacktemp2.Text) > 230)
            {
                MessageBox.Show("Ungültige Backtemperatur 2!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBacktemp2.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Temp_Phase2.ToString();
            }
        }

        private void txtBacktemp3_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtBacktemp3.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Backtemperatur 3!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBacktemp3.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Temp_Phase3.ToString();
            }
            else if (Convert.ToInt32(txtBacktemp3.Text) < 40 || Convert.ToInt32(txtBacktemp3.Text) > 230)
            {
                MessageBox.Show("Ungültige Backtemperatur 3!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBacktemp3.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Temp_Phase3.ToString();
            }
        }

        private void txtBacktemp4_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtBacktemp4.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Backtemperatur 4!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBacktemp4.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Temp_Phase4.ToString();
            }
            else if (Convert.ToInt32(txtBacktemp4.Text) < 40 || Convert.ToInt32(txtBacktemp4.Text) > 230)
            {
                MessageBox.Show("Ungültige Backtemperatur 4!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBacktemp4.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Temp_Phase4.ToString();
            }
        }



        private void txtBeschwmenge1_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtBeschwmenge1.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Beschwadungsmenge 1!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwmenge1.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Menge_Phase1.ToString();
            }
            else if (Convert.ToInt32(txtBeschwmenge1.Text) > 5 || Convert.ToInt32(txtBeschwmenge1.Text) < 0)
            {
                MessageBox.Show("Ungültige Beschwadungsmenge 1!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwmenge1.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Menge_Phase1.ToString();
            }
        }

        private void txtBeschwmenge2_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtBeschwmenge2.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Beschwadungsmenge 2!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwmenge2.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Menge_Phase2.ToString();
            }
            else if (Convert.ToInt32(txtBeschwmenge2.Text) > 5 || Convert.ToInt32(txtBeschwmenge2.Text) < 0)
            {
                MessageBox.Show("Ungültige Beschwadungsmenge 2!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwmenge2.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Menge_Phase2.ToString();
            }
        }

        private void txtBeschwmenge3_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtBeschwmenge3.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Beschwadungsmenge 3!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwmenge3.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Menge_Phase3.ToString();
            }
            else if (Convert.ToInt32(txtBeschwmenge3.Text) > 5 || Convert.ToInt32(txtBeschwmenge3.Text) < 0)
            {
                MessageBox.Show("Ungültige Beschwadungsmenge 3!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwmenge3.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Menge_Phase3.ToString();
            }
        }

        private void txtBeschwmenge4_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtBeschwmenge4.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Beschwadungsmenge 4!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwmenge4.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Menge_Phase4.ToString();
            }
            else if (Convert.ToInt32(txtBeschwmenge4.Text) > 5 || Convert.ToInt32(txtBeschwmenge4.Text) < 0)
            {
                MessageBox.Show("Ungültige Beschwadungsmenge 4!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwmenge4.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Menge_Phase4.ToString();
            }
        }

        private void txtBeschwdauer1_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtBeschwdauer1.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Beschwadungsdauer 1!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwdauer1.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Dauer_Phase1.ToString();
            }
            else if (Convert.ToInt32(txtBeschwdauer1.Text) > 5930 || Convert.ToInt32(txtBeschwdauer1.Text) < 0)
            {
                MessageBox.Show("Ungültige Beschwadungsdauer 1!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwdauer1.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Dauer_Phase1.ToString();
            }
        }

        private void txtBeschwdauer2_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtBeschwdauer2.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Beschwadungsdauer 2!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwdauer2.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Dauer_Phase2.ToString();
            }
            else if (Convert.ToInt32(txtBeschwdauer2.Text) > 5930 || Convert.ToInt32(txtBeschwdauer2.Text) < 0)
            {
                MessageBox.Show("Ungültige Beschwadungsdauer 2!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwdauer2.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Dauer_Phase2.ToString();
            }
        }

        private void txtBeschwdauer3_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtBeschwdauer3.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Beschwadungsdauer 3!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwdauer3.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Dauer_Phase3.ToString();
            }
            else if (Convert.ToInt32(txtBeschwdauer3.Text) > 5930 || Convert.ToInt32(txtBeschwdauer3.Text) < 0)
            {
                MessageBox.Show("Ungültige Beschwadungsdauer 3!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwdauer3.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Dauer_Phase3.ToString();
            }
        }

        private void txtBeschwdauer4_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtBeschwdauer4.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Beschwadungsdauer 4!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwdauer4.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Dauer_Phase4.ToString();
            }
            else if (Convert.ToInt32(txtBeschwdauer4.Text) > 5930 || Convert.ToInt32(txtBeschwdauer4.Text) < 0)
            {
                MessageBox.Show("Ungültige Beschwadungsdauer 4!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwdauer4.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Dauer_Phase4.ToString();
            }
        }

        private void txtVentilatorleistung1_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtVentilatorleistung1.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Ventilatorleistung 1!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVentilatorleistung1.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Prozent_Phase1.ToString();
            }
            else if (Convert.ToInt32(txtVentilatorleistung1.Text) != 50 && Convert.ToInt32(txtVentilatorleistung1.Text) != 100)
            {
                MessageBox.Show("Ungültige Ventilatorleistung 1!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVentilatorleistung1.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Prozent_Phase1.ToString();
            }
        }

        private void txtVentilatorleistung2_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtVentilatorleistung2.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Ventilatorleistung 2!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVentilatorleistung2.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Prozent_Phase2.ToString();
            }
            else if (Convert.ToInt32(txtVentilatorleistung2.Text) != 50 && Convert.ToInt32(txtVentilatorleistung2.Text) != 100)
            {
                MessageBox.Show("Ungültige Ventilatorleistung 2!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVentilatorleistung2.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Prozent_Phase2.ToString();
            }
        }

        private void txtVentilatorleistung3_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtVentilatorleistung3.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Ventilatorleistung 3!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVentilatorleistung3.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Prozent_Phase3.ToString();
            }
            else if (Convert.ToInt32(txtVentilatorleistung3.Text) != 50 && Convert.ToInt32(txtVentilatorleistung3.Text) != 100)
            {
                MessageBox.Show("Ungültige Ventilatorleistung 3!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVentilatorleistung3.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Prozent_Phase3.ToString();
            }
        }

        private void txtVentilatorleistung4_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtVentilatorleistung4.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Ventilatorleistung 4!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVentilatorleistung4.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Prozent_Phase4.ToString();
            }
            else if (Convert.ToInt32(txtVentilatorleistung4.Text) != 50 && Convert.ToInt32(txtVentilatorleistung4.Text) != 100)
            {
                MessageBox.Show("Ungültige Ventilatorleistung 4!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVentilatorleistung4.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Prozent_Phase4.ToString();
            }
        }

        private void txtKlappenstellung1_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtKlappenstellung1.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Klappenstellung 1!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKlappenstellung1.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Klappe_Phase1.ToString();
            }
            else if (Convert.ToInt32(txtKlappenstellung1.Text) != 0 && Convert.ToInt32(txtKlappenstellung1.Text) != 1)
            {
                MessageBox.Show("Ungültige Klappenstellung 1!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKlappenstellung1.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Klappe_Phase1.ToString();
            }
        }

        private void txtKlappenstellung2_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtKlappenstellung2.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Klappenstellung 2!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKlappenstellung2.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Klappe_Phase2.ToString();
            }
            else if (Convert.ToInt32(txtKlappenstellung2.Text) != 0 && Convert.ToInt32(txtKlappenstellung2.Text) != 1)
            {
                MessageBox.Show("Ungültige Klappenstellung 2!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKlappenstellung2.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Klappe_Phase2.ToString();
            }
        }

        private void txtKlappenstellung3_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtKlappenstellung3.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Klappenstellung 3!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKlappenstellung3.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Klappe_Phase3.ToString();
            }
            else if (Convert.ToInt32(txtKlappenstellung3.Text) != 0 && Convert.ToInt32(txtKlappenstellung3.Text) != 1)
            {
                MessageBox.Show("Ungültige Klappenstellung 3!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKlappenstellung3.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Klappe_Phase3.ToString();
            }
        }

        private void txtKlappenstellung4_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtKlappenstellung4.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Klappenstellung 4!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKlappenstellung4.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Klappe_Phase4.ToString();
            }
            else if (Convert.ToInt32(txtKlappenstellung4.Text) != 0 && Convert.ToInt32(txtKlappenstellung4.Text) != 1)
            {
                MessageBox.Show("Ungültige Klappenstellung 4!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKlappenstellung4.Text = ((BakingProgram)this.bakingPorgArr[this.indexTreeView]).P_Klappe_Phase4.ToString();
            }
        }





        //KONFIGURATIONSDATEN

        private void txtKontrast_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtKontrast.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültiger Kontrast!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKontrast.Text = this.configDatas.Kontrast.ToString();
            }
            else if (Convert.ToInt32(txtKontrast.Text) < 0 || Convert.ToInt32(txtKontrast.Text) > 40)
            {
                MessageBox.Show("Ungültiger Kontrast!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKontrast.Text = this.configDatas.Kontrast.ToString();
            }
        }

        private void txtSummer_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtSummer.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültiger Summer!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSummer.Text = this.configDatas.Summer.ToString();
            }
            else if (Convert.ToInt32(txtSummer.Text) < 0 || Convert.ToInt32(txtSummer.Text) > 15)
            {
                MessageBox.Show("Ungültiger Summer!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSummer.Text = this.configDatas.Summer.ToString();
            }
        }

        private void txtEnde_Beep_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtEnde_Beep.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige EndeBeep-Einstellung!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEnde_Beep.Text = this.configDatas.Ende_Beep.ToString();
            }
            else if (Convert.ToInt32(txtEnde_Beep.Text) < 0 || Convert.ToInt32(txtEnde_Beep.Text) > 100)
            {
                MessageBox.Show("Ungültige EndeBeep-Einstellung!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEnde_Beep.Text = this.configDatas.Ende_Beep.ToString();
            }
        }

        private void txtZeit_Klappe_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtZeit_Klappe.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Zeitklappen-Einstellung!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtZeit_Klappe.Text = this.configDatas.Zeit_Klappe.ToString();
            }
            else if (Convert.ToInt32(txtZeit_Klappe.Text) < 0 || Convert.ToInt32(txtZeit_Klappe.Text) > 60)
            {
                MessageBox.Show("Ungültige Zeitklappen-Einstellung!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtZeit_Klappe.Text = this.configDatas.Zeit_Klappe.ToString();
            }
        }

        private void txtBeschwandungs_Pause_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtBeschwandungs_Pause.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Beschwandungspause!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwandungs_Pause.Text = this.configDatas.Beschwandung_Pause.ToString();
            }
            else if (Convert.ToInt32(txtBeschwandungs_Pause.Text) < 1 || Convert.ToInt32(txtBeschwandungs_Pause.Text) > 60)
            {
                MessageBox.Show("Ungültige Beschwandungspause!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwandungs_Pause.Text = this.configDatas.Beschwandung_Pause.ToString();
            }
        }

        private void txtBeschwandungs_Menge_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtBeschwandungs_Menge.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Beschwandungsmenge!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwandungs_Menge.Text = this.configDatas.Beschwandung_Menge.ToString();
            }
            else if (Convert.ToInt32(txtBeschwandungs_Menge.Text) < 1 || Convert.ToInt32(txtBeschwandungs_Menge.Text) > 20)
            {
                MessageBox.Show("Ungültige Beschwandungsmenge!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBeschwandungs_Menge.Text = this.configDatas.Beschwandung_Menge.ToString();
            }
        }

        private void txtCoolDown_180_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtCoolDown_180.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültiger CoolDown180!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCoolDown_180.Text = this.configDatas.CoolDown_180.ToString();
            }
            else if (Convert.ToInt32(txtCoolDown_180.Text) < 0 || Convert.ToInt32(txtCoolDown_180.Text) > 100)
            {
                MessageBox.Show("Ungültige CoolDown180!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCoolDown_180.Text = this.configDatas.CoolDown_180.ToString();
            }
        }

        private void txtCoolDown_200_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtCoolDown_200.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültiger CoolDown200!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCoolDown_200.Text = this.configDatas.CoolDown_200.ToString();
            }
            else if (Convert.ToInt32(txtCoolDown_200.Text) < 0 || Convert.ToInt32(txtCoolDown_200.Text) > 100)
            {
                MessageBox.Show("Ungültige CoolDown200!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCoolDown_200.Text = this.configDatas.CoolDown_200.ToString();
            }
        }

        private void txtAnzahl_Backwaren_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtAnzahl_Backwaren.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Backwarenanzahl!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAnzahl_Backwaren.Text = this.configDatas.Anzahl_Backwaren.ToString();
            }
            else if (Convert.ToInt32(txtAnzahl_Backwaren.Text) < 1 || Convert.ToInt32(txtAnzahl_Backwaren.Text) > 30)
            {
                MessageBox.Show("Ungültige Backwarenanzahl!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAnzahl_Backwaren.Text = this.configDatas.Anzahl_Backwaren.ToString();
            }
        }

        private void txtEnergieSaving_Min_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtEnergieSaving_Min.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige EnergieSaving_Min!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEnergieSaving_Min.Text = this.configDatas.Energiesaving_Min.ToString();
            }
            else if (Convert.ToInt32(txtEnergieSaving_Min.Text) < 1 || Convert.ToInt32(txtEnergieSaving_Min.Text) > 30)
            {
                MessageBox.Show("Ungültige EnergieSaving_Min!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEnergieSaving_Min.Text = this.configDatas.Energiesaving_Min.ToString();
            }
        }

        private void txtEnergiesaving_temp_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtEnergiesaving_temp.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige EnergieSaving_Temp!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEnergiesaving_temp.Text = this.configDatas.Energiesaving_Temp.ToString();
            }
            else if (Convert.ToInt32(txtEnergiesaving_temp.Text) < 50 || Convert.ToInt32(txtEnergiesaving_temp.Text) > 100)
            {
                MessageBox.Show("Ungültige EnergieSaving_Temp!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEnergiesaving_temp.Text = this.configDatas.Energiesaving_Temp.ToString();
            }
        }

        private void txtPowerReduction_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtPowerReduction.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Powerreduction-Einstellung!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPowerReduction.Text = this.configDatas.Power_Reduction.ToString();
            }
            else if (Convert.ToInt32(txtPowerReduction.Text) < 0 || Convert.ToInt32(txtPowerReduction.Text) > 10)
            {
                MessageBox.Show("Ungültige Powerreduction-Einstellung!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPowerReduction.Text = this.configDatas.Power_Reduction.ToString();
            }
        }

        private void txtTemp_Korr_Leave(object sender, EventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtTemp_Korr.Text, out number);

            if (!result)
            {
                MessageBox.Show("Ungültige Tempkorr-Einstellung!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTemp_Korr.Text = this.configDatas.Temp_Korr.ToString();
            }
            else if (Convert.ToInt32(txtTemp_Korr.Text) < -20 || Convert.ToInt32(txtTemp_Korr.Text) > 20)
            {
                MessageBox.Show("Ungültige Tempkorr-Einstellung!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTemp_Korr.Text = this.configDatas.Temp_Korr.ToString();
            }
        }

        #endregion

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (this.bakingPorgArr != null)
            {
                SaveFileDialog dlgFileSave = new SaveFileDialog();
                dlgFileSave.AddExtension = true;
                dlgFileSave.DefaultExt = "vsb";
                dlgFileSave.Filter = "Programmdatei (*.vsb)|*.vsb";
                dlgFileSave.OverwritePrompt = true;
                dlgFileSave.InitialDirectory = Application.StartupPath;
                if (dlgFileSave.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(dlgFileSave.FileName);

                    try
                    {
                        SaveBakingProgram();
                        StringBuilder cache = new StringBuilder();
                        #region Cache Initialisieren
                        foreach (BakingProgram obj in bakingPorgArr)
                        {
                            cache.Append(obj.Vorheiztemp);
                            cache.Append(",");
                            cache.Append(obj.Abkuehltemp);
                            cache.Append(",");
                            cache.Append(obj.Grundmenge);
                            cache.Append(",");

                            cache.Append(obj.Backware_Name);
                            cache.Append(",");

                            cache.Append(obj.P_Zeit_Phase1);
                            cache.Append(",");
                            cache.Append(obj.P_Zeit_Phase2);
                            cache.Append(",");
                            cache.Append(obj.P_Zeit_Phase3);
                            cache.Append(",");
                            cache.Append(obj.P_Zeit_Phase4);
                            cache.Append(",");

                            cache.Append(obj.P_Temp_Phase1);
                            cache.Append(",");
                            cache.Append(obj.P_Temp_Phase2);
                            cache.Append(",");
                            cache.Append(obj.P_Temp_Phase3);
                            cache.Append(",");
                            cache.Append(obj.P_Temp_Phase4);
                            cache.Append(",");

                            cache.Append(obj.P_Menge_Phase1);
                            cache.Append(",");
                            cache.Append(obj.P_Menge_Phase2);
                            cache.Append(",");
                            cache.Append(obj.P_Menge_Phase3);
                            cache.Append(",");
                            cache.Append(obj.P_Menge_Phase4);
                            cache.Append(",");

                            cache.Append(obj.P_Dauer_Phase1);
                            cache.Append(",");
                            cache.Append(obj.P_Dauer_Phase2);
                            cache.Append(",");
                            cache.Append(obj.P_Dauer_Phase3);
                            cache.Append(",");
                            cache.Append(obj.P_Dauer_Phase4);
                            cache.Append(",");

                            cache.Append(obj.P_Prozent_Phase1);
                            cache.Append(",");
                            cache.Append(obj.P_Prozent_Phase2);
                            cache.Append(",");
                            cache.Append(obj.P_Prozent_Phase3);
                            cache.Append(",");
                            cache.Append(obj.P_Prozent_Phase4);
                            cache.Append(",");

                            cache.Append(obj.P_Klappe_Phase1);
                            cache.Append(",");
                            cache.Append(obj.P_Klappe_Phase2);
                            cache.Append(",");
                            cache.Append(obj.P_Klappe_Phase3);
                            cache.Append(",");
                            cache.Append(obj.P_Klappe_Phase4);
                            cache.Append(",");

                            cache.Append(obj.P_Stop);
                            cache.Append(",");
                            cache.Append(obj.P_Softheat);
                            cache.Append("\r\n");
                        }
                        #endregion
                        sw.Write(Convert.ToString(cache));

                        for (int i = 0; i < 57; i++)
                        {
                            if (i < 56) sw.WriteLine(seclanguage[i]);
                            else sw.Write(seclanguage[i]);
                        }

                        sw.Write(sw.NewLine);

                        char[] cache2 = new char[configDatas.GetByteStream().Length];

                        SaveConfigDatas();
                        
                        //Byte Array in ein Char Array konvertieren
                        for (int i = 0; i < configDatas.GetByteStream().Length; i++)
                        {
                            cache2[i] = Convert.ToChar(configDatas.GetByteStream()[i]);
                        }

                        sw.Write(cache2, 0, cache2.Length);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR!", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                        return;
                    }
                    finally
                    {
                        sw.Close();
                    }
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Flag zur Kontrolle ob ein schwerwiegender Fehler vorliegt oder nicht
            bool bigError = false;

            //Speichert die Waring-Message
            string errorBackprog = "";

            //WarningListspeicher räumen
            this.bakingPorgWarningList = null;

            //OpenFile Dialog erstellen
            OpenFileDialog dlgFileOpen = new OpenFileDialog();
            dlgFileOpen.InitialDirectory = Application.StartupPath;
            dlgFileOpen.Filter = "Programmdatei (*.vsb)|*.vsb";

            if (dlgFileOpen.ShowDialog() == DialogResult.OK)
            {
                //Streamreader definieren
                StreamReader sr = new StreamReader(dlgFileOpen.FileName);

                //Counter für die Position im bakingProgArr definieren
                int counter = 0;

                //Kontrolle ob die Anzahl der Zeilen(Backprogramme) gültig ist
                while (counter < 30 && !sr.EndOfStream)
                {
                    sr.ReadLine();
                    counter++;
                }
                if (counter != 30) bigError = true;

                /*while (!sr.EndOfStream)
                {
                    sr.Read();
                    counter++;
                }
                if (counter < 1342) bigError = true;*/

                //Zwischenspeicher welcher immer die Aktuell eingelesene Zeile enthält
                string strcache = null;

                if (!bigError)
                {
                    sr = new StreamReader(dlgFileOpen.FileName);
                    counter = 0;

                    //BackingProgramarray instanzieren
                    if (bakingPorgArr == null) bakingPorgArr = new BakingProgram[30];

                    Form1.ActiveForm.Text = "Bakemaster " + dlgFileOpen.FileName;
                }

                //Datei auslesen bis ein schwer wiegender Fehler bzw das EndofFile Zeichen erreicht wird
                while (counter < 30 && !bigError)
                {
                    try
                    {
                        while (counter < 30)
                        {
                            //Zeile auslesen
                            strcache = sr.ReadLine();

                            //String in ein Bakprogramm konvertieren, mit spezieller Datenüberprüfung
                            //Bei einen Fehler wird eine OwnExections ausgelöst
                            this.bakingPorgArr[counter] = new BakingProgram(strcache, 2);

                            //Leerzeichen trimmen
                            this.bakingPorgArr[counter].Backware_Name = this.bakingPorgArr[counter].Backware_Name.Trim();
                            counter++;
                        }
                    }
                    catch (OwnExceptions)
                    {
                        //Ein für die Steuerung ungültiger Wert ist in einem Datensatz vorhanden

                        //String in ein Bakprogramm konvertieren, ohne spezieller Datenüberprüfung
                        this.bakingPorgArr[counter++] = new BakingProgram(strcache, 1);

                        //Leerzeichen trimmen
                        this.bakingPorgArr[counter].Backware_Name = this.bakingPorgArr[counter].Backware_Name.TrimEnd(' ');

                        //WarningList Instanzieren und die Stelle des ungültigen Backprogramms speichern
                        if (this.bakingPorgWarningList == null) this.bakingPorgWarningList = new ArrayList();
                        this.bakingPorgWarningList.Add(counter - 1);

                        //Backprogrammname konvertieren
                        errorBackprog += this.bakingPorgArr[counter - 1].Backware_Name.Trim() + ", ";
                    }
                    catch (Exception)
                    {
                        //Datei Fehlerhaft(zb FormatException)
                        MessageBox.Show("Fehlerhafte Daten!", "ERROR!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                        bigError = true;
                    }
                }

                if (!bigError)//Falls keine ungültige Datei vorliegt
                {
                    //Falls Warnings vorliegen
                    if (this.bakingPorgWarningList != null)
                    {
                        //Wariningsmessage konvertieren und ausgeben
                        errorBackprog = errorBackprog.TrimEnd(' ');
                        errorBackprog = errorBackprog.TrimEnd(',');
                        if (this.bakingPorgWarningList.Count > 1)
                        {
                            MessageBox.Show("Folgende Backprogramme enthalten fehlerhafte Werte: " + errorBackprog, "Warning", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Folgendes Backprogramm enthält fehlerhafte Werte: " + errorBackprog, "Warning", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        }
                    }

                    this.tabControl2.SelectedTab = this.tabControl2.TabPages[0];
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[1];

                    this.indexTreeView = 0;
                }
                else
                {
                    MessageBox.Show("Fehlerhafte Daten1!", "ERROR!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }

                //counter = 0;

                //Kontrolle ob die Anzahl der Zeilen(Wörter) gültig ist            
                /*while (!sr.EndOfStream)
                {
                    sr.ReadLine();
                    counter++;
                }*/

                if (!bigError)
                {
                    //Datei auslesen bis ein schwer wiegender Fehler bzw das EndofFile Zeichen erreicht wird
                    //sr = new StreamReader(dlgFileOpen.FileName);
                    listBox1.SelectedIndex = -1;

                    this.seclanguage = new string[57];
                    counter = 0;

                    //Sprachdatei, Zeilenweise auslesen
                    while (counter < 57)
                    {
                        seclanguage[counter++] = sr.ReadLine().TrimEnd(' ');
                        if (this.seclanguage[counter - 1].Length > 16) this.seclanguage[counter - 1].Remove(16);
                    }

                    //GUI aktualisieren
                    //RefreshDesign();

                    //this.tabControl2.SelectedTab = this.tabControl2.TabPages[1];
                }
                else
                {
                    MessageBox.Show("Fehlerhafte Daten2!", "ERROR!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
     
                //Kontrolle ob die Anzahl der Bytes(256) gültig ist
                counter = 0;
                /*while (!sr.EndOfStream)
                {
                    sr.Read();
                    counter++;
                }*/

                //if (counter != 0)
                //{
                //    MessageBox.Show("Fehlerhafte Daten!", "ERROR!", MessageBoxButtons.OK,
                //    MessageBoxIcon.Error);
                //}
                if(!bigError)
                {
                    //sr = new StreamReader(dlgFileOpen.FileName);

                    //try
                    //{
                        StringBuilder cache = new StringBuilder();
                        StringBuilder unusedTemp = new StringBuilder(); //Wird verwendet um einen Teil der Konfiguarationsdaten zu speichern
                        //sr.Read();
                        //sr.Read();
                        //Bytes welche nicht verwendet werden, in unusedTemp speichern
                        for (int i = 0; i < 16; i++)
                        {
                            unusedTemp.Append(','); //unusedTemp wird anschließend an cache angehängt
                            unusedTemp.Append(sr.Read());
                        }

                        //Backvorgang auslesen
                        cache.Append(sr.Read() + sr.Read() * 256 + sr.Read() * 65536);
                        cache.Append(',');


                        //Bytes welche nicht verwendet werden, in unusedTemp speichern
                        for (int i = 0; i < 13; i++)
                        {
                            unusedTemp.Append(',');
                            unusedTemp.Append(sr.Read());
                        }

                        //Betriebsstundenzähler
                        for (int i = 0; i < 2; i++)
                        {
                            cache.Append(sr.Read());
                            cache.Append(',');
                        }

                        cache.Append(sr.Read() + sr.Read() * 256);
                        cache.Append(',');

                        cache.Append(sr.Read());
                        cache.Append(',');

                        //Filterwarnung
                        cache.Append(sr.Read());
                        cache.Append(',');

                        //Bytes welche nicht verwendet werden, in unusedTemp speichern
                        for (int i = 0; i < 2; i++)
                        {
                            unusedTemp.Append(',');
                            unusedTemp.Append(sr.Read());
                        }

                        //Fehlercodeszähler 
                        for (int i = 0; i < 2; i++)
                        {
                            cache.Append(sr.Read());
                            cache.Append(',');
                        }

                        //Bytes welche nicht verwendet werden, in unusedTemp speichern
                        for (int i = 0; i < 6; i++)
                        {
                            unusedTemp.Append(',');
                            unusedTemp.Append(sr.Read());
                        }

                        //Zustandsspeicher Backofen
                        for (int i = 0; i < 7; i++)
                        {
                            cache.Append(sr.Read());
                            cache.Append(',');
                        }


                        //Bytes welche nicht verwendet werden, in unusedTemp speichern
                        for (int i = 0; i < 3; i++)
                        {
                            unusedTemp.Append(',');
                            unusedTemp.Append(sr.Read());
                        }


                        //Auswahl Ofentyp
                        for (int i = 0; i < 3; i++)
                        {
                            cache.Append(sr.Read());
                            cache.Append(',');
                        }

                        //Bytes welche nicht verwendet werden, in unusedTemp speichern
                        for (int i = 0; i < 3; i++)
                        {
                            unusedTemp.Append(',');
                            unusedTemp.Append(sr.Read());
                        }


                        //Ofeneinstellung
                        for (int i = 0; i < 17; i++)
                        {
                            cache.Append(sr.Read());
                            cache.Append(',');
                        }

                        //Bytes welche nicht verwendet werden, in unusedTemp speichern
                        unusedTemp.Append(',');
                        unusedTemp.Append(sr.Read());

                        cache.Append(sr.Read());
                        cache.Append(',');


                        //Bytes welche nicht verwendet werden, in unusedTemp speichern
                        for (int i = 0; i < 170; i++)
                        {
                            unusedTemp.Append(',');
                            unusedTemp.Append(sr.Read());
                        }

                        //Einstellungsversion
                        cache.Append(sr.Read());
                        cache.Append(',');
                        cache.Append(sr.Read());

                        //Bytes welche nicht verwendet werden, in unusedTemp speichern
                        unusedTemp.Append(',');
                        unusedTemp.Append(sr.Read());

                        //Nicht verwendeten Daten anhängen..
                        cache.Append(unusedTemp);

                        //ConfigDataObject instanzieren
                        this.configDatas = new ConfigDatas(Convert.ToString(cache), 1);

                        DeleteDesign();
                        RefreshDesign();
                        //listBox1.Enabled = true;
                        //txb_lang.Enabled = true;
                        EnableTextBoxes();
                        backprogrammeDruckenToolStripMenuItem.Enabled = true;
                        konfigurationenDruckenToolStripMenuItem.Enabled = true;
                        listBox1.Enabled = true;
                        txb_lang.Enabled = true;
                        this.listBox1.SelectedIndex = 0;
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message, "ERROR!", MessageBoxButtons.OK,
                    //                    MessageBoxIcon.Error);
                    //}
                    //finally
                    //{
                    //    sr.Close();
                    //}
                }
            }
        }

        private void backprogrammeDruckenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    printBackprogramme.Print();
                }
                catch
                {
                    MessageBox.Show("Fehler beim Drucken", "Fehler");
                }
            }
        }

        private void konfigurationenDruckenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog2.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    printKonfiguration.Print();
                }
                catch
                {
                    MessageBox.Show("Fehler beim Drucken", "Fehler");
                }
            }
        }

        private void printKonfiguration_PrintPage(object sender, PrintPageEventArgs e)
        {
            string write = "";

            Graphics g = e.Graphics;
            FontFamily type = new FontFamily("Arial");
            Font headlinefont = new Font(type, 24, FontStyle.Bold, GraphicsUnit.Pixel);
            Font headlinefont2 = new Font(type, 18, FontStyle.Bold, GraphicsUnit.Pixel);
            Font normalfont = new Font(type, 12, FontStyle.Regular, GraphicsUnit.Pixel);

            g.DrawString("Konfigurationsdaten", headlinefont, Brushes.Black, 10, 10);

            switch (this.configDatas.Company)
            {
                case 0: 
                    write = "Company: GASSNER";
                    break;
                case 1:
                    write = "Company: PAN & CO";
                    break;
                default:
                    write = "FEHLER";
                    break;
            }
            
            g.DrawString(write, normalfont, Brushes.Black, 50, 50);

            switch (this.configDatas.Wassersensor)
            {
                case 0:
                    write = "Wassersensor: AUS";
                    break;
                case 1:
                    write = "Wassersensor: EIN";
                    break;
                default:
                    write = "FEHLER";
                    break;
            }

            g.DrawString(write, normalfont, Brushes.Black, 200, 50);

            switch (this.configDatas.Autostart)
            {
                case 0:
                    write = "Autostart: AUS";
                    break;
                case 1:
                    write = "Autostart: EIN";
                    break;
                default:
                    write = "FEHLER";
                    break;
            }

            g.DrawString(write, normalfont, Brushes.Black, 400, 50);

            switch (this.configDatas.Flaps)
            {
                case 0:
                    write = "Flaps: NO FLAPS";
                    break;
                case 1:
                    write = "Flaps: FLAPS";
                    break;
                default:
                    write = "FEHLER";
                    break;
            }

            g.DrawString(write, normalfont, Brushes.Black, 650, 50);

            switch (this.configDatas.Door_Active)
            {
                case 0:
                    write = "Door Active: AUS";
                    break;
                case 1:
                    write = "Door Active: EIN";
                    break;
                default:
                    write = "FEHLER";
                    break;
            }

            g.DrawString(write, normalfont, Brushes.Black, 200, 75);

            switch (this.configDatas.Ext_Summer)
            {
                case 0:
                    write = "Ext. Summer: INTERN";
                    break;
                case 1:
                    write = "Ext. Summer: INTERN & EXTERN";
                    break;
                case 2:
                    write = "Ext. Summer: EXTERN";
                    break;
                default:
                    write = "FEHLER";
                    break;
            }

            g.DrawString(write, normalfont, Brushes.Black, 400, 75);

            switch (this.configDatas.Zusatzheizung)
            {
                case 0:
                    write = "Zusatzheizung: AUS";
                    break;
                case 1:
                    write = "Zusatzheizung: EIN";
                    break;
                default:
                    write = "FEHLER";
                    break;
            }

            g.DrawString(write, normalfont, Brushes.Black, 650, 75);

            g.DrawString("Anzahl Backwaren: " + this.configDatas.Anzahl_Backwaren, normalfont, Brushes.Black, 200, 150);
            g.DrawString("Kontrast: " + this.configDatas.Kontrast, normalfont, Brushes.Black, 400, 150);
            g.DrawString("Beschwadungspause (sec): " + this.configDatas.Beschwandung_Pause, normalfont, Brushes.Black, 650, 150);
            //g.DrawString("Summer: " + this.configDatas.Summer, normalfont, Brushes.Black, 200, 165);
            g.DrawString("Tempaeratur Korrektur: " + this.configDatas.Temp_Korr, normalfont, Brushes.Black, 200, 165);
            g.DrawString("Beschwadungsmenge: " + this.configDatas.Beschwandung_Menge, normalfont, Brushes.Black, 400, 165);
            g.DrawString("Ende Beep (100ms): " + this.configDatas.Ende_Beep, normalfont, Brushes.Black, 650, 165);
            g.DrawString("Zeit Klappe: " + this.configDatas.Zeit_Klappe, normalfont, Brushes.Black, 200, 180);
            g.DrawString("CoolDown180: " + this.configDatas.CoolDown_180, normalfont, Brushes.Black, 400, 180);
            g.DrawString("CoolDown200: " + this.configDatas.CoolDown_200, normalfont, Brushes.Black, 650, 180);
            g.DrawString("Energiesaving Min.: " + this.configDatas.Energiesaving_Min, normalfont, Brushes.Black, 200, 195);
            g.DrawString("Energiesaving Temp.: " + this.configDatas.Energiesaving_Temp, normalfont, Brushes.Black, 400, 195);

            g.DrawString("Anzahl Backvorgänge: " + this.configDatas.Anzahl_Backwaren, normalfont, Brushes.Black, 200, 225);
            g.DrawString("Filterwarnung: " + this.configDatas.Betrieb_Warn_Cnt, normalfont, Brushes.Black, 400, 225);

            g.DrawString("Betriebsstundenzähler", headlinefont2, Brushes.Black, 200, 275);
            g.DrawString("Stunden: " + this.configDatas.Betrieb_Std, normalfont, Brushes.Black, 250, 315);
            g.DrawString("Minuten: " + this.configDatas.Betrieb_Min, normalfont, Brushes.Black, 400, 315);
            g.DrawString("Sekunden: " + this.configDatas.Betrieb_Sek, normalfont, Brushes.Black, 550, 315);
            g.DrawString("Overflow: " + this.configDatas.Betrieb_Overflow, normalfont, Brushes.Black, 700, 315);

            g.DrawString("Fehlercodeszähler", headlinefont2, Brushes.Black, 200, 375);
            g.DrawString("1: " + this.configDatas.Code_Error_Cnt, normalfont, Brushes.Black, 250, 415);
            g.DrawString("2: " + this.configDatas.Code_Error_Wait, normalfont, Brushes.Black, 350, 415);

            g.DrawString("Einstellungsversion", headlinefont2, Brushes.Black, 200, 475);
            g.DrawString("1: " + this.configDatas.Setup_Code_1, normalfont, Brushes.Black, 250, 515);
            g.DrawString("2: " + this.configDatas.Setup_Code_2, normalfont, Brushes.Black, 350, 515);

            g.DrawString("Zustandsspeicher Backofen", headlinefont2, Brushes.Black, 200, 575);
            g.DrawString("Letzter State: " + this.configDatas.Letzter_State, normalfont, Brushes.Black, 250, 615);
            g.DrawString("Letzte Backware: " + this.configDatas.Letzte_Backware, normalfont, Brushes.Black, 450, 615);

            g.DrawString("Restezeiten(min)", headlinefont2, Brushes.Black, 200, 675);
            g.DrawString("1: " + this.configDatas.Restzeit_Min[0], normalfont, Brushes.Black, 250, 715);
            g.DrawString("2: " + this.configDatas.Restzeit_Min[1], normalfont, Brushes.Black, 350, 715);
            g.DrawString("3: " + this.configDatas.Restzeit_Min[2], normalfont, Brushes.Black, 450, 715);
            g.DrawString("4: " + this.configDatas.Restzeit_Min[3], normalfont, Brushes.Black, 550, 715);
            //g.DrawString("5: " + this.configDatas.Restzeit_Min[4], normalfont, Brushes.Black, 650, 715);
        }

        private void printBackprogramme_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            FontFamily type = new FontFamily("Arial");
            Font headlinefont = new Font(type, 24, FontStyle.Bold, GraphicsUnit.Pixel);
            Font headlinefont2 = new Font(type, 18, FontStyle.Bold, GraphicsUnit.Pixel);
            Font normalfont = new Font(type, 12, FontStyle.Regular, GraphicsUnit.Pixel);
            //printBackprogramme.DefaultPageSettings.Landscape = true;

            g.DrawString("Backdaten", headlinefont, Brushes.Black, 10, 10);

            /*foreach (BakingProgram obj in bakingPorgArr)
            {
                g.DrawString(obj.Backware_Name, normalfont, Brushes.Black, 20, 20);
            }*/

            g.DrawString("Programmname: " + bakingPorgArr[curpage].Backware_Name, normalfont, Brushes.Black, 50, 50);

            g.DrawString("Vorheiztemperatur: " + bakingPorgArr[curpage].Vorheiztemp, normalfont, Brushes.Black, 275, 50);
            g.DrawString("Grundmenge: " + bakingPorgArr[curpage].Grundmenge, normalfont, Brushes.Black, 450, 50);
            g.DrawString("Soft Heat: " + bakingPorgArr[curpage].P_Softheat, normalfont, Brushes.Black, 650, 50);

            g.DrawString("Abkühltemperatur: " + bakingPorgArr[curpage].Abkuehltemp, normalfont, Brushes.Black, 450, 75);
            g.DrawString("Ventilator Stop: " + bakingPorgArr[curpage].P_Stop, normalfont, Brushes.Black, 650, 75);

            g.DrawString("Backzeit", headlinefont2, Brushes.Black, 50, 150);
            g.DrawString("1: " + bakingPorgArr[curpage].P_Zeit_Phase1, normalfont, Brushes.Black, 100, 190);
            g.DrawString("2: " + bakingPorgArr[curpage].P_Zeit_Phase2, normalfont, Brushes.Black, 200, 190);
            g.DrawString("3: " + bakingPorgArr[curpage].P_Zeit_Phase3, normalfont, Brushes.Black, 300, 190);
            g.DrawString("4: " + bakingPorgArr[curpage].P_Zeit_Phase4, normalfont, Brushes.Black, 400, 190);

            g.DrawString("Backtemperatur", headlinefont2, Brushes.Black, 50, 250);
            g.DrawString("1: " + bakingPorgArr[curpage].P_Temp_Phase1, normalfont, Brushes.Black, 100, 290);
            g.DrawString("2: " + bakingPorgArr[curpage].P_Temp_Phase2, normalfont, Brushes.Black, 200, 290);
            g.DrawString("3: " + bakingPorgArr[curpage].P_Temp_Phase3, normalfont, Brushes.Black, 300, 290);
            g.DrawString("4: " + bakingPorgArr[curpage].P_Temp_Phase4, normalfont, Brushes.Black, 400, 290);

            g.DrawString("Beschwadung Menge", headlinefont2, Brushes.Black, 50, 350);
            g.DrawString("1: " + bakingPorgArr[curpage].P_Menge_Phase1, normalfont, Brushes.Black, 100, 390);
            g.DrawString("2: " + bakingPorgArr[curpage].P_Menge_Phase2, normalfont, Brushes.Black, 200, 390);
            g.DrawString("3: " + bakingPorgArr[curpage].P_Menge_Phase3, normalfont, Brushes.Black, 300, 390);
            g.DrawString("4: " + bakingPorgArr[curpage].P_Menge_Phase4, normalfont, Brushes.Black, 400, 390);

            g.DrawString("Beschwadung Dauer", headlinefont2, Brushes.Black, 50, 450);
            g.DrawString("1: " + bakingPorgArr[curpage].P_Dauer_Phase1, normalfont, Brushes.Black, 100, 490);
            g.DrawString("2: " + bakingPorgArr[curpage].P_Dauer_Phase2, normalfont, Brushes.Black, 200, 490);
            g.DrawString("3: " + bakingPorgArr[curpage].P_Dauer_Phase3, normalfont, Brushes.Black, 300, 490);
            g.DrawString("4: " + bakingPorgArr[curpage].P_Dauer_Phase4, normalfont, Brushes.Black, 400, 490);

            g.DrawString("Ventilatorleistung", headlinefont2, Brushes.Black, 50, 550);
            g.DrawString("1: " + bakingPorgArr[curpage].P_Prozent_Phase1, normalfont, Brushes.Black, 100, 590);
            g.DrawString("2: " + bakingPorgArr[curpage].P_Prozent_Phase2, normalfont, Brushes.Black, 200, 590);
            g.DrawString("3: " + bakingPorgArr[curpage].P_Prozent_Phase3, normalfont, Brushes.Black, 300, 590);
            g.DrawString("4: " + bakingPorgArr[curpage].P_Prozent_Phase4, normalfont, Brushes.Black, 400, 590);

            g.DrawString("Klappenstellung", headlinefont2, Brushes.Black, 50, 650);
            g.DrawString("1: " + bakingPorgArr[curpage].P_Klappe_Phase1, normalfont, Brushes.Black, 100, 690);
            g.DrawString("2: " + bakingPorgArr[curpage].P_Klappe_Phase2, normalfont, Brushes.Black, 200, 690);
            g.DrawString("3: " + bakingPorgArr[curpage].P_Klappe_Phase3, normalfont, Brushes.Black, 300, 690);
            g.DrawString("4: " + bakingPorgArr[curpage].P_Klappe_Phase4, normalfont, Brushes.Black, 400, 690);
         
            curpage++;
            if (curpage < trVBackprogramme.Nodes[0].Nodes.Count) e.HasMorePages = true;
        }
    }
}