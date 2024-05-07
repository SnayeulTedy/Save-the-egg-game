using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaveTheEggGame
{
    public partial class Form1 : Form
    {

        bool goLeft, goRight;
        int speed = 8;
        int score = 0;
        int missed = 0;

        Random randX = new Random();
        Random randY = new Random();

        PictureBox splash = new PictureBox();
        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void MainGameTimer(object sender, EventArgs e)
        {
            txtScore.Text = "Saved: " + score;
            txtMiss.Text = "Missed: " + missed;

            if(goLeft == true && Player.Left > 0)
            {
                Player.Left -= 12;
                Player.Image = Properties.Resources.chicken_normal2;
            }
            if (goRight == true && Player.Left + Player.Width < this.ClientSize.Width)
            {
                Player.Left += 12;
                Player.Image = Properties.Resources.chicken_normal;
            }
            foreach(Control X in this.Controls)
            {
               if(X is PictureBox && (string)X.Tag == "eggs")
               {
                    X.Top += speed;

                    if (X.Top + X.Height > this.ClientSize.Height)
                    {
                        splash.Image = Properties.Resources.splash;
                        splash.Location = X.Location;
                        splash.Height = 60;
                        splash.Width = 60;
                        splash.BackColor = Color.Transparent;

                        this.Controls.Add(splash);


                        X.Top = randY.Next(80, 300) * -1;
                        X.Left = randX.Next(5, this.ClientSize.Width - X.Width);
                        missed += 1;
                        Player.Image = Properties.Resources.chicken_hurt;
                    }

                    if(Player.Bounds.IntersectsWith(X.Bounds))
                    {
                        X.Top = randY.Next(80, 300) * -1;
                        X.Left = randX.Next(5, this.ClientSize.Width - X.Width);
                        score += 1;
                    }
                }
            }
            if(score > 10)
            {
                speed = 12;
            }

            if (missed > 5)
            {
                GameTimer.Stop();
                MessageBox.Show("Game Over" + Environment.NewLine + "We've lost good eggs!" + Environment.NewLine + "Click ok to retry");
                RestartGame();
            }

    }

        private void keyisDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if(e.KeyCode == Keys.Right) 
            {
                goRight = true;
            }
        }

        private void keyisUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
        }

        private void RestartGame()
        {
            foreach(Control X in this.Controls)
            {
                if(X is PictureBox && (string)X.Tag == "eggs")
                {
                    X.Top = randY.Next(80, 300) * -1;
                    X.Left = randX.Next(5, this.ClientSize.Width - X.Width);
                }
            }

            Player.Left = this.ClientSize.Width / 2;
            Player.Image = Properties.Resources.chicken_normal;

            score = 0;
            missed = 0;
            speed = 8;

            goLeft = false;
            goRight = false;

            GameTimer.Start();
            
        }
    }
}

