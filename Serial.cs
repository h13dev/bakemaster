using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Tritronix.BakingOven
{
    public partial class Serial : Form
    {
        crypt deencrypt = new crypt();

        RegistryKey Key = Registry.CurrentUser.OpenSubKey("Software\\Tritronix\\Bakemaster", true);

        public Serial()
        {
            InitializeComponent();
        }

        private void Serial_Load(object sender, EventArgs e)
        {
            foreach (byte b in deencrypt.encrypt(deencrypt.ionumber()))
            {
                txb_pnumber.Text += b;
            }

            txb_pnumber.Text = txb_pnumber.Text.Substring(0, 16);
        }

        private void btn_serial_Click(object sender, EventArgs e)
        {
            string serialtester1 = "";
            string serialtester2 = "";
            string serialtester3 = "";
            string serialtester4 = "";
            string serialtester5 = "";
            string serialtester6 = "";

            string security = "0";

            string aa = "00" + txb_pnumber.Text.Substring(0,14);
            string ab = "01" + txb_pnumber.Text.Substring(0,14);
            string ac = "10" + txb_pnumber.Text.Substring(0,14);
            string ad = "11" + txb_pnumber.Text.Substring(0,14);
            string ae = "20" + txb_pnumber.Text.Substring(0,14);
            string af = "21" + txb_pnumber.Text.Substring(0,14);

            foreach (byte b in deencrypt.encrypt(aa))
            {
                serialtester1 += b;
            }

            if (txb_snumber.Text.Substring(0, 16) == serialtester1.Substring(0, 16))
            {
                security = "00";
            }
            else
            {
                foreach (byte b in deencrypt.encrypt(ab))
                {
                    serialtester2 += b;
                }

                if (txb_snumber.Text.Substring(0, 16) == serialtester2.Substring(0, 16))
                {
                    security = "01";
                }
                else
                {
                    foreach (byte b in deencrypt.encrypt(ac))
                    {
                        serialtester3 += b;
                    }

                    if (txb_snumber.Text.Substring(0, 16) == serialtester3.Substring(0, 16))
                    {
                        security = "10";
                    }
                    else
                    {
                        foreach (byte b in deencrypt.encrypt(ad))
                        {
                            serialtester4 += b;
                        }

                        if (txb_snumber.Text.Substring(0, 16) == serialtester4.Substring(0, 16))
                        {
                            security = "11";
                        }
                        else
                        {
                            foreach (byte b in deencrypt.encrypt(ae))
                            {
                                serialtester5 += b;
                            }

                            if (txb_snumber.Text.Substring(0, 16) == serialtester5.Substring(0, 16))
                            {
                                security = "20";
                            }
                            else
                            {
                                foreach (byte b in deencrypt.encrypt(af))
                                {
                                    serialtester6 += b;
                                }

                                if (txb_snumber.Text.Substring(0, 16) == serialtester6.Substring(0, 16))
                                {
                                    security = "21";
                                }
                            }
                        }
                    }
                }
            }

            if (txb_snumber.Text.Length == 16)
            {
                if (security != "0" && txb_snumber.Text != Registry.GetValue("HKEY_CURRENT_USER\\Software\\Tritronix\\Bakemaster", "Serial", "").ToString())
                {
                    Registry.SetValue("HKEY_CURRENT_USER\\Software\\Tritronix\\Bakemaster", "Serial", security.Substring(1,1) + txb_snumber.Text);
                    //DateTime setdate = Convert.ToDateTime(Registry.GetValue("HKEY_CURRENT_USER\\Software\\Tritronix\\Bakemaster", "i1", "") + " 00:00");
                    
                    switch (Convert.ToInt32(security.Substring(0,1)))
                    {
                        case 0:
                            Registry.SetValue("HKEY_CURRENT_USER\\Software\\Tritronix\\Bakemaster", "i1", deencrypt.encrypt(DateTime.Now.AddDays(30).ToString().Substring(0, 10)));
                            break;

                        case 1:
                            Registry.SetValue("HKEY_CURRENT_USER\\Software\\Tritronix\\Bakemaster", "i1", deencrypt.encrypt(DateTime.Now.AddDays(60).ToString().Substring(0, 10)));
                            break;

                        case 2:
                            Registry.SetValue("HKEY_CURRENT_USER\\Software\\Tritronix\\Bakemaster", "i1", deencrypt.encrypt(DateTime.Now.AddDays(90).ToString().Substring(0, 10)));
                            break;
                    }

                    Registry.SetValue("HKEY_CURRENT_USER\\Software\\Tritronix\\Bakemaster", "i0", deencrypt.encrypt(DateTime.Now.ToString().Substring(0, 10)));

                    Form1 form = new Form1(security.Substring(1,1));
                    this.Visible = false;
                    form.ShowDialog();
                    Application.Exit();
                }
                else MessageBox.Show("Fehler! Falsche Seriennummer!");
            }
            else MessageBox.Show("Fehler! Seriennummer hat die falsche Länge!");
        }
    }
}
