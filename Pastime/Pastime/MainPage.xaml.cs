using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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

            /*Label email = this.FindByName<Label>("Email");
            Label password = this.FindByName<Label>("Password");
            Button button = this.FindByName<Button>("Button");

            button.Clicked += async (sender, args) =>
            {
                //temp credentials
                if (email.Text == "test@test.com" && password.Text == "password")
                {
                    await DisplayAlert("Message",
                        "Logged in!",
                        "OK");
                }
                else
                {
                    await DisplayAlert("Message",
                        "Incorrect details!",
                        "OK");
                }
            };
            */
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
            //temp credentials
            if (email.Text == "test@test.com" && password.Text == "password")
            {
                await DisplayAlert("Message",
                    "Logged in!",
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
