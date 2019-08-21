using System;
using System.ComponentModel;
using Xamarin.Forms;
using Newtonsoft.Json;
using RestSharp;

namespace Pastime
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Validate();
        }

        //This method to validate the user login credentials
        public string Validate()
        {
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

            //return the status
            return status;
        }

        //this method will be called when user clicks the button
        async void LogMeIn(object sender, EventArgs args)
        {
            //get the response by calling Validate() method
            var response = Validate();

            //depends on what the response is, assign value for grant_access
            string grant_access;

            if (response == "success")
            {
                grant_access = "Logged in!";
            }
            else
            {
                grant_access = "Incorrect details!";
            }

            //prints message to the user's screen
            //anyway, this function is mainly focusing the logic of Login page
            //it's definitely not a completed one.
            await DisplayAlert("Message",
                grant_access,
                "OK");
        }
    }
}
