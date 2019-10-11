using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pastime.Popups;
using Pastime.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pastime.Views
{
    public partial class CreateEventView : ContentPage
    {
        private LocationPopup locationPopup;
        public CreateEventViewModel vm;
        public CreateEventView()
        {
            InitializeComponent();

            vm = new CreateEventViewModel();
            this.BindingContext = vm;

            //Create an instance of locationpopup and set the binding context
            //If this happens in the click listener, an exception will be thrown the second time the location button is clicked
            locationPopup = new LocationPopup();
            locationPopup.BindingContext = vm;
        }

        private async void OnTextChanged(object sender, EventArgs e)
        {
            await vm.GetPlacesPredictionsAsync();
        }


       
        private async void LocationPopupNavAsync(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(locationPopup);
        }
    }
}
