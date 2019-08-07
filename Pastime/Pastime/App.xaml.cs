﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pastime
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //CHange this to test specific pages
            MainPage = new OtherProfilesUI();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
