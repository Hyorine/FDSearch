using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_duplicater
{
    public partial class Form3 : Form
    {
        DriveInfo[] allDrives = DriveInfo.GetDrives();
        string felh = Environment.UserName;
        string path;
        string TXTDouble;
        string talalt;
        string filestring;
        string filestring1;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            path = @"C:\Users\" + felh + @"\Desktop\YYGD Team";
            drivers();
        }
        private void drivers()
        {
            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady == true)
                {
                    comboBox1.Items.Add(d.Name);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string libay = comboBox1.SelectedItem.ToString();
            libay = Regex.Replace(libay, @"[^0-9a-zA-Z\._]", "");
            TXTDouble = path + "\\" + libay + "\\.txtduplic.txt";
            List<String> hit = new List<String>();
            if (File.Exists(TXTDouble))
            {
                if (new FileInfo(TXTDouble).Length > 0)
                {
                    using (StreamReader file = new StreamReader(TXTDouble))
                    {
                        hit.AddRange(File.ReadAllLines(TXTDouble));
                    }
                }
            }
            if (hit.Count < 1)
            {
                MessageBox.Show("Nincs duplikált kép");
            }
            else
            {
                foreach (var fileName in hit)
                {
                    listBox1.Items.Add(fileName);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                talalt = listBox1.SelectedItem.ToString();
                string[] subs = talalt.Split('|');
                if (File.Exists(subs[0]) && File.Exists(subs[1]))
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(subs[0]))
                        {
                            filestring = sr.ReadToEnd();
                        }
                        using (StreamReader sr = new StreamReader(subs[1]))
                        {
                            filestring1 = sr.ReadToEnd();
                        }
                        richTextBox1.Text = filestring;
                        richTextBox2.Text = filestring1;
                    }
                    catch (Exception)
                    {
                        listBox1.Items.Remove(listBox1.SelectedItem);
                        richTextBox1.Clear();
                        richTextBox2.Clear();
                        listBox1.Refresh();
                        //RefreshBackUp(subs);
                    }
                }
                else
                {
                    listBox1.Items.Remove(listBox1.SelectedItem);
                    listBox1.Refresh();
                    //RefreshBackUp(subs);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                talalt = listBox1.SelectedItem.ToString();
                string[] subs = talalt.Split('|');
                listBox1.Items.Remove(listBox1.SelectedItem);
                try
                {
                    File.Delete(subs[0]);
                }
                catch (Exception)
                {
                    //RefreshBackUp(subs);
                    MessageBox.Show("Hiba! A törlés nem sikerült");
                }
                richTextBox1.Clear();
                richTextBox2.Clear();
                listBox1.Refresh();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                talalt = listBox1.SelectedItem.ToString();
                string[] subs = talalt.Split('|');
                listBox1.Items.Remove(listBox1.SelectedItem);
                try
                {
                    File.Delete(subs[1]);
                }
                catch (Exception)
                {
                    //RefreshBackUp(subs);
                    MessageBox.Show("Hiba! A törlés nem sikerült");
                }
                richTextBox1.Clear();
                richTextBox2.Clear();
                listBox1.Refresh();
            }
        }
    }
}