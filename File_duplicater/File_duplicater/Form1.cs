using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace File_duplicater
{
    public partial class Form1 : Form
    {
        DriveInfo[] allDrives = DriveInfo.GetDrives();
        List<String> DirektoryName = new List<String>();
        List<String> FileTypeList = new List<String>();
        string felh = Environment.UserName;
        string backUpDriver;
        string drive;
        string cmb1async;
        string fileType;
        BackgroundWorker bw = new BackgroundWorker();
        FileCreaters fls = new FileCreaters();
        Searcher search = new Searcher();
        Compar duplic = new Compar();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            drivers();
            FileType();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += directoryTree;
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
        private void FileType()
        {
            string[] filetype = new string[3] { ".jpg",".png",".txt" };
            for (int i = 0; i < filetype.Length; i++)
            {
                comboBox2.Items.Add(filetype[i]);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0 && comboBox2.SelectedIndex >= 0)
            {
                startSearch();
                //FileSearch();
                bw.RunWorkerAsync();
            }
            else
            {
                //Hiba
                if (comboBox1.SelectedIndex < 0)
                {
                    MessageBox.Show("Nem választottál meghajtót");
                }
                else
                {
                    MessageBox.Show("Nem választottál fájl tipust");
                }
            }
        }
        private void FileSearch()
        {
            //keresés
            comboBox1.Invoke((MethodInvoker)delegate
            {
                drive = comboBox1.SelectedItem.ToString();
                cmb1async = comboBox1.SelectedItem.ToString();
            });
            drive = Regex.Replace(drive, @"[^0-9a-zA-Z\._]", "");
            backUpDriver = fls.BackUpDirektori(felh, drive);
            search.pathSet(backUpDriver);
            fls.FileCreat(backUpDriver, drive);
            comboBox2.Invoke((MethodInvoker)delegate
            {
                fls.FileCreat(backUpDriver, comboBox2.SelectedItem.ToString());
                fileType = comboBox2.SelectedItem.ToString();
            });
            if (checkBox1.Checked)
            {
                if (new FileInfo(backUpDriver + "\\" + drive + ".txt").Length > 0)
                {
                    using (StreamReader file = new StreamReader(backUpDriver + "\\" + drive + ".txt"))
                    {
                        DirektoryName.AddRange(File.ReadAllLines(backUpDriver + "\\" + drive + ".txt"));
                    }
                }
                else
                {
                    DirektoryName = search.FullSearch(cmb1async, drive);
                }
            }
            else
            {
                DirektoryName = search.FullSearch(cmb1async, drive);
            }
            if (checkBox2.Checked)
            {
                if (new FileInfo(backUpDriver + "\\" + fileType + ".txt").Length > 0)
                {
                    using (StreamReader file = new StreamReader(backUpDriver + "\\" + fileType + ".txt"))
                    {
                        FileTypeList.AddRange(File.ReadAllLines(backUpDriver + "\\" + fileType + ".txt"));
                    }
                }
                else
                {
                    FileTypeList = search.FileSearch(DirektoryName, fileType);
                }
            }
            else
            {
                FileTypeList = search.FileSearch(DirektoryName, fileType);
            }
            fls.duplicType(backUpDriver, fileType);
            duplic.PathSet(backUpDriver, fileType);
            switch (fileType)
            {
                case ".jpg":
                    duplic.ImageFile(FileTypeList);
                    break;
                case ".png":
                    duplic.ImageFile(FileTypeList);
                    break;
                case ".txt":
                    duplic.TXTFile(FileTypeList);
                    break;
            }
            stopAsync();
            MessageBox.Show("vége");
        }
        private void directoryTree(object sender, EventArgs e)
        {
            FileSearch();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            bw.CancelAsync();
            stopAsync();
        }
        private void startSearch()
        {
            search.workSetFalse();
            duplic.workSetFalse();
            button2.Visible = true;
            button1.Visible = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
        }
        private void stopAsync()
        {
            search.workSetTrue();
            duplic.workSetTrue();
            button2.Invoke((MethodInvoker)delegate
            {
                button2.Visible = false;
            });
            button1.Invoke((MethodInvoker)delegate
            {
                button1.Visible = true;
            });
            comboBox1.Invoke((MethodInvoker)delegate
            {
                comboBox1.Enabled = true;
            });
            comboBox2.Invoke((MethodInvoker)delegate
            {
                comboBox2.Enabled = true;
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var kepform = new Form2();
            kepform.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var TXTform = new Form3();
            TXTform.Show();
        }
    }
}