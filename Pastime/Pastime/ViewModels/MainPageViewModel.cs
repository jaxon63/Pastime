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
using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json.Linq;

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
            ViewCommand = new Command<string>(async (s) => await NavigateViewEventAsync(s));
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

        //use the parameter to limit the number of retrieved events
        private JArray RetrieveAllEvents(int limit)
        {
            //api link
            string retrive_all_event = "https://vietnguyen.me/pastime/retrieve_all_events.php";
            //create a client object
            var client = new RestClient(retrive_all_event);
            //create a request
            var request = new RestRequest(Method.GET);
            //add the parameters to APIs
            request.AddParameter("limit", limit);
            //get the JSON response

            var response = client.Execute<EventJSON>(request).Content;
            var json_response = JObject.Parse(response);
            JArray items = (JArray)json_response["Events"];

            return items;
        }

        //This method will need to retrieve the events from the database
        //For now it just initialises the list with dummy data
        //There is a bug when a user returns from the "create event" page, 
        public async Task GetEventsAsync()
        {
            IsBusy = true;

            JArray items = RetrieveAllEvents(10);

            for (int i = 0; i < items.Count; i++)
            {
                var item = (JObject)items[i];

                var eventID = (string)item["eventID"];
                var name = (string)item["name"];
                var str_activity = (string)item["activity"];
                Activity activity = new Activity(str_activity, str_activity.ToLower() + ".png");
                var eqipments = item["equipment"];
                ObservableCollection<string> list = new ObservableCollection<string>();
                foreach(string element in eqipments)
                {
                    list.Add(element);
                }
                var latitude = (double)item["latitude"];
                var longitude = (double)item["longitude"];
                var max_guests = (int)item["max_guests"];
                var description = (string)item["description"];
                var date = (DateTime)item["date"];
                var end_time = (DateTime)item["end_time"];

                //I just put another param for EventID
                Event newEvent = new Event(eventID, name, null, activity, list,
                new Xamarin.Essentials.Location(latitude, longitude), max_guests, description,
                date, end_time);

                await newEvent.getLocationLocality();
                events.Add(newEvent);
            }

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

        private async Task NavigateViewEventAsync(string eventId)
        {
            IsBusy = true;
            try
            {
                EventView eventView = new EventView(eventId);
                await nav.PushAsync(eventView);

            }
            finally
            {
                IsBusy = false;
            }
            
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
