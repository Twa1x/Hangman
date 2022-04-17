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
            CurrentUser.GamesWin = "0";
            CurrentUser.GamesOver = "0";
            Users.Add(CurrentUser);

            string lines =

           currentUser.UserName + " " + currentUser.ImagePath + " " + CurrentUser.GamesWin + " " + CurrentUser.GamesOver;


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

            string fileName = @"C:\Users\INTEL\Documents\GitHub\Hangman\Hangman\Hangman\bin\Debug\Users.txt";
            IEnumerable<string> lines = File.ReadLines(fileName);
            foreach (string line in lines)
            {
                string[] subs = line.Split(' ');
                Console.WriteLine(subs);
                int i = 0;
                User user = new User();
                foreach (string word in subs)
                {
                    if (i == 0) { user.UserName = word; }
                    if (i == 1) { user.ImagePath =  word; }
                    if (i == 2) { user.GamesWin = word; }
                    if (i == 3) { user.GamesOver = word; }
                    i++;
                }
                localUsers.Add(user);

            }

            return localUsers;
        }

        private void Delete()
        {
            string name = CurrentUser.UserName;
            string[] Lines = File.ReadAllLines(@"C:\Users\INTEL\Documents\GitHub\Hangman\Hangman\Hangman\bin\Debug\Users.txt");
            File.Delete(@"C:\Users\INTEL\Documents\GitHub\Hangman\Hangman\Hangman\bin\Debug\Users.txt");
            using (StreamWriter sw = File.AppendText(@"C:\Users\INTEL\Documents\GitHub\Hangman\Hangman\Hangman\bin\Debug\Users.txt"))

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


        private ICommand avatarCommand;

        public ICommand AvatarCommand
        {
            get
            {
                if (avatarCommand == null)
                {
                    avatarCommand = new RelayCommand(ChooseAvatar);
                }
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
