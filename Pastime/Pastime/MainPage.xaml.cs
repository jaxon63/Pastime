using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
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
            var result = String.Empty;

            Entry email = this.FindByName<Entry>("Email");
            Entry password = this.FindByName<Entry>("Password");

            string login_api = "https://vietnguyen.me/pastime/login.php?email=" + email.Text + "&password=" + password.Text;

            var client = new RestClient(login_api);
            var request = new RestRequest(Method.GET);
            request.AddParameter("email", email);
            request.AddParameter("password", password);

            var response = client.Execute<LoginJSON>(request).Content;

            return response;
        }

        async void LogMeIn(object sender, EventArgs args)
        {
            var response = Validate();

            await DisplayAlert("Message",
                response,
                "OK");

            /*
            if (response == "success")
            {
                await DisplayAlert("Message",
                    "ok",
                    "OK");
            }
            else
            {
                await DisplayAlert("Message",
                    "Incorrect details!",
                    "OK");
            }
            */
        }
    }
}
