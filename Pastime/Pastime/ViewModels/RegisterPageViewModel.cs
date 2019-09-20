using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Pastime.Models;
using Xamarin.Forms;

namespace Pastime.ViewModels
{
    public class RegisterPageViewModel : INotifyPropertyChanged
    {
        private string email = string.Empty;
        private bool invalidEmail;
        private string invalidEmailMsg = string.Empty;

        private string username = string.Empty;
        private bool invalidUsername;
        private string invalidUsernameMsg = string.Empty;

        private string password = string.Empty;
        private bool invalidPassword;
        private string invalidPasswordMsg = string.Empty;

        private string cPassword = string.Empty;
        private bool invalidCPassword;
        private string invalidCPasswordMsg = "Passwords do not match";

        private string submitErrMsg = string.Empty;

        private RegisterModel model;

        public RegisterPageViewModel()
        {
            model = new RegisterModel();

            SubmitCommand = new Command(ValidateInput);
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

        public bool InvalidEmail
        {
            get => invalidEmail;
            set
            {
                if (invalidEmail == value)
                    return;
                invalidEmail = value;
                OnPropertyChanged();
            }
        }

        public string InvalidEmailMsg
        {
            get => invalidEmailMsg;
            set
            {
                if (invalidEmailMsg == value)
                    return;
                invalidEmailMsg = value;
                OnPropertyChanged();
            }
        }



        public string Username
        {
            get => username;
            set
            {
                if (username == value)
                    return;
                username = value;
                OnPropertyChanged();
            }
        }

        public bool InvalidUsername
        {
            get => invalidUsername;
            set
            {
                if (invalidUsername == value)
                    return;
                invalidUsername = value;
                OnPropertyChanged();
            }
        }

        public string InvalidUsernameMsg
        {
            get => invalidUsernameMsg;
            set
            {
                if (invalidUsernameMsg == value)
                    return;
                invalidUsernameMsg = value;
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

        public bool InvalidPassword
        {
            get => invalidPassword;
            set
            {
                if (invalidPassword == value)
                    return;
                invalidPassword = value;
                OnPropertyChanged();
            }
        }

        public string InvalidPasswordMsg
        {
            get => invalidPasswordMsg;
            set
            {
                if (invalidPasswordMsg == value)
                    return;
                invalidPasswordMsg = value;
                OnPropertyChanged();
            }
        }


        public string CPassword
        {
            get => cPassword;
            set
            {
                if (cPassword == value)
                    return;
                cPassword = value;
                OnPropertyChanged();
            }
        }

        public bool InvalidCPassword
        {
            get => invalidCPassword;
            set
            {
                if (invalidCPassword == value)
                    return;
                invalidCPassword = value;
                OnPropertyChanged();
            }
        }

        public string InvalidCPasswordMsg
        {
            get => invalidCPasswordMsg;
            set
            {
                if (invalidCPasswordMsg == value)
                    return;
                invalidCPasswordMsg = value;
                OnPropertyChanged();
            }
        }

        public string SubmitErrMsg
        {
            get => submitErrMsg;
            set
            {
                if (submitErrMsg == value)
                    return;
                submitErrMsg = value;
                OnPropertyChanged();
            }
        }

        //Client side validation from the RegisterModel
        //This function updates the UI with appropriate error messages based on their input
        private void ValidateInput()
        {
            InvalidEmail = !model.ValidateEmail(email, out string emailErrMsg);
            InvalidEmailMsg = emailErrMsg;

            InvalidUsername = !model.ValidateUsername(username, out string usernameErrMsg);
            InvalidUsernameMsg = usernameErrMsg;

            InvalidPassword = !model.ValidatePassword(password, out string passwordErrMsg);
            InvalidPasswordMsg = passwordErrMsg;

            InvalidCPassword = !model.ValidateCPassword(password, cPassword);

            if (!invalidEmail && !invalidUsername && !invalidPassword && !invalidCPassword)
            {
                Submit();
            }
        }


        private void Submit()
        {
            var status = model.SubmitRegister(email, username, password, cPassword);
            if(status == "success")
            {
                Console.WriteLine("Account created");
                SubmitErrMsg = string.Empty;
                //TODO: navigate to main page
            } else
            {
                SubmitErrMsg = status;
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SubmitCommand { private set; get; }

        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
