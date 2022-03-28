/*
 * ==========================================================================================
 * File Name: QueryBuilder.cs
 * Project Name: Lab 5
 * ==========================================================================================
 * Creator's Name and Email: Chris Seals, sealscm@etsu.edu
 * Date Created: Mar-23-2022
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
using Lab_5.Models;

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

        /*
         * Read command to select a single record by a given Id number from the parameter.
         * Note: For datatypes of type DateTime, the stored value in the SQLite DB must be in YYYY-MM-DD format.
         * If it is not, then a default null value will be assigned upon object creation.
         */
        public T Read<T> (int id) where T : new()
        {
            var command = connection.CreateCommand();

            command.CommandText = $"select * from {typeof(T).Name} where Id = {id}";

            var reader = command.ExecuteReader();

            T data = new T();

            while (reader.Read())
            {;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    //verify if data type is int, then convert to int, because sqlite's default integer converts to int64
                    if (typeof(T).GetProperty(reader.GetName(i)).PropertyType == typeof(int))
                    {
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, Convert.ToInt32(reader.GetValue(i)));
                    }

                    //verify if data type is DateTime, then make sure it is in the correct format
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

                    //other data types will be set here
                    else
                    {
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, reader.GetValue(i));
                    }
                }
            }
            return data;
        }

        /*
         * ReadAll command to select all records from a table in the Sqlite DB.
         * Note: For datatypes of type DateTime, the stored value in the SQLite DB must be in YYYY-MM-DD format.
         * If it is not, then a default null value will be assigned upon object creation.
         */
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
                    //verify if data type is int, then convert to int, because sqlite's default integer is 64 based
                    if (typeof(T).GetProperty(reader.GetName(i)).PropertyType == typeof(int))
                    {
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, Convert.ToInt32(reader.GetValue(i)));
                    }

                    //verify if data type is DateTime, then make sure it is in the correct format
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

                    //other data types will be set here
                    else
                    {
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, reader.GetValue(i));
                    }
                }

                datas.Add(data);
            }
            return datas;
        }


        /*
         * Creates an object to insert into the Sqlite DB.
         * Note: Must match PK constraint of Id, so it must be unique to the DB upon insertion,
         * and any foreign keys must also be correctly referenced if included.
         */
        public void Create<T> (T obj)
        {
            //Get objects property names
            PropertyInfo[] properties = typeof(T).GetProperties();

            //Get values from properties
            List<string> values = new List<string>();
            foreach (PropertyInfo property in properties)
            {
                //format DateTime for DB
                if (property.PropertyType == typeof(DateTime))
                {
                    values.Add("\"" + ((DateTime)property.GetValue(obj)).Year + "-" + ((DateTime)property.GetValue(obj)).Month + "-" + ((DateTime)property.GetValue(obj)).Day + "\"");
                }
                //format string for DB
                else if (property.PropertyType == typeof(string))
                {
                    values.Add("\"" + property.GetValue(obj).ToString() + "\"");
                }
                //format other data types for insert statement
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

        /*
         * Updates a record by taking a parametered object and taking the values of its properties.
         * Note: It is assumed that the object will inherit the IClassModel interface, so
         * the Id field will be referenced as such.
         */
        public void Update<T> (T obj) where T : IClassModel
        {
            //Get objects property names
            PropertyInfo[] properties = typeof(T).GetProperties();

            //Get values from properties
            List<string> values = new List<string>();
            foreach (PropertyInfo property in properties)
            {
                //format DateTime for DB
                if (property.PropertyType == typeof(DateTime))
                {
                    values.Add("\"" + ((DateTime)property.GetValue(obj)).Year + "-" + ((DateTime)property.GetValue(obj)).Month + "-" + ((DateTime)property.GetValue(obj)).Day + "\"");
                }
                //format string for DB
                else if (property.PropertyType == typeof(string))
                {
                    values.Add("\"" + property.GetValue(obj).ToString() + "\"");
                }
                //format other data types for insert statement
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

            command.CommandText = $"update {typeof(T).Name} set {sb} where Id = {obj.Id}";
            var reader = command.ExecuteNonQuery();
        }

        /*
         * Delete command to delete the parametered object from the database.
         * Note: The object used has to inherit the IClassModel interface
         * to use the Id property correctly.
         */
        public void Delete<T> (T obj) where T : IClassModel
        {
            var command = connection.CreateCommand();

            command.CommandText = $"delete from {typeof(T).Name} where Id = {obj.Id}";
            var reader = command.ExecuteNonQuery();
        }

        /*
         * To close resources commited to reading the Sqlite DB file
         */
        public void Dispose()
        {
            connection.Dispose();
        }
    }
}