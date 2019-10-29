using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Pastime.Models;
using Pastime.Views;
using RestSharp;
using Xamarin.Forms;

namespace Pastime.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private string email = string.Empty;
        private string password = string.Empty;

        private int loginCode;
        private bool isBusy;

        private bool invalidLogin;
        private string loginErrMsg = "Incorrent email or password";

        private string feedback = string.Empty;

        //The navigation is passed from the view to the viewmodel
        private readonly INavigation nav;

        //The model to perform business logic
        private readonly LoginModel lm;

        public LoginPageViewModel(INavigation nav)
        {
            LoginCommand = new Command(async () => await LogMeIn());
            CreateAccountCommand = new Command(CreateAccountNavAsync);
            ResendCommand = new Command(ResendEmail);

            lm = new LoginModel();
            this.nav = nav;
        }

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (isBusy == value)
                    return;
                isBusy = value;
                OnPropertyChanged();
            }
        }

        public string Feedback
        {
            get => feedback;
            set
            {
                if (feedback == value)
                    return;
                feedback = value;
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
        public ICommand CreateAccountCommand { private set; get; }
        public ICommand ResendCommand { private set; get; }

        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CreateAccountNavAsync()
        {
            Application.Current.MainPage = new NavigationPage(new RegisterPage());
        }

        private void ResendEmail()
        {

            Feedback = "Email has been sent";
            string resend_api = "https://vietnguyen.me/pastime/resend.php";
            //create a client object
            var client = new RestClient(resend_api);
            var request = new RestRequest(Method.GET);
            request.AddParameter("email", Email);
            client.Execute(request);
        }

        private async Task LogMeIn()
        {
            IsBusy = true;
            LoginCode = lm.LogMeIn(email, password, out string current_user);
            switch (LoginCode)
            {
                case 1:
                    InvalidLogin = false;
                    Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;
                    Application.Current.MainPage = new MasterView();
                    IsBusy = false;
                    break;
                case 2:
                    InvalidLogin = true;

                    ConfirmEmail confirmEmail = new ConfirmEmail();
                    confirmEmail.BindingContext = this;

                    await nav.PushAsync(new NavigationPage(confirmEmail));
                    IsBusy = false;
                    break;
                case 0:
                    InvalidLogin = true;
                    Password = string.Empty;
                    IsBusy = false;
                    break;
                default:
                    IsBusy = false;
                    break;


            }
        }
    }
}