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

            MessagingCenter.Subscribe<EventsAttendingViewModel>(this, "leave_confirm", async (sender) =>
            {
                bool confirm = await DisplayAlert("Leave Event", "Are you sure you want to leave?", "Yes", "Cancel");
                if(confirm)
                {
                    MessagingCenter.Send<EventsAttendingView>(this, "confirmed_leave");
                }
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await vm.GetEventsAsync();
        }

    }
}
