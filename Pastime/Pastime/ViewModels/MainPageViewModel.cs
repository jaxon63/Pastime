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

    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Event> events;
        private INavigation nav;

        public MainPageViewModel(INavigation nav)
        {
            ObservableCollection<string> list = new ObservableCollection<string>();
            list.Add("hello");

            events = new ObservableCollection<Event>();

            events.Add(new Event("Soccer Event!", null, new Activity("Soccer", "soccer.png"), list, new Xamarin.Essentials.Location(100, 100), 3, "This is a description of this event its long enough", new DateTime(), new DateTime()));
            events.Add(new Event("Basketball Event!", null, new Activity("Basketball", "basketball.png"), list, new Xamarin.Essentials.Location(100, 100), 3, "This is a description of this event its long enough", new DateTime(), new DateTime()));
            events.Add(new Event("Hockey Event!", null, new Activity("Hockey", "hockey.png"), list, new Xamarin.Essentials.Location(100, 100), 3, "This is a description of this event its long enough", new DateTime(), new DateTime()));

            this.nav = nav;

            CreateEventCommand = new Command(CreateEventNavigate);
            ViewCommand = new Command(async () => await NavigateViewEventAsync());
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

        private async Task NavigateViewEventAsync()
        {
            
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
