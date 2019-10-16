using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Pastime.Models;

namespace Pastime.ViewModels
{
    public class EditAccountViewModel : INotifyPropertyChanged
    {
        private User user;
        private UserModel model;
        private string name;
        private string email;
        private string password;

        public EditAccountViewModel()
        {
            model = new UserModel();
            user = model.User;

            Console.WriteLine(user.Username);
        }

        public string Name
        {
            get => user.Username;
           
            set
            {
                if (name == value)
                    return;
                name = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => user.Email;

            set
            {
                if (email == value)
                    return;
                email = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => user.Password;

            set
            {
                if (password == value)
                    return;
                password = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnSubmit()
        {

        }

        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
