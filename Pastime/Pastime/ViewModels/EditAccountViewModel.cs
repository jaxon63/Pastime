using System;
using System.ComponentModel;

namespace Pastime.ViewModels
{
    public class EditAccountViewModel : INotifyPropertyChanged
    {
        private User user;
        private string name;
        private string email;
        private string password;

        public EditAccountViewModel(User user)
        {
            this.user = user;
            this.name = this.user.getUsername();
            this.email = this.user.getEmail();
            this.password = this.user.getPassword();
        }

        public string Name { get { return name; } set { name = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Password { get { return password;  } set { password = value; } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnSubmit()
        {
            
        }
    }
}
