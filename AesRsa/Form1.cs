using AesRsa.Kripto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AesRsa
{
    public partial class Form1 : Form
    {

        AESEncryption aes = new AESEncryption();
        RSAEncryption rsa = new RSAEncryption(1024);
        Char zadnjoGeneriranoE = ' ';
        string saveFileOption;
        string FileUP;
        string FileUPD;


        public Form1()
        {
            InitializeComponent();

            button4.Enabled = false;
            btnLoadKey.Enabled = false;
            EncryptBut.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            btnPublicVector.Enabled = false;
           

        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
               string  nalozenaDatoteka = openFileDialog1.FileName;
              
                // label1.Text = "Naložena datoteka: " + nalozenaDatoteka.Split('\\').Last();
                //btnSifriraj.Enabled = true;
                //btnDesifriraj.Enabled = true;
            }
        }

        private void EncryptionTab_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

       
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text.Contains("AES"))
            {
                button4.Text = "Save key";
                btnPublicVector.Text = "Save vector";

               
               
                
            }
            else if (comboBox3.Text.Contains("RSA"))
            {
                button4.Text = "Save private key";
                btnPublicVector.Text = "Save public key";

               
            }
            else
            {
                button4.Enabled = false;
                btnPublicVector.Enabled = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Rijndael r = Rijndael.Create();

            button4.Enabled = true;
            btnPublicVector.Enabled = true;


            switch (comboBox3.Text)
            {
                  

                case "AES 128":

                    aes.RijndaelKey = Utilities.GenerateByteArray(16);
                    aes.RijndaelVector = Utilities.GenerateByteArray(16);

                    textBox3.Text = "Key: " + Environment.NewLine + Environment.NewLine + BitConverter.ToString(aes.RijndaelKey);
                    textBox3.Text += System.Environment.NewLine + "Vektor: " + Environment.NewLine + Environment.NewLine + BitConverter.ToString(aes.RijndaelVector);

                    zadnjoGeneriranoE = 'a';

                    break;
                case "AES 192":

                    aes.RijndaelKey = Utilities.GenerateByteArray(24);
                    aes.RijndaelVector = Utilities.GenerateByteArray(16);

                    textBox3.Text = "Key: " + Environment.NewLine + Environment.NewLine + BitConverter.ToString(aes.RijndaelKey);
                    textBox3.Text += System.Environment.NewLine + "Vektor: " + Environment.NewLine + Environment.NewLine + BitConverter.ToString(aes.RijndaelVector);

                    zadnjoGeneriranoE = 'a';

                    break;
                case "AES 256":

                    aes.RijndaelKey = Utilities.GenerateByteArray(32);
                    aes.RijndaelVector = Utilities.GenerateByteArray(16);

                    textBox3.Text = "Key: " + Environment.NewLine + Environment.NewLine + BitConverter.ToString(aes.RijndaelKey);
                    textBox3.Text += System.Environment.NewLine + "Vektor: " + Environment.NewLine + Environment.NewLine + BitConverter.ToString(aes.RijndaelVector);

                    zadnjoGeneriranoE = 'a';

                    break;
                case "RSA 1024":

                    rsa = new RSAEncryption(1024);

                    textBox3.Text = "Private Key " + Environment.NewLine + Environment.NewLine + Utilities.PrintXML(rsa.getPrivateKey());
                    textBox3.Text += System.Environment.NewLine + "Public Key " + Environment.NewLine + Environment.NewLine + Utilities.PrintXML(rsa.getPublicKey());

                    zadnjoGeneriranoE = 'r';

                    break;
                case "RSA 2048":

                    rsa = new RSAEncryption(2048);

                    textBox3.Text = "Private Key " + Environment.NewLine + Environment.NewLine + Utilities.PrintXML(rsa.getPrivateKey());
                    textBox3.Text += System.Environment.NewLine + "Public Key " + Environment.NewLine + Environment.NewLine + Utilities.PrintXML(rsa.getPublicKey());

                    zadnjoGeneriranoE = 'r';

                    break;

                default:
                    button4.Enabled = false;

                    MessageBox.Show("Erorr!", "");
                    return;
            }
        }

        private void EncryptBut_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            saveFileOption = "key1";
            if (comboBox3.Text.Contains("AES"))
            {
                saveFileDialog1.FileName = "AESKey.key";
            }
            else
            {
                saveFileDialog1.FileName = "RSAPrivateKey.key";
            }

            saveFileDialog1.Filter = "Key files|*.key|All files|*.*";
            saveFileDialog1.ShowDialog();

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
            string filename = saveFileDialog1.FileName;
            
            switch (saveFileOption)
            {
            case "key1":
                    if (zadnjoGeneriranoE.Equals('a'))
                    {
                        Utilities.SaveByteArrayToFile(filename, aes.RijndaelKey);
                        //File.WriteAllText(filename, Utilities.ByteArrayToStr(aes.RijndaelKey));
                    }
                    else if (zadnjoGeneriranoE.Equals('r'))
                    {
                        File.WriteAllText(filename, rsa.getPrivateKey());
                    }
                    break;

                case "key2":
                    if (zadnjoGeneriranoE.Equals('a'))
                    {
                        Utilities.SaveByteArrayToFile(filename, aes.RijndaelVector);
                        //File.WriteAllText(filename, Utilities.ByteArrayToStr(aes.RijndaelVector));
                    }
                    else if (zadnjoGeneriranoE.Equals('r'))
                    {
                        File.WriteAllText(filename, rsa.getPublicKey());
                    }
                    break;
               
                case "sif":
                    try
                    {
                        string data = File.ReadAllText(FileUP);
                        
                        if (comboBox1.Text.Contains("AES"))
                        {
                            aes.AESEncrypt(data, filename);
                        }
                        else if (comboBox1.Text.Contains("RSA"))
                        {
                            File.WriteAllText(filename, Convert.ToBase64String(rsa.RSAEncrypt(Utilities.StrToByteArray(data))));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "no");
                    }
                    break;

                  case "desif":
                       try
                       {
                           if (comboBox2.Text.Contains("AES"))
                           {
                               File.WriteAllText(filename, aes.AESDecrypt(FileUPD));
                           }
                           else if (comboBox2.Text.Contains("RSA"))
                           {
                               string datatmp = File.ReadAllText(FileUPD);
                               File.WriteAllText(filename, Utilities.ByteArrayToStr(rsa.RSADecrypt(Convert.FromBase64String(datatmp))));
                           }
                       }
                       catch (Exception ex)
                       {
                           MessageBox.Show("Error: " + ex.Message, "no");
                       }
                       break;
                  
            }
            
        }

        private void EncryptBut_Click_1(object sender, EventArgs e)
        {

            saveFileOption = "sif";
            saveFileDialog1.FileName = "Encrypted-" + FileUP.Split('\\').Last(); ;
            saveFileDialog1.Filter = "All files|*.*";
            saveFileDialog1.ShowDialog();


        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "";
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                FileUP = openFileDialog1.FileName;

                 label3.Text = "File: " + FileUP.Split('\\').Last();

                 EncryptBut.Enabled = true;
                
            }



        }

        private void btnLoadKey_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Key files|*.key|All files|*.*";

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;

                if (comboBox1.Text.Contains("AES"))
                {
                    aes.RijndaelKey = Utilities.ReadByteArrayFromFile(file);
                    textBox1.Text = BitConverter.ToString(aes.RijndaelKey);

                }

                else if (comboBox1.Text.Contains("RSA"))
                {
                    string key = File.ReadAllText(file);
                    rsa.setPublicKey(key);
                    string xxx = rsa.getPublicKey();
                    textBox1.Text =  Environment.NewLine + Environment.NewLine + Utilities.PrintXML(rsa.getPublicKey());
                }
            }

            if (comboBox1.Text.Contains("AES"))
            {

                openFileDialog1.Filter = "Key files|*.key|All files|*.*";

                DialogResult result2 = openFileDialog1.ShowDialog();
                if (result2 == DialogResult.OK)
                {
                    string file = openFileDialog1.FileName;

                    if (comboBox1.Text.Contains("AES"))
                    {
                        aes.RijndaelVector = Utilities.ReadByteArrayFromFile(file);
                        textBox1.Text = Environment.NewLine + Environment.NewLine + BitConverter.ToString(aes.RijndaelVector);
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            openFileDialog1.Filter = "";
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                FileUPD = openFileDialog1.FileName;

                label4.Text = "File: " + FileUPD.Split('\\').Last();
              

            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Key files|*.key|All files|*.*";

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;

                if (comboBox2.Text.Contains("AES"))
                {
                    aes.RijndaelKey = Utilities.ReadByteArrayFromFile(file);
                    textBox2.Text = BitConverter.ToString(aes.RijndaelKey);

                }
                else if (comboBox2.Text.Contains("RSA"))
                {
                    string key = File.ReadAllText(file);
                    rsa.setPrivateKey(key);
                    string xxx = rsa.getPrivateKey();
                    textBox2.Text = Environment.NewLine + Environment.NewLine + Utilities.PrintXML(rsa.getPrivateKey());
                }
            }

            if (comboBox2.Text.Contains("AES"))
            {

                openFileDialog1.Filter = "Key files|*.key|All files|*.*";

                DialogResult result2 = openFileDialog1.ShowDialog();
                if (result2 == DialogResult.OK)
                {
                    string file = openFileDialog1.FileName;

                    if (comboBox2.Text.Contains("AES"))
                    {
                        aes.RijndaelVector = Utilities.ReadByteArrayFromFile(file);
                        textBox2.Text += Environment.NewLine + Environment.NewLine + BitConverter.ToString(aes.RijndaelVector);
                    }
                }
            }

            button1.Enabled = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            saveFileOption = "desif";
            saveFileDialog1.FileName = "Decrypted_" + FileUPD.Split('\\').Last(); ;
            saveFileDialog1.Filter = "All files|*.*";
            saveFileDialog1.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Contains('A'))
            {
                btnLoadKey.Enabled = true;
            }
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        
        {
            
            if (comboBox2.Text.Contains('A'))
            {
                button2.Enabled = true;
            }
        }

        private void btnPublicVector_Click(object sender, EventArgs e)
        {
            saveFileOption = "key2";
            if (comboBox3.Text.Contains("AES"))
            {
                saveFileDialog1.FileName = "AESVector.key";
            }
            else
            {
                saveFileDialog1.FileName = "RSAPublicKey.key";
            }

            saveFileDialog1.Filter = "Key files|*.key|All files|*.*";
            saveFileDialog1.ShowDialog();
        }

    

     
       

        }

       
       
    }

