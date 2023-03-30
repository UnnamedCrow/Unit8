using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalTask
{

    internal class Program
    {
        static bool WriteToDirection(string[] Names, Student[] Students, string Path)
        {

            for (int i = 0; i < Names.Length; i++)
            {
                // Create a new file name
                string name = Path + "\\" + Names[i] + ".txt";
                // Open new filestream
                if (!File.Exists(name))
                {
                    FileStream newStream = File.Create(name);
                    // Close filestream
                    newStream.Dispose();
                }
                try
                {
                    // Open new write stream into file
                    using (StreamWriter wr = new StreamWriter(name, false))
                    {
                        for (int j = 0; j < Students.Length; j++)
                        {
                            // Write into file all students from one group
                            if (Students[j].Group == Names[i])
                                wr.WriteLine("Students name: {0}, students birth: {1}", Students[j].Name, Students[j].DateOfBirth.Date);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }  
            }
            return true;
        }
        // Getting groups names for group files
        static string[] FilesName(string[] Arr)
        {
            string[] Names = new string[Arr.Length];
            Array.Sort(Arr);
            for (int i = 0; i < Arr.Length - 1; i++)
            {

                if (Arr[i] == Arr[i + 1])
                    continue;
                Names[i] = Arr[i];
            }
            Names[Names.Length - 1] = Arr[Arr.Length - 1];
            Array.Sort(Names);
            Array.Reverse(Names);
            int count = 0;
            for (int i = 0; i < Names.Length; i++)
                if (Names[i] == null)
                    count++;
            Array.Resize(ref Names, Names.Length - count);
            Array.Reverse(Names);
            return Names;
        }
        static void Main(string[] args)
        {
            string Path = @"C:\Users\Professional\Downloads\Students.dat";
            string DirectPath = @"C:\Users\Professional\Desktop\Students";
            Student[] Students;

            // Reading array with students data
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileRead = new FileStream(Path, FileMode.Open))
            {
                Students = (Student[])binaryFormatter.Deserialize(fileRead);
            }

            // Create directory on the worktable 
            if (!Directory.Exists(DirectPath))
                Directory.CreateDirectory(DirectPath);

            // Copy group information to the array
            string[] Groups = new string[Students.Length];
            for (int i = 0; i < Groups.Length; i++)
            {
                Groups[i] = Students[i].Group;
            }

            // Creating new filenames for group files
            string[] fileNames = FilesName(Groups);

            // Writing students data into group files
            if (WriteToDirection(fileNames, Students, DirectPath))
                Console.WriteLine("Well done!");
            else
                Console.WriteLine("Ooops! You have a problem!");

            Console.ReadLine();
        }

    }
}