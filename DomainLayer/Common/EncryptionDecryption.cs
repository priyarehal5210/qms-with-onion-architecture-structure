using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Common
{
    public static class EncryptionDecryption
    {
        public static string encrypt(string password)
        {
            byte[] storePassword=ASCIIEncoding.ASCII.GetBytes(password);
            string encryptPassword=Convert.ToBase64String(storePassword);
            return encryptPassword;
        }
        public static string decrypt(string password)
        {
            byte[] encryptedPassword=Convert.FromBase64String(password);
            string decryptedPassword=ASCIIEncoding.ASCII.GetString(encryptedPassword);
            return decryptedPassword;
        }
    }
}
