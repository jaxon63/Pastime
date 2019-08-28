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
        public EditAccountView()
        {
            InitializeComponent();          

            this.BindingContext = new EditAccountViewModel();   
        }

        //TODO : I think there has to be a way to do this in one method by passing the element tapped's ID

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
            PopupNavigation.Instance.PushAsync(new ChangeEmailPopup());

        }
        
    }
}
