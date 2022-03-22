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
            PropertyInfo[] property = typeof(Author).GetProperties();

            foreach (PropertyInfo propertyInfo in property)
            {
                Console.WriteLine(propertyInfo.Name);
            }


        }
    }
}