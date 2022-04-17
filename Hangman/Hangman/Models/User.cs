using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hangman.Models
{
    public class User : ObservableObject
    {
        private string _userName;
        private string _imagePath;
        private int _userId;
        private string _gamesWin;
        private string _gamesOver;


        public string UserName
        {
            get { return _userName; }
            set { OnPropertyChanged(ref _userName, value); }
        }
  
        public string ImagePath
        {
            get { return _imagePath; }
            set { OnPropertyChanged(ref _imagePath, value); }
        }


        public int UserId
        {
            get { return _userId; }
            set { OnPropertyChanged(ref _userId, value); }
        }

      
        public string GamesWin
        {
            get { return _gamesWin; }
            set { OnPropertyChanged(ref _gamesWin, value); }

        }
       
        public string GamesOver
        { 
            get { return _gamesOver; }
            set { OnPropertyChanged(ref _gamesOver, value); }
        }
    }
}
