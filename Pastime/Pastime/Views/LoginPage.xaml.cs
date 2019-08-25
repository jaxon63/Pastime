using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using Xamarin.Forms;

namespace Pastime.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            Validate();
        }

        //This method to validate the user login credentials
        public List<string> Validate()
        {
            List<string> results = new List<string>();

            //Get the user's inputs
            //Check your related input fields in the MainPage.xaml file
            Entry email = this.FindByName<Entry>("Email");
            Entry password = this.FindByName<Entry>("Password");

            //declare your related api's address here
            //mine is Login, yours maybe Register etc.
            string login_api = "https://vietnguyen.me/pastime/login.php";

            //create a client object
            var client = new RestClient(login_api);

            //create a request
            //for Login I used GET method, but for Register or Profile update
            //you'll need to use POST method
            var request = new RestRequest(Method.GET);

            //add the parameters to your APIs
            //note: profile update API needs to have action parameter
            //please check the with me for correct parameter declarations to use the APIs.
            request.AddParameter("email", email.Text);
            request.AddParameter("password", password.Text);

            //get the JSON response
            var response = client.Execute<LoginJSON>(request).Content;
            //my JSON response only have 1 JSON object
            //that's why I used .login[0] (index 0 object)
            var status = JsonConvert.DeserializeObject<LoginJSON>(response).login[0].status;
            results.Add(status);

            var current_user = JsonConvert.DeserializeObject<LoginJSON>(response).login[0].current_user;
            results.Add(current_user);

            //return the status
            return results;
        }

        //this method will be called when user clicks the button
        async void LogMeIn(object sender, EventArgs args)
        {
            //get the response by calling Validate() method
            List<string> response = Validate();

            string status = response[0];
            string current_user = response[1];

            if (status == "success")
            {
                var nextPage = new TestingPage
                {
                    CurrentUser = current_user
                };
                await Navigation.PushAsync(nextPage);
            }
            else
            {
                await DisplayAlert("Message", "Incorrect details!", "OK");
            }
        }
    }
}
