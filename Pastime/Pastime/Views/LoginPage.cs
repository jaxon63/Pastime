using System;

using Xamarin.Forms;

namespace Pastime.Views
{
    public class LoginPage : ContentPage
    {
        public LoginPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

