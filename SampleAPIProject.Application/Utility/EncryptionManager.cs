using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace RoadMech.Mechanic.API.Models.Utility
{
    /// <summary>
    /// Class used to manage Encryption and Decryption od password
    /// </summary>
    public class EncryptionManager
    {
        #region "Local Variables"

        // This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private const string initVector = "le69hrko450t90i3";
        private const string strPass = "RoadMech";

        // This constant is used to determine the keysize of the encryption algorithm.
        private const int keysize = 256;

        #endregion "Local Variables"

        #region "User Defined Methods"
        /// <summary>
        /// Method used to Encrypt Password.
        /// </summary>
        /// <param name="plainText">Get password to encrypt</param>
        /// <returns>Encrypted password</returns>
        public static string Encrypt(string plainText, bool isUrlEncode = false)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(strPass, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string encryptedText = Convert.ToBase64String(cipherTextBytes);
            return isUrlEncode ? HttpUtility.UrlEncode(encryptedText) : encryptedText;
        }

        /// <summary>
        /// Method is used to Decrypt the Encrypted Password ...
        /// </summary>
        /// <param name="cyphertext">Get encrypted password</param>
        /// <returns>Decrypted password</returns>
        public static string Decrypt(string cyphertext)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            string converted = Convert.ToBase64String(initVectorBytes);
            byte[] cipherTextBytes = Convert.FromBase64String(cyphertext);
            PasswordDeriveBytes password = new PasswordDeriveBytes(strPass, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
        #endregion "User Defined Methods"
    }
}
