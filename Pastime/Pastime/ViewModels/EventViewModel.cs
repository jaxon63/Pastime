using Pastime.Models;
using Pastime.Views;
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
        private Event viewEvent;
        private int guestsAttending;
        public EventViewModel(INavigation nav, string eventId)
        {
            this.nav = nav;

            //TODO: Delete this later
            ObservableCollection<String> list = new ObservableCollection<string>();
            list.Add("Soccer ball");


            //TODO: pass the event to the page when the event is clicked on
            //Create a new testing event object just temporarily
            this.viewEvent = new Event("asASA", "New Event", null, new Activity("Soccer", "soccer.png"), 
                list, new Xamarin.Essentials.Location(100, 100), 4, 
                "This is a description of the event. It has to be 50 characters long or something like that", new DateTime(), new DateTime());
                
            //Commands
            BackCommand = new Command( NavigateBack);
        }

        public Event ViewEvent
        {
            get => viewEvent;
            set
            {
                if (viewEvent == value)
                    return;
                viewEvent = value;
                OnPropertyChanged();
            }
        }

        public int GuestsAttending
        {
            get
            {
                return viewEvent.getGuestCount();
            }
        }

        //Methods
        private void  NavigateBack()
        {
            Application.Current.MainPage = new MasterView();
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
