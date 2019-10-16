using System;
using System.Collections.Generic;
using Pastime.Models;
using Pastime.Popups;
using Pastime.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pastime.Views
{
    public partial class EditAccountView : ContentPage
    {
        private EditAccountViewModel vm;
        public EditAccountView()
        {
            InitializeComponent();
            vm = new EditAccountViewModel();
            this.BindingContext = vm;

        }

        private void EditNamePopup(object o, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new ChangeNamePopup());
        }

        private void EditPasswordPopup(object o, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new ChangePasswordPopup());
        }

        private void EditEmailPopup(object o, EventArgs e)
        {
            ChangeEmailPopup newPopup = new ChangeEmailPopup();
            newPopup.BindingContext = this.vm;
            PopupNavigation.Instance.PushAsync(new ChangeEmailPopup());
        }
    }
}
