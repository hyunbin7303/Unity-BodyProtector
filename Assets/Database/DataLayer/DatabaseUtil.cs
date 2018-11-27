using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Varlab.DataLayer
{
    /// <summary>
    /// 
    /// </summary>
    public class DatabaseUtil : IDisposable
    {
        public static string DB_NAME = "bodyprotector.db";
        public static string CONN = "URI=file:" + Application.dataPath + "/Database/Source/" + DB_NAME;

        private IDbConnection db;


        public void OpenConnection()
        {
            try
            {
                SqliteConnection db = new SqliteConnection(CONN);
                // Open connection to database
                db.Open();
            }
            catch (Exception ex)
            {
                Debug.LogError("[ERROR] Failed to open database. " + ex.Message);
            }
        }

        public void CloseConnection()
        {
            try
            {
                db.Close();
            }
            catch (Exception ex)
            {
                Debug.LogError("[ERROR] Failed to close database connection. " + ex.Message);
            }
        }

        public void ExecuteInsert(string table, string query, string[] columns, string[] args)
        {
            IDbCommand cmd = db.CreateCommand();

            string columnFormat = "(";
            for (int i = 0; i < columns.Length; i++)
            {
                columnFormat += "{" + i + "}";
                if (i != (columns.Length - 1))
                {
                    columnFormat += ",";
                }
            }
            columnFormat += ")";

            columnFormat = string.Format(columnFormat, columns);
        }

        public void Dispose()
        {
            if (db != null)
            {
                db.Close();
                db.Dispose();
            }
        }


        public static SqliteConnection BuildDbConnectionObj()
        {
            return new SqliteConnection(CONN);
        }

        public static string FormatValue(string val, Type type)
        {
            string result = val;
            string typeName = type.Name;

            switch (typeName.ToLower())
            {
                case "string":
                case "String":
                    result = "'" + val + "'";
                    break;
                case "int":
                case "int32":
                case "uint32":
                    break;
                default:
                    break;
            }

            return result;
        }

        public static string BuildValues(string[] args)
        {
            string result = "";

            for (int i = 0; i < args.Length; i++)
            {
                result += args[i];
                if (i == args.Length - 1) { break; }
                if (i != args.Length - 1)
                {
                    result += ",";
                }
            }

            return result;
        }

        public static int ToInt32(IDataReader reader, string column)
        {
            return Convert.ToInt32(reader[column]);
        }

        public static string ToString(IDataReader reader, string column)
        {
            string result = null;
            //if (Convert.To)
            if (!Convert.IsDBNull(reader[column]))
                result = reader[column].ToString();
            return result;
        }
    }
}
