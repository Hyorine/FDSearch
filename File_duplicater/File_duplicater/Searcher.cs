using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace File_duplicater
{
    class Searcher
    {
        private string path;
        bool workRun;
        public Searcher()
        {
            workRun = false;
        }
        public void pathSet(string cpath)
        {
            path = cpath;
        }
        public List<String> FullSearch(string sdir, string direct)
        {
            List<String> files = new List<String>();
            files.Add(sdir);
            bool exit = false;
            int counter = 0;
            string drive = path + "\\" + direct + ".txt";
            do
            {
                try
                {
                    files.AddRange(Directory.GetDirectories(files[counter]));
                    using (StreamWriter writer = new StreamWriter(drive, true))
                    {
                        writer.Write(files[counter] + "\r");
                    }
                    if (counter >= files.Count)
                    {
                        exit = true;
                    }
                    counter++;
                    if (workRun)
                    {
                        exit = true;
                    }
                }
                catch (Exception excpt)
                {
                    using (StreamWriter writer = new StreamWriter(path + "\\Erorr.txt", true))
                    {
                        writer.Write(excpt + "\r");
                    }
                    if (counter >= files.Count)
                    {
                        exit = true;
                    }
                    counter++;
                    if (workRun)
                    {
                        exit = true;
                    }
                }
            } while (exit == false);
            return files;
        }
        public void workSetTrue()
        {
            workRun = true;
        }
        public void workSetFalse()
        {
            workRun = false;
        }
        public List<String> FileSearch(List<String> directorys,string type) {
            List<String> files = new List<String>();
            string fileText = path +"\\"+ type+".txt";
            foreach (var f in directorys)
            {
                try
                {
                    List<String> test = new List<String>();
                    files.AddRange(Directory.GetFiles(f, "*" + type));
                    test.AddRange(Directory.GetFiles(f, "*" + type));
                    using (StreamWriter writer = new StreamWriter(fileText, true))
                    {
                        foreach (var ti in test)
                        {
                            writer.Write(ti + "\r");
                        }
                    }
                }
                catch (Exception excpt)
                {
                    using (StreamWriter writer = new StreamWriter(path + "\\Erorr.txt", true))
                    {
                        writer.Write(excpt + "\r");
                    }
                }
            }
            return files;
        }
    }
}