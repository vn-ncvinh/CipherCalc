﻿using System;
using System.Windows.Forms;

namespace SubstitutionCipher
{
    public partial class Calc : Form
    {
        private char[] plainText;
        private char[] encryptedText;
        private string decrypedText = String.Empty;
        private bool mode = false;

        public Calc()
        {
            InitializeComponent();
            label7.Text = plainTextBox.Text.Length.ToString() + " Characters";
            label8.Text = encryptedTextBox.Text.Length.ToString() + " Characters";
            strToArray();
            calculation();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            encryptedTextBox.Text = Shuffle(plainTextBox.Text);
            label7.Text = plainTextBox.Text.Length.ToString() + " Characters";
            strToArray();
            calculation();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label8.Text = encryptedTextBox.Text.Length.ToString() + " Characters";
            strToArray();
            calculation();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            calculation();
        }

        private void modeButton_Click(object sender, EventArgs e)
        {
            if (!mode)
            {
                label5.Text = "Text";
                label6.Text = "Encrypted";
                modeButton.Text = "Encryption Mode";
                label9.Text = "Encryption Mode";
                mode = true;
            }
            else
            {
                label5.Text = "Cipher";
                label6.Text = "Decrypted";
                modeButton.Text = "Decryption Mode";
                label9.Text = "Decryption Mode";
                mode = false;
            }
            char[] arrayTemp = plainText;
            string strTemp = cipherRichTextBox.Text;
            plainText = encryptedText;
            encryptedText = arrayTemp;
            cipherRichTextBox.Text = decryptedRichTextBox.Text;
            decryptedRichTextBox.Text = strTemp;
            cryptChar(cipherRichTextBox.Text);
            decryptedRichTextBox.Text = decrypedText;
            calculation();
        }

        private void strToArray()
        {
            if (plainTextBox.Text.Length == encryptedTextBox.Text.Length)
            {
                cipherRichTextBox.Enabled = true;
                modeButton.Enabled = true;
                plainText = plainTextBox.Text.ToString().ToCharArray();
                encryptedText = encryptedTextBox.Text.ToString().ToCharArray();
            }
            else
            {
                cipherRichTextBox.Enabled = false;
                modeButton.Enabled = false;
            }
        }

        private void calculation()
        {
            cryptChar(cipherRichTextBox.Text);
            decryptedRichTextBox.Text = decrypedText;
        }

        private void cryptChar(string text)
        {
            decrypedText = String.Empty;
            foreach (char cipherChar in text)
            {
                if (cipherChar.ToString() == "\n")
                {
                    decrypedText += "\n";
                }
                else
                {
                    int cipherIndex = Array.IndexOf(encryptedText, cipherChar);
                    if (cipherIndex >= 0)
                    {
                        try
                        {
                            decrypedText += plainText.GetValue(cipherIndex);
                        }
                        catch
                        {
                            MessageBox.Show("An unavoidable exception occurred.", "Substitution Cipher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }
                    }
                    else
                    {
                        decrypedText += cipherChar;
                    }
                }
            }
        }

     

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version: 1.1.3.0\nMIT License\nCopyright (C) Tsubasa FUJII", "Substitution Cipher", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void decryptedRichTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            encryptedTextBox.Text = Shuffle(plainTextBox.Text);
        }

        public static string Shuffle(string str)
        {
            char[] array = str.ToCharArray();
            Random rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
            return new string(array);
        }

        private void Calc_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cipherRichTextBox.Text=Clipboard.GetText();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(decryptedRichTextBox.Text);
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader file = new System.IO.StreamReader(openFileDialog1.FileName);
                plainTextBox.Text = file.ReadLine();
                encryptedTextBox.Text = file.ReadLine();
                file.Close();
            }
        }

        private void Savekey_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog1.FileName);
                file.WriteLine(plainTextBox.Text);
                file.WriteLine(encryptedTextBox.Text);
                file.Close();
                MessageBox.Show("OK!", "Substitution Cipher", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

    }
}
