using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask
{
    [Serializable]
    class Student
    {
        string Name { get; set; }
        string Group{ get; set; }
        DateTime DateOfBirth{ get; set; }
        ///Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string Path = @"C:\Users\Professional\Downloads\Students.dat";
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileRead = new FileStream(Path, FileMode.Open))
            {
                Student[] Students = (Student[]) binaryFormatter.Deserialize(fileRead);
                Console.WriteLine(Students.Length);
            }
            Console.ReadLine();
        }
        
    }
}
