using System;
using System.ComponentModel;
using Xamarin.Forms;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using Pastime.ViewModels;

namespace Pastime
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            this.BindingContext = new RegisterPageViewModel();
        }

        //The original create user code, just in case
        /*
        public List<string> CreateUser()
        {
            List<string> results = new List<string>();

            //Get the user's inputs
            //Check your related input fields in the MainPage.xaml file
            Entry email = this.FindByName<Entry>("Email");
            Entry username = this.FindByName<Entry>("Username");
            Entry password = this.FindByName<Entry>("Password");
            Entry verify_password = this.FindByName<Entry>("VerifyPassword");

            string register_api = "https://vietnguyen.me/pastime/register.php";

            //create a client object
            var client = new RestClient(register_api);

            //create a request
            var request = new RestRequest(Method.GET);

            //add the parameters to APIs
            request.AddParameter("email", email.Text);
            request.AddParameter("username", username.Text);
            request.AddParameter("password", password.Text);
            request.AddParameter("verify_password", verify_password.Text);

            //get the JSON response
            var response = client.Execute<RegisterJSON>(request).Content;
            var status = JsonConvert.DeserializeObject<RegisterJSON>(response).register[0].status;
            results.Add(status);

            if (status == "failed")
            {
                var reason = JsonConvert.DeserializeObject<RegisterJSON>(response).register[0].reason;
                results.Add(reason);
            }

            //return the status
            return results;
        }

        async void SubmitRegister(object sender, EventArgs args)
        {
            //get the response by calling Validate() method
            List<string> response = CreateUser();

            string status = response[0];
            if (status == "success")
            {
                await DisplayAlert("Message", "Created!", "OK");
            }
            else
            {
                string reason = response[1];
                await DisplayAlert("Message", reason, "OK");
            }
        }
        */
    }
}
