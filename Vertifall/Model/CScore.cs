using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertifall.Model
{
    public class CScore
    {
        private string username;
        private int score;
        private int standing;

        public CScore() { }

        public CScore(string username, int score, int standing)
        {
            Username = username;
            Score = score;
            Standing = standing;
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public int Standing
        {
            get { return standing; }
            set { standing = value; }
        }

    }
}
