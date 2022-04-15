using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Models
{
    public class User : ObservableObject
    {
        private string _userName;
        private string _imagePath;

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
    }
}
