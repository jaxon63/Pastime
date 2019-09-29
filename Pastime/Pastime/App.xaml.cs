using System;
using Pastime.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Pastime.Views.CreateEventViewModal;

namespace Pastime
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new CreateEventViewModalName());
            

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
