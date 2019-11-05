using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Pastime.Models;
using Xamarin.Forms;

namespace Pastime.ViewModels
{
    public class EditAccountViewModel : INotifyPropertyChanged
    {
        private User user;
        private UserModel model;
        private string name;
        private string sendUserName;
        private string email;
        private string sendEmail; //this will be the email sent to the model to update the current email of the user
        private string password;
        private string sendCurrentPassword;
        private string sendNewPassword;
        private string sendCPassword;
        private string emailErrMsg = string.Empty;
        private string usernameErrMsg = string.Empty;
        private string passwordErrmsg = string.Empty;
        private string displaySuccessMessage = string.Empty;

        public EditAccountViewModel()
        {
            model = new UserModel();
            user = model.User;
            SaveNewEmailCommand = new Command(SaveEmail);
            SaveNewPasswordCommand = new Command(SavePassword);
            SaveNewUsernameCommand = new Command(SaveUsername);
        }


        public User User
        {
            get => user;
            set
            {
                if (user == value)
                    return;
                user = value;
                OnPropertyChanged();
            }
        }

        public string DisplayInitial
        {
            get => user.Username[0].ToString().ToUpper();
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

        public string SendEmail
        {
            get => sendEmail;
            set
            {
                if (sendEmail == value)
                    return;
                sendEmail = value;
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

        public string SendCurrentPassword
        {
            get => sendCurrentPassword;
            set
            {
                if (sendCurrentPassword == value)
                    return;
                sendCurrentPassword = value;
                OnPropertyChanged();
            }
        }

        public string SendNewPassword
        {
            get => sendNewPassword;
            set
            {
                if (sendNewPassword == value)
                    return;
                sendNewPassword = value;
                OnPropertyChanged();
            }
        }
        public string SendCPassword
        {
            get => sendCPassword;
            set
            {
                if (sendCPassword == value)
                    return;
                sendCPassword = value;
                OnPropertyChanged();
            }
        }

        public string SendUsername
        {
            get => sendUserName;
            set
            {
                if (sendUserName == value)
                    return;
                sendUserName = value;
                OnPropertyChanged();
            }

        }



        public string EmailErrMsg
        {
            get => emailErrMsg;
            set
            {
                if (emailErrMsg == value)
                    return;
                emailErrMsg = value;
                OnPropertyChanged();
            }
        }

        public string UsernameErrMsg
        {
            get => usernameErrMsg;
            set
            {
                if (usernameErrMsg == value)
                    return;
                usernameErrMsg = value;
                OnPropertyChanged();
            }

        }

        public string PasswordErrMsg
        {
            get => passwordErrmsg;
            set
            {
                if (passwordErrmsg == value)
                    return;
                passwordErrmsg = value;
                OnPropertyChanged();
            }

        }


        public string DisplaySuccessMessage
        {
            get => displaySuccessMessage;
            set
            {
                if (displaySuccessMessage == value)
                    return;
                displaySuccessMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveNewEmailCommand { private set; get; }
        public ICommand SaveNewPasswordCommand { private set; get; }
        public ICommand SaveNewUsernameCommand { private set; get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SaveEmail()
        {
            if (!model.SaveNewEmail(SendEmail, SendCurrentPassword, out string errMsg))
            {
                EmailErrMsg = errMsg;
                SendCurrentPassword = string.Empty;
            }
            else
            {
                EmailErrMsg = string.Empty;
                SendEmail = string.Empty;
                SendCurrentPassword = string.Empty;
                Email = User.Email;
                DisplaySuccessMessage = "Email changed successfully!";
                MessagingCenter.Send<EditAccountViewModel>(this, "updated");
            }
        }

        private void SavePassword()
        {
            if (!model.SaveNewPassword(SendCurrentPassword, SendNewPassword, SendCPassword, out string errMsg))
            {
                SendCurrentPassword = string.Empty;
                SendNewPassword = string.Empty;
                SendCPassword = string.Empty;
                PasswordErrMsg = errMsg;
            }
            else
            {
                PasswordErrMsg = string.Empty;
                SendCurrentPassword = string.Empty;
                SendNewPassword = string.Empty;
                SendCPassword = string.Empty;
                DisplaySuccessMessage = "Password changed successfully!";
                MessagingCenter.Send<EditAccountViewModel>(this, "updated");
            }
        }

        private void SaveUsername()
        {
            if (!model.SaveNewUsername(SendUsername, SendCurrentPassword, out string errMsg))
            {
                UsernameErrMsg = errMsg;
                SendCurrentPassword = string.Empty;
            }
            else
            {
                UsernameErrMsg = string.Empty;
                SendCurrentPassword = string.Empty;
                SendUsername = string.Empty;
                Name = User.Username;
                DisplaySuccessMessage = "Username changed successfully!";
                MessagingCenter.Send<EditAccountViewModel>(this, "updated");
            }
        }

        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
