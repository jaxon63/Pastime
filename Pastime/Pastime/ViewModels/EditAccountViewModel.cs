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
        private string email;
        private string sendEmail; //this will be the email sent to the model to update the current email of the user
        private string password;
        private string sendPassword;
        private string emailErrMsg = string.Empty;
        private string displaySuccessMessage = string.Empty;

        public EditAccountViewModel()
        {
            model = new UserModel();
            user = model.User;
            SaveNewEmailCommand = new Command(SaveEmail);
        }

        public string DisplayInitial
        {
            get => user.Username[0].ToString();
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

        public string SendPassword
        {
            get => sendPassword;
            set
            {
                if (sendPassword == value)
                    return;
                sendPassword = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void SaveEmail()
        {
            if(!model.SaveNewEmail(SendEmail, SendPassword, out string errMsg))
            {
                EmailErrMsg = errMsg;
                Password = string.Empty;
            }
            else
            {
                EmailErrMsg = string.Empty;
                SendEmail = string.Empty;
                SendPassword = string.Empty;
                Email = user.Email;
                DisplaySuccessMessage = "Email changed successfully!";
                MessagingCenter.Send<EditAccountViewModel>(this, "updated");
            }

        }

        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
