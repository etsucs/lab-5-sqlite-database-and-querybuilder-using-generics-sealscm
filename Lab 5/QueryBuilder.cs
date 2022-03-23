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
using System.Reflection;
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
            connection = new SqliteConnection ("Data Source=" + locationOfDatabase);
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
                    if (typeof(T).GetProperty(reader.GetName(i)).PropertyType == typeof(int))
                    {
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, Convert.ToInt32(reader.GetValue(i)));
                    }
                    else if (typeof(T).GetProperty(reader.GetName(i)).PropertyType == typeof(DateTime) && reader.GetValue(i).ToString().Split('-').Length == 3)
                    {
                        string[] date = reader.GetValue(i).ToString().Split('-');
                        int[] dateNum = new int[3];
                        for (int l = 0; l < 3; l++)
                        {
                            dateNum[l] = Convert.ToInt32(date[l]);
                        }
                        var dateTime = new DateTime(dateNum[0], dateNum[1], dateNum[2]);
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, dateTime);
                    }
                    else
                    {
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, reader.GetValue(i));
                    }
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
                    if (typeof(T).GetProperty(reader.GetName(i)).PropertyType == typeof(int))
                    {
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, Convert.ToInt32(reader.GetValue(i)));
                    }
                    else if (typeof(T).GetProperty(reader.GetName(i)).PropertyType == typeof(DateTime) && reader.GetValue(i).ToString().Split('-').Length == 3)
                    {
                        string[] date = reader.GetValue(i).ToString().Split('-');
                        int[] dateNum = new int[3];
                        for (int l = 0; l < 3; l++)
                        {
                            dateNum[l] = Convert.ToInt32(date[l]);
                        }
                        var dateTime = new DateTime(dateNum[0], dateNum[1], dateNum[2]);
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, dateTime);
                    }
                    else
                    {
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, reader.GetValue(i));
                    }
                }

                datas.Add(data);
            }
            return datas;
        }

        public void Create<T> (T obj)
        {
            //Get objects property names
            PropertyInfo[] properties = typeof(T).GetProperties();

            //Get values from properties
            List<string> values = new List<string>();
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(DateTime))
                {
                    values.Add("\"" + ((DateTime)property.GetValue(obj)).Year + "-" + ((DateTime)property.GetValue(obj)).Month + "-" + ((DateTime)property.GetValue(obj)).Day + "\"");
                }
                else if (property.PropertyType == typeof(string))
                {
                    values.Add("\"" + property.GetValue(obj).ToString() + "\"");
                }
                else
                {
                    values.Add(property.GetValue(obj).ToString());
                }
            }

            //Formatting string to make it correct for sql statement
            StringBuilder sb = new StringBuilder();
            string val = "";
            for (int i = 0; i < values.Count; i++)
            {
                if(i == values.Count - 1)
                {
                    sb.Append($"{values[i]}");
                }
                else
                {
                    sb.Append($"{values[i]}, ");
                }
            }

            var command = connection.CreateCommand();

            command.CommandText = $"insert into {typeof(T).Name} values ({sb})";

            var reader = command.ExecuteNonQuery();
        }

        public void Update<T> (T obj)
        {
            //Get objects property names
            PropertyInfo[] properties = typeof(T).GetProperties();

            //Get values from properties
            List<string> values = new List<string>();
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(DateTime))
                {
                    values.Add("\"" + ((DateTime)property.GetValue(obj)).Year + "-" + ((DateTime)property.GetValue(obj)).Month + "-" + ((DateTime)property.GetValue(obj)).Day + "\"");
                }
                else if (property.PropertyType == typeof(string))
                {
                    values.Add("\"" + property.GetValue(obj).ToString() + "\"");
                }
                else
                {
                    values.Add(property.GetValue(obj).ToString());
                }
            }

            //Formatting string to make it correct for sql statement
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < values.Count; i++)
            {
                if (i == values.Count - 1)
                {
                    sb.Append($"{properties[i].Name} = {values[i]}");
                }
                else
                {
                    sb.Append($"{properties[i].Name} = {values[i]}, ");
                }
            }

            var command = connection.CreateCommand();

            command.CommandText = $"update {typeof(T).Name} set {sb} where {properties[0].Name} = {values[0]}";
            var reader = command.ExecuteNonQuery();
        }

        public void Delete<T> (T obj)
        {
            //Get objects property names
            PropertyInfo[] properties = typeof(T).GetProperties();

            //Get values from properties
            List<string> values = new List<string>();
            foreach (PropertyInfo property in properties)
            {
                values.Add(property.GetValue(obj).ToString());
            }

            var command = connection.CreateCommand();

            command.CommandText = $"delete from {typeof(T).Name} where {properties[0].Name} = {values[0]}";
            var reader = command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            connection.Close();
            connection.Dispose();
        }
    }
}