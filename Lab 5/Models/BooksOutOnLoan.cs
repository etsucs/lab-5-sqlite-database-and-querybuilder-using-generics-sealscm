/*
 * ==========================================================================================
 * File Name: BooksOutOnLoan.cs
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
    public class BooksOutOnLoan : IClassModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime DateIssued { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime DateReturned { get; set; }

        public override string ToString()
        {
            return Id + ", " + BookId + ", " + UserId + ", " + DateIssued + ", " + DueDate + ", " + DateReturned;
        }
    }
}