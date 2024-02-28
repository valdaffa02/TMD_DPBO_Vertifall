using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Vertifall.Model;
using Vertifall.ViewModel;

namespace Vertifall.View
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Game game;
        private MenuC menu;
        private Canvas mainCanvas;

        private bool gameRunning;

        const double WINDOW_WIDTH = 800;
        const double WINDOW_HEIGHT = 600;

        public GameWindow()
        {
            gameRunning = false;
            //game = new Game("WPF_GAME");
            menu = new MenuC("WPF_MENU");
            //game.Background = Brushes.LightSkyBlue;
            //this.KeyDown += game.Game_KeyDown;
            //this.KeyUp += game.Game_KeyUp;
            InitializeComponent(menu);
        }

        private void InitializeComponent(Canvas canvas)
        {
            // Create the main window
            this.Title = "WPF " + canvas.Name;
            this.Width = WINDOW_WIDTH;
            this.Height = WINDOW_HEIGHT;

            // Create canvas
            mainCanvas = canvas;
            mainCanvas.Name = canvas.Name;
            mainCanvas.Focusable = true;

            this.Content = mainCanvas;
        }

        public void checkCondition(bool UsernameExist, CScore ExistingUsername)
        {
            Console.WriteLine(menu.GameStarted + " " + gameRunning);
            if (menu.GameStarted && !gameRunning)
            {
                Console.WriteLine("Game Started and game isnt running");
                gameRunning = true;
                game = new Game("WPF_GAME", UsernameExist, ExistingUsername);
                this.KeyDown += game.Game_KeyDown;
                this.KeyUp += game.Game_KeyUp;
                this.Content = game;

            }
            else if (gameRunning && game.GameOver)
            {
                gameRunning = false;
                menu = new MenuC("WPF_MENU");
                this.Content = menu;
            }
        }
    }
}
