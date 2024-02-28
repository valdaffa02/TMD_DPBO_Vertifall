using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows;
using Vertifall.Model;
using Vertifall.View;

namespace Vertifall.ViewModel
{
    public class Game : Canvas
    {

        // game variable will be shown in display
        CScore cscore;
        int score;
        int standing;
        string username;

        bool existingUsername;

        // gravity constant as downward pulling force
        const int GRAVITY = 8;

        // boolean for game over indicator
        bool gameOver;

        // background image
        ImageBrush bgImage;

        // gameTimer as the gameloop
        DispatcherTimer gameTimer;

        // player object
        Player player;

        Platform[] platforms;

        // UI elements
        Label txtScore, txtStanding, txtUsername, txtInput, txtCollision;
        Rectangle uiBackground;

        // randomizer for platform length
        Random rand;

        //int tickCount;
        //DateTime lastTickTime;

        public bool GameOver
        {
            get { return gameOver; }
            set { gameOver = value; }
        }

        // Constructor
        public Game(string name, bool existingUsername, CScore cscore)
        {
            this.cscore = cscore;
            this.Name = name;

            this.existingUsername = existingUsername;

            bgImage = new ImageBrush();
            bgImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/background.png"));
            this.Background = bgImage;

            InitializeComponent();

            // make gameEngine subscribe to gameTimer rick event
            gameTimer.Tick += gameEngine;

            gameTimer.Interval = TimeSpan.FromMilliseconds(16);

            // subscriptions for key input handling
            this.KeyDown += Game_KeyDown;
            this.KeyUp += Game_KeyUp;

            // initialize game
            StartGame();

        }

        // Game components initialization
        private void InitializeComponent()
        {
            gameTimer = new DispatcherTimer();

            player = new Player("player", 50, 50, 375, 100, "pack://application:,,,/Images/tmd_player_idle.png");

            platforms = new Platform[11];

            rand = new Random();

            for (int i = 0; i < 11; i++)
            {
                platforms[i] = new Platform("platform", 60, 60, 0, i * 60, "pack://application:,,,/Images/platform_singular.png");
            }

            // UI elements initialization
            txtScore = new Label();
            txtScore.Name = "txtScore";
            txtScore.Content = "Score: ";
            txtScore.FontSize = 18;
            txtScore.FontWeight = FontWeights.Bold;
            txtScore.Foreground = new SolidColorBrush(Colors.Yellow);
            Canvas.SetTop(txtScore, 7);
            Canvas.SetLeft(txtScore, 8);

            txtStanding = new Label();
            txtStanding.Name = "txtStanding";
            txtStanding.Content = "Standing: ";
            txtStanding.FontSize = 18;
            txtStanding.FontWeight = FontWeights.Bold;
            txtStanding.Foreground = new SolidColorBrush(Colors.Yellow);
            Canvas.SetTop(txtStanding, 7);
            Canvas.SetLeft(txtStanding, 150);

            txtUsername = new Label();
            txtUsername.Name = "txtUsername";
            txtUsername.Content = "Username: ";
            txtUsername.FontSize = 18;
            txtUsername.FontWeight = FontWeights.Bold;
            txtUsername.Foreground = new SolidColorBrush(Colors.Yellow);
            Canvas.SetTop(txtUsername, 7);
            Canvas.SetLeft(txtUsername, 300);

            txtInput = new Label();
            txtInput.Name = "txtInput";
            txtInput.Content = "Input: ";
            txtInput.FontSize = 18;
            txtInput.FontWeight = FontWeights.Bold;
            Canvas.SetTop(txtInput, 0);
            Canvas.SetRight(txtInput, 0);

            txtCollision = new Label();
            txtCollision.Name = "txtCollision";
            txtCollision.Content = "Collision: ";
            txtCollision.FontSize = 18;
            txtCollision.FontWeight = FontWeights.Bold;
            Canvas.SetTop(txtCollision, 80);
            Canvas.SetRight(txtCollision, 0);

            uiBackground = new Rectangle();
            uiBackground.Width = 600;
            uiBackground.Height = 50;
            uiBackground.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/top-panel.png")));
            Canvas.SetTop(uiBackground, 0);
            Canvas.SetLeft(uiBackground, 0);

            //draw to Window's canvas
            player.Draw(this);
            for (int i = 0; i < 11; i++)
            {
                platforms[i].Draw(this);
            }
            this.Children.Add(uiBackground);
            this.Children.Add(txtScore);
            this.Children.Add(txtStanding);
            this.Children.Add(txtUsername);
            //this.Children.Add(txtInput);
            this.Children.Add(txtCollision);
        }

        private void StartGame()
        {
            //game variables initialization
            gameOver = false;
            //set score and standing to 0
            //score = 0;
            //standing = 0;
            //set the score text and standing text to their own respective integer
            txtScore.Content = "Score: " + cscore.Score;
            txtStanding.Content = "Standing: " + cscore.Standing;
            txtUsername.Content = "Username: " + cscore.Username;


            //tickCount = 0;
            //lastTickTime = DateTime.Now;

            //start the gameTimer
            gameTimer.Start();
        }

