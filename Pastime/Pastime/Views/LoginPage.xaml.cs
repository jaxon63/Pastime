using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Pastime.ViewModels;
using RestSharp;
using Xamarin.Forms;

namespace Pastime.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginPageViewModel(this.Navigation);
            NavigationPage.SetHasBackButton(this, false);
        }
    }
}
