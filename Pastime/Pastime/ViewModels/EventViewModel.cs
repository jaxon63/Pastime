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
        private Event displayEvent;
        private int guestsAttending;
        private string current_user;
        private EventModel model;
        private bool hasJoined;
        private bool isHost;
        private bool isBusy;

        public EventViewModel(INavigation nav, Event e)
        {
            this.nav = nav;

            model = new EventModel();

            current_user = Application.Current.Properties["current_user"].ToString();

            this.displayEvent = e;
            foreach(string guest in displayEvent.Guests)
            {
                if(current_user == guest)
                {
                    Console.WriteLine(guest);
                    HasJoined = true;
                }
            }

            Console.WriteLine(HasJoined);

            if (displayEvent.Host == current_user)
            {
                IsHost = true;
            }

            //Commands
            BackCommand = new Command(NavigateBack);
            JoinCommand = new Command(async () => await JoinEventAsync());
            LeaveCommand = new Command(LeaveEvent);
            CancelCommand = new Command(CancelEvent);
        }

        public Event DisplayEvent
        {
            get => displayEvent;
            set
            {
                if (displayEvent == value)
                    return;
                displayEvent = value;
                OnPropertyChanged();
            }
        }

        public int GuestsAttending
        {
            get
            {
                return displayEvent.getGuestCount();
            }
        }

        public bool HasJoined
        {
            get => hasJoined;
            set
            {
                if (hasJoined == value)
                    return;
                hasJoined = value;
                OnPropertyChanged();
                OnPropertyChanged("JoinButtonEnabled");
                OnPropertyChanged("LeaveButtonEnabled");
            }
        }

        public bool IsHost
        {
            get => isHost;
            set
            {
                if (isHost == value)
                    return;
                isHost = value;
                OnPropertyChanged();
            }
        }

        public bool JoinButtonEnabled
        {
            get { return (!HasJoined && !IsHost); }
        }

        public bool LeaveButtonEnabled
        {
            get { return HasJoined && !IsHost; }
        }

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (isBusy == value)
                    return;
                isBusy = value;
                OnPropertyChanged();

            }
        }

        //Methods
        private void NavigateBack()
        {
            Application.Current.MainPage = new MasterView();
        }

        //TODO: doesn't update the attendee count until the page is reloaded
        private async Task JoinEventAsync()
        {
            IsBusy = true;
            if (model.JoinEvent(current_user, DisplayEvent.EventId))
            {
                HasJoined = true;
            }
            else
            {
                HasJoined = false;
            }
            IsBusy = false;
        }

        private void LeaveEvent()
        {
            IsBusy = true;
            Console.WriteLine("Leaving");
           // HasJoined = false;
            IsBusy = false;
        }

        private void CancelEvent()
        {
            Console.WriteLine("Cancel event");
        }

        public ICommand BackCommand { private set; get; }
        public ICommand JoinCommand { private set; get; }
        public ICommand LeaveCommand { private set; get; }
        public ICommand CancelCommand { private set; get; }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
