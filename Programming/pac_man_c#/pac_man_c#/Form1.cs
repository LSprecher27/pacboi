using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace pac_man_c_
{
    public partial class Form1 : Form
    {
        bool goUp, goDown, goLeft, goRight;
        bool noUp, noDown, noLeft, noRight;
        List<PictureBox> walls = new List<PictureBox>();
        List<PictureBox> coins = new List<PictureBox>();
        int speed = 12;
        int score = 0;

        // ghost will be added here


        public Form1()
        {
            InitializeComponent();
            SetUp();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            // LEFT arrow, and no wall blocking the left
            if (e.KeyCode == Keys.Left && noLeft == false)
            {
                goRight = goDown = goUp = false;     // stop the other directions
                noRight = noDown = noUp = false;     // clear the other wall blocks
                goLeft = true;                       // start moving left
                pacman.Image = Properties.Resources.pacman;   // face left
            }

            // RIGHT arrow
            if (e.KeyCode == Keys.Right && noRight == false)
            {
                goLeft = goUp = goDown = false;
                noLeft = noUp = noDown = false;
                goRight = true;
                pacman.Image = Properties.Resources.pacman;
            }

            // UP arrow
            if (e.KeyCode == Keys.Up && noUp == false)
            {
                goLeft = goRight = goDown = false;
                noLeft = noRight = noDown = false;
                goUp = true;
                pacman.Image = Properties.Resources.pacman;
            }

            // DOWN arrow
            if (e.KeyCode == Keys.Down && noDown == false)
            {
                goLeft = goRight = goUp = false;
                noLeft = noRight = noUp = false;
                goDown = true;
                pacman.Image = Properties.Resources.pacman;
            }
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            PlayerMovements();

            foreach (PictureBox wall in walls)
            {
                CheckBoundaries(pacman, wall);
            }

            // Check Pac-Man against every coin
            foreach (PictureBox coin in coins)
            {
                CollectingCoins(pacman, coin);
            }

            // TEMPORARY: when every coin is collected, bring them all back
            // (Part 26 replaces this with a "You Win" message)
            if (score == coins.Count)
            {
                ShowCoins();
                score = 0;
            }
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            // Hide the menu (this hides the button and labels too)
            pnlMenu.Enabled = false;
            pnlMenu.Visible = false;

            // Reset movement and wall blocks
            goLeft = goRight = goUp = goDown = false;
            noLeft = noRight = noUp = noDown = false;

            score = 0;

            // (Ghost resets get added here in Part 24)

            gameTimer.Start();   // start the game loop
        }

        // Collects the walls and coins into lists and creates the ghosts
        private void SetUp()
        {
            foreach (Control x in this.Controls)
            {

                if (x is PictureBox && (string)x.Tag == "wall")
                {
                    walls.Add((PictureBox)x);
                }


                if (x is PictureBox && (string)x.Tag == "coin")
                {
                    coins.Add((PictureBox)x);
                }
            }
            // Create each ghost: (this form, its picture, starting X, starting Y)
            // Then add it to the ghosts list
            red = new Ghost(this, Properties.Resources.red, 100, 100);
            ghosts.Add(red);

            blue = new Ghost(this, Properties.Resources.blue, 848, 597);
            ghosts.Add(blue);

            yellow = new Ghost(this, Properties.Resources.yellow, 132, 584);
            ghosts.Add(yellow);

            pink = new Ghost(this, Properties.Resources.pink, 877, 130);
            ghosts.Add(pink);

            this.Text = walls.Count + " " + coins.Count;


        }

        // Moves Pac-Man in the direction he is going
        private void PlayerMovements()
        {

            if (goLeft) { pacman.Left -= speed; }   // move left
            if (goRight) { pacman.Left += speed; }   // move right
            if (goUp) { pacman.Top -= speed; }    // move up
            if (goDown) { pacman.Top += speed; }    // move down

        }

        // Makes all the coins visible again
        private void ShowCoins()
        {
            foreach (PictureBox coin in coins)
            {
                coin.Visible = true;
            }
        }

        // Stops Pac-Man if he runs into a wall
        private void CheckBoundaries(PictureBox pacman, PictureBox wall)
        {
            // Are Pac-Man and this wall overlapping?
            if (pacman.Bounds.IntersectsWith(wall.Bounds))
            {
                if (goLeft)
                {
                    noLeft = true;                    // block left
                    goLeft = false;                   // stop moving
                    pacman.Left = wall.Right + 2;     // push him just right of the wall
                }

                if (goRight)
                {
                    noRight = true;
                    goRight = false;
                    pacman.Left = wall.Left - pacman.Width - 2;   // just left of the wall
                }

                if (goUp)
                {
                    noUp = true;
                    goUp = false;
                    pacman.Top = wall.Bottom + 2;     // just below the wall
                }

                if (goDown)
                {
                    noDown = true;
                    goDown = false;
                    pacman.Top = wall.Top - pacman.Height - 2;    // just above the wall
                }
            }
        }

        // Collects a coin if Pac-Man touches it
        private void CollectingCoins(PictureBox pacman, PictureBox coin)
        {
            if (pacman.Bounds.IntersectsWith(coin.Bounds))
            {
                // Only collect coins that are still showing
                if (coin.Visible)
                {
                    coin.Visible = false;   // hide the coin
                    score++;                // add 1 to the score
                }
            }
        }

        // Ends the game if a ghost touches Pac-Man
        private void GhostCollision(Ghost g, PictureBox pacman, PictureBox ghost)
        {

        }

        // Stops the game and shows the menu with a message
        private void GameOver(string message)
        {

        }

        private void pnlMenu_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