        private void gameEngine(object sender, EventArgs e)
        {
            // updating game objects
            player.Update();
            if (player.VelocityY < GRAVITY)
            {
                player.VelocityY += 2;
            }
            for (int i = 0; i < 11; i++)
            {
                platforms[i].Update();
                CollisionHandling(platforms[i], player);
                //DrawHitBoxOutline(platforms[i].ObjRectangle, Brushes.Yellow);

                BackToBottom(platforms[i]);

            }
            //DrawHitBoxOutline(player.ObjRectangle, Brushes.Yellow);
            //UpdateTPS();
            txtScore.Content = "Score: " + cscore.Score;
            txtStanding.Content = "Standing: " + cscore.Standing;
            txtUsername.Content = "Username: " + cscore.Username;

            if (Canvas.GetTop(player.ObjRectangle) < -1 * (player.ObjRectangle.Height) || Canvas.GetTop(player.ObjRectangle) > 600)
            {
                gameOver = true;
            }

            if (gameOver)
            {
                StopGame();
            }
        }

        private void CollisionHandling(Platform platform, Player player)
        {
            Rect playerRect = new Rect(Canvas.GetLeft(player.ObjRectangle), Canvas.GetTop(player.ObjRectangle), player.ObjRectangle.Width, player.ObjRectangle.Height);
            Rect platformRect = new Rect(Canvas.GetLeft(platform.ObjRectangle), Canvas.GetTop(platform.ObjRectangle), platform.ObjRectangle.Width, platform.ObjRectangle.Height);

            if (playerRect.IntersectsWith(platformRect) && playerRect.Top < platformRect.Top + player.ObjRectangle.Height)
            {
                // check if the player is colliding from the top
                bool isCollidingFromTop = (playerRect.Bottom > platformRect.Top && playerRect.Bottom < platformRect.Top + (player.ObjRectangle.Height / 4)) && (playerRect.Left < platformRect.Right - 13);

                // check if the player is colliding from the sides
                bool isCollidingFromSides = (playerRect.Right >= platformRect.Left && playerRect.Left < platformRect.Left) ||
                                            (playerRect.Left <= platformRect.Right - 12 && playerRect.Right > platformRect.Right);

                if (isCollidingFromTop)
                {
                    // if player collides from the top
                    player.VelocityY = 0;
                    // place the character on top of the platform
                    Canvas.SetTop(player.ObjRectangle, platformRect.Top - player.ObjRectangle.Height);
                    txtCollision.Content = "Collision: Top";
                    if (platform.Status == "active")
                    {
                        platform.Status = "claimed";
                        cscore.Score += platform.AttachedScore;
                        cscore.Standing++;
                    }
                }
                else if (isCollidingFromSides)
                {
                    if (player.MoveLeft && player.VelocityX < 0)
                    {
                        // if moving left and colliding from the right side of the platform
                        player.VelocityX = 0;
                        Canvas.SetLeft(player.ObjRectangle, platformRect.Left + platformRect.Width - 13);
                        txtCollision.Content = "Collision: Left";
                    }
                }
            }
        }

        //stop game
        private void StopGame()
        {
            //stop gametimer
            gameTimer.Stop();
            MessageBox.Show("GAME OVER!");

            if (existingUsername)
            {
                TScore.UpdateCScore(cscore);
            }
            else
            {
                TScore.InsertCScore(cscore);
            }

            try
            {
                if (Parent is GameWindow gameWindow)
                {

                    gameWindow.checkCondition(false, null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        public void Game_KeyDown(object sender, KeyEventArgs e)
        {
            // Call the HandleKeyDown method of player
            try
            {
                player.HandleKeyDown(e.Key);
                txtInput.Content = "Input: " + e.Key;
            }
            catch (Exception f)
            {
                Console.WriteLine(f.ToString());
            }
        }

        public void Game_KeyUp(object sender, KeyEventArgs e)
        {
            // Call the HandleKeyDown method of player
            try
            {
                if (e.Key == Key.Space)
                {
                    if (!gameOver)
                    {
                        gameOver = true;
                    }
                }
                player.HandleKeyUp(e.Key);
                txtInput.Content = "Input: - ";
            }
            catch (Exception f)
            {
                Console.WriteLine(f.ToString());
            }
        }

        //void to make out of view platform go back to the bottom
        public void BackToBottom(Platform platform)
        {
            if (Canvas.GetTop(platform.ObjRectangle) <= -60)
            {
                platform.ObjRectangle.Width = rand.Next(1, 10) * 60;
                Canvas.SetTop(platform.ObjRectangle, 600);
                platform.Status = "active";
                platform.AttachedScore = (int)(100 - (platform.ObjRectangle.Width / 6));
            }
        }

        /*private void UpdateTPS()
        {
            tickCount++;
            if (tickCount >= 60)
            {
                var currentTime = DateTime.Now;
                var elapsedSeconds = (currentTime - lastTickTime).TotalSeconds;
                var tps = Math.Round(tickCount / elapsedSeconds);
                txtScore.Content = "TPS: " + tps;
                tickCount = 0;
                lastTickTime = currentTime;
            }
        }*/
    }
}
