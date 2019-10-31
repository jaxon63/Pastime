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
        private int numOfGuests;
        private string current_user;
        private EventModel model;
        private bool hasJoined;
        private bool isHost;
        private bool isBusy;
        private bool isVisible = true;


        public EventViewModel(INavigation nav, Event e)
        {
            this.nav = nav;

            model = new EventModel();

            current_user = Application.Current.Properties["current_user"].ToString();

            this.displayEvent = e;
            foreach (string guest in displayEvent.Guests)
            {
                if (current_user == guest)
                {
                    Console.WriteLine(guest);
                    HasJoined = true;
                }
            }

            //Initialise initial value
            //Will update this in the view when user successfully joins
            numOfGuests = e.NumOfGuests;

            if (displayEvent.Host == current_user)
            {
                IsHost = true;
            }

            //Commands
            BackCommand = new Command(NavigateBack);
            JoinCommand = new Command(JoinEvent);
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

        public int NumOfGuests
        {
            get => numOfGuests;
            set
            {
                if (numOfGuests == value)
                    return;
                numOfGuests = value;
                OnPropertyChanged();
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
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                if (isVisible == value)
                    return;
                isVisible = value;
                OnPropertyChanged();

            }
        }


        //Methods
        private void NavigateBack()
        {
            MessagingCenter.Send<EventViewModel>(this, "navigate_back");
        }

        //TODO: doesn't update the attendee count until the page is reloaded
        private void JoinEvent()
        {
            MessagingCenter.Send<EventViewModel>(this, "join_confirm");
            MessagingCenter.Subscribe<EventView>(this, "confirmed_join", (sender) =>
            {
                if (model.JoinEvent(current_user, DisplayEvent.EventId))
                {
                    NumOfGuests++;
                    HasJoined = true;
                    MessagingCenter.Send<EventViewModel>(this, "update_main_page");
                }
                else
                {
                    HasJoined = false;
                }

            });

        }

        private void LeaveEvent()
        {
            IsBusy = true;

            MessagingCenter.Send<EventViewModel>(this, "leave_confirm");
            MessagingCenter.Subscribe<EventView>(this, "confirmed_leave", (sender) =>
            {
                if (model.LeaveEvent(current_user, DisplayEvent.EventId))
                {
                    NumOfGuests--;
                    HasJoined = false;
                    MessagingCenter.Send<EventViewModel>(this, "update_main_page");

                }
            });
            IsBusy = false;

        }

        private void CancelEvent()
        {
            MessagingCenter.Send<EventViewModel>(this, "cancel_message");
            MessagingCenter.Subscribe<EventView>(this, "confirmed_cancel", (sender) =>
            {
                if (model.CancelEvent(DisplayEvent.EventId))
                {
                    Console.WriteLine("Success");
                    MessagingCenter.Send<EventViewModel>(this, "navigate_back");
                }
            });
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
