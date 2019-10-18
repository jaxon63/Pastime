using System;
using Pastime.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Pastime.Views.CreateEventViewModal;
using Pastime.ViewModels;

namespace Pastime
{
    public partial class Application : Xamarin.Forms.Application
    {
        public Application()
        {

            InitializeComponent();

            bool isLoggedIn = Current.Properties.ContainsKey("IsLoggedIn") ? Convert.ToBoolean(Current.Properties["IsLoggedIn"]) : false;
            Console.WriteLine(Application.Current.Properties["current_user"]);
            Console.WriteLine(Application.Current.Properties["IsLoggedIn"]);
            if (!isLoggedIn)
            {
                //Load login page is user is not logged in
                MainPage = new LoginPage();
            }
            else
            {
                //Load mainpage if the user has previously logged in
                MainPage = new MasterView();
            }
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
