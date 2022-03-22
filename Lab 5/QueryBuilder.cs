/*
 * ==========================================================================================
 * File Name: QueryBuilder.cs
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
using Microsoft.Data.Sqlite;

namespace Lab_5
{
    public class QueryBuilder : IDisposable
    {
        SqliteConnection connection;

        public QueryBuilder (string locationOfDatabase)
        {

        }

        public T Read<T> (int id) where T : new()
        {
            return new T ();
        }

        public List<T> ReadAll<T> ()
        {
            return new List<T> ();
        }

        public void Create<T> (T obj)
        {

        }

        public void Update<T> (T obj)
        {

        }

        public void Delete<T> (T obj)
        {

        }

        public void Dispose()
        {

        }
    }
}