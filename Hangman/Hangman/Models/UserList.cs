using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Models
{
    public class UserList : ObservableObject
    {

        public ObservableCollection<User> Users { get; set; }
        public User SelectedUser { get; set; }
        public UserList()
        {
            Users = new ObservableCollection<User>();
        }
    }
}
