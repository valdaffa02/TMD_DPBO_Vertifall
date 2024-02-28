using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;
using Vertifall.Model;
using Vertifall.View;

namespace Vertifall.ViewModel
{
    public class MenuC : Canvas
    {

        private Label txtTitle;
        private TextBox txtUsername;
        private Button btnStart;
        private ListView scoreList;
        private GridView gridView;

        private bool gameStarted;
        //private string activeUsername;
        private CScore cscore;


        /*public Label TxtTitle
        {
            get { return txtTitle; }
            set { txtTitle = value; }
        }

        public TextBox TxtUsername
        {
            get { return txtUsername; }
            set { txtUsername = value; }
        }

        public Button BtnStart
        {
            get { return btnStart; }
            set { btnStart = value; }
        }

        public ListView ScoreList
        {
            get { return scoreList; }
            set { scoreList = value; }
        }

        public GridView GridView
        {
            get { return gridView; }
            set { gridView = value; }
        }*/

        public bool GameStarted
        {
            get { return gameStarted; }
            set { gameStarted = value; }
        }

        public CScore Cscore
        {
            get { return Cscore; }
            set { Cscore = value; }
        }



        public MenuC(string name)
        {
            this.Name = name;
            this.gameStarted = false;

            InitializeComponent();
        }

        private void InitializeComponent()
        {

            Console.WriteLine("Hello!");
            //Console.WriteLine(TScore.GetScores());
            //TScore.ReadUsername();
            this.Background = new SolidColorBrush(Colors.White);

            txtTitle = new Label();
            txtTitle.Name = "Title";
            txtTitle.FontSize = 28;
            txtTitle.FontFamily = new FontFamily("Bahnschrift");
            txtTitle.FontWeight = FontWeights.ExtraBold;
            txtTitle.Margin = new Thickness(0, 15, 0, 0);
            txtTitle.Content = "Keep Standing";
            txtTitle.Width = 194;
            Canvas.SetTop(txtTitle, 0);
            Canvas.SetLeft(txtTitle, 400 - (txtTitle.Width / 2));

            btnStart = new Button();
            btnStart.Name = "StartButton";
            btnStart.Content = "Start Game";
            btnStart.Height = 30;
            btnStart.Width = 100;
            btnStart.Margin = new Thickness(433, 100, 217, 0);
            btnStart.Click += btnStart_Click;


            txtUsername = new TextBox();
            txtUsername.Name = "UsernameTextBox";
            txtUsername.Height = 30;
            txtUsername.Width = 200;
            txtUsername.Margin = new Thickness(209, 100, 341, 0);

            scoreList = new ListView();
            scoreList.Name = "ScoreList";
            scoreList.BorderBrush = Brushes.LightGray;
            scoreList.Margin = new Thickness(180);
            scoreList.Height = 300;
            scoreList.ItemsSource = TScore.ReadAll().DefaultView;

            gridView = new GridView();
            scoreList.View = gridView;

            gridView.Columns.Add(new GridViewColumn
            {
                Width = 200,
                DisplayMemberBinding = new Binding("username"),
                Header = "Username"
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Width = 100,
                DisplayMemberBinding = new Binding("score"),
                Header = "Score"
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Width = 100,
                DisplayMemberBinding = new Binding("standing"),
                Header = "Standing"
            });





            this.Children.Add(txtTitle);
            this.Children.Add(btnStart);
            this.Children.Add(txtUsername);
            this.Children.Add(scoreList);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != "")
            {
                try
                {
                    if (Parent is GameWindow gameWindow)
                    {
                        cscore = new CScore();
                        if (UsernameExist(txtUsername.Text))
                        {
                            cscore.Username = txtUsername.Text;
                            cscore.Score = UserScore(txtUsername.Text);
                            cscore.Standing = UserStanding(txtUsername.Text);
                        }
                        else
                        {
                            cscore.Username = txtUsername.Text;
                            cscore.Score = 0;
                            cscore.Standing = 0;
                        }

                        this.gameStarted = true;

                        Console.WriteLine("Button Clicked!" + cscore.Username);

                        gameWindow.checkCondition(UsernameExist(txtUsername.Text), cscore);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private bool UsernameExist(string username)
        {
            DataView itemsSource = (DataView)scoreList.ItemsSource;
            bool usernameExist = itemsSource.Table.AsEnumerable().Any(row => row.Field<string>("username") == username);
            Console.WriteLine(usernameExist);

            return usernameExist;
        }

        private int UserScore(string username)
        {
            DataView itemsSource = (DataView)scoreList.ItemsSource;
            DataRow[] matchingRows = itemsSource.Table.Select($"username = '{username}'");

            if (matchingRows.Length > 0)
            {
                int score = Convert.ToInt32(matchingRows[0]["score"]);
                Console.WriteLine($"Score : {score}");

                return score;
            }
            else
            {
                Console.WriteLine($"Username not found.");

                return 0;
            }
        }

        private int UserStanding(string username)
        {
            DataView itemsSource = (DataView)scoreList.ItemsSource;
            DataRow[] matchingRows = itemsSource.Table.Select($"username = '{username}'");

            if (matchingRows.Length > 0)
            {
                int standing = Convert.ToInt32(matchingRows[0]["standing"]);
                Console.WriteLine($"standing : {standing}");

                return standing;
            }
            else
            {
                Console.WriteLine($"Username not found.");

                return 0;
            }
        }
    }
}
