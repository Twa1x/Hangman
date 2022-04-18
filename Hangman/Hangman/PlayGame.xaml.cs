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
        private Game HangmanGame { get; set; }
        private List<Button> Buttons { get; set; }
        private List<Label> Labels { get; set; }
        private Image StageImage { get; set; }

        public PlayGame()
        {
            InitializeComponent();
            Labels = new List<Label>();
            Buttons = new List<Button>();
            CreateNewGameBtn();
          
            Cars.IsChecked = false;
            Mountains.IsChecked = false;
            Movies.IsChecked = false;
            Rivers.IsChecked = false;
            States.IsChecked = false;
        }
      
        private void NewGameBtnClick(object sender, RoutedEventArgs e)
        {



            string[] tempWords = new string[50];
           
            int counter = 0;
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
        
            for(int i = 0; i < counter; i++)
            {
                words[i]=tempWords[i];
            }
         
        
            InitializeGameField(words[new Random().Next(0, words.Length)]);
           
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
                GameGrid.Children[1].IsEnabled = true;
            }
            else if (HangmanGame.IsGameOver())
            {
                FinishGame("You Lose!");
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
            CreateCharacterBtns(HangmanGame.Alphabet);
            CreateCharacterLbl(HangmanGame.Lenght);
        }

        #region Game Field Initialization
        private void CreateNewGameBtn()
        {
            Button button = new Button();
            button.IsEnabled= false;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.HorizontalAlignment = HorizontalAlignment.Right;
            button.Width = 150;
            button.Height = 35;
            button.Name = "_newgame";
            button.Content = "New Game";
            button.Click += new RoutedEventHandler(NewGameBtnClick);

            GameGrid.Children.Add(button);
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

       
    }
}
