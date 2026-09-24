using System.Drawing;         // for Image
using System.Windows.Forms;   // for PictureBox and Form

namespace pac_man_c_
{
    internal class Ghost
    {
        int speed = 8;    // speed when moving in a straight line
        int xSpeed = 4;   // left/right speed in seek mode (half speed)
        int ySpeed = 4;   // up/down speed in seek mode (half speed)

        // The area the ghosts are allowed to move in.
        // These numbers come from YOUR form. See "Finding your numbers" below.
        int maxHeight = 685;
        int minHeight = 81;
        int maxWidth = 970;
        int minWidth = 76;

        int change = 0;   // countdown until the ghost picks a new direction
        Random random = new Random();

        // The five things a ghost can do
        string[] directions = { "left", "right", "up", "down", "seek" };
        string direction = "left";   // the ghost starts by moving left

        // The ghost's picture box. Public so Form1 can use it.
        public PictureBox image = new PictureBox();
        // The constructor runs once, every time a new Ghost is created
        public Ghost(Form game, Image img, int x, int y)
        {
            image.Image = img;                                  // the ghost picture
            image.SizeMode = PictureBoxSizeMode.StretchImage;   // fit the picture to the box
            image.Width = 50;
            image.Height = 50;
            image.Left = x;                                     // starting position
            image.Top = y;

            game.Controls.Add(image);   // put the ghost on the form
        }
    }
}