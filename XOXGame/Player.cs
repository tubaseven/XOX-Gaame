using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XOXGame
{
    class Player
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public int Score { get; private set; }

        public Player(string name, string symbol)
        {
            Name = name;
            Symbol = symbol;
            Score = 0;
        }

        public void WinTheGame()
        {
            Score++;
        }

        public void ResetScore()
        {
            Score = 0;
        }
    }
}
