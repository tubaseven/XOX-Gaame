using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XOXGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private XoXGame game;
        private void button1_Click(object sender, EventArgs e)
        {
            game = new XoXGame();
            game.GameStarted += new xoxGameHandler(game_Started);
            game.TurnChanged += new xoxGameHandler(turn_Changed);
            game.GameOver += new xoxGameOverHandler(game_Over);
            game.StartGame();
        }

        private void game_Started(XoXGame game)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(100,100);
                    btn.Location = new Point(105*j+10, 105 * i + 10);
                    btn.Tag = 3 * i + j;
                    btn.Click += Btn_Click;
                    this.groupBox1.Controls.Add(btn);
                }
            }

            label1.Text = game.Player1.Name+":"+game.Player1.Score;
            label2.Text = game.Player2.Name + ":" + game.Player2.Score;
            changeActiveLabel();
        }

        private void changeActiveLabel()
        {
            if (game.ActivePlayer == game.Player1)
            {
                label1.Font = new Font("",15);
                label2.Font = default(Font);
            }
            else
            {
                label1.Font = default(Font);
                label2.Font = new Font("", 15);
            }
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            var btn = (sender as Button);
            var fieldIndex = Convert.ToInt32(btn.Tag);
            btn.Text = game.ActivePlayer.Symbol;
            game.PlayTurn(fieldIndex);
        }

        private void turn_Changed(XoXGame game)
        {
            changeActiveLabel();
        }
        private void game_Over(XoXGame game,List<List<int>> winningConditions)
        {
            game.ActivePlayer.WinTheGame();
            label1.Text = game.Player1.Name + ":" + game.Player1.Score;
            label2.Text = game.Player2.Name + ":" + game.Player2.Score;
            foreach (var condition in winningConditions)
            {
                groupBox1.Controls[condition[0]].BackColor = Color.GreenYellow;
                groupBox1.Controls[condition[1]].BackColor = Color.GreenYellow;
                groupBox1.Controls[condition[2]].BackColor = Color.GreenYellow;
            }
        }
    }
}
