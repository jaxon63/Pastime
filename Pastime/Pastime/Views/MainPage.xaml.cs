using System;
using System.ComponentModel;
using Xamarin.Forms;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using Pastime.ViewModels;

namespace Pastime
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    public partial class MainPage : ContentPage
    {
        private MainPageViewModel vm;
        public MainPage()
        {
            InitializeComponent();
            this.vm = new MainPageViewModel(Navigation);
            this.BindingContext = vm;
            Console.WriteLine(Application.Current.Properties["IsLoggedIn"]);
            Console.WriteLine(Application.Current.Properties["current_user"]);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await vm.GetEventsAsync();
        }





    }
}
