using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_duplicater
{
    class Compar
    {
        string drive;
        string fileType;
        bool workRun;
        private List<String> filename;
        private List<String> comparList;
        private Bitmap compbmp;
        private Bitmap smalcompbmp;
        private Bitmap filebmp;
        private Bitmap smalfilebmp;
        private int bmpSize = 16;
        private bool egyezik;
        private bool egyedi = true;
        public Compar()
        {
            workRun = false;
        }
        public List<String> filenameSet()
        {
            return filename;
        }
        public List<String> comparListSet()
        {
            return comparList;
        }
        public void PathSet(string _drive, string _fileType)
        {
            drive = _drive;
            fileType = _fileType;
        }
        public void ImageFile(List<String> fileName)
        {
            filename = new List<string>(fileName);
            comparList = new List<string>(fileName);
            NegatList();
            ComparInvestigationImage();

        }
        public void TXTFile(List<String> fileName) {
            filename = new List<string>(fileName);
            comparList = new List<string>(fileName);
            NegatList();
            ComparInvestigationTXT();
        }

        private void NegatList()
        {
            List<String> hit = new List<String>();
            List<int> hitIndex = new List<int>();
            if (new FileInfo(drive + "\\" + fileType + "checked.txt").Length > 0)
            {
                using (StreamReader file = new StreamReader(drive + "\\" + fileType + "checked.txt"))
                {
                    hit.AddRange(File.ReadAllLines(drive + "\\" + fileType + "checked.txt"));
                }
            }
            if (hit.Count > 0)
            {
                int index = 0;
                foreach (var Com in comparList)
                {
                    bool unique = false;
                    foreach (var h in hit)
                    {
                        if (Com == h)
                        {
                            unique = true;
                            break;
                        }
                        if (workRun)
                        {
                            break;
                        }
                    }
                    if (workRun)
                    {
                        break;
                    }
                    if (unique)
                    {
                        hitIndex.Add(index);
                    }
                    index++;
                }
                foreach (var h in hitIndex)
                {
                    comparList.RemoveAt(h);
                }
            }
        }
        private void ComparInvestigationImage() {
            try
            {
                foreach (string Comp in comparList)
                {
                    using (compbmp = new Bitmap(Comp))
                    {
                        smalcompbmp = new Bitmap(compbmp, bmpSize, bmpSize);
                    }
                    egyedi = true;
                    foreach (string files in filename)
                    {
                        if (files != Comp)
                        {
                            egyezik = true;
                            using (filebmp = new Bitmap(files))
                            {
                                smalfilebmp = new Bitmap(filebmp, bmpSize, bmpSize);
                            }
                            for (int i = 0; i < bmpSize; i++)
                            {
                                for (int j = 0; j < bmpSize; j++)
                                {
                                    if (smalcompbmp.GetPixel(i, j).ToString() != smalfilebmp.GetPixel(i, j).ToString())
                                    {
                                        egyezik = false;
                                        break;
                                    }
                                }
                                if (!egyezik)
                                {
                                    break;
                                }
                            }
                            if (egyezik)
                            {
                                // a két kép egyezik
                                egyedi = false;
                                string duplicated = drive + "\\" + fileType + "duplic.txt";
                                //string duplicated = @"C:\Users\gyoks\AppData\Local\YYGD Team\C\.jpgduplic.txt";
                                using (StreamWriter writer = new StreamWriter(duplicated, true))
                                {
                                    writer.Write(Comp + "|" + files + "\r");
                                }
                            }
                        }
                        if (workRun)
                        {
                            break;
                        }
                    }
                    if (egyedi)
                    {
                        //nincs egyezés semmivel
                        string unique = drive + "\\" + fileType + "checked.txt";
                        //string unique = @"C:\Users\gyoks\AppData\Local\YYGD Team\C\.jpgchecked.txt";
                        using (StreamWriter writer = new StreamWriter(unique, true))
                        {
                            writer.Write(Comp + "\r");
                        }
                    }
                    if (workRun)
                    {
                        break;
                    }
                }
            }
            catch (Exception excpt)
            {
                using (StreamWriter writer = new StreamWriter(drive + "\\Erorr.txt", true))
                {
                    writer.Write(excpt + "\r");
                }
            }
        }
        public void ComparInvestigationTXT() {
            try
            {
                foreach (string Comp in comparList)
                {
                    egyedi = true;
                    long csize = new FileInfo(Comp).Length;
                    foreach (string files in filename)
                    {
                        if (Comp != files)
                        {
                            long fsize = new FileInfo(files).Length;
                            egyezik = false;
                            if (csize == fsize)
                            {
                                //van esély
                                string Compstring;
                                string filestring;
                                using (StreamReader sr = new StreamReader(Comp))
                                {
                                    Compstring = sr.ReadToEnd();
                                }
                                using (StreamReader sr = new StreamReader(files))
                                {
                                    filestring = sr.ReadToEnd();
                                }
                                if (Compstring.Equals(filestring))
                                {
                                    egyezik = true;
                                }
                            }
                            if (egyezik)
                            {
                                // a két txt egyezik
                                egyedi = false;
                                string duplicated = drive + "\\" + fileType + "duplic.txt";
                                using (StreamWriter writer = new StreamWriter(duplicated, true))
                                {
                                    writer.Write(Comp + "|" + files + "\r");
                                }
                            }
                            if (workRun)
                            {
                                break;
                            }
                        }
                    }
                    if (egyedi)
                    {
                        //nincs egyezés semmivel
                        string unique = drive + "\\" + fileType + "checked.txt";
                        using (StreamWriter writer = new StreamWriter(unique, true))
                        {
                            writer.Write(Comp + "\r");
                        }
                    }
                    if (workRun)
                    {
                        break;
                    }
                }
            }
            catch (Exception excpt)
            {
                using (StreamWriter writer = new StreamWriter(drive + "\\Erorr.txt", true))
                {
                    writer.Write(excpt + "\r");
                }
            }
        }
        public void workSetTrue()
        {
            workRun = true;
        }
        public void workSetFalse()
        {
            workRun = false;
        }
    }
}
