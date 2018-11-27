using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;
using Varlab.Database.Domain;

namespace Varlab.DataLayer
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountDAL
    {
        public static string TABLE = "Account";
        public static string COLUMNS = "AccountID,Username,Email,PasswordHash,HighScore,TotalKills,TotalDeaths,RoundsWon,RoundsLost";


        /// <summary>
        /// Creates an account.
        /// </summary>
        /// <param name="account">An instance of the account.</param>
        public void CreateAccount(Account account)
        {
            SqliteConnection conn = DatabaseUtil.BuildDbConnectionObj();

            try
            {
                // Open database connection
                conn.Open();
                SqliteCommand cmd = conn.CreateCommand();

                // Setup our SQL query strings
                string query = "INSERT INTO " + TABLE + " ({0}) VALUES ({1})";

                string parameters = "AccountID,Username,Email,PasswordHash";
                string arg1 = DatabaseUtil.FormatValue(account.AccountID.ToString(), typeof(int));
                string arg2 = DatabaseUtil.FormatValue(account.Username, typeof(string));
                string arg3 = DatabaseUtil.FormatValue(account.Email, typeof(string));
                string arg4 = DatabaseUtil.FormatValue(account.PasswordHash, typeof(string));
                string values = DatabaseUtil.BuildValues(new string[] { arg1, arg2, arg3, arg4});

                query = string.Format(query, parameters, values);

                // Execute nonquery command
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            finally
            {
                if (conn != null) { conn.Close();  }
            }
        }


        public Account GetAccount(string username)
        {
            Account account = new Account();
            SqliteConnection conn = DatabaseUtil.BuildDbConnectionObj();

            try
            {
                // Open database connection
                conn.Open();
                SqliteCommand cmd = conn.CreateCommand();

                // Setup our SQL query strings
                string query = "SELECT " + COLUMNS + " FROM " + TABLE + " WHERE Username={0}";

                string arg1 = DatabaseUtil.FormatValue(username, typeof(string));
                string values = DatabaseUtil.BuildValues(new string[] { arg1 });

                query = string.Format(query, values);

                // Execute nonquery command
                cmd.CommandText = query;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        account.AccountID = DatabaseUtil.ToInt32(reader, "AccountID");
                        account.Username = reader["Username"].ToString();
                        account.Email = DatabaseUtil.ToString(reader, "Email");
                        account.PasswordHash = DatabaseUtil.ToString(reader, "PasswordHash");
                        account.HighScore = DatabaseUtil.ToInt32(reader, "HighScore");
                        account.TotalKills = DatabaseUtil.ToInt32(reader, "TotalKills");
                        account.TotalDeaths = DatabaseUtil.ToInt32(reader, "TotalDeaths");
                        account.RoundsWon = DatabaseUtil.ToInt32(reader, "RoundsWon");
                        account.RoundsLost = DatabaseUtil.ToInt32(reader, "RoundsLost");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            return account;
        }
    }
}
