using Hangman.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace Hangman
{
    /// <summary>
    /// Interaction logic for PlayGame.xaml
    /// </summary>
    /// 



    public enum GameLanguage
    {
        En,

    }


    public partial class PlayGame : Window
    {

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int delay = 60;
        private DateTime deadline;
        private Game HangmanGame { get; set; }

        private int wins;
        private List<Button> Buttons { get; set; }
        private List<Label> Labels { get; set; }
        private Image StageImage { get; set; }

        Label labelTimer = new Label();

        private User _currentUser;


        public PlayGame(User currentUser)
        {
            wins = 0;
            InitializeComponent();
            Labels = new List<Label>();
            Buttons = new List<Button>();
            CreateNewGameBtn();

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);

            _currentUser = currentUser;
            Cars.IsChecked = false;
            Mountains.IsChecked = false;
            Movies.IsChecked = false;
            Rivers.IsChecked = false;
            States.IsChecked = false;

            deadline = DateTime.Now.AddSeconds(delay);
            dispatcherTimer.Start();
            StartTimer();

        }


        private void StartTimer()
        {
            //se seteaza momentul in care trebuie sa se opreasca timer-ul
            //se adauga la data curenta un numar de secunde egal cu delay-ul
            //mai exact, peste 20 de secunde, trebuie sa se opreasca timer-ul
            //se pot adauga si minute, ore, etc... la data curenta
            deadline = DateTime.Now.AddSeconds(delay);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            int secondsRemaining = (deadline - DateTime.Now).Seconds;
            if (secondsRemaining == 0)
            {
                dispatcherTimer.Stop();
                dispatcherTimer.IsEnabled = false;
                this.Close();
                MessageBox.Show("Time has expired!");
                delay = 60;
            }
            else
            {
                labelTimer.Content = secondsRemaining.ToString();
            }
        }


        private void NewGameBtnClick(object sender, RoutedEventArgs e)
        {

            if (wins == 5)
            {

                FinishGame("You Win 5 TIMES IN A ROW!");
                if (AllCategories.IsChecked == true)
                {
                    ChangeStatistic(_currentUser.UserName, "AllCategories");
                }
                if (Cars.IsChecked == true)
                {
                    ChangeStatistic(_currentUser.UserName, "Cars");
                }
                if (Rivers.IsChecked == true)
                {
                    ChangeStatistic(_currentUser.UserName, "Rivers");
                }
                if (States.IsChecked == true)
                {
                    ChangeStatistic(_currentUser.UserName, "CarsStates");
                }
                if (Mountains.IsChecked == true)
                {
                    ChangeStatistic(_currentUser.UserName, "Mountains");
                }
                if (Movies.IsChecked == true)
                {
                    ChangeStatistic(_currentUser.UserName, "Movies");
                }

            }
            string[] tempWords = new string[50];

            int counter = 0;
            if (AllCategories.IsChecked == true)
            {
                foreach (string line in System.IO.File.ReadLines(@".\Categories\AllCategories.txt"))
                {

                    tempWords[counter++] = line;

                }

            }
            if (Cars.IsChecked == true)
            {
                foreach (string line in System.IO.File.ReadLines(@".\Categories\Cars.txt"))
                {

                    tempWords[counter++] = line;

                }
            }
            if (Movies.IsChecked == true)
            {
                foreach (string line in System.IO.File.ReadLines(@".\Categories\Movies.txt"))
                {

                    tempWords[counter++] = line;

                }
            }
            if (Rivers.IsChecked == true)
            {
                foreach (string line in System.IO.File.ReadLines(@".\Categories\Rivers.txt"))
                {

                    tempWords[counter++] = line;

                }
            }
            if (States.IsChecked == true)
            {
                foreach (string line in System.IO.File.ReadLines(@".\Categories\States.txt"))
                {

                    tempWords[counter++] = line;

                }
            }

            if (Mountains.IsChecked == true)
            {
                foreach (string line in System.IO.File.ReadLines(@".\Categories\Mountains.txt"))
                {

                    tempWords[counter++] = line;

                }
            }



            string[] words = new string[counter];

            for (int i = 0; i < counter; i++)
            {
                words[i] = tempWords[i];
            }


            InitializeGameField(words[new Random().Next(0, words.Length)]);

        }

        private void ChangeStatistic(string userName, string category)
        {
            string fileName = @".\Users.txt";

            string[] Lines = File.ReadAllLines(fileName);
            File.Delete(fileName);
            using (StreamWriter sw = File.AppendText(fileName))

            {
                foreach (string line in Lines)
                {
                    if (line.IndexOf(userName) >= 0)
                    {
                        string[] tempLine = line.Split(' ');

                        switch (category)
                        {
                            case "Lost":
                                {
                                    int number = Int32.Parse(tempLine[2]);
                                    tempLine[2] = (number + 1).ToString();
                                    break;
                                }
                            case "AllCategories":
                                {

                                    int number = Int32.Parse(tempLine[2]);
                                    tempLine[3] = (number + 1).ToString();
                                    break;
                                }
                            case "Cars":
                                {
                                    int number = Int32.Parse(tempLine[2]);
                                    tempLine[4] = (number + 1).ToString();
                                    break;
                                }
                            case "Rivers":
                                {

                                    int number = Int32.Parse(tempLine[2]);
                                    tempLine[5] = (number + 1).ToString();
                                    break;
                                }
                            case "States":
                                {

                                    int number = Int32.Parse(tempLine[2]);
                                    tempLine[6] = (number + 1).ToString();
                                    break;
                                }
                            case "Mountains":
                                {

                                    int number = Int32.Parse(tempLine[2]);
                                    tempLine[7] = (number + 1).ToString();
                                    break;
                                }
                            case "Movies":
                                {

                                    int number = Int32.Parse(tempLine[2]);
                                    tempLine[8] = (number + 1).ToString();
                                    break;
                                }
                                break;
                        }
                        sw.WriteLine(tempLine[0] + " " + tempLine[1] + " " + tempLine[2] + " " + tempLine[3] + " " + tempLine[4] + " " + tempLine[5] + " " + tempLine[6] + " " + tempLine[7] + " " + tempLine[8]);

                    }
                    else
                    {
                        sw.WriteLine(line);
                    }
                }
            }
        }

        private void CharacterBtnClick(object sender, RoutedEventArgs e)
        {
            int[] temp = HangmanGame.TakeCharacter((sender as Button).Content.ToString()[0]);

            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] == 1)
                {
                    Labels[i].Content = HangmanGame.Word[i];
                }
            }

            StageImage.Source = HangmanGame.GetStageImage();

            if (Labels.Count(l => l.Content == null) == 0)
            {
                FinishGame("You Win!");
                wins++;
                GameGrid.Children[1].IsEnabled = true;
            }
            else if (HangmanGame.IsGameOver())
            {
                FinishGame("You Lose!");
                wins = 0;
                GameGrid.Children[1].IsEnabled = true;

            }
            else
            {
                (sender as Button).IsEnabled = false;
            }
        }

        private void FinishGame(string message)
        {
            MessageBox.Show(message);
            Buttons.ForEach(b => b.IsEnabled = false);
        }

        private void InitializeGameField(string word)
        {
            HangmanGame = new Game(word, GameLanguage.En);

            Labels.Clear();
            Buttons.Clear();
            GameGrid.Children.Clear();

            CreateImage();
            StageImage.Source = HangmanGame.GetStageImage();

            CreateNewGameBtn();
            CreateLevels();
            CreateLabel();
            CreateCharacterBtns(HangmanGame.Alphabet);
            CreateCharacterLbl(HangmanGame.Lenght);
        }

        #region Game Field Initialization
        private void CreateNewGameBtn()
        {
            Button button = new Button();
            button.IsEnabled = false;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.HorizontalAlignment = HorizontalAlignment.Right;
            button.Width = 150;
            button.Height = 35;
            button.Name = "_newgame";
            button.Content = "New Game";
            button.Click += new RoutedEventHandler(NewGameBtnClick);

            GameGrid.Children.Add(button);
        }

        private void CreateLabel()
        {

            labelTimer.Content = delay.ToString();
            labelTimer.HorizontalAlignment = HorizontalAlignment.Center;
            labelTimer.VerticalAlignment = VerticalAlignment.Center;
            GameGrid.Children.Add(labelTimer);
        }


        private void CreateLevels()
        {
            TextBlock level1 = new TextBlock();
            level1.Text = "LEVEL 1";
            level1.Name = "lv1";
            level1.VerticalAlignment = VerticalAlignment.Center;
            level1.HorizontalAlignment = HorizontalAlignment.Left;
            level1.Width = 150;
            level1.Height = 35;
            level1.Foreground = Brushes.Red;

            GameGrid.Children.Add(level1);


            TextBlock level2 = new TextBlock();
            level2.Text = "LEVEL 2";
            level2.Name = "lv2";
            level2.VerticalAlignment = VerticalAlignment.Center;
            level2.HorizontalAlignment = HorizontalAlignment.Left;
            level2.Width = 150;
            level2.Height = 35;
            level2.Margin = new Thickness(0d, 30d, 0d, 0d);
            level2.Foreground = Brushes.Red;
            GameGrid.Children.Add(level2);

            TextBlock level3 = new TextBlock();
            level3.Text = "LEVEL 3";
            level3.Name = "lv3";
            level3.VerticalAlignment = VerticalAlignment.Center;
            level3.HorizontalAlignment = HorizontalAlignment.Left;
            level3.Width = 150;
            level3.Height = 35;
            level3.Margin = new Thickness(0, 60, 0, 0);
            level3.Foreground = Brushes.Red;
            GameGrid.Children.Add(level3);


            TextBlock level4 = new TextBlock();
            level4.Text = "LEVEL 4";
            level4.Name = "lv4";
            level4.VerticalAlignment = VerticalAlignment.Center;
            level4.HorizontalAlignment = HorizontalAlignment.Left;
            level4.Width = 150;
            level4.Height = 35;
            level4.Margin = new Thickness(0, 90, 0, 0);
            level4.Foreground = Brushes.Red;
            GameGrid.Children.Add(level4);


            TextBlock level5 = new TextBlock();
            level5.Text = "LEVEL 5";
            level5.Name = "lv5";
            level5.VerticalAlignment = VerticalAlignment.Center;
            level5.HorizontalAlignment = HorizontalAlignment.Left;
            level5.Width = 150;
            level5.Height = 35;
            level5.Foreground = Brushes.Red;
            level5.Margin = new Thickness(0, 120, 0, 0);
            GameGrid.Children.Add(level5);

        }
        private void CreateImage()
        {
            StageImage = new Image();

            StageImage.Name = "StageImage";
            StageImage.VerticalAlignment = VerticalAlignment.Center;
            StageImage.HorizontalAlignment = HorizontalAlignment.Center;
            StageImage.Width = 150;
            StageImage.Height = 150;

            GameGrid.Children.Add(StageImage);
        }

        private void CreateCharacterLbl(int lenght)
        {
            for (int i = 0; i < lenght; i++)
            {
                Label label = new Label();
                label.FontSize = 20;
                label.FontWeight = FontWeight;
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.VerticalContentAlignment = VerticalAlignment.Center;
                label.BorderThickness = new Thickness(1, 1, 1, 1);
                label.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x2D, 0x2D, 0x30));
                label.Height = label.Width = 38;
                label.HorizontalAlignment = HorizontalAlignment.Left;
                label.VerticalAlignment = VerticalAlignment.Top;

                label.Name = "Character" + i.ToString();

                label.Margin = new Thickness(i * label.Width, 0d, 0d, 0d);

                Labels.Add(label);

                GameGrid.Children.Add(label);
            }
        }

        private void CreateCharacterBtns(char[] alph)
        {
            double bot = 0;
            int count = 0;
            for (int i = 0; i < alph.Length; i++, count++)
            {
                Button button = new Button();
                button.FontSize = 20;
                button.FontWeight = FontWeight;
                button.HorizontalContentAlignment = HorizontalAlignment.Center;
                button.VerticalContentAlignment = VerticalAlignment.Center;
                button.Height = button.Width = 38;
                button.HorizontalAlignment = HorizontalAlignment.Left;
                button.VerticalAlignment = VerticalAlignment.Bottom;

                button.Content = alph[i].ToString();

                if ((count + 1) * button.Width > GameGrid.Width)
                {
                    count = 0;
                    bot += button.Height;
                }

                button.Margin = new Thickness(count * button.Width, 0, 0, bot);
                button.Click += new RoutedEventHandler(CharacterBtnClick);

                Buttons.Add(button);

                GameGrid.Children.Add(button);
            }
        }
        #endregion

        private void Cars_Click(object sender, RoutedEventArgs e)
        {
            Cars.IsChecked = true;
            GameGrid.Children[1].IsEnabled = true;
        }

        private void Movies_Click(object sender, RoutedEventArgs e)
        {
            Movies.IsChecked = true;
            GameGrid.Children[1].IsEnabled = true;
        }

        private void States_Click(object sender, RoutedEventArgs e)
        {
            States.IsChecked = true;
            GameGrid.Children[1].IsEnabled = true;
        }

        private void Mountains_Click(object sender, RoutedEventArgs e)
        {
            Mountains.IsChecked = true;
            GameGrid.Children[1].IsEnabled = true;
        }

        private void Rivers_Click(object sender, RoutedEventArgs e)
        {
            Rivers.IsChecked = true;
            GameGrid.Children[1].IsEnabled = true;
        }

        private void AllCategories_Click(object sender, RoutedEventArgs e)
        {
            AllCategories.IsChecked = true;
            GameGrid.Children[1].IsEnabled = true;
        }
    }
}
