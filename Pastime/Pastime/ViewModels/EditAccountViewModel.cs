using System;
using System.ComponentModel;
using Pastime.Models;

namespace Pastime.ViewModels
{
    public class EditAccountViewModel : INotifyPropertyChanged
    {
        private User user;
        private string name;
        private string email;
        private string password;

        public string Name { get { return name; } set { name = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Password { get { return password;  } set { password = value; } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnSubmit()
        {
            
        }
    }
}
