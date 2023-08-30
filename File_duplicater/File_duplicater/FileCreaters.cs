using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace File_duplicater
{
    class FileCreaters
    {
        public string BackUpDirektori(string UserName, string drive)
        {
            //string files = @"C:\Users\" + UserName + @"\AppData\Local\YYGD Team";
            string files = @"C:\Users\" + UserName + @"\Desktop\YYGD Team";
            string driveDirektor = files + "\\" + drive;
            if (!File.Exists(files))
            {
                Directory.CreateDirectory(files);
            }
            if (!File.Exists(driveDirektor))
            {
                Directory.CreateDirectory(driveDirektor);
            }
            return driveDirektor;
        }
        public void FileCreat(string dir, string typ)
        {
            string name = dir + "\\" + typ + ".txt";
            if (!File.Exists(name))
            {
                using (StreamWriter sw = File.CreateText(name)) { };
            }
            if (!File.Exists(dir + "\\Erorr.txt"))
            {
                using (StreamWriter sw = File.CreateText(dir + "\\Erorr.txt")) { };
            }
        }

        public void TypeSave(string path, List<String> DirektoryName)
        {

            using (StreamWriter writer = new StreamWriter(path , true))
            {
                foreach (string dir in DirektoryName)
                {
                    writer.Write(dir + "\r");
                }
            }
        }
        public void duplicType(string dir, string typ)
        {
            string name = dir + "\\" + typ + "duplic.txt";
            if (!File.Exists(name))
            {
                using (StreamWriter sw = File.CreateText(name)) { };
            }
            string names = dir + "\\" + typ + "checked.txt";
            if (!File.Exists(names))
            {
                using (StreamWriter sw = File.CreateText(names)) { };
            }
        }
    }
}