using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Management;
using System.IO;
using System.Text;

namespace Tritronix.BakingOven
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            crypt deencrypt = new crypt();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            #region SERIAL REGISTRY

            RegistryKey Key = Registry.CurrentUser.OpenSubKey("Software\\Tritronix\\Bakemaster", true);

            if (Key == null)
            {
                Registry.CurrentUser.CreateSubKey("Software\\Tritronix\\Bakemaster");
                //Registry.SetValue("HKEY_CURRENT_USER\\Software\\Tritronix\\Bakemaster", "Serial", "xxxxxxxxxxxxxxxx");
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Tritronix\\Bakemaster", "ComPort", "");
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Tritronix\\Bakemaster", "i0", deencrypt.encrypt(DateTime.Now.ToString().Substring(0,10)));
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Tritronix\\Bakemaster", "i1", "");
            }

            string serialtester = "";
            string serialtester2 = "";

            foreach (byte b in deencrypt.encrypt(deencrypt.ionumber()))
            {
                serialtester += b;
            }

            foreach (byte b in deencrypt.encrypt(serialtester))
            {
                serialtester2 += b;
            }

            //MessageBox.Show(deencrypt.ionumber().Substring(0,16));
            //MessageBox.Show(deencrypt.ionumber().Substring(0, 16));
            //MessageBox.Show(serialtester2.Substring(1, 14));
            //if (Registry.GetValue("HKEY_CURRENT_USER\\Software\\Tritronix\\Bakemaster", "Serial", "").ToString().Length != 16)
            //{
            //    Application.Run(new Serial());
            //}

            if (deencrypt.datechecker() == true) 
            {
                string start = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Tritronix\\Bakemaster", "Serial", "").ToString().Substring(0, 1);

                if (start == "1") Application.Run(new Form1("1"));
                else Application.Run(new Form1("0"));               
            }
            else if (deencrypt.datechecker() == false) Application.Run(new Serial());

            #endregion
        }
    }
}