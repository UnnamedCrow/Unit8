using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    internal class Program
    {

        /// Counting directory length
        static long GetDirectoryLenght(string Path)
        {
            string[] subDirectories = Directory.GetDirectories(Path);
            string[] files = Directory.GetFiles(Path);
            long Length = 0;

            ///if directory have subdirectories 
            if (subDirectories.Length > 0)
                foreach (string s in subDirectories)
                {
                    Length += GetDirectoryLenght(s);
                }
            ///if directory have files
            if (files.Length > 0)
                foreach (string s in files)
                {
                    FileInfo fileInfo = new FileInfo(s);
                    Length += fileInfo.Length;
                }
            return Length;
        }

        /// Deleting long time used directories and files
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
            long size = 0;
            if (Directory.Exists(Path))
            {
                size = GetDirectoryLenght(Path);
                Console.WriteLine("Directory size is: " + size);
                try
                {
                    CleanDirectory(Path);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            Console.WriteLine("Long time used directories and files deleted! Released {0} bytes", size - GetDirectoryLenght(Path));
            Console.WriteLine("Directory size now is: " + GetDirectoryLenght(Path));
            Console.ReadLine();
        }
    }
}
