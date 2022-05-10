using Hangman.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;


namespace Hangman.ViewModels
{
    public class UserViewModel
    {
        private User currentUser;


        public User CurrentUser
        {
            get { return currentUser; }
            set
            {
                if (currentUser == value) return;
                currentUser = value;
                NotifyPropertyChanged("CurrentPerson");
            }
        }
        public ObservableCollection<User> Users { get; set; }


        private void Add()
        {

            CurrentUser = new User();
            ChooseAvatar();
            InputDialog inputDialog = new InputDialog("Please enter the username: ");
            if (inputDialog.ShowDialog() == true)
            {
                currentUser.UserName = inputDialog.Answer;
            }
            Users.Add(CurrentUser);
            string lines = CurrentUser.Concatenate();
            File.AppendAllText("Users.txt", lines);
            File.AppendAllText("Users.txt", "\n");


        }

        private void ChooseAvatar()
        {
            var openFileDialog = new OpenFileDialog();


            openFileDialog.Filter = "All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                currentUser.ImagePath = openFileDialog.FileName;
            }
        }

        private ObservableCollection<User> Load()
        {
            ObservableCollection<User> localUsers = new ObservableCollection<User>();

            string fileName = @".\Users.txt";
            IEnumerable<string> lines = File.ReadLines(fileName);
            foreach (string line in lines)
            {
                string[] subs = line.Split(' ');

                int i = 0;
                User user = new User();
                foreach (string word in subs)
                {
                    if (i == 0) { user.UserName = word; }
                    if (i == 1) { user.ImagePath = word; }
                    if (i == 2) { user.GamesWin = word; }
                    if (i == 3) { user.GamesOver = word; }
                    if (i == 4) { user.GamesWinCars = word; }
                    if (i == 5) { user.GamesWinRivers = word; }
                    if (i == 6) { user.GamesWinStates = word; }
                    if (i == 7) { user.GamesWinMountains = word; }
                    if (i == 8) { user.GamesWinMovies = word; }
                    i++;
                }
                localUsers.Add(user);

            }

            return localUsers;
        }

        private void Delete()
        {
            string fileName = @".\Users.txt";
            string name = CurrentUser.UserName;
            string[] Lines = File.ReadAllLines(fileName);
            File.Delete(fileName);
            File.Delete(currentUser.UserName + "SAVED.txt");
            using (StreamWriter sw = File.AppendText(fileName))

            {
                foreach (string line in Lines)
                {
                    if (line.IndexOf(name) >= 0)
                    {

                        continue;
                    }
                    else
                    {
                        sw.WriteLine(line);
                    }
                }
            }
            CurrentUser.UserName = "";
            CurrentUser.ImagePath = "";
        }

        private void Close()
        {
            System.Environment.Exit(0);
        }

        private void Play()
        {
            PlayGame playGame = new PlayGame(CurrentUser);
            playGame.Show();


        }

        private void Statistics()
        {
            string fileName = @".\Users.txt";
            string name = CurrentUser.UserName;
            string[] Lines = File.ReadAllLines(fileName);
            TextBox localTextBox = new TextBox();
            foreach (string line in Lines)
            {
                string[] words = line.Split(' ');

                localTextBox.Text += ("Nume: " + words[0] + "\nImagePath: " + words[1] + "\nGames Lost: " + words[3] + "\nAllCategories Games Win: " + words[2] + "\nCars Games Win: " + words[4]
                    + "\nRivers Games Win: " + words[5] + "\nStates Games Win: " + words[6] + "\nMountains Games Win: " + words[7] + "\nMovies Games Win: " + words[8]);
                localTextBox.Text += "\n--------------------------------------------------------------------------------------------------------------------------\n";
            }

            StatisticsDialog statisticsDialog = new StatisticsDialog(localTextBox);
            statisticsDialog.Show();

        }

     

        private void Help()
        {

            HelpDialog helpDialog = new HelpDialog();
            helpDialog.Show();
        }

        private ICommand closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new RelayCommand(Close);
                }
                return closeCommand;
            }
        }


        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(Add);
                }
                return addCommand;
            }
        }




        private ICommand helpCommand;

        public ICommand HelpCommand
        {
            get
            {
                if (helpCommand == null)
                {
                    helpCommand = new RelayCommand(Help);

                }
                return helpCommand;
            }
        }

        private ICommand statisticsCommand;
        public ICommand StatisticsCommand
        {
            get
            {
                if (statisticsCommand == null)
                {
                    statisticsCommand = new RelayCommand(Statistics);
                }
                return statisticsCommand;
            }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(Delete);
                }
                return deleteCommand;
            }
        }
        public ICommand playCommand;

        public ICommand PlayCommand
        {
            get
            {
                if (playCommand == null)
                {
                    playCommand = new RelayCommand(Play);
                }
                return playCommand;
            }
        }

        private ICommand avatarCommand;

        public ICommand AvatarCommand
        {
            get
            {


                avatarCommand = new RelayCommand(ChooseAvatar);

                return avatarCommand;
            }
        }

        public UserViewModel()
        {

            Users = Load();
            CurrentUser = new User();

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
