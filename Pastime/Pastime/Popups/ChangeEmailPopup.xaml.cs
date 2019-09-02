using Pastime.Models;
using Pastime.ViewModels;
using Pastime.Views;
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
    public partial class ChangeEmailPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        User user;
        public ChangeEmailPopup()
        {
            InitializeComponent();
             
            this.BindingContext = new EditAccountViewModel();


            //Subscribe to messages sent from the view model
            //checks if the password is valid, updates if so
            MessagingCenter.Subscribe<EditAccountViewModel>(this, "updated", async (obj) =>
            {
                await PopupNavigation.Instance.PopAsync();

            });

            //displays an error message in the UI if the password is incorrect
            MessagingCenter.Subscribe<EditAccountViewModel>(this, "invalid password", (obj) =>
            {
                passwordErrorMsg.Text = "Password is incorrect";
            });
            MessagingCenter.Subscribe<EditAccountViewModel>(this, "invalid email", (obj) =>
            {
                emailErrorMsg.Text = "Email is invalid";
            });
        }

      

        private async void OnCloseButtonClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

       


    }
}