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
        private User current_user;
        private bool isRefreshing;

        public MainPageViewModel(INavigation nav)
        {
            events = new ObservableCollection<Event>();

            this.nav = nav;

            MessagingCenter.Subscribe<EventViewModel>(this, "update_main_page", async (sender) =>
            {
                await RefreshEvents();

            });

            CreateEventCommand = new Command(async () => await CreateEventNavigateAsync());
            ViewCommand = new Command<Event>(async (e) => await NavigateViewEventAsync(e));
            RefreshCommand = new Command(async () =>
            {
                IsRefreshing = true;
                await RefreshEvents();
                IsRefreshing = false;

            });
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
                OnPropertyChanged("IsVisible");
            }
        }

        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (isRefreshing == value)
                    return;

                isRefreshing = value;
                OnPropertyChanged();

            }
        }



        public bool IsVisible
        {
            get => !IsBusy;
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

        private void IncrementEventGuestCount(string event_id)
        {
            foreach (Event item in Events)
            {
                if (item.EventId == event_id)
                {
                    item.NumOfGuests++;
                    OnPropertyChanged("Events");
                }

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

        private async Task RefreshEvents()
        {
            Events.Clear();
            await GetEventsAsync();
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


        private async Task NavigateViewEventAsync(Event e)
        {
            IsBusy = true;
            try
            {
                EventView eventView = new EventView(e);
                await nav.PushAsync(eventView);

            }
            finally
            {
                IsBusy = false;
            }

        }

        public async Task GetEventsAsync()
        {

            IsBusy = true;


            JArray items = RetrieveAllEvents(10);

            for (int i = 0; i < items.Count; i++)
            {
                var item = (JObject)items[i];
                var eventID = (string)item["eventID"];

                var name = (string)item["name"];
                var host = (string)item["host"];
                var numOfGuests = (int)item["total"];
                var str_activity = (string)item["activity"];
                Activity activity = new Activity(str_activity, str_activity.ToLower() + ".png");
                var eqipments = item["equipment"];
                ObservableCollection<string> list = new ObservableCollection<string>();
                foreach (string element in eqipments)
                {
                    list.Add(element);
                }
                var latitude = (double)item["latitude"];
                var longitude = (double)item["longitude"];
                var max_guests = (int)item["max_guests"];
                var description = (string)item["description"];

                List<string> attendeeList = new List<string>();
                foreach (string element in item["attendees"])
                {
                    attendeeList.Add(element);
                }


                var date = (DateTime)item["date"];
                var end_time = (DateTime)item["end_time"];

                var attendees = item["attendees"];
                List<string> listAttendees = new List<string>();
                foreach (string attendee in attendees)
                {
                    listAttendees.Add(attendee);
                }

                Event newEvent = new Event(eventID, name, host, activity, list, new Xamarin.Essentials.Location(latitude, longitude), max_guests, numOfGuests, listAttendees, description,
                date, end_time);

                await newEvent.getLocationLocality();
                Events.Add(newEvent);
            }

            IsBusy = false;
        }

        public ICommand ViewCommand { private set; get; }
        public ICommand CreateEventCommand { private set; get; }
        public ICommand RefreshCommand { private set; get; }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
