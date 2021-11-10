namespace Tritronix.BakingOven
{
    partial class Serial
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Serial));
            this.txb_pnumber = new System.Windows.Forms.TextBox();
            this.txb_snumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_serial = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txb_pnumber
            // 
            this.txb_pnumber.Location = new System.Drawing.Point(12, 29);
            this.txb_pnumber.Name = "txb_pnumber";
            this.txb_pnumber.Size = new System.Drawing.Size(200, 22);
            this.txb_pnumber.TabIndex = 0;
            // 
            // txb_snumber
            // 
            this.txb_snumber.Location = new System.Drawing.Point(12, 74);
            this.txb_snumber.Name = "txb_snumber";
            this.txb_snumber.Size = new System.Drawing.Size(200, 22);
            this.txb_snumber.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Programmnummer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Seriennummer";
            // 
            // btn_serial
            // 
            this.btn_serial.Location = new System.Drawing.Point(137, 102);
            this.btn_serial.Name = "btn_serial";
            this.btn_serial.Size = new System.Drawing.Size(75, 23);
            this.btn_serial.TabIndex = 4;
            this.btn_serial.Text = "Weiter";
            this.btn_serial.UseVisualStyleBackColor = true;
            this.btn_serial.Click += new System.EventHandler(this.btn_serial_Click);
            // 
            // Serial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 137);
            this.Controls.Add(this.btn_serial);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txb_snumber);
            this.Controls.Add(this.txb_pnumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Serial";
            this.Text = "Serial";
            this.Load += new System.EventHandler(this.Serial_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txb_pnumber;
        private System.Windows.Forms.TextBox txb_snumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_serial;
    }
}