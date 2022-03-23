/*
 * ==========================================================================================
 * File Name: Program.cs
 * Project Name: Lab 5
 * ==========================================================================================
 * Creator's Name and Email: Chris Seals, sealscm@etsu.edu
 * Date Created: Mar-21-2022
 * Course: CSCI-2910-001
 * ==========================================================================================
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lab_5.Models;

namespace Lab_5
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = FileRoot.GetDefaultDirectory();
            string fullPath = path + $"{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}Library.db";

            QueryBuilder qb = new QueryBuilder(fullPath);

            Author me = qb.Read<Author>(1);
            Console.WriteLine("Single Read command\n" + me + "\n");

            List<Author> Authors = qb.ReadAll<Author>();

            Console.WriteLine("Read All command");
            foreach (var item in Authors)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\n");

            Author newA = new Author();
            newA.Id = 4;
            newA.FirstName = "John";
            newA.Surname = "Belemy";
            
            qb.Create<Author>(newA);

            Authors = qb.ReadAll<Author>();
            Console.WriteLine("Create command:");
            foreach (var item in Authors)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\n");

            newA.Surname = "Larry";
            qb.Update<Author>(newA);

            Authors = qb.ReadAll<Author>();

            Console.WriteLine("Update command:");
            foreach (var item in Authors)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\n");

            qb.Delete<Author>(newA);

            Authors = qb.ReadAll<Author>();

            Console.WriteLine("Delete command:");
            foreach (var item in Authors)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\n");

            qb.Dispose();
        }
    }
}