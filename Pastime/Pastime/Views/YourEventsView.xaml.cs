using System;
using System.Collections.Generic;
using Pastime.ViewModels;
using Xamarin.Forms;

namespace Pastime.Views
{
    public partial class YourEventsView : ContentPage
    {
        private HostEventsViewModel vm;
        public YourEventsView()
        {
            InitializeComponent();
            this.vm = new HostEventsViewModel();
            this.BindingContext = vm;

            MessagingCenter.Subscribe<HostEventsViewModel>(this, "cancel_message", async (sender) =>
            {
                bool cancel = await DisplayAlert("Cancel Event", "Are you sure you want to cancel this event?", "Yes", "Cancel");
                if (cancel)
                {
                    MessagingCenter.Send<YourEventsView>(this, "cancel");
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
