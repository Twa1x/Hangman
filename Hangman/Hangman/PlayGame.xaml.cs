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
        //nume, categorie, cuvant,, litere corecte, stage, levele, timer; 
        private string[] saveString = new string[8];
        private string[] openString = new string[8];
        private Game HangmanGame { get; set; }

        private int wins;
        private List<Button> Buttons { get; set; }
        private List<Label> Labels { get; set; }
        private Image StageImage { get; set; }

        Button openButton = new Button();
        Button backButton = new Button();
        Button saveButton = new Button();
        Label labelTimer = new Label();
        TextBlock level1 = new TextBlock();
        TextBlock level2 = new TextBlock();
        TextBlock level3 = new TextBlock();
        TextBlock level4 = new TextBlock();
        TextBlock level5 = new TextBlock();
        private User _currentUser;
        Button button = new Button();

        public PlayGame(User currentUser, string text)
        {
            _currentUser = currentUser;
            Console.WriteLine(text);
            wins = 0;
            InitializeComponent();
            Labels = new List<Label>();
            Buttons = new List<Button>();
            CreateNewGameBtn();

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);


            Cars.IsEnabled = true;
            Mountains.IsEnabled = true;
            Movies.IsEnabled = true;
            Rivers.IsEnabled = true;
            States.IsEnabled = true;

            deadline = DateTime.Now.AddSeconds(delay);
            dispatcherTimer.Start();

            Imagine.Source = new BitmapImage(new Uri(currentUser.ImagePath, UriKind.Absolute));
            labelNume.Content = currentUser.UserName;
        }

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

            Imagine.Source = new BitmapImage(new Uri(currentUser.ImagePath, UriKind.Absolute));
            labelNume.Content = currentUser.UserName;

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

                ChangeStatistic(_currentUser.UserName, "Lost");
            }
            else
            {
                labelTimer.Content = secondsRemaining.ToString();
            }
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            this.Close();
            PlayGame playGame = new PlayGame(_currentUser);
            playGame.Show();

        }

        private void SaveBtnClick(object sender, RoutedEventArgs e)
        {
            saveString[0] = "NUMELE USER-ULUI: " + _currentUser.UserName;

            switch (wins)
            {
                case 0: { saveString[5] = "LEVEL: 0 "; break; }
                case 1: { saveString[5] = "LEVEL: 1 "; break; }
                case 2: { saveString[5] = "LEVEL: 2 "; break; }
                case 3: { saveString[5] = "LEVEL: 3 "; break; }
                case 4: { saveString[5] = "LEVEL: 4 "; break; }
                case 5: { saveString[5] = "LEVEL: 5 "; break; }
                default:
                    break;
            }
            saveString[4] = "NUMAR DE GRESELI: " + HangmanGame.Stage.ToString();
            saveString[6] = "TIMP RAMAS: " + labelTimer.Content.ToString();


            foreach (var item in saveString)
            {
                Console.WriteLine(item);
            }
            string path = _currentUser.UserName + "SAVED.txt";



            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                foreach (var item in saveString)
                    sw.WriteLine(item);

            }


        }
        private void NewGameBtnClick(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            saveString = new string[8];
            StartTimer();
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
            saveString[3] += "LITERE CORECTE: ";
            saveString[7] += "LITERE GRESITE: ";
            int counter = 0;
            if (AllCategories.IsEnabled == true)
            {
                saveString[1] += "CATEGORIA: AllCategories";
                foreach (string line in System.IO.File.ReadLines(@".\Categories\AllCategories.txt"))
                {

                    tempWords[counter++] = line;

                }

            }
            if (Cars.IsEnabled == true)
            {
                saveString[1] += "CATEGORIA: Cars";
                foreach (string line in System.IO.File.ReadLines(@".\Categories\Cars.txt"))
                {

                    tempWords[counter++] = line;
                }
            }
            if (Movies.IsEnabled == true)
            {
                saveString[1] += "CATEGORIA: Movies";
                foreach (string line in System.IO.File.ReadLines(@".\Categories\Movies.txt"))
                {

                    tempWords[counter++] = line;

                }
            }
            if (Rivers.IsEnabled == true)
            {
                saveString[1] += "CATEGORIA: Rivers";
                foreach (string line in System.IO.File.ReadLines(@".\Categories\Rivers.txt"))
                {

                    tempWords[counter++] = line;

                }
            }
            if (States.IsEnabled == true)
            {
                saveString[1] += "CATEGORIA: States";
                foreach (string line in System.IO.File.ReadLines(@".\Categories\States.txt"))
                {

                    tempWords[counter++] = line;

                }
            }

            if (Mountains.IsEnabled == true)
            {
                saveString[1] += "CATEGORIA: Mountains";
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


            InitializeGameField(words[new Random().Next(0, words.Length)], 0);

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
                                    int number = Int32.Parse(tempLine[3]);
                                    tempLine[3] = (number + 1).ToString();
                                    break;
                                }
                            case "AllCategories":
                                {

                                    int number = Int32.Parse(tempLine[2]);
                                    tempLine[2] = (number + 1).ToString();
                                    break;
                                }
                            case "Cars":
                                {
                                    int number = Int32.Parse(tempLine[4]);
                                    tempLine[4] = (number + 1).ToString();
                                    break;
                                }
                            case "Rivers":
                                {

                                    int number = Int32.Parse(tempLine[5]);
                                    tempLine[5] = (number + 1).ToString();
                                    break;
                                }
                            case "States":
                                {

                                    int number = Int32.Parse(tempLine[6]);
                                    tempLine[6] = (number + 1).ToString();
                                    break;
                                }
                            case "Mountains":
                                {

                                    int number = Int32.Parse(tempLine[7]);
                                    tempLine[7] = (number + 1).ToString();
                                    break;
                                }
                            case "Movies":
                                {

                                    int number = Int32.Parse(tempLine[8]);
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


        private void HelpCharacterBtnOpen(char ch)
        {
            int[] temp = HangmanGame.TakeCharacter(ch);


            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] == 1)
                {
                    Labels[i].Content = HangmanGame.Word[i];
                    saveString[3] += Labels[i].Content + ", ";
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
                backButton.IsEnabled = true;
                ChangeStatistic(_currentUser.UserName, "Lost");
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
                    saveString[3] += Labels[i].Content + ", ";
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
                backButton.IsEnabled = true;
                ChangeStatistic(_currentUser.UserName, "Lost");
            }
            else
            {
                (sender as Button).IsEnabled = false;
                string chr = (sender as Button).Content.ToString();
                if (saveString[3] != null)
                    if (!(saveString[3].ToString().Contains(chr)))
                        saveString[7] += (sender as Button).Content + ", ";
            }
        }

        private void FinishGame(string message)
        {
            MessageBox.Show(message);
            Buttons.ForEach(b => b.IsEnabled = false);
        }

        private void InitializeGameField(string word, int stage)
        {
            saveString[2] += "CUVANTUL: " + word;
            HangmanGame = new Game(word, GameLanguage.En);
            HangmanGame.Stage = stage;
            Labels.Clear();
            Buttons.Clear();
            GameGrid.Children.Clear();

            CreateImage();
            StageImage.Source = HangmanGame.GetStageImage();

            CreateNewGameBtn();
            CreateLevels();
            CreateLabel();
            CreateSaveGameBtn();
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
            backButton.Margin = new Thickness(0, 100, 0, 0);
            backButton.Click += new RoutedEventHandler(BackBtnClick);
        }


        private void CreateSaveGameBtn()
        {
            saveButton.IsEnabled = true;
            saveButton.VerticalAlignment = VerticalAlignment.Center;
            saveButton.HorizontalAlignment = HorizontalAlignment.Right;
            saveButton.Width = 150;
            saveButton.Height = 35;
            saveButton.Name = "_save";
            saveButton.Content = "Save the game!";
            GameGrid.Children.Add(saveButton);
            saveButton.Margin = new Thickness(0, 150, 0, 0);
            saveButton.Click += new RoutedEventHandler(SaveBtnClick);
        }

        private void CreateLabel()
        {

            labelTimer.Content = delay.ToString();
            labelTimer.Foreground = Brushes.Red;
            labelTimer.FontSize = 16;
            labelTimer.FontStyle = FontStyles.Oblique;
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

        private void OpenGameBtn_Click(object sender, RoutedEventArgs e)
        {

            //nume, categorie, cuvant,, litere corecte, stage, levele, timer, cuvinte gresite; 

            string fileName = @".\" + _currentUser.UserName + "SAVED.txt";

            string[] Lines = new string[8];
          

            try
            {
                // Do not initialize this variable here.
                Lines = File.ReadAllLines(fileName);
                int index = 0;
                foreach (string line in Lines)
                {
                    int tempIndex = line.IndexOf(":");
                    tempIndex = tempIndex + 2;
                    string tempString = line.Substring(tempIndex, line.Length - tempIndex);
                    openString[index++] = tempString;

                }

                InitializeGameField(openString[2], Int32.Parse(openString[4]));

                for (int i = 0; i < openString[3].Length; i++)
                {
                    if (openString[3][i] != ',' && openString[3][i] != ' ')
                    {
                        HelpCharacterBtnOpen(openString[3][i]);
                    }
                }

                saveString = openString;

                foreach (string line in openString)
                {
                    Console.WriteLine(line);
                }

                wins = Int32.Parse(openString[5]);
                switch (wins)
                {
                    case 1: { level1.Foreground = Brushes.LightGreen; break; }
                    case 2: { level1.Foreground = Brushes.LightGreen; level2.Foreground = Brushes.LightGreen; break; }
                    case 3: { level1.Foreground = Brushes.LightGreen; level2.Foreground = Brushes.LightGreen; level3.Foreground = Brushes.LightGreen; break; }
                    case 4: { level1.Foreground = Brushes.LightGreen; level2.Foreground = Brushes.LightGreen; level3.Foreground = Brushes.LightGreen; level4.Foreground = Brushes.LightGreen; break; }
                    case 5: { level1.Foreground = Brushes.LightGreen; level2.Foreground = Brushes.LightGreen; level3.Foreground = Brushes.LightGreen; level4.Foreground = Brushes.LightGreen; level5.Foreground = Brushes.LightGreen; break; }
                    default:
                        break;
                }


                foreach (var item in Buttons)
                {
                    if (openString[3].Contains(item.Content.ToString()) || openString[7].Contains(item.Content.ToString()))
                    {
                        item.IsEnabled = false;
                    }
                }

                dispatcherTimer.Stop();
                saveString = new string[8];
                StartTimer();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                deadline = DateTime.Now.AddSeconds(Int32.Parse(openString[6]));
                dispatcherTimer.Start();


            }
            catch
            {
                MessageBox.Show("NU AI SALVAT NICIUN JOC");
                
            }
                     

           

        }
    }
}
