using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net;
using Newtonsoft.Json;

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
        }

        async void LogMeIn(object sender, EventArgs args)
        {
            /*
            await DisplayAlert("Message",
                "Logged in!",
                "OK");
            */

            Entry email = this.FindByName<Entry>("Email");
            Entry password = this.FindByName<Entry>("Password");

            string login_api = "https://vietnguyen.me/pastime/login.php?email=" + email + "&password=" + password;
            //WebClient client = new WebClient();
            //string response = client.DownloadString(login_api);
            //LoginJSON json = JsonConvert.DeserializeObject<LoginJSON>(response);

            //temp credentials
            if (email.Text == "test@test.com" && password.Text == "password")
            {
                await DisplayAlert("Message",
                    login_api,
                    "OK");
            }
            else
            {
                await DisplayAlert("Message",
                    "Incorrect details!",
                    "OK");
            }
        }
    }
}
