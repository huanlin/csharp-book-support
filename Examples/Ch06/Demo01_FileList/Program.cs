using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Demo01_FileList
{
    class Program 
    { 
        static void Main(string[] args)
        {
            FindFiles();

            FindFiles_Linq();

            FindFiles_Linq_Fluent();
        }

        static void FindFiles()
        {
            Console.WriteLine("========== Output of FindFiles() ============");

            DirectoryInfo dir = new DirectoryInfo(@"c:\temp");

            FileInfo[] fileInfoArray = dir.GetFiles("*.*", SearchOption.AllDirectories);

            foreach (FileInfo fi in fileInfoArray)
            {
                if (fi.Extension == ".txt")
                {
                    Console.WriteLine(fi.FullName);    
                }                
            }                        
        }

        static void FindFiles_Linq()
        {
            Console.WriteLine("========== Output of FindFiles_Linq() ============");

            DirectoryInfo dir = new DirectoryInfo(@"c:\temp");

            FileInfo[] fileInfoArray = dir.GetFiles("*.*", SearchOption.AllDirectories);

            IEnumerable<string> queryFiles =
                from file in fileInfoArray
                where file.Extension == ".txt"
                select file.FullName;

            foreach (string filename in queryFiles)
            {
                Console.WriteLine(filename);
            }            
        }

        static void FindFiles_Linq_Fluent()
        {
            Console.WriteLine("========== Output of FindFiles_Linq_Fluent() ============");

            DirectoryInfo dir = new DirectoryInfo(@"c:\temp");

            FileInfo[] fileInfoArray = dir.GetFiles("*.*", SearchOption.AllDirectories);

            IEnumerable<string> queryFiles = fileInfoArray
                .Where(f => f.Extension == ".txt")
                .Select(f => f.FullName);

            foreach (string filename in queryFiles)
            {
                Console.WriteLine(filename);
            }                        
        }

        static void Doable_But_Nonsense()
        {
            DirectoryInfo dir = new DirectoryInfo(@"c:\temp");

            FileInfo[] fileInfoArray = dir.GetFiles("*.*", SearchOption.AllDirectories);

            IEnumerable<FileInfo> filtered = Enumerable.Where(fileInfoArray, f => f.Extension == ".txt");
            IEnumerable<string> projected = Enumerable.Select(filtered, f => f.FullName);

            foreach (string filename in projected)
            {
                Console.WriteLine(filename);
            }

        }
    }
}
