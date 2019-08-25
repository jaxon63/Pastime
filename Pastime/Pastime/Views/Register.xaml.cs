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
            Entry name = this.FindByName<Entry>("Name");

            request.AddParameter("email", email.Text);
            request.AddParameter("password", password.Text);
            request.AddParameter("verify_password", verify_password.Text);
        }

        async void Registration(object sender, EventArgs args)
        {
            
        }

        async void OnBackButtonPressed(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
