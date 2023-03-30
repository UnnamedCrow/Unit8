using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    internal class Program
    {

        static void CleanDirectory(string Path)
        {
            string[] subDirectories = Directory.GetDirectories(Path);
            string[] files = Directory.GetFiles(Path);
            
            /// Deleting long time used files
            foreach (string s in files)
            {
                var a = DateTime.Now - File.GetLastAccessTime(s);
                if (a > TimeSpan.FromMinutes(30))
                {
                    File.Delete(s);
                }

            }

            /// Recursive go around directions tree
            foreach (string s in subDirectories)
            {
                if (Directory.Exists(s))
                {
                    if (Directory.GetFileSystemEntries(s).Length == 0)
                        Directory.Delete(s);
                    else
                        CleanDirectory(s);
                }
            }

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Input directories path");
            string Path = Console.ReadLine();
            if (Directory.Exists(Path))
            {
                try
                {
                    CleanDirectory(Path);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            Console.WriteLine("Long time used directories and files deleted!");
            Console.ReadLine();
        }
    }
}
