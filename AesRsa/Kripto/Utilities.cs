using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AesRsa.Kripto
{
    class Utilities
    {
        private static Random r;

        public static byte[] GenerateByteArray(int size)
        {
            if (r == null) r = new Random();
            byte[] array = new byte[size];

            r.NextBytes(array);

            return array;
        }

        public static byte[] StrToByteArray(string str)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            return encoding.GetBytes(str);
        }

        public static string ByteArrayToStr(byte[] byteArray)
        {
            UnicodeEncoding enc = new UnicodeEncoding();
            return enc.GetString(byteArray);
        }

        public static void SaveByteArrayToFile(string fileName, byte[] byteArray)
        {
            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);

                fileStream.Write(byteArray, 0, byteArray.Length);

                return;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        public static byte[] ReadByteArrayFromFile(string fileName)
        {
            FileStream fileStream = File.OpenRead(fileName);
            try
            {
                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, Convert.ToInt32(fileStream.Length));
                fileStream.Close();
                return bytes;
            }
            finally
            {
                fileStream.Close();
            }

        }

        public static String PrintXML(String XML)
        {
            String Result = "";

            MemoryStream mStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.Unicode);
            XmlDocument document = new XmlDocument();

            try
            {
                // Load the XmlDocument with the XML.
                document.LoadXml(XML);

                writer.Formatting = Formatting.Indented;

                // Write the XML into a formatting XmlTextWriter
                document.WriteContentTo(writer);
                writer.Flush();
                mStream.Flush();

                // Have to rewind the MemoryStream in order to read
                // its contents.
                mStream.Position = 0;

                // Read MemoryStream contents into a StreamReader.
                StreamReader sReader = new StreamReader(mStream);

                // Extract the text from the StreamReader.
                String FormattedXML = sReader.ReadToEnd();

                Result = FormattedXML;
            }
            catch (XmlException) { }

            mStream.Close();
            writer.Close();

            return Result;
        }
    }
}
