using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Management;
using System.IO;
using System.Security;
using System.Security.Cryptography;

namespace Tritronix.BakingOven
{
    public class crypt
    {
        public byte[] encrypt(string tocrypt)
        {
            //byte[] key = new byte[] { 24, 12, 16, 10, 1, 2, 3, 6, 5, 8, 4, 7, 9, 13, 11, 15, 14, 20, 18, 19, 17, 23, 21, 22 };
            byte[] key = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24};
            byte[] iv = new byte[] { 65, 110, 68, 26, 69, 178, 200, 219 };
            byte[] ret = new byte[] { };
            //DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
            DESCryptoServiceProvider dec = new DESCryptoServiceProvider();

            // MemoryStream Objekt erzeugen
            MemoryStream memoryStream = new MemoryStream();

            // CryptoStream Objekt erzeugen und den Initialisierungs-Vektor
            // sowie den Schlüssel übergeben.
            CryptoStream cryptoStream = new CryptoStream(
            memoryStream, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write);

            // Eingabestring in ein Byte-Array konvertieren
            byte[] toEncrypt = new ASCIIEncoding().GetBytes(tocrypt);

            // Byte-Array in den Stream schreiben und flushen.
            cryptoStream.Write(toEncrypt, 0, toEncrypt.Length);
            cryptoStream.FlushFinalBlock();

            // Ein Byte-Array aus dem Memory-Stream auslesen
            ret = memoryStream.ToArray();
            // Stream schließen.
            cryptoStream.Close();
            memoryStream.Close();

            return ret;
        }

        public string decrypt(byte[] data)
        {
            string ret = "";
            byte[] key = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
            //byte[] iv = new byte[] { 65, 110, 68, 26, 69, 178, 200, 219 };
            byte[] iv = new byte[] { 65, 110, 68, 26, 69, 178, 200, 219 };
            // Ein MemoryStream Objekt erzeugen und das Byte-Array
            // mit den verschlüsselten Daten zuweisen.
            MemoryStream memoryStream = new MemoryStream(data);

            // Ein CryptoStream Objekt erzeugen und den MemoryStream hinzufügen.
            // Den Schlüssel und Initialisierungsvektor zum entschlüsseln verwenden.
            CryptoStream cryptoStream = new CryptoStream(
            memoryStream,
            new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read);
            // Buffer erstellen um die entschlüsselten Daten zuzuweisen.
            byte[] fromEncrypt = new byte[data.Length];

            // Read the decrypted data out of the crypto stream
            // and place it into the temporary buffer.
            // Die entschlüsselten Daten aus dem CryptoStream lesen
            // und im temporären Puffer ablegen.
            cryptoStream.Read(fromEncrypt, 0, fromEncrypt.Length);

            ret = new ASCIIEncoding().GetString(fromEncrypt).ToString();

            return ret;
        }

        
        public string ionumber()
        {
            //Erzeugt das Datum für Serialdate ddmmyy
            string serialdate = DateTime.Today.Day.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            
            ManagementObjectSearcher system = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk");
            ManagementObjectCollection queryCollection1 = system.Get();

            foreach (ManagementObject mo in queryCollection1)
            {
                try
                {
                    //saver += mo["SerialNumber"].ToString();

                    serialdate += mo.GetPropertyValue("VolumeSerialNumber").ToString();
                }
                catch
                {
                    serialdate += mo.GetPropertyValue("DeviceID").ToString();
                }
            }

            return serialdate.Substring(0,16);
        }

        public bool datechecker()
        {
            DateTime date0;
            DateTime date1;

            //Überprüfung ob Date0 kleiner als Date1 ist
            RegistryKey Key = Registry.CurrentUser.OpenSubKey("Software\\Tritronix\\Bakemaster", true);
            
            //Console.WriteLine(decrypt((Byte[])(Registry.GetValue("HKEY_CURRENT_USER\\Software\\Tritronix\\Bakemaster", "Date0", ""))));
            //Datum entschlüsseln
            try
            {
                date0 = Convert.ToDateTime(decrypt((Byte[])(Registry.GetValue("HKEY_CURRENT_USER\\Software\\Tritronix\\Bakemaster", "i0", ""))).Substring(0, 10) + " 00:00");
                date1 = Convert.ToDateTime(decrypt((Byte[])(Registry.GetValue("HKEY_CURRENT_USER\\Software\\Tritronix\\Bakemaster", "i1", ""))).Substring(0, 10) + " 00:00");
 
                if (date0 < date1 && DateTime.Today >= date0)
                {
                    Registry.SetValue("HKEY_CURRENT_USER\\Software\\Tritronix\\Bakemaster", "i0", encrypt(DateTime.Today.ToString().Substring(0, 10)));
                    date0 = Convert.ToDateTime(decrypt((Byte[])(Registry.GetValue("HKEY_CURRENT_USER\\Software\\Tritronix\\Bakemaster", "i0", ""))).Substring(0, 10) + " 00:00");
                }

                //Wenn Datum1 kleiner als Datum2 ist und Das jetztige Datum größer als Datum1 ist
                if (date0 < date1 && DateTime.Today >= date0)
                {
                    return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
