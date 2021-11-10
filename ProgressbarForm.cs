using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Tritronix.BakingOven
{
    public partial class ProgressbarForm : Form
    {

        /// <summary>
        /// Dient zum erstellen eines ProgressbarForms
        /// </summary>
        /// <param name="name">Angezeigter Name des Fensters</param>
        /// <param name="text">Name des Labels, z.B.: "Bitte Warten..."</param>
        /// <param name="progMaxValue">Maximaler Wert des Progressbars</param>
        /// <param name="progStepValue">Schrittgröße des Progressbars</param>
        public ProgressbarForm(string name, string text, int progMaxValue, int progStepValue)
        {
            InitializeComponent();
            this.Text = name;
            this.lblMain.Text = text;
            this.pgbMain.Maximum = progMaxValue;
            this.pgbMain.Step = progStepValue;
        }

        ///////////////////////////////
        //METHODES
        /// <summary>
        /// Erhöht den Wert der Progressbar um die Schrittweite
        /// ist der max. Wert erreicht wird das ProgressbarForm geschlossen
        /// </summary>
        public void ProgressbarPerformStep()
        {
            try
            {
                this.pgbMain.PerformStep();
                if (this.pgbMain.Value == this.pgbMain.Maximum) this.Dispose();
            }
            catch(InvalidOperationException)
            {
                //MaximumValue wurde überschritten...
                this.Dispose();
            }
        }

        /// <summary>
        /// Setzt bzw gibt den Wert des Progrogressbars zurück
        /// </summary>
        public int ProgressbarValueSetGet
        {
            set
            {
                try { this.pgbMain.Value = value; }
                catch (Exception ex) { throw ex; }
            }
            get { return this.pgbMain.Value; }
        }

        /// <summary>
        /// Verändert die Stepgröße
        /// </summary>
        public int ProgressBarStepSizeSetGet 
        {
            set { this.pgbMain.Step = value; }
            get { return this.pgbMain.Step; }
        }

        /// <summary>
        /// Setzt bzw gibt den Text des Labels zurück
        /// </summary>
        public string LabelText
        {
            set { this.lblMain.Text = value; }
            get { return this.lblMain.Text; }
        }

        /// <summary>
        /// Setzt bzw. gibt den Text der Form zurück
        /// </summary>
        public string FormText
        {
            set { this.Text = value; }
            get { return this.Text; }
        }

        private void ProgressbarForm_Load(object sender, EventArgs e)
        {
            if (this.Height != 115) this.Height = 115;
        }
    }
}