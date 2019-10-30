using Pastime.Models;
using Pastime.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pastime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventView : ContentPage
    {
        private EventViewModel viewModel;
        public EventView(Event e)
        {
            InitializeComponent();

            viewModel = new EventViewModel(Navigation, e);
            this.BindingContext = viewModel;

            MessagingCenter.Subscribe<EventViewModel>(this, "join_confirm", async (sender) =>
            {
                bool confirm = await DisplayAlert("Join Event", "Would you like to attend this event?", "Yes", "Cancel");
                if (confirm)
                {
                    MessagingCenter.Send<EventView>(this, "confirmed_join");
                }
            });

            MessagingCenter.Subscribe<EventViewModel>(this, "leave_confirm", async (sender) =>
            {
                bool confirm = await DisplayAlert("Leave Event", "Are you sure you want to leave?", "Yes", "Cancel");
                if (confirm)
                {
                    MessagingCenter.Send<EventView>(this, "confirmed_leave");
                }
            });

            MessagingCenter.Subscribe<EventViewModel>(this, "cancel_message", async (sender) =>
            {
                bool confirm = await DisplayAlert("Cancel Event", "Are you sure you want to cancel?", "Yes", "Cancel");
                if (confirm)
                {
                    MessagingCenter.Send<EventView>(this, "confirmed_cancel");
                }
            });

            MessagingCenter.Subscribe<EventViewModel>(this, "navigate_back", async (sender) =>
            {
                await Navigation.PopAsync();
            });

        }
        public EventViewModel ViewModel
        {
            get => viewModel;
        }
    }
}