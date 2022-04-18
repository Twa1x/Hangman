using System;
using System.Collections.Generic;
using System.IO;
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
        private int    _userId;
        private string _gamesOver;

        private string _gamesWin;
        private string _gamesWinCars;
        private string _gamesWinRivers;
        private string _gamesWinStates;
        private string _gamesWinMountains;
        private string _gamesWinMovies;
        

        public User()
        {
            _userName = "";
            _imagePath = "";
            _userId= 0;
            _gamesOver = "0";
            _gamesWin = "0";
            _gamesWinCars = "0";
            _gamesWinRivers = "0";
            _gamesWinStates = "0";
            _gamesWinMountains = "0";
            _gamesWinMovies = "0";
        }


       
        public string Concatenate()
        {
            return _userName + " " + _imagePath + " " + _gamesOver + " " + _gamesWin + " " + _gamesWinCars + " " + _gamesWinRivers + " " + _gamesWinStates + " " + _gamesWinMountains + " " + _gamesWinMovies;
        }
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

        public string GamesWinCars
        {
            get { return _gamesWinCars; }
            set { OnPropertyChanged(ref _gamesWinCars, value); }
        }

        public string GamesWinRivers
        {
            get { return _gamesWinRivers; }
            set { OnPropertyChanged(ref _gamesWinRivers, value); }
        }

        public string GamesWinStates
        {
            get { return _gamesWinStates; }
            set { OnPropertyChanged(ref _gamesWinStates, value); }
        }

        public string GamesWinMountains
        {
            get { return _gamesWinMountains; }
            set { OnPropertyChanged(ref _gamesWinMountains, value); }
        }

        public string GamesWinMovies
        {
            get { return _gamesWinMovies; } 
            set { OnPropertyChanged(ref _gamesWinMovies, value); }  

        }
    }
}
