using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Pastime.Models;
using Xamarin.Forms;

namespace Pastime.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private string email = string.Empty;
        private string password = string.Empty;

        private bool invalidLogin;
        private string loginErrMsg = "Incorrent email or password";

        //The navigation is passed from the view to the viewmodel
        private readonly INavigation nav;

        //The model to perform business logic
        private readonly LoginModel lm;

        public LoginPageViewModel(INavigation nav)
        {
            //TODO: Fix this problem
            LoginCommand = new Command(async () => await LogMeIn());

            lm = new LoginModel();
            this.nav = nav;
        }

        public string Email
        {
            get => email;
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
            get => password;
            set
            {
                if (password == value)
                    return;
                password = value;
                OnPropertyChanged();
            }
        }

        public bool InvalidLogin
        {
            get => invalidLogin;
            set
            {
                if (invalidLogin == value)
                    return;
                invalidLogin = value;
                OnPropertyChanged();
            }
        }

        public string LoginErrMsg
        {
            get => loginErrMsg;
            set
            {
                if (loginErrMsg == value)
                    return;
                loginErrMsg = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LoginCommand { private set; get; }

        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       

        private async Task LogMeIn()
        {
            InvalidLogin = !lm.LogMeIn(email, password, out string current_user);
            if(!InvalidLogin)
            {
                Xamarin.Forms.Application.Current.Properties["IsLoggedIn"] = bool.TrueString;
                var nextPage = new TestingPage
                {
                    CurrentUser = current_user
                };
                await nav.PushAsync(nextPage);
            }
            else
            {
                Password = string.Empty;
            }

            //The original LogMeIn function, keeping it for now just in case

            /*
            //get the response by calling Validate() method in the LoginModel
            List<string> response = lm.Validate(email, password);

            string status = response[0];
            string current_user = response[1];

            if (status == "success")
            {
                InvalidLogin = false;
                Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;

                var nextPage = new TestingPage
                {
                    CurrentUser = current_user
                };
                await nav.PushAsync(nextPage);
            }
            else
                InvalidLogin = true; */
        } 
    }
}
