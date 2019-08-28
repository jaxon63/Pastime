using System;
using System.ComponentModel;
using Xamarin.Forms;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using Pastime.Views;
using Pastime.Models;

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

        async void OnEditAccountButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditAccountView());
        }
      
        

       

        
    }
}
