
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Pastime.Droid
{
    [Activity(Label = "Login", MainLauncher = true)]
    public class Login : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            TextView txtUserName = FindViewById<TextView>(Resource.Id.txtUserName);
            TextView txtPassword = FindViewById<TextView>(Resource.Id.txtPassword);
            Button Send_Button = FindViewById<Button>(Resource.Id.btnLogin);
            Send_Button.Click += delegate {
                //temp credentials
                if (txtUserName.Text == "admin" && txtPassword.Text == "password")
                {
                    Toast.MakeText(this, "Logged in!", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "Incorrect credentials!", ToastLength.Long).Show();
                }
            };
        }
    }
}
