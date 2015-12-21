using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AesRsa
{
    class AESEncryption
    {
        private Rijndael RijndaelAlg;

        private byte[] Key;
        private byte[] Vector;


        public AESEncryption()
        {
            RijndaelAlg = Rijndael.Create();

            Key = RijndaelAlg.Key;
            Vector = RijndaelAlg.IV;
        }

        public void AESEncrypt(string data, string destinationFileName)
        {
            try
            {
                FileStream fStream = new FileStream(destinationFileName, FileMode.OpenOrCreate);
                CryptoStream cryptStream = new CryptoStream(fStream, RijndaelAlg.CreateEncryptor(Key, Vector), CryptoStreamMode.Write);
                StreamWriter writer = new StreamWriter(cryptStream);

                try
                {
                    writer.Write(data);
                }
                catch (Exception streamException)
                {
                    throw new Exception("Error saving the data to file. Error: " + streamException.Message);
                }
                finally
                {
                    writer.Close();
                    cryptStream.Close();
                    fStream.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem saving the data to file. Error: " + ex.Message);
            }
        }



        public string AESDecrypt(string sourceFileName)
        {
            string result = "";

            if (File.Exists(sourceFileName))
            {

                FileStream fStream = new FileStream(sourceFileName, FileMode.Open);
                CryptoStream decryptStream = new CryptoStream(fStream, RijndaelAlg.CreateDecryptor(Key, Vector), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(decryptStream);

                try
                {
                    result = reader.ReadToEnd();
                }
                catch (Exception decryptEx)
                {
                    throw new Exception("Error decrypting source file. Error: " + decryptEx.Message);
                }
                finally
                {
                    reader.Close();
                    decryptStream.Close();
                    fStream.Close();
                }
            }
            else
            {
                throw new Exception("The source file could not be located. File name: " + sourceFileName);
            }

            return result;
        }

        public byte[] RijndaelKey
        {
            get { return Key; }
            set { Key = value; }
        }


        public byte[] RijndaelVector
        {
            get { return Vector; }
            set { Vector = value; }
        }

    }

}
