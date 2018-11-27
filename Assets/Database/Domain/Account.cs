
using System;
using UnityEngine;

namespace Varlab.Database.Domain
{
    /// <summary>
    /// An entity model representing the Account table.
    /// </summary>
    [Serializable]
    public class Account
    {
        public int AccountID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public int HighScore { get; set; }
        public int TotalKills { get; set; }
        public int TotalDeaths { get; set; }
        public int RoundsWon { get; set; }
        public int RoundsLost { get; set; }
        public bool IsOnline { get; set; }


        public Account() : this(0, null, null)
        {
        }

        public Account(int accountID, string username, string email)
        {
            AccountID = accountID;
            Username = username;
            Email = email;
            PasswordHash = "";

            HighScore = 0;
            TotalKills = 0;
            TotalDeaths = 0;
            RoundsWon = 0;
            RoundsLost = 0;
        }
    }
}
