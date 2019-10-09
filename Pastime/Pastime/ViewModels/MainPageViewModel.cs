using Pastime.Models;
using Pastime.Views;
using Pastime.Views.CreateEventViewModal;
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

    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Event> events;
        private INavigation nav;
        private bool isBusy;

        public MainPageViewModel(INavigation nav)
        {
            ObservableCollection<string> list = new ObservableCollection<string>();
            list.Add("hello");

            events = new ObservableCollection<Event>();

            /*events.Add(new Event("Soccer Event!", null, new Activity("Soccer", "soccer.png"), list, new Xamarin.Essentials.Location(100, 100), 3, "This is a description of this event its long enough", new DateTime(), new DateTime()));
            events.Add(new Event("Basketball Event!", null, new Activity("Basketball", "basketball.png"), list, new Xamarin.Essentials.Location(100, 100), 3, "This is a description of this event its long enough", new DateTime(), new DateTime()));
            events.Add(new Event("Hockey Event!", null, new Activity("Hockey", "hockey.png"), list, new Xamarin.Essentials.Location(100, 100), 3, "This is a description of this event its long enough", new DateTime(), new DateTime()));

    */

            this.nav = nav;

            CreateEventCommand = new Command(async () => await CreateEventNavigateAsync());
            ViewCommand = new Command(async () => await NavigateViewEventAsync());
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

       
        //This method will need to retrieve the events from the database
        //For now it just initialises the list with dummy data
        //There is a bug when a user returns from the "create event" page, 
        public async Task GetEventsAsync()
        {
            IsBusy = true;
            ObservableCollection<string> list = new ObservableCollection<string>();
            list.Add("hello");

            Event newEvent =  new Event("Soccer Event!", null, new Activity("Soccer", "soccer.png"), list, new Xamarin.Essentials.Location(38, -144), 3, "This is a description of this event its long enough", new DateTime(), new DateTime());
            Event newEvent2 = new Event("Basketball Event!", null, new Activity("Basketball", "basketball.png"), list, new Xamarin.Essentials.Location(0, 0), 3, "This is a description of this event its long enough", new DateTime(), new DateTime());

            await newEvent.getLocationLocality();
            await newEvent2.getLocationLocality();

            events.Add(newEvent);
            events.Add(newEvent2);

            IsBusy = false;

        }

   

        public ObservableCollection<Event> Events
        {
            get => events;
            set
            {
                if (events == value)
                    return;

                events = value;
                OnPropertyChanged();
            }
        }
        

        //TODO
        private async Task CreateEventNavigateAsync()
        {
            IsBusy = true;
            try
            {
                await nav.PushModalAsync(new CreateEventViewModalName());
            }
            finally
            {
                IsBusy = false;
            }

            }

        private async Task NavigateViewEventAsync()
        {
            Console.WriteLine("Testing");
            await nav.PushAsync(new EventView());
        }

        public ICommand ViewCommand { private set; get; }
        public ICommand CreateEventCommand { private set; get; }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
