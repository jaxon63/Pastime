using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Newtonsoft.Json;
using RestSharp;

namespace Pastime
{
    public partial class TestingPage : ContentPage
    {
        public TestingPage()
        {
            InitializeComponent();
            //NavigationPage.SetHasBackButton(this, false);

        }

        //get-set property CurrentUser
        public string CurrentUser { get; set; }

        void CheckCurrentUser(object sender, EventArgs args)
        {
            Label message = this.FindByName<Label>("Message");
            message.Text = CurrentUser;
        }

        async void OnBackButtonPressed(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
