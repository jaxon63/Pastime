using System;
using System.ComponentModel;
using System.Windows.Input;
using Pastime.Models;
using Pastime.Popups;
using Xamarin.Forms;

namespace Pastime.ViewModels
{
    public class EditAccountViewModel : INotifyPropertyChanged
    {
        private User user;
        private string email;
        private string password;
        private string name;

        public event PropertyChangedEventHandler PropertyChanged;


        public EditAccountViewModel()
        {
            //user =  new User(1, "steven@hello.com", "password", "steveny1", 2019, "", "", new Uri("http://www.contoso.com/"), 0);

            //Test user object

            //Test command for displaying user email
            Something = new Command(doSomething);

            //Command that calls update functions
            SaveEmailCommand = new Command<string>(updateEmail);


        }

        public ICommand Something { private set; get; }
        public ICommand SaveEmailCommand { private set; get; }
        public INavigation Navigation { get; set; }
        public string Email
        {
            set
            {
                if (email == value)
                    return;
                email = value;
                OnPropertyChanged(nameof(Email));
            }
            get => email;
        }
        public string Password { get => password; set => password = value; }
        public string Name { get => name; set => name = value; }

        private void updateEmail(String pemail)
        {
            User newUser = new User(user.UserId, user.Email, user.Password, user.Username, user.YearJoined, user.FaveSport, user.Bio, user.DpUri, user.Rating);

            if (String.IsNullOrEmpty(this.email))
            {
                MessagingCenter.Send<EditAccountViewModel>(this, "invalid email");
                if (password != user.Password)
                {
                    MessagingCenter.Send<EditAccountViewModel>(this, "invalid password");
                }
            }
            else
            {
                if (password == "password")
                {
                    newUser.Email = Email;
                    Console.WriteLine(newUser.Email);
                    Console.WriteLine(Email);
                    MessagingCenter.Send<EditAccountViewModel>(this, "updated");

                }
                else
                {
                    MessagingCenter.Send<EditAccountViewModel>(this, "invalid password");
                }
            }

        }


        //Can be deleted
        private void doSomething()
        {
            Console.WriteLine("Test: " + Email);
        }

        void OnPropertyChanged(String text)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(text));
        }


        //TODO: updated details should appear

        public void OnSubmit()
        {

        }
    }
}
