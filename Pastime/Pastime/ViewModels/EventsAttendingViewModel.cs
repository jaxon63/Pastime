using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using Pastime.Models;
using Pastime.Views;
using RestSharp;
using Xamarin.Forms;

namespace Pastime.ViewModels
{
    public class EventsAttendingViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Event> events;
        private string current_user;
        private bool isBusy;
        private EventModel model;

        public EventsAttendingViewModel()
        {
            current_user = Application.Current.Properties["current_user"].ToString();
            events = new ObservableCollection<Event>();
            this.model = new EventModel();

            LeaveCommand = new Command<string>((id) => LeaveEvent(id));
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

        public bool IsVisible
        {
            get => !IsBusy;
        }

        private void LeaveEvent(string eventid)
        {
            MessagingCenter.Send<EventsAttendingViewModel>(this, "leave_confirm");
            MessagingCenter.Subscribe<EventsAttendingView>(this, "confirmed_leave", async (sender) =>
            {
                if (model.LeaveEvent(current_user, eventid))
                {
                    Console.WriteLine("Success");
                    for (int i = events.Count - 1; i >= 0; i--)
                    {
                        if (events[i].EventId == eventid)
                        {
                            events.RemoveAt(i);
                        }
                    }
                }
            });

        }

        private JArray RetrieveAllEvents()
        {
            //api link
            string retrive_all_event = "https://vietnguyen.me/pastime/event_by_attendee.php";

            //create a client object
            var client = new RestClient(retrive_all_event);

            //create a request
            var request = new RestRequest(Method.GET);

            //add the parameters to APIs
            request.AddParameter("username", current_user);

            //get the JSON response
            var response = client.Execute<EventJSON>(request).Content;
            var json_response = JObject.Parse(response);
            JArray items = (JArray)json_response["Events"];

            return items;
        }

        public async Task GetEventsAsync()
        {
            if (events.Count <= 0)
            {
                IsBusy = true;
                JArray items = RetrieveAllEvents();

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


                    Event newEvent = new Event(eventID, name, host, activity, list,
                    new Xamarin.Essentials.Location(latitude, longitude), max_guests, numOfGuests, attendeeList, description,
                    date, end_time);

                    await newEvent.getLocationLocality();
                    Console.WriteLine(newEvent.Name);
                    events.Add(newEvent);
                }
                IsBusy = false;

            }
        }

        public ICommand LeaveCommand { private set; get; }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
