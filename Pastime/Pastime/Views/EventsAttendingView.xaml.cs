using System;
using System.Collections.Generic;
using Pastime.ViewModels;
using Xamarin.Forms;

namespace Pastime.Views
{
    public partial class EventsAttendingView : ContentPage
    {
        EventsAttendingViewModel vm;
        public EventsAttendingView()
        {
            InitializeComponent();
            vm = new EventsAttendingViewModel();
            this.BindingContext = vm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await vm.GetEventsAsync();
        }
    }
}
