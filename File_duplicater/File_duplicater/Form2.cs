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
    public partial class Form2 : Form
    {
        //string path = @"C:\Users\gyoks\AppData\Local\YYGD Team";
        string felh = Environment.UserName;
        DriveInfo[] allDrives = DriveInfo.GetDrives();
        string path;
        string talalt;
        string jpgTXT;
        string pngTXT;
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
       // C: \Users\gyoks\Desktop\YYGD Team\C
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
            jpgTXT = path + "\\" + libay + "\\.jpgduplic.txt";
            pngTXT = path + "\\" + libay + "\\.pngduplic.txt";
            List<String> hit = new List<String>();
            if (File.Exists(jpgTXT))
            {
                if (new FileInfo(jpgTXT).Length > 0)
                {
                    using (StreamReader file = new StreamReader(jpgTXT))
                    {
                        hit.AddRange(File.ReadAllLines(jpgTXT));
                    }
                }
            }
            if (File.Exists(pngTXT)) {
                if (new FileInfo(pngTXT).Length > 0) 
                {
                    using (StreamReader file = new StreamReader(pngTXT))
                    {
                        hit.AddRange(File.ReadAllLines(pngTXT));
                    }
                }
            }
            if (hit.Count < 1)
            {
                MessageBox.Show("Nincs duplikált kép");
            }
            else {
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
                        pictureBox1.Image = Image.FromFile(subs[0]);
                        pictureBox2.Image = Image.FromFile(subs[1]);
                    }
                    catch (Exception)
                    {
                        listBox1.Items.Remove(listBox1.SelectedItem);
                        pictureBox1.Image = null;
                        pictureBox2.Image = null;
                        listBox1.Refresh();
                        RefreshBackUp(subs);
                    }
                }
                else
                {
                    listBox1.Items.Remove(listBox1.SelectedItem);
                    listBox1.Refresh();
                    RefreshBackUp(subs);
                }
            }
        }
        public void RefreshBackUp(string[] paths) {
            string fileExt = Path.GetExtension(paths[0]);
            string fileExt2 = Path.GetExtension(paths[1]);
            List<String> DirektoryNames = new List<String>();
            if (fileExt ==".jpg" && fileExt2 == ".jpg")
            {
                foreach (var item in listBox1.Items)
                {
                    DirektoryNames.Add(item.ToString());
                }
                using (StreamWriter writer = new StreamWriter(jpgTXT,false))
                {
                    foreach (var names in DirektoryNames)
                    {
                        writer.WriteLine(names);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                talalt = listBox1.SelectedItem.ToString();
                string[] subs = talalt.Split('|');
                pictureBox1.Image.Dispose();
                listBox1.Items.Remove(listBox1.SelectedItem);
                try
                {
                    File.Delete(subs[0]);
                }
                catch
                {
                    RefreshBackUp(subs);
                    MessageBox.Show("Hiba! A törlés nem sikerült");
                }
                pictureBox1.Image = null;
                pictureBox2.Image = null;
                pictureBox1.Refresh();
                pictureBox2.Refresh();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                talalt = listBox1.SelectedItem.ToString();
                string[] subs = talalt.Split('|');
                pictureBox2.Image.Dispose();
                listBox1.Items.Remove(listBox1.SelectedItem);
                try
                {
                    File.Delete(subs[1]);
                }
                catch
                {
                    RefreshBackUp(subs);
                    MessageBox.Show("Hiba! A törlés nem sikerült");
                }
                pictureBox1.Image = null;
                pictureBox2.Image = null;
                pictureBox1.Refresh();
                pictureBox2.Refresh();
            }
        }
    }
}
