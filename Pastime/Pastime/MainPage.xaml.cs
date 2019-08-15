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

        public string Validate()
        {
            Entry email = this.FindByName<Entry>("Email");
            Entry password = this.FindByName<Entry>("Password");

            string login_api = "https://vietnguyen.me/pastime/login.php";

            var client = new RestClient(login_api);
            var request = new RestRequest(Method.GET);
            request.AddParameter("email", email.Text);
            request.AddParameter("password", password.Text);

            var response = client.Execute<LoginJSON>(request).Content;
            var status = JsonConvert.DeserializeObject<LoginJSON>(response).login[0].status;

            return status;
        }

        async void LogMeIn(object sender, EventArgs args)
        {
            var response = Validate();

            string grant_access;

            if (response == "success")
            {
                grant_access = "Logged in!";
            }
            else
            {
                grant_access = "Incorrect details!";
            }

            await DisplayAlert("Message",
                grant_access,
                "OK");
        }
    }
}
