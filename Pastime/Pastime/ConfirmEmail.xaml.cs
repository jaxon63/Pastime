using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Xamarin.Forms;

namespace Pastime
{
    public partial class ConfirmEmail : ContentPage
    {
        public ConfirmEmail()
        {
            InitializeComponent();
        }

        public string Email { get; set; }

        void ResendEmail(object sender, EventArgs args)
        {
            string resend_api = "https://vietnguyen.me/pastime/resend.php";
            //create a client object
            var client = new RestClient(resend_api);
            var request = new RestRequest(Method.GET);
            request.AddParameter("email", Email);
            client.Execute(request);
        }

        async void OnBackButtonPressed(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
