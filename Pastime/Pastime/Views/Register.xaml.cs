using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Newtonsoft.Json;
using RestSharp;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace Pastime
{

    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
            //NavigationPage.SetHasBackButton(this, false);
            Validate();
        }

        public bool EmailValidate(Entry email)
        {
            var emailText = email.Text;

            Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if (!string.IsNullOrWhiteSpace(emailText) && emailRegex.IsMatch(emailText))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int PasswordValidate(Entry password, Entry verify_password)
        {
            var passwordText = password.Text;
            var verify_Text = verify_password.Text;

            if (passwordText.Length >= 8 && passwordText == verify_Text)
            {
                return 0;
            }
            else if (passwordText.Length < 8)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public List<string> Validate()
        {
            List<string> results = new List<string>();

            Entry email = this.FindByName<Entry>("Email");
            Entry password = this.FindByName<Entry>("Password");
            Entry verify_password = this.FindByName<Entry>("Verify_Password");
            Entry username = this.FindByName<Entry>("Name");

            if (EmailValidate(email) && PasswordValidate(password, verify_password) == 0)
            {
                //Hopefully the registration API is register.php, otherwise I'll need to change this
                string register_api = "https://vietnguyen.me/pastime/register.php";

                //creating the new client opject
                var client = new RestClient(register_api);

                //creating the request, as per Viet's comment, I'm using the POST method
                var request = new RestRequest(Method.POST);

                //adding all the parameters for the user (all the information that's input when they're registering)
                request.AddParameter("username", username.Text);
                request.AddParameter("email", email.Text);
                request.AddParameter("password", password.Text);
                request.AddParameter("verify_password", verify_password.Text);

                var response = client.Execute<RegisterJSON>(request).Content;

                var status = JsonConvert.DeserializeObject<RegisterJSON>(response).register[0].status;
                results.Add(status);

                var reason = JsonConvert.DeserializeObject<RegisterJSON>(response).register[0].reason;
                results.Add(reason);

                return results;
            }
            else
            {
                results.Add("failed");
                
                if (!EmailValidate(email))
                {
                    results.Add("Email is wrong");
                }
                else if ((PasswordValidate(password, verify_password) == 1)||(PasswordValidate(password, verify_password) == 2))
                {
                    results.Add("Password is wrong");
                }
                else
                {
                    results.Add("Email and Password are wrong");
                }

                return results;
            }

            
        }

        async void Registration(object sender, EventArgs args)
        {
            //get the response by calling Validate() method
            List<string> response = Validate();

            string status = response[0];
            string reason = response[1];

            if (status == "success")
            {
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert(status, reason, "OK");
            }
        }

        async void OnBackButtonPressed(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
