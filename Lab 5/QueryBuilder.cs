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
            connection = new SqliteConnection (locationOfDatabase);
            connection.Open();
        }

        public T Read<T> (int id) where T : new()
        {
            var command = connection.CreateCommand();

            command.CommandText = $"select * from {typeof(T).Name} where Id = {id}";

            var reader = command.ExecuteReader();

            T data = new T();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    typeof(T).GetProperty(reader.GetName(i)).SetValue(data, reader.GetValue(i));
                }
            }

            return data;
        }
        
        public List<T> ReadAll<T> () where T : new()
        {
            var command = connection.CreateCommand();

            command.CommandText = $"select * from {typeof(T).Name}";

            var reader = command.ExecuteReader();

            T data;

            var datas = new List<T>();

            while (reader.Read())
            {
                data = new T();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    typeof(T).GetProperty(reader.GetName(i)).SetValue(data, reader.GetValue(i));
                }

                datas.Add(data);
            }

            return datas;
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
            connection.Dispose();
        }
    }
}