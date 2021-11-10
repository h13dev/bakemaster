namespace Tritronix.BakingOven
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            this.connection.Exit();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Backwaren");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbPVerbindungen = new System.Windows.Forms.TabPage();
            this.label40 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.cmBDatabits = new System.Windows.Forms.ComboBox();
            this.cmBStopbit = new System.Windows.Forms.ComboBox();
            this.cmBParity = new System.Windows.Forms.ComboBox();
            this.cmBBaudrate = new System.Windows.Forms.ComboBox();
            this.cmBCOM_Ports = new System.Windows.Forms.ComboBox();
            this.tbPBackProgramm = new System.Windows.Forms.TabPage();
            this.trVBackprogramme = new System.Windows.Forms.TreeView();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemDatei = new System.Windows.Forms.ToolStripMenuItem();
            this.neuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.öffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speichernUnterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.sprachdatenÖffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sprachdatenSpeichernUnterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.configDatenOeffnen = new System.Windows.Forms.ToolStripMenuItem();
            this.configDatenspeichern = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.übertragungToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backdatenAbrufenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.backdatenSendenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.druckenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backprogrammeDruckenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.konfigurationenDruckenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label41 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.txtSoftHeat = new System.Windows.Forms.TextBox();
            this.txtVentilatorStop = new System.Windows.Forms.TextBox();
            this.txtKlappenstellung3 = new System.Windows.Forms.TextBox();
            this.txtKlappenstellung4 = new System.Windows.Forms.TextBox();
            this.txtKlappenstellung2 = new System.Windows.Forms.TextBox();
            this.txtKlappenstellung1 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtVentilatorleistung3 = new System.Windows.Forms.TextBox();
            this.txtVentilatorleistung4 = new System.Windows.Forms.TextBox();
            this.txtVentilatorleistung2 = new System.Windows.Forms.TextBox();
            this.txtVentilatorleistung1 = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtBeschwdauer3 = new System.Windows.Forms.TextBox();
            this.txtBeschwdauer4 = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.txtBeschwdauer2 = new System.Windows.Forms.TextBox();
            this.txtBeschwdauer1 = new System.Windows.Forms.TextBox();
            this.txtBeschwmenge3 = new System.Windows.Forms.TextBox();
            this.txtBeschwmenge4 = new System.Windows.Forms.TextBox();
            this.txtBeschwmenge2 = new System.Windows.Forms.TextBox();
            this.txtBeschwmenge1 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtBacktemp4 = new System.Windows.Forms.TextBox();
            this.txtBacktemp3 = new System.Windows.Forms.TextBox();
            this.txtBacktemp2 = new System.Windows.Forms.TextBox();
            this.txtBacktemp1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBackzeit3 = new System.Windows.Forms.TextBox();
            this.txtBackzeit4 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBackzeit2 = new System.Windows.Forms.TextBox();
            this.txtBackzeit1 = new System.Windows.Forms.TextBox();
            this.txtGrundmenge = new System.Windows.Forms.TextBox();
            this.txtAbkühltemp = new System.Windows.Forms.TextBox();
            this.txtVorheiztemp = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txb_lang = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtPowerReduction = new System.Windows.Forms.TextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.txtAnzahl_Backwaren = new System.Windows.Forms.TextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.txtTemp_Korr = new System.Windows.Forms.TextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.txtEnergiesaving_temp = new System.Windows.Forms.TextBox();
            this.label57 = new System.Windows.Forms.Label();
            this.txtEnergieSaving_Min = new System.Windows.Forms.TextBox();
            this.label56 = new System.Windows.Forms.Label();
            this.cmbZusatzheizung = new System.Windows.Forms.ComboBox();
            this.label55 = new System.Windows.Forms.Label();
            this.cmbDoorActive = new System.Windows.Forms.ComboBox();
            this.label54 = new System.Windows.Forms.Label();
            this.cmbAutostart = new System.Windows.Forms.ComboBox();
            this.label53 = new System.Windows.Forms.Label();
            this.cmbWasserSensor = new System.Windows.Forms.ComboBox();
            this.label52 = new System.Windows.Forms.Label();
            this.cmbExt_Summer = new System.Windows.Forms.ComboBox();
            this.label50 = new System.Windows.Forms.Label();
            this.txtZeit_Klappe = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.txtCoolDown_200 = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.txtCoolDown_180 = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.txtBeschwandungs_Menge = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.txtBeschwandungs_Pause = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.txtEnde_Beep = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.txtSummer = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.txtKontrast = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.cmbFlaps = new System.Windows.Forms.ComboBox();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txtRestzeit_Min5 = new System.Windows.Forms.TextBox();
            this.txtRestzeit_Min4 = new System.Windows.Forms.TextBox();
            this.txtRestzeit_Min3 = new System.Windows.Forms.TextBox();
            this.txtRestzeit_Min2 = new System.Windows.Forms.TextBox();
            this.txtRestzeit_Min1 = new System.Windows.Forms.TextBox();
            this.txtLetzte_Backware = new System.Windows.Forms.TextBox();
            this.txtLetzter_State = new System.Windows.Forms.TextBox();
            this.txtSetup_Code2 = new System.Windows.Forms.TextBox();
            this.txtSetup_Code1 = new System.Windows.Forms.TextBox();
            this.txtBetrieb_Warn_Cnt = new System.Windows.Forms.TextBox();
            this.txtError_Wait = new System.Windows.Forms.TextBox();
            this.txtError_Cnt = new System.Windows.Forms.TextBox();
            this.txtBetrieb_Overflow = new System.Windows.Forms.TextBox();
            this.txtBetrieb_Sek = new System.Windows.Forms.TextBox();
            this.txtBetrieb_Min = new System.Windows.Forms.TextBox();
            this.txtBetrieb_Std = new System.Windows.Forms.TextBox();
            this.txtNumberOfBakingActs = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblFehlercodeszaehler = new System.Windows.Forms.Label();
            this.lblFilterwarnung = new System.Windows.Forms.Label();
            this.lblBetriebsstundenzähler = new System.Windows.Forms.Label();
            this.lblNumberofBakingActs = new System.Windows.Forms.Label();
            this.printBackprogramme = new System.Drawing.Printing.PrintDocument();
            this.printKonfiguration = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDialog2 = new System.Windows.Forms.PrintDialog();
            this.statusStrip.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbPVerbindungen.SuspendLayout();
            this.tbPBackProgramm.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 318);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(743, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(136, 17);
            this.toolStripStatusLabel1.Text = "(c) 2011, Tritronix GmbH";
            this.toolStripStatusLabel1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbPVerbindungen);
            this.tabControl1.Controls.Add(this.tbPBackProgramm);
            this.tabControl1.Location = new System.Drawing.Point(10, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(148, 279);
            this.tabControl1.TabIndex = 0;
            // 
            // tbPVerbindungen
            // 
            this.tbPVerbindungen.Controls.Add(this.label40);
            this.tbPVerbindungen.Controls.Add(this.label39);
            this.tbPVerbindungen.Controls.Add(this.label38);
            this.tbPVerbindungen.Controls.Add(this.label37);
            this.tbPVerbindungen.Controls.Add(this.label36);
            this.tbPVerbindungen.Controls.Add(this.cmBDatabits);
            this.tbPVerbindungen.Controls.Add(this.cmBStopbit);
            this.tbPVerbindungen.Controls.Add(this.cmBParity);
            this.tbPVerbindungen.Controls.Add(this.cmBBaudrate);
            this.tbPVerbindungen.Controls.Add(this.cmBCOM_Ports);
            this.tbPVerbindungen.Location = new System.Drawing.Point(4, 22);
            this.tbPVerbindungen.Name = "tbPVerbindungen";
            this.tbPVerbindungen.Size = new System.Drawing.Size(140, 253);
            this.tbPVerbindungen.TabIndex = 2;
            this.tbPVerbindungen.Text = "Verbindung";
            this.tbPVerbindungen.UseVisualStyleBackColor = true;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(16, 189);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(48, 13);
            this.label40.TabIndex = 10;
            this.label40.Text = "Stopbits:";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(16, 149);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(55, 13);
            this.label39.TabIndex = 9;
            this.label39.Text = "Datenbits:";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(16, 109);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(40, 13);
            this.label38.TabIndex = 8;
            this.label38.Text = "Parität:";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(16, 69);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(53, 13);
            this.label37.TabIndex = 7;
            this.label37.Text = "Baudrate:";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(16, 28);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(67, 13);
            this.label36.TabIndex = 6;
            this.label36.Text = "Schnittstelle:";
            // 
            // cmBDatabits
            // 
            this.cmBDatabits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmBDatabits.FormattingEnabled = true;
            this.cmBDatabits.Location = new System.Drawing.Point(19, 165);
            this.cmBDatabits.Name = "cmBDatabits";
            this.cmBDatabits.Size = new System.Drawing.Size(101, 21);
            this.cmBDatabits.TabIndex = 5;
            // 
            // cmBStopbit
            // 
            this.cmBStopbit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmBStopbit.FormattingEnabled = true;
            this.cmBStopbit.Location = new System.Drawing.Point(19, 205);
            this.cmBStopbit.Name = "cmBStopbit";
            this.cmBStopbit.Size = new System.Drawing.Size(101, 21);
            this.cmBStopbit.TabIndex = 6;
            // 
            // cmBParity
            // 
            this.cmBParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmBParity.Enabled = false;
            this.cmBParity.FormattingEnabled = true;
            this.cmBParity.Location = new System.Drawing.Point(19, 125);
            this.cmBParity.Name = "cmBParity";
            this.cmBParity.Size = new System.Drawing.Size(101, 21);
            this.cmBParity.TabIndex = 4;
            // 
            // cmBBaudrate
            // 
            this.cmBBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmBBaudrate.FormattingEnabled = true;
            this.cmBBaudrate.Location = new System.Drawing.Point(19, 85);
            this.cmBBaudrate.Name = "cmBBaudrate";
            this.cmBBaudrate.Size = new System.Drawing.Size(101, 21);
            this.cmBBaudrate.TabIndex = 3;
            // 
            // cmBCOM_Ports
            // 
            this.cmBCOM_Ports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmBCOM_Ports.FormattingEnabled = true;
            this.cmBCOM_Ports.Location = new System.Drawing.Point(19, 45);
            this.cmBCOM_Ports.Name = "cmBCOM_Ports";
            this.cmBCOM_Ports.Size = new System.Drawing.Size(101, 21);
            this.cmBCOM_Ports.TabIndex = 2;
            // 
            // tbPBackProgramm
            // 
            this.tbPBackProgramm.Controls.Add(this.trVBackprogramme);
            this.tbPBackProgramm.Location = new System.Drawing.Point(4, 22);
            this.tbPBackProgramm.Name = "tbPBackProgramm";
            this.tbPBackProgramm.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tbPBackProgramm.Size = new System.Drawing.Size(140, 253);
            this.tbPBackProgramm.TabIndex = 1;
            this.tbPBackProgramm.Text = "Backprogramme";
            this.tbPBackProgramm.UseVisualStyleBackColor = true;
            // 
            // trVBackprogramme
            // 
            this.trVBackprogramme.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trVBackprogramme.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trVBackprogramme.Location = new System.Drawing.Point(0, 0);
            this.trVBackprogramme.Name = "trVBackprogramme";
            treeNode1.Name = "Backwaren";
            treeNode1.Text = "Backwaren";
            this.trVBackprogramme.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.trVBackprogramme.Size = new System.Drawing.Size(139, 247);
            this.trVBackprogramme.TabIndex = 7;
            this.trVBackprogramme.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trVBackprogramme_NodeMouseClick);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDatei,
            this.übertragungToolStripMenuItem,
            this.druckenToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(743, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip";
            // 
            // toolStripMenuItemDatei
            // 
            this.toolStripMenuItemDatei.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.neuToolStripMenuItem,
            this.toolStripSeparator5,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripSeparator2,
            this.öffnenToolStripMenuItem,
            this.speichernUnterToolStripMenuItem,
            this.toolStripSeparator3,
            this.sprachdatenÖffnenToolStripMenuItem,
            this.sprachdatenSpeichernUnterToolStripMenuItem,
            this.toolStripSeparator1,
            this.configDatenOeffnen,
            this.configDatenspeichern,
            this.toolStripSeparator4,
            this.beendenToolStripMenuItem});
            this.toolStripMenuItemDatei.Name = "toolStripMenuItemDatei";
            this.toolStripMenuItemDatei.Size = new System.Drawing.Size(46, 20);
            this.toolStripMenuItemDatei.Text = "Datei";
            // 
            // neuToolStripMenuItem
            // 
            this.neuToolStripMenuItem.Name = "neuToolStripMenuItem";
            this.neuToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.neuToolStripMenuItem.Text = "Neu";
            this.neuToolStripMenuItem.Click += new System.EventHandler(this.neuToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(231, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(234, 22);
            this.toolStripMenuItem1.Text = "Öffnen...";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(234, 22);
            this.toolStripMenuItem2.Text = "Speichern unter...";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(231, 6);
            // 
            // öffnenToolStripMenuItem
            // 
            this.öffnenToolStripMenuItem.Name = "öffnenToolStripMenuItem";
            this.öffnenToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.öffnenToolStripMenuItem.Text = "Backdaten öffnen...";
            this.öffnenToolStripMenuItem.Click += new System.EventHandler(this.öffnenToolStripMenuItem_Click);
            // 
            // speichernUnterToolStripMenuItem
            // 
            this.speichernUnterToolStripMenuItem.Name = "speichernUnterToolStripMenuItem";
            this.speichernUnterToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.speichernUnterToolStripMenuItem.Text = "Backdaten speichern unter...";
            this.speichernUnterToolStripMenuItem.Click += new System.EventHandler(this.speichernUnterToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(231, 6);
            // 
            // sprachdatenÖffnenToolStripMenuItem
            // 
            this.sprachdatenÖffnenToolStripMenuItem.Name = "sprachdatenÖffnenToolStripMenuItem";
            this.sprachdatenÖffnenToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.sprachdatenÖffnenToolStripMenuItem.Text = "Sprachdaten öffnen...";
            this.sprachdatenÖffnenToolStripMenuItem.Click += new System.EventHandler(this.sprachdatenÖffnenToolStripMenuItem_Click);
            // 
            // sprachdatenSpeichernUnterToolStripMenuItem
            // 
            this.sprachdatenSpeichernUnterToolStripMenuItem.Name = "sprachdatenSpeichernUnterToolStripMenuItem";
            this.sprachdatenSpeichernUnterToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.sprachdatenSpeichernUnterToolStripMenuItem.Text = "Sprachdaten speichern unter...";
            this.sprachdatenSpeichernUnterToolStripMenuItem.Click += new System.EventHandler(this.sprachdatenSpeichernUnterToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(231, 6);
            // 
            // configDatenOeffnen
            // 
            this.configDatenOeffnen.Name = "configDatenOeffnen";
            this.configDatenOeffnen.Size = new System.Drawing.Size(234, 22);
            this.configDatenOeffnen.Text = "Konfigdaten öffnen...";
            this.configDatenOeffnen.Click += new System.EventHandler(this.configDatenOeffnen_Click);
            // 
            // configDatenspeichern
            // 
            this.configDatenspeichern.Name = "configDatenspeichern";
            this.configDatenspeichern.Size = new System.Drawing.Size(234, 22);
            this.configDatenspeichern.Text = "Konfigdaten speichern unter...";
            this.configDatenspeichern.Click += new System.EventHandler(this.configDatenspeichern_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(231, 6);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.beendenToolStripMenuItem.Text = "Beenden";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // übertragungToolStripMenuItem
            // 
            this.übertragungToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backdatenAbrufenToolStripMenuItem1,
            this.backdatenSendenToolStripMenuItem1});
            this.übertragungToolStripMenuItem.Name = "übertragungToolStripMenuItem";
            this.übertragungToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.übertragungToolStripMenuItem.Text = "Übertragung";
            // 
            // backdatenAbrufenToolStripMenuItem1
            // 
            this.backdatenAbrufenToolStripMenuItem1.Name = "backdatenAbrufenToolStripMenuItem1";
            this.backdatenAbrufenToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.backdatenAbrufenToolStripMenuItem1.Text = "Daten abrufen";
            this.backdatenAbrufenToolStripMenuItem1.Click += new System.EventHandler(this.backdatenAbrufenToolStripMenuItem1_Click);
            // 
            // backdatenSendenToolStripMenuItem1
            // 
            this.backdatenSendenToolStripMenuItem1.Name = "backdatenSendenToolStripMenuItem1";
            this.backdatenSendenToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.backdatenSendenToolStripMenuItem1.Text = "Daten senden";
            this.backdatenSendenToolStripMenuItem1.Click += new System.EventHandler(this.backdatenSendenToolStripMenuItem1_Click);
            // 
            // druckenToolStripMenuItem
            // 
            this.druckenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backprogrammeDruckenToolStripMenuItem,
            this.konfigurationenDruckenToolStripMenuItem});
            this.druckenToolStripMenuItem.Name = "druckenToolStripMenuItem";
            this.druckenToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.druckenToolStripMenuItem.Text = "Drucken";
            // 
            // backprogrammeDruckenToolStripMenuItem
            // 
            this.backprogrammeDruckenToolStripMenuItem.Enabled = false;
            this.backprogrammeDruckenToolStripMenuItem.Name = "backprogrammeDruckenToolStripMenuItem";
            this.backprogrammeDruckenToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.backprogrammeDruckenToolStripMenuItem.Text = "Backprogramme Drucken";
            this.backprogrammeDruckenToolStripMenuItem.Click += new System.EventHandler(this.backprogrammeDruckenToolStripMenuItem_Click);
            // 
            // konfigurationenDruckenToolStripMenuItem
            // 
            this.konfigurationenDruckenToolStripMenuItem.Enabled = false;
            this.konfigurationenDruckenToolStripMenuItem.Name = "konfigurationenDruckenToolStripMenuItem";
            this.konfigurationenDruckenToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.konfigurationenDruckenToolStripMenuItem.Text = "Konfigurationen Drucken";
            this.konfigurationenDruckenToolStripMenuItem.Click += new System.EventHandler(this.konfigurationenDruckenToolStripMenuItem_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Location = new System.Drawing.Point(170, 32);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(564, 279);
            this.tabControl2.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label41);
            this.tabPage1.Controls.Add(this.txtName);
            this.tabPage1.Controls.Add(this.label34);
            this.tabPage1.Controls.Add(this.label35);
            this.tabPage1.Controls.Add(this.txtSoftHeat);
            this.tabPage1.Controls.Add(this.txtVentilatorStop);
            this.tabPage1.Controls.Add(this.txtKlappenstellung3);
            this.tabPage1.Controls.Add(this.txtKlappenstellung4);
            this.tabPage1.Controls.Add(this.txtKlappenstellung2);
            this.tabPage1.Controls.Add(this.txtKlappenstellung1);
            this.tabPage1.Controls.Add(this.label23);
            this.tabPage1.Controls.Add(this.txtVentilatorleistung3);
            this.tabPage1.Controls.Add(this.txtVentilatorleistung4);
            this.tabPage1.Controls.Add(this.txtVentilatorleistung2);
            this.tabPage1.Controls.Add(this.txtVentilatorleistung1);
            this.tabPage1.Controls.Add(this.label28);
            this.tabPage1.Controls.Add(this.txtBeschwdauer3);
            this.tabPage1.Controls.Add(this.txtBeschwdauer4);
            this.tabPage1.Controls.Add(this.label33);
            this.tabPage1.Controls.Add(this.txtBeschwdauer2);
            this.tabPage1.Controls.Add(this.txtBeschwdauer1);
            this.tabPage1.Controls.Add(this.txtBeschwmenge3);
            this.tabPage1.Controls.Add(this.txtBeschwmenge4);
            this.tabPage1.Controls.Add(this.txtBeschwmenge2);
            this.tabPage1.Controls.Add(this.txtBeschwmenge1);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.txtBacktemp4);
            this.tabPage1.Controls.Add(this.txtBacktemp3);
            this.tabPage1.Controls.Add(this.txtBacktemp2);
            this.tabPage1.Controls.Add(this.txtBacktemp1);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.txtBackzeit3);
            this.tabPage1.Controls.Add(this.txtBackzeit4);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtBackzeit2);
            this.tabPage1.Controls.Add(this.txtBackzeit1);
            this.tabPage1.Controls.Add(this.txtGrundmenge);
            this.tabPage1.Controls.Add(this.txtAbkühltemp);
            this.tabPage1.Controls.Add(this.txtVorheiztemp);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage1.Size = new System.Drawing.Size(556, 253);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Backprogramme";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(6, 3);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(83, 13);
            this.label41.TabIndex = 131;
            this.label41.Text = "Programmname:";
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(9, 19);
            this.txtName.MaxLength = 16;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(121, 20);
            this.txtName.TabIndex = 9;
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label34.Location = new System.Drawing.Point(262, 41);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(55, 13);
            this.label34.TabIndex = 129;
            this.label34.Text = "Soft Heat:";
            this.label34.Visible = false;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label35.Location = new System.Drawing.Point(133, 81);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(79, 13);
            this.label35.TabIndex = 128;
            this.label35.Text = "Ventilator Stop:";
            // 
            // txtSoftHeat
            // 
            this.txtSoftHeat.Enabled = false;
            this.txtSoftHeat.Location = new System.Drawing.Point(264, 58);
            this.txtSoftHeat.Name = "txtSoftHeat";
            this.txtSoftHeat.Size = new System.Drawing.Size(121, 20);
            this.txtSoftHeat.TabIndex = 12;
            this.txtSoftHeat.Visible = false;
            this.txtSoftHeat.Leave += new System.EventHandler(this.txtSoftHeat_Leave);
            // 
            // txtVentilatorStop
            // 
            this.txtVentilatorStop.Enabled = false;
            this.txtVentilatorStop.Location = new System.Drawing.Point(132, 98);
            this.txtVentilatorStop.Name = "txtVentilatorStop";
            this.txtVentilatorStop.Size = new System.Drawing.Size(121, 20);
            this.txtVentilatorStop.TabIndex = 14;
            this.txtVentilatorStop.Leave += new System.EventHandler(this.txtVentilatorStop_Leave);
            // 
            // txtKlappenstellung3
            // 
            this.txtKlappenstellung3.Enabled = false;
            this.txtKlappenstellung3.Location = new System.Drawing.Point(475, 185);
            this.txtKlappenstellung3.Name = "txtKlappenstellung3";
            this.txtKlappenstellung3.Size = new System.Drawing.Size(47, 20);
            this.txtKlappenstellung3.TabIndex = 37;
            this.txtKlappenstellung3.Leave += new System.EventHandler(this.txtKlappenstellung3_Leave);
            // 
            // txtKlappenstellung4
            // 
            this.txtKlappenstellung4.Enabled = false;
            this.txtKlappenstellung4.Location = new System.Drawing.Point(475, 210);
            this.txtKlappenstellung4.Name = "txtKlappenstellung4";
            this.txtKlappenstellung4.Size = new System.Drawing.Size(47, 20);
            this.txtKlappenstellung4.TabIndex = 38;
            this.txtKlappenstellung4.Leave += new System.EventHandler(this.txtKlappenstellung4_Leave);
            // 
            // txtKlappenstellung2
            // 
            this.txtKlappenstellung2.Enabled = false;
            this.txtKlappenstellung2.Location = new System.Drawing.Point(475, 161);
            this.txtKlappenstellung2.Name = "txtKlappenstellung2";
            this.txtKlappenstellung2.Size = new System.Drawing.Size(47, 20);
            this.txtKlappenstellung2.TabIndex = 36;
            this.txtKlappenstellung2.Leave += new System.EventHandler(this.txtKlappenstellung2_Leave);
            // 
            // txtKlappenstellung1
            // 
            this.txtKlappenstellung1.Enabled = false;
            this.txtKlappenstellung1.Location = new System.Drawing.Point(475, 136);
            this.txtKlappenstellung1.Name = "txtKlappenstellung1";
            this.txtKlappenstellung1.Size = new System.Drawing.Size(47, 20);
            this.txtKlappenstellung1.TabIndex = 35;
            this.txtKlappenstellung1.Leave += new System.EventHandler(this.txtKlappenstellung1_Leave);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label23.Location = new System.Drawing.Point(472, 120);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(82, 13);
            this.label23.TabIndex = 117;
            this.label23.Text = "Klappenstellung";
            // 
            // txtVentilatorleistung3
            // 
            this.txtVentilatorleistung3.Enabled = false;
            this.txtVentilatorleistung3.Location = new System.Drawing.Point(385, 186);
            this.txtVentilatorleistung3.Name = "txtVentilatorleistung3";
            this.txtVentilatorleistung3.Size = new System.Drawing.Size(47, 20);
            this.txtVentilatorleistung3.TabIndex = 33;
            this.txtVentilatorleistung3.Leave += new System.EventHandler(this.txtVentilatorleistung3_Leave);
            // 
            // txtVentilatorleistung4
            // 
            this.txtVentilatorleistung4.Enabled = false;
            this.txtVentilatorleistung4.Location = new System.Drawing.Point(385, 210);
            this.txtVentilatorleistung4.Name = "txtVentilatorleistung4";
            this.txtVentilatorleistung4.Size = new System.Drawing.Size(47, 20);
            this.txtVentilatorleistung4.TabIndex = 34;
            this.txtVentilatorleistung4.Leave += new System.EventHandler(this.txtVentilatorleistung4_Leave);
            // 
            // txtVentilatorleistung2
            // 
            this.txtVentilatorleistung2.Enabled = false;
            this.txtVentilatorleistung2.Location = new System.Drawing.Point(385, 162);
            this.txtVentilatorleistung2.Name = "txtVentilatorleistung2";
            this.txtVentilatorleistung2.Size = new System.Drawing.Size(47, 20);
            this.txtVentilatorleistung2.TabIndex = 32;
            this.txtVentilatorleistung2.Leave += new System.EventHandler(this.txtVentilatorleistung2_Leave);
            // 
            // txtVentilatorleistung1
            // 
            this.txtVentilatorleistung1.Enabled = false;
            this.txtVentilatorleistung1.Location = new System.Drawing.Point(385, 137);
            this.txtVentilatorleistung1.Name = "txtVentilatorleistung1";
            this.txtVentilatorleistung1.Size = new System.Drawing.Size(47, 20);
            this.txtVentilatorleistung1.TabIndex = 31;
            this.txtVentilatorleistung1.Leave += new System.EventHandler(this.txtVentilatorleistung1_Leave);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label28.Location = new System.Drawing.Point(382, 120);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(87, 13);
            this.label28.TabIndex = 108;
            this.label28.Text = "Ventilatorleistung";
            // 
            // txtBeschwdauer3
            // 
            this.txtBeschwdauer3.Enabled = false;
            this.txtBeschwdauer3.Location = new System.Drawing.Point(296, 186);
            this.txtBeschwdauer3.Name = "txtBeschwdauer3";
            this.txtBeschwdauer3.Size = new System.Drawing.Size(47, 20);
            this.txtBeschwdauer3.TabIndex = 29;
            this.txtBeschwdauer3.Leave += new System.EventHandler(this.txtBeschwdauer3_Leave);
            // 
            // txtBeschwdauer4
            // 
            this.txtBeschwdauer4.Enabled = false;
            this.txtBeschwdauer4.Location = new System.Drawing.Point(295, 210);
            this.txtBeschwdauer4.Name = "txtBeschwdauer4";
            this.txtBeschwdauer4.Size = new System.Drawing.Size(47, 20);
            this.txtBeschwdauer4.TabIndex = 30;
            this.txtBeschwdauer4.Leave += new System.EventHandler(this.txtBeschwdauer4_Leave);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label33.Location = new System.Drawing.Point(292, 120);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(80, 13);
            this.label33.TabIndex = 101;
            this.label33.Text = "Beschw. Dauer";
            // 
            // txtBeschwdauer2
            // 
            this.txtBeschwdauer2.Enabled = false;
            this.txtBeschwdauer2.Location = new System.Drawing.Point(296, 162);
            this.txtBeschwdauer2.Name = "txtBeschwdauer2";
            this.txtBeschwdauer2.Size = new System.Drawing.Size(47, 20);
            this.txtBeschwdauer2.TabIndex = 28;
            this.txtBeschwdauer2.Leave += new System.EventHandler(this.txtBeschwdauer2_Leave);
            // 
            // txtBeschwdauer1
            // 
            this.txtBeschwdauer1.Enabled = false;
            this.txtBeschwdauer1.Location = new System.Drawing.Point(295, 137);
            this.txtBeschwdauer1.Name = "txtBeschwdauer1";
            this.txtBeschwdauer1.Size = new System.Drawing.Size(47, 20);
            this.txtBeschwdauer1.TabIndex = 27;
            this.txtBeschwdauer1.Leave += new System.EventHandler(this.txtBeschwdauer1_Leave);
            // 
            // txtBeschwmenge3
            // 
            this.txtBeschwmenge3.Enabled = false;
            this.txtBeschwmenge3.Location = new System.Drawing.Point(205, 187);
            this.txtBeschwmenge3.Name = "txtBeschwmenge3";
            this.txtBeschwmenge3.Size = new System.Drawing.Size(47, 20);
            this.txtBeschwmenge3.TabIndex = 25;
            this.txtBeschwmenge3.Leave += new System.EventHandler(this.txtBeschwmenge3_Leave);
            // 
            // txtBeschwmenge4
            // 
            this.txtBeschwmenge4.Enabled = false;
            this.txtBeschwmenge4.Location = new System.Drawing.Point(205, 211);
            this.txtBeschwmenge4.Name = "txtBeschwmenge4";
            this.txtBeschwmenge4.Size = new System.Drawing.Size(47, 20);
            this.txtBeschwmenge4.TabIndex = 26;
            this.txtBeschwmenge4.Leave += new System.EventHandler(this.txtBeschwmenge4_Leave);
            // 
            // txtBeschwmenge2
            // 
            this.txtBeschwmenge2.Enabled = false;
            this.txtBeschwmenge2.Location = new System.Drawing.Point(205, 162);
            this.txtBeschwmenge2.Name = "txtBeschwmenge2";
            this.txtBeschwmenge2.Size = new System.Drawing.Size(47, 20);
            this.txtBeschwmenge2.TabIndex = 24;
            this.txtBeschwmenge2.Leave += new System.EventHandler(this.txtBeschwmenge2_Leave);
            // 
            // txtBeschwmenge1
            // 
            this.txtBeschwmenge1.Enabled = false;
            this.txtBeschwmenge1.Location = new System.Drawing.Point(205, 138);
            this.txtBeschwmenge1.Name = "txtBeschwmenge1";
            this.txtBeschwmenge1.Size = new System.Drawing.Size(47, 20);
            this.txtBeschwmenge1.TabIndex = 23;
            this.txtBeschwmenge1.Leave += new System.EventHandler(this.txtBeschwmenge1_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(202, 121);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(84, 13);
            this.label14.TabIndex = 90;
            this.label14.Text = "Beschw. Menge";
            // 
            // txtBacktemp4
            // 
            this.txtBacktemp4.Enabled = false;
            this.txtBacktemp4.Location = new System.Drawing.Point(115, 210);
            this.txtBacktemp4.Name = "txtBacktemp4";
            this.txtBacktemp4.Size = new System.Drawing.Size(47, 20);
            this.txtBacktemp4.TabIndex = 22;
            this.txtBacktemp4.Leave += new System.EventHandler(this.txtBacktemp4_Leave);
            // 
            // txtBacktemp3
            // 
            this.txtBacktemp3.Enabled = false;
            this.txtBacktemp3.Location = new System.Drawing.Point(115, 186);
            this.txtBacktemp3.Name = "txtBacktemp3";
            this.txtBacktemp3.Size = new System.Drawing.Size(47, 20);
            this.txtBacktemp3.TabIndex = 21;
            this.txtBacktemp3.Leave += new System.EventHandler(this.txtBacktemp3_Leave);
            // 
            // txtBacktemp2
            // 
            this.txtBacktemp2.Enabled = false;
            this.txtBacktemp2.Location = new System.Drawing.Point(115, 162);
            this.txtBacktemp2.Name = "txtBacktemp2";
            this.txtBacktemp2.Size = new System.Drawing.Size(47, 20);
            this.txtBacktemp2.TabIndex = 20;
            this.txtBacktemp2.Leave += new System.EventHandler(this.txtBacktemp2_Leave);
            // 
            // txtBacktemp1
            // 
            this.txtBacktemp1.Enabled = false;
            this.txtBacktemp1.Location = new System.Drawing.Point(115, 137);
            this.txtBacktemp1.Name = "txtBacktemp1";
            this.txtBacktemp1.Size = new System.Drawing.Size(47, 20);
            this.txtBacktemp1.TabIndex = 19;
            this.txtBacktemp1.Leave += new System.EventHandler(this.txtBacktemp1_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(112, 121);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 81;
            this.label9.Text = "Backtemperatur";
            // 
            // txtBackzeit3
            // 
            this.txtBackzeit3.Enabled = false;
            this.txtBackzeit3.Location = new System.Drawing.Point(27, 186);
            this.txtBackzeit3.Name = "txtBackzeit3";
            this.txtBackzeit3.Size = new System.Drawing.Size(47, 20);
            this.txtBackzeit3.TabIndex = 17;
            this.txtBackzeit3.Leave += new System.EventHandler(this.txtBackzeit3_Leave);
            // 
            // txtBackzeit4
            // 
            this.txtBackzeit4.Enabled = false;
            this.txtBackzeit4.Location = new System.Drawing.Point(27, 210);
            this.txtBackzeit4.Name = "txtBackzeit4";
            this.txtBackzeit4.Size = new System.Drawing.Size(47, 20);
            this.txtBackzeit4.TabIndex = 18;
            this.txtBackzeit4.Leave += new System.EventHandler(this.txtBackzeit4_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(6, 212);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 13);
            this.label8.TabIndex = 78;
            this.label8.Text = "4:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(7, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 13);
            this.label7.TabIndex = 77;
            this.label7.Text = "3:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(6, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 76;
            this.label6.Text = "2:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(6, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 75;
            this.label5.Text = "1:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(22, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 74;
            this.label4.Text = "Backzeit";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(133, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 73;
            this.label3.Text = "Grundmenge:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(6, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 72;
            this.label2.Text = "Abkühltemperatur:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(4, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 71;
            this.label1.Text = "Vorheiztemperatur:";
            // 
            // txtBackzeit2
            // 
            this.txtBackzeit2.Enabled = false;
            this.txtBackzeit2.Location = new System.Drawing.Point(27, 162);
            this.txtBackzeit2.Name = "txtBackzeit2";
            this.txtBackzeit2.Size = new System.Drawing.Size(47, 20);
            this.txtBackzeit2.TabIndex = 16;
            this.txtBackzeit2.Leave += new System.EventHandler(this.txtBackzeit2_Leave);
            // 
            // txtBackzeit1
            // 
            this.txtBackzeit1.Enabled = false;
            this.txtBackzeit1.Location = new System.Drawing.Point(27, 137);
            this.txtBackzeit1.Name = "txtBackzeit1";
            this.txtBackzeit1.Size = new System.Drawing.Size(47, 20);
            this.txtBackzeit1.TabIndex = 15;
            this.txtBackzeit1.Leave += new System.EventHandler(this.txtBackzeit1_Leave);
            // 
            // txtGrundmenge
            // 
            this.txtGrundmenge.Enabled = false;
            this.txtGrundmenge.Location = new System.Drawing.Point(135, 58);
            this.txtGrundmenge.Name = "txtGrundmenge";
            this.txtGrundmenge.Size = new System.Drawing.Size(121, 20);
            this.txtGrundmenge.TabIndex = 11;
            this.txtGrundmenge.Leave += new System.EventHandler(this.txtGrundmenge_Leave);
            // 
            // txtAbkühltemp
            // 
            this.txtAbkühltemp.Enabled = false;
            this.txtAbkühltemp.Location = new System.Drawing.Point(6, 98);
            this.txtAbkühltemp.Name = "txtAbkühltemp";
            this.txtAbkühltemp.Size = new System.Drawing.Size(121, 20);
            this.txtAbkühltemp.TabIndex = 13;
            this.txtAbkühltemp.Leave += new System.EventHandler(this.txtAbkühltemp_Leave);
            // 
            // txtVorheiztemp
            // 
            this.txtVorheiztemp.Enabled = false;
            this.txtVorheiztemp.Location = new System.Drawing.Point(9, 58);
            this.txtVorheiztemp.Name = "txtVorheiztemp";
            this.txtVorheiztemp.Size = new System.Drawing.Size(121, 20);
            this.txtVorheiztemp.TabIndex = 10;
            this.txtVorheiztemp.Leave += new System.EventHandler(this.txtVorheiztemp_Leave);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txb_lang);
            this.tabPage2.Controls.Add(this.listBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage2.Size = new System.Drawing.Size(556, 253);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sprachdatei";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txb_lang
            // 
            this.txb_lang.Enabled = false;
            this.txb_lang.Location = new System.Drawing.Point(132, 6);
            this.txb_lang.MaxLength = 16;
            this.txb_lang.Name = "txb_lang";
            this.txb_lang.Size = new System.Drawing.Size(120, 20);
            this.txb_lang.TabIndex = 40;
            // 
            // listBox1
            // 
            this.listBox1.Enabled = false;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 6);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 238);
            this.listBox1.TabIndex = 39;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtPowerReduction);
            this.tabPage3.Controls.Add(this.label60);
            this.tabPage3.Controls.Add(this.txtAnzahl_Backwaren);
            this.tabPage3.Controls.Add(this.label59);
            this.tabPage3.Controls.Add(this.txtTemp_Korr);
            this.tabPage3.Controls.Add(this.label58);
            this.tabPage3.Controls.Add(this.txtEnergiesaving_temp);
            this.tabPage3.Controls.Add(this.label57);
            this.tabPage3.Controls.Add(this.txtEnergieSaving_Min);
            this.tabPage3.Controls.Add(this.label56);
            this.tabPage3.Controls.Add(this.cmbZusatzheizung);
            this.tabPage3.Controls.Add(this.label55);
            this.tabPage3.Controls.Add(this.cmbDoorActive);
            this.tabPage3.Controls.Add(this.label54);
            this.tabPage3.Controls.Add(this.cmbAutostart);
            this.tabPage3.Controls.Add(this.label53);
            this.tabPage3.Controls.Add(this.cmbWasserSensor);
            this.tabPage3.Controls.Add(this.label52);
            this.tabPage3.Controls.Add(this.cmbExt_Summer);
            this.tabPage3.Controls.Add(this.label50);
            this.tabPage3.Controls.Add(this.txtZeit_Klappe);
            this.tabPage3.Controls.Add(this.label51);
            this.tabPage3.Controls.Add(this.txtCoolDown_200);
            this.tabPage3.Controls.Add(this.label49);
            this.tabPage3.Controls.Add(this.txtCoolDown_180);
            this.tabPage3.Controls.Add(this.label48);
            this.tabPage3.Controls.Add(this.txtBeschwandungs_Menge);
            this.tabPage3.Controls.Add(this.label47);
            this.tabPage3.Controls.Add(this.txtBeschwandungs_Pause);
            this.tabPage3.Controls.Add(this.label46);
            this.tabPage3.Controls.Add(this.txtEnde_Beep);
            this.tabPage3.Controls.Add(this.label45);
            this.tabPage3.Controls.Add(this.txtSummer);
            this.tabPage3.Controls.Add(this.label44);
            this.tabPage3.Controls.Add(this.txtKontrast);
            this.tabPage3.Controls.Add(this.label42);
            this.tabPage3.Controls.Add(this.cmbFlaps);
            this.tabPage3.Controls.Add(this.cmbCompany);
            this.tabPage3.Controls.Add(this.label32);
            this.tabPage3.Controls.Add(this.label31);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(556, 253);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Konfigurationen";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtPowerReduction
            // 
            this.txtPowerReduction.Enabled = false;
            this.txtPowerReduction.Location = new System.Drawing.Point(700, 158);
            this.txtPowerReduction.Name = "txtPowerReduction";
            this.txtPowerReduction.Size = new System.Drawing.Size(100, 20);
            this.txtPowerReduction.TabIndex = 177;
            this.txtPowerReduction.Leave += new System.EventHandler(this.txtPowerReduction_Leave);
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(700, 142);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(86, 13);
            this.label60.TabIndex = 176;
            this.label60.Text = "PowerReduction";
            // 
            // txtAnzahl_Backwaren
            // 
            this.txtAnzahl_Backwaren.Enabled = false;
            this.txtAnzahl_Backwaren.Location = new System.Drawing.Point(8, 88);
            this.txtAnzahl_Backwaren.Name = "txtAnzahl_Backwaren";
            this.txtAnzahl_Backwaren.Size = new System.Drawing.Size(100, 20);
            this.txtAnzahl_Backwaren.TabIndex = 175;
            this.txtAnzahl_Backwaren.Leave += new System.EventHandler(this.txtAnzahl_Backwaren_Leave);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(6, 70);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(99, 13);
            this.label59.TabIndex = 174;
            this.label59.Text = "Anzahl Backwaren:";
            // 
            // txtTemp_Korr
            // 
            this.txtTemp_Korr.Enabled = false;
            this.txtTemp_Korr.Location = new System.Drawing.Point(246, 88);
            this.txtTemp_Korr.Name = "txtTemp_Korr";
            this.txtTemp_Korr.Size = new System.Drawing.Size(100, 20);
            this.txtTemp_Korr.TabIndex = 173;
            this.txtTemp_Korr.Leave += new System.EventHandler(this.txtTemp_Korr_Leave);
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(246, 72);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(79, 13);
            this.label58.TabIndex = 172;
            this.label58.Text = "Temp-Korr (C°):";
            // 
            // txtEnergiesaving_temp
            // 
            this.txtEnergiesaving_temp.Enabled = false;
            this.txtEnergiesaving_temp.Location = new System.Drawing.Point(458, 200);
            this.txtEnergiesaving_temp.Name = "txtEnergiesaving_temp";
            this.txtEnergiesaving_temp.Size = new System.Drawing.Size(83, 20);
            this.txtEnergiesaving_temp.TabIndex = 171;
            this.txtEnergiesaving_temp.Leave += new System.EventHandler(this.txtEnergiesaving_temp_Leave);
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(455, 182);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(97, 13);
            this.label57.TabIndex = 170;
            this.label57.Text = "Energiesaving (°C):";
            // 
            // txtEnergieSaving_Min
            // 
            this.txtEnergieSaving_Min.Enabled = false;
            this.txtEnergieSaving_Min.Location = new System.Drawing.Point(453, 142);
            this.txtEnergieSaving_Min.Name = "txtEnergieSaving_Min";
            this.txtEnergieSaving_Min.Size = new System.Drawing.Size(88, 20);
            this.txtEnergieSaving_Min.TabIndex = 169;
            this.txtEnergieSaving_Min.Leave += new System.EventHandler(this.txtEnergieSaving_Min_Leave);
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(450, 126);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(102, 13);
            this.label56.TabIndex = 168;
            this.label56.Text = "Energiesaving (min):";
            // 
            // cmbZusatzheizung
            // 
            this.cmbZusatzheizung.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZusatzheizung.FormattingEnabled = true;
            this.cmbZusatzheizung.Location = new System.Drawing.Point(130, 198);
            this.cmbZusatzheizung.Name = "cmbZusatzheizung";
            this.cmbZusatzheizung.Size = new System.Drawing.Size(105, 21);
            this.cmbZusatzheizung.TabIndex = 167;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(127, 182);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(79, 13);
            this.label55.TabIndex = 166;
            this.label55.Text = "Zusatzheizung:";
            // 
            // cmbDoorActive
            // 
            this.cmbDoorActive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDoorActive.FormattingEnabled = true;
            this.cmbDoorActive.Location = new System.Drawing.Point(130, 142);
            this.cmbDoorActive.Name = "cmbDoorActive";
            this.cmbDoorActive.Size = new System.Drawing.Size(105, 21);
            this.cmbDoorActive.TabIndex = 165;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(127, 126);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(66, 13);
            this.label54.TabIndex = 164;
            this.label54.Text = "Door Active:";
            // 
            // cmbAutostart
            // 
            this.cmbAutostart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAutostart.FormattingEnabled = true;
            this.cmbAutostart.Location = new System.Drawing.Point(130, 86);
            this.cmbAutostart.Name = "cmbAutostart";
            this.cmbAutostart.Size = new System.Drawing.Size(105, 21);
            this.cmbAutostart.TabIndex = 163;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(127, 70);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(52, 13);
            this.label53.TabIndex = 162;
            this.label53.Text = "Autostart:";
            // 
            // cmbWasserSensor
            // 
            this.cmbWasserSensor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWasserSensor.FormattingEnabled = true;
            this.cmbWasserSensor.Location = new System.Drawing.Point(130, 30);
            this.cmbWasserSensor.Name = "cmbWasserSensor";
            this.cmbWasserSensor.Size = new System.Drawing.Size(105, 21);
            this.cmbWasserSensor.TabIndex = 161;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(127, 14);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(77, 13);
            this.label52.TabIndex = 160;
            this.label52.Text = "Wassersensor:";
            // 
            // cmbExt_Summer
            // 
            this.cmbExt_Summer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExt_Summer.FormattingEnabled = true;
            this.cmbExt_Summer.Location = new System.Drawing.Point(8, 198);
            this.cmbExt_Summer.Name = "cmbExt_Summer";
            this.cmbExt_Summer.Size = new System.Drawing.Size(103, 21);
            this.cmbExt_Summer.TabIndex = 159;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(5, 182);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(69, 13);
            this.label50.TabIndex = 158;
            this.label50.Text = "Ext. Summer:";
            // 
            // txtZeit_Klappe
            // 
            this.txtZeit_Klappe.Enabled = false;
            this.txtZeit_Klappe.Location = new System.Drawing.Point(248, 198);
            this.txtZeit_Klappe.Name = "txtZeit_Klappe";
            this.txtZeit_Klappe.Size = new System.Drawing.Size(100, 20);
            this.txtZeit_Klappe.TabIndex = 157;
            this.txtZeit_Klappe.Leave += new System.EventHandler(this.txtZeit_Klappe_Leave);
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(245, 182);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(64, 13);
            this.label51.TabIndex = 156;
            this.label51.Text = "Zeit Klappe:";
            // 
            // txtCoolDown_200
            // 
            this.txtCoolDown_200.Enabled = false;
            this.txtCoolDown_200.Location = new System.Drawing.Point(352, 200);
            this.txtCoolDown_200.Name = "txtCoolDown_200";
            this.txtCoolDown_200.Size = new System.Drawing.Size(100, 20);
            this.txtCoolDown_200.TabIndex = 155;
            this.txtCoolDown_200.Leave += new System.EventHandler(this.txtCoolDown_200_Leave);
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(352, 182);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(80, 13);
            this.label49.TabIndex = 154;
            this.label49.Text = "CoolDown 200:";
            // 
            // txtCoolDown_180
            // 
            this.txtCoolDown_180.Enabled = false;
            this.txtCoolDown_180.Location = new System.Drawing.Point(352, 142);
            this.txtCoolDown_180.Name = "txtCoolDown_180";
            this.txtCoolDown_180.Size = new System.Drawing.Size(100, 20);
            this.txtCoolDown_180.TabIndex = 153;
            this.txtCoolDown_180.Leave += new System.EventHandler(this.txtCoolDown_180_Leave);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(352, 125);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(80, 13);
            this.label48.TabIndex = 152;
            this.label48.Text = "CoolDown 180:";
            // 
            // txtBeschwandungs_Menge
            // 
            this.txtBeschwandungs_Menge.Enabled = false;
            this.txtBeschwandungs_Menge.Location = new System.Drawing.Point(352, 88);
            this.txtBeschwandungs_Menge.Name = "txtBeschwandungs_Menge";
            this.txtBeschwandungs_Menge.Size = new System.Drawing.Size(100, 20);
            this.txtBeschwandungs_Menge.TabIndex = 151;
            this.txtBeschwandungs_Menge.Leave += new System.EventHandler(this.txtBeschwandungs_Menge_Leave);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(350, 71);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(121, 13);
            this.label47.TabIndex = 150;
            this.label47.Text = "Beschwandungsmenge:";
            // 
            // txtBeschwandungs_Pause
            // 
            this.txtBeschwandungs_Pause.Enabled = false;
            this.txtBeschwandungs_Pause.Location = new System.Drawing.Point(352, 30);
            this.txtBeschwandungs_Pause.Name = "txtBeschwandungs_Pause";
            this.txtBeschwandungs_Pause.Size = new System.Drawing.Size(100, 20);
            this.txtBeschwandungs_Pause.TabIndex = 149;
            this.txtBeschwandungs_Pause.Leave += new System.EventHandler(this.txtBeschwandungs_Pause_Leave);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(350, 13);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(144, 13);
            this.label46.TabIndex = 148;
            this.label46.Text = "Beschwandungspause (sek):";
            // 
            // txtEnde_Beep
            // 
            this.txtEnde_Beep.Enabled = false;
            this.txtEnde_Beep.Location = new System.Drawing.Point(248, 142);
            this.txtEnde_Beep.Name = "txtEnde_Beep";
            this.txtEnde_Beep.Size = new System.Drawing.Size(100, 20);
            this.txtEnde_Beep.TabIndex = 147;
            this.txtEnde_Beep.Leave += new System.EventHandler(this.txtEnde_Beep_Leave);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(246, 125);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(103, 13);
            this.label45.TabIndex = 146;
            this.label45.Text = "Ende Beep (100ms):";
            // 
            // txtSummer
            // 
            this.txtSummer.Enabled = false;
            this.txtSummer.Location = new System.Drawing.Point(700, 88);
            this.txtSummer.Name = "txtSummer";
            this.txtSummer.Size = new System.Drawing.Size(100, 20);
            this.txtSummer.TabIndex = 145;
            this.txtSummer.Leave += new System.EventHandler(this.txtSummer_Leave);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(700, 71);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(48, 13);
            this.label44.TabIndex = 144;
            this.label44.Text = "Summer:";
            // 
            // txtKontrast
            // 
            this.txtKontrast.Enabled = false;
            this.txtKontrast.Location = new System.Drawing.Point(248, 30);
            this.txtKontrast.Name = "txtKontrast";
            this.txtKontrast.Size = new System.Drawing.Size(100, 20);
            this.txtKontrast.TabIndex = 143;
            this.txtKontrast.Leave += new System.EventHandler(this.txtKontrast_Leave);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(246, 13);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(49, 13);
            this.label42.TabIndex = 142;
            this.label42.Text = "Kontrast:";
            // 
            // cmbFlaps
            // 
            this.cmbFlaps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFlaps.FormattingEnabled = true;
            this.cmbFlaps.Location = new System.Drawing.Point(8, 142);
            this.cmbFlaps.Name = "cmbFlaps";
            this.cmbFlaps.Size = new System.Drawing.Size(103, 21);
            this.cmbFlaps.TabIndex = 139;
            // 
            // cmbCompany
            // 
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(6, 30);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(105, 21);
            this.cmbCompany.TabIndex = 138;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(5, 126);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(35, 13);
            this.label32.TabIndex = 135;
            this.label32.Text = "Flaps:";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(3, 14);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(54, 13);
            this.label31.TabIndex = 133;
            this.label31.Text = "Company:";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.txtRestzeit_Min5);
            this.tabPage4.Controls.Add(this.txtRestzeit_Min4);
            this.tabPage4.Controls.Add(this.txtRestzeit_Min3);
            this.tabPage4.Controls.Add(this.txtRestzeit_Min2);
            this.tabPage4.Controls.Add(this.txtRestzeit_Min1);
            this.tabPage4.Controls.Add(this.txtLetzte_Backware);
            this.tabPage4.Controls.Add(this.txtLetzter_State);
            this.tabPage4.Controls.Add(this.txtSetup_Code2);
            this.tabPage4.Controls.Add(this.txtSetup_Code1);
            this.tabPage4.Controls.Add(this.txtBetrieb_Warn_Cnt);
            this.tabPage4.Controls.Add(this.txtError_Wait);
            this.tabPage4.Controls.Add(this.txtError_Cnt);
            this.tabPage4.Controls.Add(this.txtBetrieb_Overflow);
            this.tabPage4.Controls.Add(this.txtBetrieb_Sek);
            this.tabPage4.Controls.Add(this.txtBetrieb_Min);
            this.tabPage4.Controls.Add(this.txtBetrieb_Std);
            this.tabPage4.Controls.Add(this.txtNumberOfBakingActs);
            this.tabPage4.Controls.Add(this.label29);
            this.tabPage4.Controls.Add(this.label30);
            this.tabPage4.Controls.Add(this.label27);
            this.tabPage4.Controls.Add(this.label26);
            this.tabPage4.Controls.Add(this.label25);
            this.tabPage4.Controls.Add(this.label24);
            this.tabPage4.Controls.Add(this.label22);
            this.tabPage4.Controls.Add(this.label21);
            this.tabPage4.Controls.Add(this.label20);
            this.tabPage4.Controls.Add(this.label19);
            this.tabPage4.Controls.Add(this.label18);
            this.tabPage4.Controls.Add(this.label17);
            this.tabPage4.Controls.Add(this.label16);
            this.tabPage4.Controls.Add(this.label15);
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.label12);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Controls.Add(this.lblFehlercodeszaehler);
            this.tabPage4.Controls.Add(this.lblFilterwarnung);
            this.tabPage4.Controls.Add(this.lblBetriebsstundenzähler);
            this.tabPage4.Controls.Add(this.lblNumberofBakingActs);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage4.Size = new System.Drawing.Size(556, 253);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Information";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // txtRestzeit_Min5
            // 
            this.txtRestzeit_Min5.Location = new System.Drawing.Point(379, 220);
            this.txtRestzeit_Min5.Name = "txtRestzeit_Min5";
            this.txtRestzeit_Min5.ReadOnly = true;
            this.txtRestzeit_Min5.Size = new System.Drawing.Size(59, 20);
            this.txtRestzeit_Min5.TabIndex = 170;
            this.txtRestzeit_Min5.Visible = false;
            // 
            // txtRestzeit_Min4
            // 
            this.txtRestzeit_Min4.Location = new System.Drawing.Point(379, 196);
            this.txtRestzeit_Min4.Name = "txtRestzeit_Min4";
            this.txtRestzeit_Min4.ReadOnly = true;
            this.txtRestzeit_Min4.Size = new System.Drawing.Size(59, 20);
            this.txtRestzeit_Min4.TabIndex = 169;
            // 
            // txtRestzeit_Min3
            // 
            this.txtRestzeit_Min3.Location = new System.Drawing.Point(379, 171);
            this.txtRestzeit_Min3.Name = "txtRestzeit_Min3";
            this.txtRestzeit_Min3.ReadOnly = true;
            this.txtRestzeit_Min3.Size = new System.Drawing.Size(59, 20);
            this.txtRestzeit_Min3.TabIndex = 168;
            // 
            // txtRestzeit_Min2
            // 
            this.txtRestzeit_Min2.Location = new System.Drawing.Point(379, 147);
            this.txtRestzeit_Min2.Name = "txtRestzeit_Min2";
            this.txtRestzeit_Min2.ReadOnly = true;
            this.txtRestzeit_Min2.Size = new System.Drawing.Size(59, 20);
            this.txtRestzeit_Min2.TabIndex = 167;
            // 
            // txtRestzeit_Min1
            // 
            this.txtRestzeit_Min1.Location = new System.Drawing.Point(379, 123);
            this.txtRestzeit_Min1.Name = "txtRestzeit_Min1";
            this.txtRestzeit_Min1.ReadOnly = true;
            this.txtRestzeit_Min1.Size = new System.Drawing.Size(59, 20);
            this.txtRestzeit_Min1.TabIndex = 166;
            // 
            // txtLetzte_Backware
            // 
            this.txtLetzte_Backware.Location = new System.Drawing.Point(423, 69);
            this.txtLetzte_Backware.Name = "txtLetzte_Backware";
            this.txtLetzte_Backware.ReadOnly = true;
            this.txtLetzte_Backware.Size = new System.Drawing.Size(59, 20);
            this.txtLetzte_Backware.TabIndex = 165;
            // 
            // txtLetzter_State
            // 
            this.txtLetzter_State.Location = new System.Drawing.Point(423, 45);
            this.txtLetzter_State.Name = "txtLetzter_State";
            this.txtLetzter_State.ReadOnly = true;
            this.txtLetzter_State.Size = new System.Drawing.Size(59, 20);
            this.txtLetzter_State.TabIndex = 164;
            // 
            // txtSetup_Code2
            // 
            this.txtSetup_Code2.Location = new System.Drawing.Point(226, 205);
            this.txtSetup_Code2.Name = "txtSetup_Code2";
            this.txtSetup_Code2.ReadOnly = true;
            this.txtSetup_Code2.Size = new System.Drawing.Size(59, 20);
            this.txtSetup_Code2.TabIndex = 163;
            // 
            // txtSetup_Code1
            // 
            this.txtSetup_Code1.Location = new System.Drawing.Point(227, 180);
            this.txtSetup_Code1.Name = "txtSetup_Code1";
            this.txtSetup_Code1.ReadOnly = true;
            this.txtSetup_Code1.Size = new System.Drawing.Size(59, 20);
            this.txtSetup_Code1.TabIndex = 162;
            // 
            // txtBetrieb_Warn_Cnt
            // 
            this.txtBetrieb_Warn_Cnt.Location = new System.Drawing.Point(194, 132);
            this.txtBetrieb_Warn_Cnt.Name = "txtBetrieb_Warn_Cnt";
            this.txtBetrieb_Warn_Cnt.ReadOnly = true;
            this.txtBetrieb_Warn_Cnt.Size = new System.Drawing.Size(100, 20);
            this.txtBetrieb_Warn_Cnt.TabIndex = 161;
            // 
            // txtError_Wait
            // 
            this.txtError_Wait.Location = new System.Drawing.Point(227, 65);
            this.txtError_Wait.Name = "txtError_Wait";
            this.txtError_Wait.ReadOnly = true;
            this.txtError_Wait.Size = new System.Drawing.Size(59, 20);
            this.txtError_Wait.TabIndex = 160;
            // 
            // txtError_Cnt
            // 
            this.txtError_Cnt.Location = new System.Drawing.Point(227, 41);
            this.txtError_Cnt.Name = "txtError_Cnt";
            this.txtError_Cnt.ReadOnly = true;
            this.txtError_Cnt.Size = new System.Drawing.Size(59, 20);
            this.txtError_Cnt.TabIndex = 159;
            // 
            // txtBetrieb_Overflow
            // 
            this.txtBetrieb_Overflow.Location = new System.Drawing.Point(9, 205);
            this.txtBetrieb_Overflow.Name = "txtBetrieb_Overflow";
            this.txtBetrieb_Overflow.ReadOnly = true;
            this.txtBetrieb_Overflow.Size = new System.Drawing.Size(58, 20);
            this.txtBetrieb_Overflow.TabIndex = 158;
            // 
            // txtBetrieb_Sek
            // 
            this.txtBetrieb_Sek.Location = new System.Drawing.Point(9, 147);
            this.txtBetrieb_Sek.Name = "txtBetrieb_Sek";
            this.txtBetrieb_Sek.ReadOnly = true;
            this.txtBetrieb_Sek.Size = new System.Drawing.Size(58, 20);
            this.txtBetrieb_Sek.TabIndex = 157;
            // 
            // txtBetrieb_Min
            // 
            this.txtBetrieb_Min.Location = new System.Drawing.Point(9, 123);
            this.txtBetrieb_Min.Name = "txtBetrieb_Min";
            this.txtBetrieb_Min.ReadOnly = true;
            this.txtBetrieb_Min.Size = new System.Drawing.Size(58, 20);
            this.txtBetrieb_Min.TabIndex = 156;
            // 
            // txtBetrieb_Std
            // 
            this.txtBetrieb_Std.Location = new System.Drawing.Point(9, 98);
            this.txtBetrieb_Std.Name = "txtBetrieb_Std";
            this.txtBetrieb_Std.ReadOnly = true;
            this.txtBetrieb_Std.Size = new System.Drawing.Size(58, 20);
            this.txtBetrieb_Std.TabIndex = 155;
            // 
            // txtNumberOfBakingActs
            // 
            this.txtNumberOfBakingActs.Location = new System.Drawing.Point(9, 38);
            this.txtNumberOfBakingActs.Name = "txtNumberOfBakingActs";
            this.txtNumberOfBakingActs.ReadOnly = true;
            this.txtNumberOfBakingActs.Size = new System.Drawing.Size(100, 20);
            this.txtNumberOfBakingActs.TabIndex = 154;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(206, 207);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(16, 13);
            this.label29.TabIndex = 153;
            this.label29.Text = "2:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(206, 183);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(16, 13);
            this.label30.TabIndex = 152;
            this.label30.Text = "1:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(190, 163);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(100, 13);
            this.label27.TabIndex = 151;
            this.label27.Text = "Einstellungsversion:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(358, 150);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(16, 13);
            this.label26.TabIndex = 150;
            this.label26.Text = "2:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(358, 174);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(16, 13);
            this.label25.TabIndex = 149;
            this.label25.Text = "3:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(358, 198);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(16, 13);
            this.label24.TabIndex = 148;
            this.label24.Text = "4:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(358, 223);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(16, 13);
            this.label22.TabIndex = 147;
            this.label22.Text = "5:";
            this.label22.Visible = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(358, 125);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(16, 13);
            this.label21.TabIndex = 146;
            this.label21.Text = "1:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(327, 98);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(88, 13);
            this.label20.TabIndex = 145;
            this.label20.Text = "Restezeiten(min):";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(330, 72);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(90, 13);
            this.label19.TabIndex = 144;
            this.label19.Text = "Letzte Backware:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(347, 47);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(70, 13);
            this.label18.TabIndex = 143;
            this.label18.Text = "Letzter State:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(340, 15);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(143, 13);
            this.label17.TabIndex = 142;
            this.label17.Text = "Zustandsspeicher Backofen:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(72, 207);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(49, 13);
            this.label16.TabIndex = 141;
            this.label16.Text = "Overflow";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(72, 149);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 13);
            this.label15.TabIndex = 140;
            this.label15.Text = "Sekunden";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(72, 125);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 13);
            this.label13.TabIndex = 139;
            this.label13.Text = "Minuten";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(70, 101);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 13);
            this.label12.TabIndex = 138;
            this.label12.Text = "Stunden";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(206, 67);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 13);
            this.label11.TabIndex = 137;
            this.label11.Text = "2:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(206, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 13);
            this.label10.TabIndex = 136;
            this.label10.Text = "1:";
            // 
            // lblFehlercodeszaehler
            // 
            this.lblFehlercodeszaehler.AutoSize = true;
            this.lblFehlercodeszaehler.Location = new System.Drawing.Point(188, 15);
            this.lblFehlercodeszaehler.Name = "lblFehlercodeszaehler";
            this.lblFehlercodeszaehler.Size = new System.Drawing.Size(96, 13);
            this.lblFehlercodeszaehler.TabIndex = 135;
            this.lblFehlercodeszaehler.Text = "Fehlercodeszähler:";
            // 
            // lblFilterwarnung
            // 
            this.lblFilterwarnung.AutoSize = true;
            this.lblFilterwarnung.Location = new System.Drawing.Point(190, 111);
            this.lblFilterwarnung.Name = "lblFilterwarnung";
            this.lblFilterwarnung.Size = new System.Drawing.Size(73, 13);
            this.lblFilterwarnung.TabIndex = 134;
            this.lblFilterwarnung.Text = "Filterwarnung:";
            // 
            // lblBetriebsstundenzähler
            // 
            this.lblBetriebsstundenzähler.AutoSize = true;
            this.lblBetriebsstundenzähler.Location = new System.Drawing.Point(6, 69);
            this.lblBetriebsstundenzähler.Name = "lblBetriebsstundenzähler";
            this.lblBetriebsstundenzähler.Size = new System.Drawing.Size(114, 13);
            this.lblBetriebsstundenzähler.TabIndex = 133;
            this.lblBetriebsstundenzähler.Text = "Betriebsstundenzähler:";
            // 
            // lblNumberofBakingActs
            // 
            this.lblNumberofBakingActs.AutoSize = true;
            this.lblNumberofBakingActs.Location = new System.Drawing.Point(6, 12);
            this.lblNumberofBakingActs.Name = "lblNumberofBakingActs";
            this.lblNumberofBakingActs.Size = new System.Drawing.Size(133, 13);
            this.lblNumberofBakingActs.TabIndex = 132;
            this.lblNumberofBakingActs.Text = "Anzahl der Backvorgänge:";
            // 
            // printBackprogramme
            // 
            this.printBackprogramme.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printBackprogramme_PrintPage);
            // 
            // printKonfiguration
            // 
            this.printKonfiguration.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printKonfiguration_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.Document = this.printBackprogramme;
            this.printDialog1.UseEXDialog = true;
            // 
            // printDialog2
            // 
            this.printDialog2.Document = this.printKonfiguration;
            this.printDialog2.UseEXDialog = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(743, 340);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bakemaster";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tbPVerbindungen.ResumeLayout(false);
            this.tbPVerbindungen.PerformLayout();
            this.tbPBackProgramm.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbPBackProgramm;
        private System.Windows.Forms.TreeView trVBackprogramme;
        private System.Windows.Forms.TabPage tbPVerbindungen;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDatei;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ComboBox cmBCOM_Ports;
        private System.Windows.Forms.ToolStripMenuItem übertragungToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backdatenAbrufenToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem backdatenSendenToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem öffnenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speichernUnterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem neuToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ComboBox cmBStopbit;
        private System.Windows.Forms.ComboBox cmBParity;
        private System.Windows.Forms.ComboBox cmBBaudrate;
        private System.Windows.Forms.ComboBox cmBDatabits;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox txtSoftHeat;
        private System.Windows.Forms.TextBox txtVentilatorStop;
        private System.Windows.Forms.TextBox txtKlappenstellung4;
        private System.Windows.Forms.TextBox txtKlappenstellung3;
        private System.Windows.Forms.TextBox txtKlappenstellung2;
        private System.Windows.Forms.TextBox txtKlappenstellung1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtVentilatorleistung3;
        private System.Windows.Forms.TextBox txtVentilatorleistung4;
        private System.Windows.Forms.TextBox txtVentilatorleistung2;
        private System.Windows.Forms.TextBox txtVentilatorleistung1;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtBeschwdauer3;
        private System.Windows.Forms.TextBox txtBeschwdauer4;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox txtBeschwdauer2;
        private System.Windows.Forms.TextBox txtBeschwdauer1;
        private System.Windows.Forms.TextBox txtBeschwmenge3;
        private System.Windows.Forms.TextBox txtBeschwmenge4;
        private System.Windows.Forms.TextBox txtBeschwmenge2;
        private System.Windows.Forms.TextBox txtBeschwmenge1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtBacktemp4;
        private System.Windows.Forms.TextBox txtBacktemp3;
        private System.Windows.Forms.TextBox txtBacktemp2;
        private System.Windows.Forms.TextBox txtBacktemp1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBackzeit3;
        private System.Windows.Forms.TextBox txtBackzeit4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBackzeit2;
        private System.Windows.Forms.TextBox txtBackzeit1;
        private System.Windows.Forms.TextBox txtGrundmenge;
        private System.Windows.Forms.TextBox txtAbkühltemp;
        private System.Windows.Forms.TextBox txtVorheiztemp;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem sprachdatenÖffnenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sprachdatenSpeichernUnterToolStripMenuItem;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox txb_lang;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label lblNumberofBakingActs;
        private System.Windows.Forms.Label lblBetriebsstundenzähler;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblFehlercodeszaehler;
        private System.Windows.Forms.Label lblFilterwarnung;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtBetrieb_Overflow;
        private System.Windows.Forms.TextBox txtBetrieb_Sek;
        private System.Windows.Forms.TextBox txtBetrieb_Min;
        private System.Windows.Forms.TextBox txtBetrieb_Std;
        private System.Windows.Forms.TextBox txtNumberOfBakingActs;
        private System.Windows.Forms.TextBox txtRestzeit_Min1;
        private System.Windows.Forms.TextBox txtLetzte_Backware;
        private System.Windows.Forms.TextBox txtLetzter_State;
        private System.Windows.Forms.TextBox txtSetup_Code2;
        private System.Windows.Forms.TextBox txtSetup_Code1;
        private System.Windows.Forms.TextBox txtBetrieb_Warn_Cnt;
        private System.Windows.Forms.TextBox txtError_Wait;
        private System.Windows.Forms.TextBox txtError_Cnt;
        private System.Windows.Forms.TextBox txtRestzeit_Min5;
        private System.Windows.Forms.TextBox txtRestzeit_Min4;
        private System.Windows.Forms.TextBox txtRestzeit_Min3;
        private System.Windows.Forms.TextBox txtRestzeit_Min2;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtSummer;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox txtKontrast;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.ComboBox cmbFlaps;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.TextBox txtZeit_Klappe;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox txtCoolDown_200;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox txtCoolDown_180;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox txtBeschwandungs_Menge;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox txtBeschwandungs_Pause;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox txtEnde_Beep;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.ComboBox cmbDoorActive;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.ComboBox cmbAutostart;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.ComboBox cmbWasserSensor;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.ComboBox cmbExt_Summer;
        private System.Windows.Forms.TextBox txtEnergiesaving_temp;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.TextBox txtEnergieSaving_Min;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.ComboBox cmbZusatzheizung;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.TextBox txtAnzahl_Backwaren;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.TextBox txtTemp_Korr;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.TextBox txtPowerReduction;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.ToolStripMenuItem configDatenOeffnen;
        private System.Windows.Forms.ToolStripMenuItem configDatenspeichern;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem druckenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backprogrammeDruckenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konfigurationenDruckenToolStripMenuItem;
        private System.Drawing.Printing.PrintDocument printBackprogramme;
        private System.Drawing.Printing.PrintDocument printKonfiguration;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.PrintDialog printDialog2;
    }
}

