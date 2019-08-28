﻿using Pastime.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pastime.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ChangePasswordPopup()
        {
            InitializeComponent();
            this.BindingContext = new EditAccountViewModel();

        }

        private async void OnCloseButtonClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            
        }
    }
}