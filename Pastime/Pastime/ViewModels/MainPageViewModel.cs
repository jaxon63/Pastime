using Pastime.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        public void CreateEventNavigate()
        {
            Console.WriteLine("Hello there");
        }      

        public ICommand CreateEventCommand { private set; get; }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
