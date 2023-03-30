using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    internal class Program
    {
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
        static void Main(string[] args)
        {
            Console.WriteLine("Input directories path");
            string Path = Console.ReadLine();
            if (Directory.Exists(Path))
            {
                Console.WriteLine("Directory length is " + GetDirectoryLenght(Path)+ " bytes");
            }
            Console.ReadLine(); 
        }
    }
}
