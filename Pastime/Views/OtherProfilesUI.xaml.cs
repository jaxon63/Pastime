﻿using Pastime.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pastime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OtherProfilesUI : ContentPage
    {
        public OtherProfilesUI()
        {
            InitializeComponent();

            //User object for testing
            //User user = new User("hellO@steven.com", "password1", "steveny1");

            //Set the binding context for Other User profiles and pass user information
            //this.BindingContext = new OtherProfilesViewModel(user.getUsername(), "I like cricket and other stuff I guess", "Member since: 2014", "Location: Hawthorn", "Favourite Sport: Cricket" );
        }
        
    }
}