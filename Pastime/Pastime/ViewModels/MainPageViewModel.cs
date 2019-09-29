using Pastime.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
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
            List<string> list = new List<string>();
            list.Add("Hello");
            events = new ObservableCollection<Event>();
            events.Add(new Event(1, "New Event", new User(1, "hello", "password", "steveny1", 2001, "Soccer", "hello", null, 1), new Activity("Soccer", "soccer.png", list), new Xamarin.Essentials.Location(100, 100), 4, "hello" , new DateTime(), new DateTime()));
            events.Add(new Event(1, "New Event2", new User(1, "hello", "password", "steveny1", 2001, "Soccer", "hello", null, 1), new Activity("Soccer", "soccer.png", list), new Xamarin.Essentials.Location(100, 100), 4, "hello", new DateTime(), new DateTime()));
            events.Add(new Event(1, "New Event3", new User(1, "hello", "password", "steveny1", 2001, "Soccer", "hello", null, 1), new Activity("Soccer", "soccer.png", list), new Xamarin.Essentials.Location(100, 100), 4, "hello", new DateTime(), new DateTime()));

            this.nav = nav;

            CreateEventCommand = new Command(CreateEventNavigate);
            LogoutCommand = new Command(Logout);
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
