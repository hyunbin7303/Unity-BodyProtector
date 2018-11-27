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
                Debug.Log("[AccountDAL.cs][CreateAccount]: Opening db connection");
                conn.Open();
                SqliteCommand cmd = conn.CreateCommand();

                // Setup our SQL query strings
                string query = "INSERT INTO " + TABLE + " ({0}) VALUES ({1})";

                string parameters = "AccountID,Username,Email,PasswordHash";
                string arg1 = DatabaseUtil.FormatValue(account.AccountID.ToString(), typeof(int));
                string arg2 = DatabaseUtil.FormatValue(account.Username, typeof(string));
                string arg3 = DatabaseUtil.FormatValue(account.Email, typeof(string));
                string arg4 = DatabaseUtil.FormatValue(account.PasswordHash, typeof(string));
                string values = DatabaseUtil.BuildValues(new string[] { arg1, arg2, arg3, arg4 });

                query = string.Format(query, parameters, values);

                // Execute nonquery command
                cmd.CommandText = query;
                Debug.Log("[AccountDAL.cs][CreateAccount]: Executing SQL insert command...");
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (result > 0)
                {
                    Debug.Log("[AccountDAL.cs][CreateAccount]: SUCCESS. Created the account!");
                }
                else
                {
                    Debug.Log("[AccountDAL.cs][CreateAccount]: INVALID. Could not create the account...");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                Debug.Log("[AccountDAL.cs][CreateAccount]: ERROR. Exception thrown. Could not create account");
            }
            finally
            {
                if (conn != null)
                {
                    Debug.Log("[AccountDAL.cs][CreateAccount]: Closing db connection");
                    conn.Close();
                    conn.Dispose();
                }
            }
        }


        public Account GetAccountByID(int id)
        {
            Account account = new Account();
            SqliteConnection conn = DatabaseUtil.BuildDbConnectionObj();

            try
            {
                // Open database connection
                Debug.Log("[AccountDAL.cs]: Opening db connection");
                conn.Open();
                SqliteCommand cmd = conn.CreateCommand();

                // Setup our SQL query strings
                string query = "SELECT " + COLUMNS + " FROM " + TABLE + " WHERE AccountID={0}";

                string arg1 = DatabaseUtil.FormatValue(id.ToString(), typeof(int));
                string values = DatabaseUtil.BuildValues(new string[] { arg1 });

                query = string.Format(query, values);

                // Execute nonquery command
                cmd.CommandText = query;
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
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

                        Debug.Log("[AccountDAL.cs]: SUCCESS. Found the accountID=" + id);
                    }
                    else
                    {
                        account = null;
                        Debug.Log("[AccountDAL.cs]: Could not find Account info. accountID=" + id + " does not exist");
                    }
                }

                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                Debug.LogError("[AccountDAL.cs]: ERROR. Exception thrown. Failed to retrieve the Account info");
                account = null;
            }
            finally
            {
                if (conn != null)
                {
                    Debug.Log("[AccountDAL.cs]: Closing db connection");
                    conn.Close();
                    conn.Dispose();
                }
            }

            return account;
        }


        public Account GetAccountByUsername(string username)
        {
            Account account = null;
            SqliteConnection conn = DatabaseUtil.BuildDbConnectionObj();

            try
            {
                // Open database connection
                Debug.Log("[AccountDAL.cs][GetAccountByUsename]: Opening db connection");
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

                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    Debug.Log("[AccountDAL.cs][GetAccountByUsename]: Closing db connection");
                    conn.Close();
                    conn.Dispose();
                }
            }

            return account;
        }
    }
}