using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Newtonsoft.Json;
using RestSharp;

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

        public List<string> Validate()
        {
            Entry email = this.FindByName<Entry>("Email");
            Entry password = this.FindByName<Entry>("Password");
            Entry verify_password = this.FindByName<Entry>("Verify_Password");
            Entry username = this.FindByName<Entry>("Name");
            
            //Hopefully the registration API is register.php, otherwise I'll need to change this
            string register_api = "https://vietnguyen.me/pastime/register.php"
            
            //creating the new client opject
            var client = new RestClient(register_api);
            
            //creating the request, as per Viet's comment, I'm using the POST method
            var reqeust = new RestRequest(Method.POST);
            
            //adding all the parameters for the user (all the information that's input when they're registering)
            request.AddParameter("username", username.Text);
            request.AddParameter("email", email.Text);
            request.AddParameter("password", password.Text);
            request.AddParameter("verify_password", verify_password.Text);
            
            var response = client.Execute<RegisterJSON>(request).Content;
            
            var status = JsonConvert.DeserializeObject<RegisterJSON>(response).register[0].status;
            results.Add(Status);
            
            var reason = JsonConvert.DeserializeObject<RegisterJSON>(response).register[0].reason;
            result.Add(reason);
            
            return result;
        }

        async void Registration(object sender, EventArgs args)
        {
            //get the response by calling Validate() method
            List<string> response = Validate();

            string status = response[0];
            string reason = response[1];

            if (status == "success")
            {
                await Navigation.PushAsync(new MainPage);
            }
            else
            {
                await DisplayAlert(reason);
            }
        }

        async void OnBackButtonPressed(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
