using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace XOXGame
{
    delegate void xoxGameHandler(XoXGame game);
    delegate void xoxGameOverHandler(XoXGame game,List<List<int>> winningConditions);
    class XoXGame
    {
        public event xoxGameHandler GameStarted;
        public event xoxGameHandler TurnChanged;
        public event xoxGameOverHandler GameOver;
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player ActivePlayer { get; set; }

        private List<List<int>> gameOverConditions =new List<List<int>>()
        {
            new List<int>() {0,1,2},
            new List<int>() {3,4,5},
            new List<int>() {6,7,8},
            new List<int>() {0,3,6},
            new List<int>() {1,4,7},
            new List<int>() {2,5,8},
            new List<int>() {0,4,8},
            new List<int>() {2,4,6},
        };
        private string[] symbolArray = new string[9];
        public XoXGame()
        {
              Player1 = new Player("Player-1","X");
              Player2 = new Player("Player-2", "O");
        }

        public void StartGame()
        {
            ActivePlayer = Player1;
            GameStarted(this);
        }

        public void PlayTurn(int fieldIndex)
        {
            symbolArray[fieldIndex] = ActivePlayer.Symbol;
            if (IsGameOver(fieldIndex))
            {
                GameOver(this,GetWinningConditions());
                return;
            }

            ActivePlayer = ActivePlayer == Player1 ? Player2 : Player1;
            TurnChanged(this);
        }

        private List<List<int>> GetWinningConditions()
        {
            List<List<int>> result = new List<List<int>>();
            foreach (var condition in gameOverConditions)
            {
                if (symbolArray[condition[0]] == symbolArray[condition[1]] &&
                    symbolArray[condition[1]] == symbolArray[condition[2]])
                {
                    result.Add(condition);
                }
            }

            return result;
        }
        private bool IsGameOver(int fieldIndex)
        {
            var result = false;
            foreach (var condition in gameOverConditions.Where(x=>x.Contains(fieldIndex)))
            {
                if (symbolArray[condition[0]] == symbolArray[condition[1]] &&
                    symbolArray[condition[1]] == symbolArray[condition[2]])
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
