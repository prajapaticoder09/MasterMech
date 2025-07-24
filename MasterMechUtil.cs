using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace MasterMech
{
	public class MasterMechUtil
	{

        public enum OPMode
        {
            New,
            Open,
            Delete
        }

        // Database Related propeties
		public static string sServer = "";
		public static string sDB = "";
		public static string sDBUID = "";
		public static string sPWD = "";

        // Financial Year of the logged in
        private static string sFinYear;
        public static string sFY
        {
            set
            {
                if (Regex.IsMatch(value, @"^(\d{4})-\d{2}$", RegexOptions.IgnoreCase))                   
                    sFinYear = value;
                else
                    throw new ArgumentException(String.Format("{0} is not a valid value for", value)," sFY");
            }
            get
            {
                return sFinYear;
            }

        }
        
        // Application Realted Properies
        public static string sUserID="";

        // Company Related informations
        public static string sCoName;
        public static string sStAddr;
        public static string sArAddr;
        public static string sCity;
        public static string sState;
        public static string sPINCode;
        public static string sCountry;
        public static string sGSTNo;




		public static bool ShowError(string isMsg)
		{
#if DEBUG
			System.Diagnostics.Debug.WriteLine(isMsg);
#else
			MessageBox.Show(isMsg);
#endif

			return true;
		}

        public static string ConnStr
        {
            get
            { 
               // return "User id=" + sDBUID + ";Password=" + sPWD + ";Initial Catalog=" + sDB + ";Data Source=" + sServer;

                return "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MasterMech;Data Source=DESKTOP-K69ERBE\\MSSQLSERVER01";
            }
        }

       
        public static bool DBConnected()
        {
            try
            {
                using (var lObjConn = new SqlConnection(ConnStr))
                {
                    var lsQry = "SELECT 1";

                    var lObjCommand = new SqlCommand(lsQry, lObjConn);
                    lObjConn.Open();
                    lObjCommand.ExecuteScalar();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static string Encrypt(string encryptString)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static string[] FYList()
        {
            string[] lsFYList = new string[10];
            int lnCount = 0;
            int lnCurrYear = DateTime.Now.Year;
            int lnYear = lnCurrYear - 5;


            for (lnCount = 0; lnCount < 10; lnCount++)
                lsFYList[lnCount] = lnYear.ToString() + "-" + (lnYear++ + 1).ToString().Substring(2);

            return lsFYList;
        }

        public static string CurrFY()
        {
            if (DateTime.Now.Month >= 4)
                return (DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Year + 1).ToString().Substring(2));
            else
                return ((DateTime.Now.Year-1).ToString() + "-" + DateTime.Now.Year.ToString().Substring(2));

        }

        public static bool ValidNumber(string isNumber)
        {
            if (isNumber.Length == 0)
                return false;
            for (int lnCnt = 0; lnCnt < isNumber.Length; lnCnt++)
            {
                if (((byte)isNumber[lnCnt]) < 48 || ((byte)isNumber[lnCnt]) > 57)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
