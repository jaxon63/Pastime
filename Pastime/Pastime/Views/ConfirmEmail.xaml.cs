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
       

        async void OnBackButtonPressed(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

   
    }
}
