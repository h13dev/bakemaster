namespace Tritronix.BakingOven
{
    partial class ProgressbarForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressbarForm));
            this.pgbMain = new System.Windows.Forms.ProgressBar();
            this.lblMain = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pgbMain
            // 
            this.pgbMain.Location = new System.Drawing.Point(16, 30);
            this.pgbMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pgbMain.Name = "pgbMain";
            this.pgbMain.Size = new System.Drawing.Size(345, 43);
            this.pgbMain.TabIndex = 0;
            // 
            // lblMain
            // 
            this.lblMain.AutoSize = true;
            this.lblMain.Location = new System.Drawing.Point(13, 9);
            this.lblMain.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(98, 17);
            this.lblMain.TabIndex = 1;
            this.lblMain.Text = "Bitte Warten...";
            // 
            // ProgressbarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 82);
            this.ControlBox = false;
            this.Controls.Add(this.lblMain);
            this.Controls.Add(this.pgbMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressbarForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Progressbar";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ProgressbarForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgbMain;
        private System.Windows.Forms.Label lblMain;
    }
}

