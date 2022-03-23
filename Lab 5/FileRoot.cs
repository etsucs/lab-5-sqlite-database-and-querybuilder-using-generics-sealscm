/*
 * ==========================================================================================
 * File Name: FileRoot.cs
 * Project Name: Lab 5
 * ==========================================================================================
 * Creator's Name and Email: Chris Seals, sealscm@etsu.edu
 * Course: CSCI-2910-001
 * Date Created: Feb-15-2022
 * ==========================================================================================
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace Lab_5
{
    public class FileRoot
    {
        public static string GetDefaultDirectory()
        {
            string output = "";

            DirectoryInfo path = new DirectoryInfo(Directory.GetCurrentDirectory());

            //Gets out of bin folder on runtime
            output = path.Parent.Parent.Parent.ToString();

            return output;
        }
    }
}