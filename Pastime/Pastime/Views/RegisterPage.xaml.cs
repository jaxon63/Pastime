using System;
using System.ComponentModel;
using Xamarin.Forms;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using Pastime.ViewModels;

namespace Pastime
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            this.BindingContext = new RegisterPageViewModel();
        }
    }
}
