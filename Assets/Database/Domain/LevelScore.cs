
namespace Varlab.Database.Domain
{
    /// <summary>
    /// Tracks the score for the current session.
    /// </summary>
    public class LevelScore
    {
        public int LevelScoreID { get; set; }
        public int AccountID { get; set; }

        public int Score { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }


        public LevelScore() : this(0, 0)
        {
        }

        public LevelScore(int levelScoreID, int accountID)
        {
            LevelScoreID = levelScoreID;
            AccountID = accountID;

            Score = 0;
            Kills = 0;
            Deaths = 0;
        }
    }
}
