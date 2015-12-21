using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AesRsa
{
    class RSAEncryption
    {
        private RSACryptoServiceProvider RSAProvider;
        private RSAParameters RSAPublicParameters;
        private RSAParameters RSAPrivateParameters;
        private bool OAEP;

        public RSAEncryption(int bits)
        {
            RSAProvider = new RSACryptoServiceProvider(bits);
            RSAPublicParameters = RSAProvider.ExportParameters(false);
            RSAPrivateParameters = RSAProvider.ExportParameters(true);
            OAEP = false;
        }

        public byte[] RSAEncrypt(byte[] DataToEncrypt)
        {
            byte[] encryptedData;
            try
            {

                RSAProvider = new RSACryptoServiceProvider();

                RSAProvider.ImportParameters(RSAPublicParameters);

                encryptedData = RSAProvider.Encrypt(DataToEncrypt, OAEP);
            }
            catch (CryptographicException e)
            {
                throw new Exception(e.Message);
            }

            return encryptedData;
        }

        public byte[] RSADecrypt(byte[] DataToDecrypt)
        {
            byte[] decryptedData;
            try
            {

                RSAProvider = new RSACryptoServiceProvider();

                RSAProvider.ImportParameters(RSAPrivateParameters);

                decryptedData = RSAProvider.Decrypt(DataToDecrypt, OAEP);

            }
            catch (CryptographicException e)
            {
                throw new Exception(e.Message);
            }
            return decryptedData;
        }

        public string getPublicKey()
        {
            return RSAProvider.ToXmlString(false);
        }

        public void setPublicKey(string PublicKey)
        {
            RSAProvider.FromXmlString(PublicKey);
            RSAPublicParameters = RSAProvider.ExportParameters(false);
        }

        public string getPrivateKey()
        {
            return RSAProvider.ToXmlString(true);
        }

        public void setPrivateKey(string PrivateKey)
        {
            RSAProvider.FromXmlString(PrivateKey);
            RSAPrivateParameters = RSAProvider.ExportParameters(true);
        }

        public bool getOAEP()
        {
            return OAEP;
        }
        public void setOAEP(bool OAEP)
        {
            this.OAEP = OAEP;
        }

    }
}
