using Hangman.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
          
            InputDialog inputDialog = new InputDialog("Please enter the username: ");
            if (inputDialog.ShowDialog() == true) 
            {
                currentUser.UserName = inputDialog.Answer;
            }
            Users.Add(CurrentUser);

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
            Users = new ObservableCollection<User>();
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
