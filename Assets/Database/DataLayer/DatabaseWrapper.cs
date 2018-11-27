using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Varlab.Database
{
    /// <summary>
    /// 
    /// </summary>
    public class DatabaseWrapper : IDisposable
    {
        public static string DB_NAME = "bodyprotector.db";
        public static string CONN = "URI=file:" + Application.dataPath + "/Database/Source/" + DB_NAME;

        private IDbConnection db;


        public void OpenConnection()
        {
            try
            {
                db = new SqliteConnection(CONN);
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
    }
}
