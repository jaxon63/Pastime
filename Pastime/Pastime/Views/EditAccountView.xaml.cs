using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            Label label = (Label)FindByName("SuccessMessage");
            label.Opacity = 0;

            MessagingCenter.Subscribe<EditAccountViewModel>(this, "updated", async (sender) =>
            {
                var taskAnimation = label.FadeTo(1, 3000);
                var taskDelay = Task.Delay(3000);

                await Task.WhenAll(taskAnimation, taskDelay);
                await label.FadeTo(0, 1000);

                               
                

            });

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
            PopupNavigation.Instance.PushAsync(newPopup);
        }
    }
}
