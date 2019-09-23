using System;
using System.Collections.Generic;
using Pastime.ViewModels;
using Xamarin.Forms;

namespace Pastime.Views
{
    public partial class CreateEventView : ContentPage
    {
        public CreateEventViewModel vm;
        public CreateEventView()
        {
            InitializeComponent();
            vm = new CreateEventViewModel();

            this.BindingContext = vm;


        }

        private async void OnTextChanged(object sender, EventArgs e)
        {

            await vm.GetPlacesPredictionsAsync();


        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}
