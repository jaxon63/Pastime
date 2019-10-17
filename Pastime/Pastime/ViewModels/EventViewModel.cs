using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pastime.Models;
using Pastime.Views;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pastime.ViewModels
{
    public class EventViewModel : INotifyPropertyChanged
    {
        private INavigation nav;
        private Event selectedEvent;
        private int guestsAttending;

        public EventViewModel(INavigation nav, Event e)
        {
            this.nav = nav;

            this.selectedEvent = e;

            //Commands
            BackCommand = new Command(NavigateBack);
            JoinCommand = new Command( JoinEventAsync);
        }

        public Event SelectedEvent
        {
            get => selectedEvent;
            set
            {
                if (selectedEvent == value)
                    return;
                selectedEvent = value;
                OnPropertyChanged();
            }
        }

        public int GuestsAttending
        {
            get
            {
                return selectedEvent.getGuestCount();
            }
        }

        //Methods
        private void NavigateBack()
        {
            Application.Current.MainPage = new MasterView();
        }
        private void JoinEventAsync()
        {
            MessagingCenter.Send<EventViewModel>(this, "openJoinDialog");
            MessagingCenter.Subscribe<EventView>(this, "join", (sender) =>
            {
                Console.WriteLine("TODO: Add current_user to this event");
            });
        }

        public ICommand BackCommand { private set; get; }
        public ICommand JoinCommand { private set; get; }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
