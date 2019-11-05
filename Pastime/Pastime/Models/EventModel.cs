using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using RestSharp;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Pastime.Models
{
    public class EventModel
    {
        private List<Activity> activities;
        private string current_user;

        public EventModel()
        {
            //TODO: Activities should be stored on and retrieved from the database so
            //an app administrator can add/delete them as needed
            activities = new List<Activity>();
            current_user = Application.Current.Properties["current_user"].ToString();

            InitializeActivityList();
        }

        private void InitializeActivityList()
        {
            Activity soccer = new Activity("Soccer", "soccer.png");
            Activity hockey = new Activity("Hockey", "soccer.png");
            Activity basketball = new Activity("Basketball", "soccer.png");

            AddToActivitiesList(soccer);
            AddToActivitiesList(hockey);
            AddToActivitiesList(basketball);
        }


        public List<Activity> GetActivities()
        {
            List<Activity> result = new List<Activity>();
            foreach (Activity activity in activities)
            {
                result.Add(activity);
            }
            return result;
        }

        public void AddToActivitiesList(Activity activity)
        {
            activities.Add(activity);
        }

        //Client side validation
        //output parameter will be the error message displayed on the UI
        public bool ValidateName(string name, out string errMsg)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                errMsg = "Please enter a name for the event";
                return false;

            }
            else if (name.Length > 30)
            {
                errMsg = "Please enter a name less than 30 characters long";
                return false;
            }
            else
            {
                errMsg = string.Empty;
                return true;
            }
        }

        public bool ValidateDesc(string desc, out string errMsg)
        {
            if (string.IsNullOrWhiteSpace(desc))
            {
                errMsg = "Please enter a description for the event";
                return false;
            }
            else if (desc.Length < 20)
            {
                errMsg = "Please enter at least 20 characters";
                return false;
            }
            else if (desc.Length > 300)
            {
                errMsg = "Description can be no more than 300 characters long";
                return false;
            }
            else
            {
                errMsg = string.Empty;
                return true;
            }
        }

        public bool ValidateLocationString(string loc, out string errMsg)
        {
            if (string.IsNullOrWhiteSpace(loc))
            {
                errMsg = "Location can not be empty";
                return false;
            }
            else
            {
                errMsg = string.Empty;
                return true;
            }
        }

        public bool ValidateSport(string sport, out string errMsg)
        {
            if (string.IsNullOrWhiteSpace(sport))
            {
                errMsg = "Please select a sport " + sport;

                return false;
            }
            else
            {
                errMsg = string.Empty;
                return true;
            }
        }

        public bool ValidateEventDate(DateTime eventDate, TimeSpan endTime, out string errMsg)
        {
            if (eventDate < DateTime.Now.Add(new TimeSpan((int)0.45, 0, 0)))
            {
                errMsg = "Please choose a later start time";
                return false;
            }
            else if (eventDate.TimeOfDay > endTime)
            {
                errMsg = "Please choose a later end time";
                return false;
            }
            errMsg = string.Empty;
            return true;
        }

        public List<string> SaveEvent(string name, string activity, string equipment,
            double latitude, double longitude, int maxGuests, string desc,
            DateTime date, DateTime endTime)
        {
            List<string> results = new List<string>();

            //api link
            string create_event = "https://vietnguyen.me/pastime/create_event.php";

            //create a client object
            var client = new RestClient(create_event);

            //create a request
            var request = new RestRequest(Method.GET);

            //add the parameters to APIs
            request.AddParameter("name", name);
            request.AddParameter("host", current_user);
            request.AddParameter("activity", activity);
            request.AddParameter("equipment", equipment);
            request.AddParameter("latitude", latitude);
            request.AddParameter("longitude", longitude);
            request.AddParameter("max_guests", maxGuests);
            request.AddParameter("description", desc);
            request.AddParameter("date", date);
            request.AddParameter("end_time", endTime);


            //get the JSON response
            var response = client.Execute<CreateEventJSON>(request).Content;
            var status = JsonConvert.DeserializeObject<CreateEventJSON>(response).create_event[0].status;
            results.Add(status);

            if (status == "success")
            {
                var unique_code = JsonConvert.DeserializeObject<CreateEventJSON>(response).create_event[0].event_code;
                results.Add(unique_code);
            }
            else
            {
                var reason = JsonConvert.DeserializeObject<CreateEventJSON>(response).create_event[0].reason;
                results.Add(reason);
            }
            return results;

        }

        public Event CreateEvent(string name, Activity activity,
            ObservableCollection<string> equipment, Location location,
            int maxGuests, string desc, DateTime date, DateTime endTime)
        {
            string eqipment_raw = "";
            for (int i = 0; i < equipment.Count; i++)
            {
                if (i < equipment.Count - 1)
                {
                    eqipment_raw += equipment[i] + ", ";
                }
                else
                {
                    eqipment_raw += equipment[i];
                }
            }
            //to check if new event is recorded (testing purpose)
            //goto: https://vietnguyen.me/pastime/event_table.php



            //CREATE EVENT JSON OBJECT RETURNED VALUES
            List<String> response = SaveEvent(name, activity.Name, eqipment_raw,
                location.Latitude, location.Longitude,
                maxGuests, desc, date, endTime);

            string status = response[0];

            if (status == "success")
            {
                Event result = new Event(null, name, null, activity, equipment,
                    location, maxGuests, 0, null, desc, date, endTime);

                return result;
            }
            else
            {
                return null;
            }
        }

        public bool JoinEvent(string username, string event_id)
        {
            string join_event = "https://vietnguyen.me/pastime/join_event.php";

            var client = new RestClient(join_event);

            var request = new RestRequest(Method.GET);

            request.AddParameter("username", username);
            request.AddParameter("event_code", event_id);
            var response = client.Execute(request).Content;

            var json_response = JObject.Parse(response);
            JArray items = (JArray)json_response["Join"];
            var item = items[0];
            if (item["status"].ToString() == "failed")
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool LeaveEvent(string username, string event_id)
        {
            string leave_event = "https://vietnguyen.me/pastime/leave_event.php";

            var client = new RestClient(leave_event);

            var request = new RestRequest(Method.GET);

            request.AddParameter("username", username);
            request.AddParameter("event_code", event_id);
            var response = client.Execute(request).Content;

            var json_response = JObject.Parse(response);
            JArray items = (JArray)json_response["Leave"];
            var item = items[0];
            if (item["status"].ToString() == "failed")
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool CancelEvent(string event_id)
        {
            string cancel_event = "http://vietnguyen.me/pastime/delete_event.php";

            var client = new RestClient(cancel_event);

            var request = new RestRequest(Method.GET);

            request.AddParameter("username", current_user);
            request.AddParameter("event_code", event_id);
            var response = client.Execute(request).Content;

            var json_response = JObject.Parse(response);
            JArray items = (JArray)json_response["Delete_Event"];
            var item = items[0];
            if (item["status"].ToString() == "failed")
            {
                return false;
            }
            else
            {
                return true;
            }

        }

    }
}
