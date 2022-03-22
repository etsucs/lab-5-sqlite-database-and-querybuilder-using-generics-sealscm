/*
 * ==========================================================================================
 * File Name: Categories.cs
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
    public class Categories : IClassModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}