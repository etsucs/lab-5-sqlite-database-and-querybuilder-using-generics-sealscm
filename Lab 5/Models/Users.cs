/*
 * ==========================================================================================
 * File Name: Users.cs
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
    public class Users : IClassModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public string OtherUserDetails { get; set; }
        public double AmountOfFine { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return Id + ", " + UserName + ", " + UserAddress + ", " + OtherUserDetails + ", " + AmountOfFine + ", " + Email + ", " + PhoneNumber;
        }
    }
}