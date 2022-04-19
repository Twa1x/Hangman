﻿using Hangman.Models;
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

        Button backButton = new Button();

        Label labelTimer = new Label();
        TextBlock level1 = new TextBlock();
        TextBlock level2 = new TextBlock();
        TextBlock level3 = new TextBlock();
        TextBlock level4 = new TextBlock();
        TextBlock level5 = new TextBlock();
        private User _currentUser;
        Button button = new Button();

        public PlayGame(User currentUser)
        {
            wins = 0;
            InitializeComponent();
            Labels = new List<Label>();
            Buttons = new List<Button>();
            CreateNewGameBtn();

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);

            _currentUser = currentUser;
            Cars.IsEnabled = true;
            Mountains.IsEnabled = true;
            Movies.IsEnabled = true;
            Rivers.IsEnabled = true;
            States.IsEnabled = true;

            deadline = DateTime.Now.AddSeconds(delay);
            dispatcherTimer.Start();
            StartTimer();

        }


        private void StartTimer()
        {

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

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            PlayGame playGame = new PlayGame(_currentUser);
            playGame.Show();
            this.Close();
        }
        private void NewGameBtnClick(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            deadline = DateTime.Now.AddSeconds(delay);
            dispatcherTimer.Start();
            if (wins == 5)
            {

                FinishGame("You Win 5 TIMES IN A ROW!");
                if (AllCategories.IsEnabled == true)
                {
                    ChangeStatistic(_currentUser.UserName, "AllCategories");
                }
                if (Cars.IsEnabled == true)
                {
                    ChangeStatistic(_currentUser.UserName, "Cars");
                }
                if (Rivers.IsEnabled == true)
                {
                    ChangeStatistic(_currentUser.UserName, "Rivers");
                }
                if (States.IsEnabled == true)
                {
                    ChangeStatistic(_currentUser.UserName, "CarsStates");
                }
                if (Mountains.IsEnabled == true)
                {
                    ChangeStatistic(_currentUser.UserName, "Mountains");
                }
                if (Movies.IsEnabled == true)
                {
                    ChangeStatistic(_currentUser.UserName, "Movies");
                }
                wins = 0;

            }


            

            string[] tempWords = new string[50];

            int counter = 0;
            if (AllCategories.IsEnabled == true)
            {
                foreach (string line in System.IO.File.ReadLines(@".\Categories\AllCategories.txt"))
                {

                    tempWords[counter++] = line;

                }

            }
            if (Cars.IsEnabled == true)
            {
                foreach (string line in System.IO.File.ReadLines(@".\Categories\Cars.txt"))
                {

                    tempWords[counter++] = line;

                }
            }
            if (Movies.IsEnabled == true)
            {
                foreach (string line in System.IO.File.ReadLines(@".\Categories\Movies.txt"))
                {

                    tempWords[counter++] = line;

                }
            }
            if (Rivers.IsEnabled == true)
            {
                foreach (string line in System.IO.File.ReadLines(@".\Categories\Rivers.txt"))
                {

                    tempWords[counter++] = line;

                }
            }
            if (States.IsEnabled == true)
            {
                foreach (string line in System.IO.File.ReadLines(@".\Categories\States.txt"))
                {

                    tempWords[counter++] = line;

                }
            }

            if (Mountains.IsEnabled == true)
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
                switch (wins)
                {
                    case 1: { level1.Foreground = Brushes.LightGreen; break; }
                    case 2: { level2.Foreground = Brushes.LightGreen; break; }
                    case 3: { level3.Foreground = Brushes.LightGreen; break; }
                    case 4: { level4.Foreground = Brushes.LightGreen; break; }
                    case 5: { level5.Foreground = Brushes.LightGreen; break; }
                    default:
                        break;
                }
               
              
                GameGrid.Children[1].IsEnabled = true;
                backButton.IsEnabled = true;
            }
            else if (HangmanGame.IsGameOver())
            {
                FinishGame("You Lose!");
                level1.Foreground = Brushes.Red;
                level2.Foreground = Brushes.Red;
                level3.Foreground = Brushes.Red;
                level4.Foreground = Brushes.Red;
                level5.Foreground = Brushes.Red;
                wins = 0;
                GameGrid.Children[1].IsEnabled = true;
                backButton.IsEnabled=true;
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
            CreateBackGameBtn();
            CreateCharacterBtns(HangmanGame.Alphabet);
            CreateCharacterLbl(HangmanGame.Lenght);
        }

        #region Game Field Initialization
        private void CreateNewGameBtn()
        {
           
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

        private void CreateBackGameBtn()
        {
            backButton.IsEnabled = false;
            backButton.VerticalAlignment = VerticalAlignment.Center;
            backButton.HorizontalAlignment = HorizontalAlignment.Right;
            backButton.Width = 150;
            backButton.Height = 35;
            backButton.Name = "_back";
            backButton.Content = "Back to categories";       
            GameGrid.Children.Add(backButton);
            backButton.Margin = new Thickness(0,100,0,0);
            backButton.Click += new RoutedEventHandler(BackBtnClick);
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

            level1.Text = "LEVEL 1";
            level1.Name = "lv1";
            level1.VerticalAlignment = VerticalAlignment.Center;
            level1.HorizontalAlignment = HorizontalAlignment.Left;
            level1.Width = 150;
            level1.Height = 35;
           // level1.Foreground = Brushes.Red;

            GameGrid.Children.Add(level1);



            level2.Text = "LEVEL 2";
            level2.Name = "lv2";
            level2.VerticalAlignment = VerticalAlignment.Center;
            level2.HorizontalAlignment = HorizontalAlignment.Left;
            level2.Width = 150;
            level2.Height = 35;
            level2.Margin = new Thickness(0d, 30d, 0d, 0d);
           // level2.Foreground = Brushes.Red;
            GameGrid.Children.Add(level2);


            level3.Text = "LEVEL 3";
            level3.Name = "lv3";
            level3.VerticalAlignment = VerticalAlignment.Center;
            level3.HorizontalAlignment = HorizontalAlignment.Left;
            level3.Width = 150;
            level3.Height = 35;
            level3.Margin = new Thickness(0, 60, 0, 0);
           //level3.Foreground = Brushes.Red;
            GameGrid.Children.Add(level3);



            level4.Text = "LEVEL 4";
            level4.Name = "lv4";
            level4.VerticalAlignment = VerticalAlignment.Center;
            level4.HorizontalAlignment = HorizontalAlignment.Left;
            level4.Width = 150;
            level4.Height = 35;
            level4.Margin = new Thickness(0, 90, 0, 0);
           // level4.Foreground = Brushes.Red;
            GameGrid.Children.Add(level4);



            level5.Text = "LEVEL 5";
            level5.Name = "lv5";
            level5.VerticalAlignment = VerticalAlignment.Center;
            level5.HorizontalAlignment = HorizontalAlignment.Left;
            level5.Width = 150;
            level5.Height = 35;
           // level5.Foreground = Brushes.Red;
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
                label.BorderBrush = Brushes.Red;
                label.Height = label.Width = 38;
                label.HorizontalAlignment = HorizontalAlignment.Left;
                label.VerticalAlignment = VerticalAlignment.Top;
                label.Background = Brushes.White;
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
            Cars.IsEnabled = true;
            Movies.IsEnabled = false;
            AllCategories.IsEnabled = false;
            Rivers.IsEnabled = false;
            States.IsEnabled = false;
            Mountains.IsEnabled = false;
            button.IsEnabled = true;
       
        }

        private void Movies_Click(object sender, RoutedEventArgs e)
        {
            Movies.IsEnabled = true;
            
            AllCategories.IsEnabled = false;
            Cars.IsEnabled = false;

            Rivers.IsEnabled = false;
            States.IsEnabled = false;
            Mountains.IsEnabled = false;
            button.IsEnabled = true;

        }

        private void States_Click(object sender, RoutedEventArgs e)
        {
            States.IsEnabled = true;
            AllCategories.IsEnabled = false;
            Cars.IsEnabled = false;
            Rivers.IsEnabled = false;
            Movies.IsEnabled = false;
            Mountains.IsEnabled = false;
            button.IsEnabled = true;

        }

        private void Mountains_Click(object sender, RoutedEventArgs e)
        {
            Mountains.IsEnabled = true;
            AllCategories.IsEnabled = false;
            Cars.IsEnabled = false;
            Rivers.IsEnabled = false;
            States.IsEnabled = false;
            Movies.IsEnabled = false;
            button.IsEnabled = true;
   
        }

        private void Rivers_Click(object sender, RoutedEventArgs e)
        {
            Rivers.IsEnabled = true;
            AllCategories.IsEnabled = false;
            Cars.IsEnabled = false;
            Movies.IsEnabled = false;
            States.IsEnabled = false;
            Mountains.IsEnabled = false;
            button.IsEnabled = true;
      
        }

        private void AllCategories_Click(object sender, RoutedEventArgs e)
        {
            AllCategories.IsEnabled = true;
            Movies.IsEnabled = false;
            Cars.IsEnabled = false;
            Rivers.IsEnabled = false;
            States.IsEnabled = false;
            Mountains.IsEnabled = false;
            button.IsEnabled = true;
          
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            AllCategories.IsEnabled = true;
            Movies.IsEnabled = true;
            Cars.IsEnabled = true;
            Rivers.IsEnabled = true;
            States.IsEnabled = true;
            Mountains.IsEnabled = true;
            button.IsEnabled = false;
        }

      

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
         
            this.Close();
          
        }
    }
}
