using Hangman.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {

        

      
        public SignIn()
        {
            InitializeComponent(); 
            DeleteUser.IsEnabled = false;
            PlayGame.IsEnabled = false;
            Avatar.IsEnabled = false;
        }
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
                     

            DeleteUser.IsEnabled = true;
            PlayGame.IsEnabled = true;
            Avatar.IsEnabled = true;
        }

     
    }
}
