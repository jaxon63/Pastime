using System;
using System.ComponentModel;
using System.Windows.Input;
using Pastime.Models;
using Xamarin.Forms;

namespace Pastime.ViewModels
{
    public class EditAccountViewModel : INotifyPropertyChanged
    {
        private User user;
        private string email;
        private string password;


        public EditAccountViewModel()
        {
            this.user = new User(1, "steven@hello.com", "password", "steveny1", 2014, "bball", "hello", new Uri("http://www.contoso.com/"), 3);
            SaveCommand = new Command(updateEmail);
        }

        public ICommand SaveCommand { private set; get; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }

        void  updateEmail()
        {
            if(password == user.Password)
            {
                //TODO: popup should pop after this method executes successfuylly?
                string newEmail = this.email;
                user.Email = newEmail;
            
            } else
            {
                //TODO: display error message
                Console.WriteLine("no way hosay");

            }

        }



        public event PropertyChangedEventHandler PropertyChanged;

        public void OnSubmit()
        {
            
        }
    }
}
