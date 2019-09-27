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

        private int loginCode;
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

        public int LoginCode
        {
            get => loginCode;
            set
            {
                if (loginCode == value)
                    return;
                loginCode = value;
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
            LoginCode = lm.LogMeIn(email, password, out string current_user);

            switch (LoginCode)
            {
                case 1:
                    Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;
                    var nextPage = new TestingPage
                    {
                        CurrentUser = current_user
                    };
                    await nav.PushAsync(nextPage);
                    break;
                case 2:
                    var resendEmail = new ConfirmEmail
                    {
                        Email = email
                    };
                    await nav.PushAsync(resendEmail);
                    break;
                case 0:
                    Password = string.Empty;
                    break;
                default:
                    break;
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
