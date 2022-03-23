/*
 * ==========================================================================================
 * File Name: Books.cs
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
using System.Text;
using System.Threading.Tasks;

namespace Lab_5.Models
{
    public class Books : IClassModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public DateTime DateOfPublication { get; set; }

        public override string ToString()
        {
            return Id + ", " + Title + ", " + Isbn + ", " + DateOfPublication;
        }
    }
}