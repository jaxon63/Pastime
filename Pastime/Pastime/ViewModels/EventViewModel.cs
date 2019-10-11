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
        private int guestsAttending;
        public EventViewModel(INavigation nav, string eventId)
        {
            this.nav = nav;

            //TODO: Delete this later
            ObservableCollection<String> list = new ObservableCollection<string>();
            list.Add("Soccer ball");


            //TODO: pass the event to the page when the event is clicked on
            //Create a new testing event object just temporarily
            /*this.displayEvent = new Event("asASA", "New Event", null, new Activity("Soccer", "soccer.png"), 
                list, new Xamarin.Essentials.Location(100, 100), 4, 
                "This is a description of the event. It has to be 50 characters long or something like that", new DateTime(), new DateTime()); */
            this.displayEvent = retrieveEvent(eventId);
            Console.WriteLine(displayEvent.Name);
            //Commands
            BackCommand = new Command( NavigateBack);
        }

        private Event retrieveEvent(string eventId)
        {
            //variables to populate new event object
            Event returnEvent;
            string name;
            Activity activity;
            ObservableCollection<string> equipment = new ObservableCollection<string>();
            double lat;
            double lon;
            Xamarin.Essentials.Location location;
            int maxGuests;
            string description;
            DateTime date;
            DateTime endTime;

            //api link
            string retrive_event = "https://vietnguyen.me/pastime/retrieve_event.php?eventID=" + eventId;

            //create a client object
            var client = new RestClient(retrive_event);

            //create a request
            var request = new RestRequest(Method.GET);
            
            //get the JSON response
            var response = client.Execute<EventJSON>(request).Content;
            var json_response = JObject.Parse(response);

            //Extract the relevant data from the response
            name = (string)json_response["Event"][0]["name"];
            activity = new Activity((string)json_response["Event"][0]["activity"], (string)json_response["Event"][0]["name"] + ".png");

            //add equipment to equipment list
            var getEquip = json_response["Event"][0]["equipment"];
            foreach(string element in getEquip)
            {
                equipment.Add(element);
            }

            lat = (double)json_response["Event"][0]["latitude"];
            lon = (double)json_response["Event"][0]["longitude"];
            location = new Xamarin.Essentials.Location(lat, lon);

            maxGuests = (int)json_response["Event"][0]["max_guests"];
            description = (string)json_response["Event"][0]["description"];
            date = (DateTime)json_response["Event"][0]["date"];
            endTime = (DateTime)json_response["Event"][0]["end_time"];

            returnEvent = new Event(eventId, name, null, activity, equipment, location, maxGuests, description, date, endTime);
            
            if (returnEvent != null)
                return returnEvent;
            else
                return null;

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

        public int GuestsAttending
        {
            get
            {
                return displayEvent.getGuestCount();
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
