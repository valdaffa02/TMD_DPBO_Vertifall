using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace Vertifall.Model
{
    public class Player : GameObject
    {
        private bool moveLeft;
        private bool moveRight;
        private bool isJumping;

        private int vel_y;
        private int vel_x;

        private double spriteInt;
        private bool slowFall;

        // Getters and setters
        public bool MoveLeft
        {
            get { return moveLeft; }
            set { moveLeft = value; }
        }

        public bool MoveRight
        {
            get { return moveRight; }
            set { moveRight = value; }
        }

        public bool IsJumping
        {
            get { return isJumping; }
            set { isJumping = value; }
        }

        public int VelocityY
        {
            get { return vel_y; }
            set { vel_y = value; }
        }

        public int VelocityX
        {
            get { return vel_x; }
            set { vel_x = value; }
        }

        public bool SlowFall
        {
            get { return slowFall; }
            set { slowFall = value; }
        }

        public Player() { }

        public Player(string name, int height, int width, int leftBound, int topBound, Brush brush) : base(name, height, width, leftBound, topBound, brush)
        {
            moveLeft = false;
            moveRight = false;
            isJumping = false;

            vel_x = 0;
            vel_y = 0;

            spriteInt = 0;
        }

        public Player(string name, int height, int width, int leftBound, int topBound, string uri) : base(name, height, width, leftBound, topBound, uri)
        {
            moveLeft = false;
            moveRight = false;
            isJumping = false;

            vel_x = 0;
            vel_y = 0;

            spriteInt = 0;
        }




        public override void Update()
        {

            //animate player sprite
            animate();

            // logic for player movement
            if (moveRight && Canvas.GetLeft(objRectangle) < 750)
            {
                vel_x = 10;
                Canvas.SetLeft(objRectangle, Canvas.GetLeft(objRectangle) + vel_x);
                objRectangle.RenderTransform = null;
            }
            if (moveLeft && Canvas.GetLeft(objRectangle) > 0)
            {
                vel_x = -10;
                Canvas.SetLeft(objRectangle, Canvas.GetLeft(objRectangle) + vel_x);

                objRectangle.RenderTransformOrigin = new Point(0.5, 0.5);
                objRectangle.RenderTransform = new ScaleTransform(-1, 1);
            }

            if (!moveLeft && !moveRight)
            {
                vel_x = 0;

            }

            if (isJumping)
            {
                vel_y = -22;
                isJumping = false;
            }





            // Apply vel_y to the player
            if (slowFall)
            {
                Canvas.SetTop(objRectangle, Canvas.GetTop(objRectangle) + (vel_y / 2));
            }
            else
            {
                Canvas.SetTop(objRectangle, Canvas.GetTop(objRectangle) + vel_y);
            }

        }

        // to animate player sprite
        private void animate()
        {
            spriteInt += .5;

            if (spriteInt > 4)
            {
                spriteInt = 0;
            }

            if (vel_x > 0 && vel_y == 0)
            {
                switch (spriteInt)
                {
                    case 1:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/tmd_player_walk1.png"));
                        break;
                    case 2:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/tmd_player_walk2.png"));
                        break;
                    case 3:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/tmd_player_walk3.png"));
                        break;
                    case 4:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/tmd_player_walk4.png"));
                        break;

                }
            }
            if (slowFall)
            {
                this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/tmd_player_jump.png"));
            }
            else if (!slowFall || vel_x == 0)
            {
                this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/tmd_player_idle.png"));
            }
        }

        public void HandleKeyDown(Key key)
        {
            // Handle key down events for player movement
            if (key == Key.Left || key == Key.A)
            {
                moveLeft = true;
            }
            if (key == Key.Right || key == Key.D)
            {
                moveRight = true;
            }
            if (key == Key.Space)
            {
                slowFall = true;
            }
            if (key == Key.Up || key == Key.W)
            {
                if (!isJumping && vel_y == 0)
                {
                    isJumping = true;
                }

            }
        }

        public void HandleKeyUp(Key key)
        {
            // Handle key up events for player movement
            if (key == Key.Left || key == Key.A)
            {
                moveLeft = false;
            }
            if (key == Key.Right || key == Key.D)
            {
                moveRight = false;
            }
            if (key == Key.Space)
            {
                slowFall = false;
            }
            if (key == Key.Up || key == Key.W)
            {
                isJumping = false;
            }
        }

        // Add additional properties and methods specific to the Player class
    }
}
