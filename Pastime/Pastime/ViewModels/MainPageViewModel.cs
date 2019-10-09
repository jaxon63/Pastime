using Pastime.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

            CreateEventCommand = new Command(CreateEventNavigate);
            LogoutCommand = new Command(Logout);
        }

        

        //This method will need to retrieve the events from the database
        //For now it just initialises the list with dummy data
        public async Task GetEventsAsync()
        {
            ObservableCollection<string> list = new ObservableCollection<string>();
            list.Add("hello");

            Event newEvent =  new Event("Soccer Event!", null, new Activity("Soccer", "soccer.png"), list, new Xamarin.Essentials.Location(38, -144), 3, "This is a description of this event its long enough", new DateTime(), new DateTime());
            Event newEvent2 = new Event("Basketball Event!", null, new Activity("Basketball", "basketball.png"), list, new Xamarin.Essentials.Location(0, 0), 3, "This is a description of this event its long enough", new DateTime(), new DateTime());

            await newEvent.getLocationLocality();
            await newEvent2.getLocationLocality();

            events.Add(newEvent);
            events.Add(newEvent2);

        }

   

        public ObservableCollection<Event> Events
        {
            get => events;
            set
            {
                events = value;
            }
        }

        //TODO
        public void CreateEventNavigate()
        {
            Console.WriteLine("Hello there");
        }

        //TODO
        public void Logout()
        {
            Console.WriteLine("Logout!");
        }

        public ICommand LogoutCommand { private set; get; }
        public ICommand CreateEventCommand { private set; get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
