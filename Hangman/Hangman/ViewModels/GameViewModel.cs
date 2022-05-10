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
    public class GameViewModel
    {
        public GameViewModel(User user)
        {
            currentUser= user; 
        }
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Play()
        {
            PlayGame playGame = new PlayGame(CurrentUser);
            playGame.Show();


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

    }
}



