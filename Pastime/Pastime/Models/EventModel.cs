using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Essentials;


namespace Pastime.Models
{
    public class EventModel
    {
        private int eventId;
        private string name;
        private User host;
        private List<User> guests;
        private Activity activity;
        private List<string> equipmentNeeded;
        private Xamarin.Essentials.Location location;
        private int maxGuests;
        private string description;
        private TimeSpan startTime;
        private DateTime endTime;
        private bool active;

        public EventModel()
        {

        }

        //validation methods.
        //output parameter will be the error message displayed on the UI
        public bool validateName(string name, out string errMsg)
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

        public bool validateDesc(string desc, out string errMsg)
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

        public bool validateLocationString(string loc, out string errMsg)
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

        public bool validateSport(string sport, out string errMsg)
        {
            if (string.IsNullOrWhiteSpace(sport))
            {
                errMsg = "Please select a sport";
                return false;
            }
            else
            {
                errMsg = string.Empty;
                return true;
            }
        }

        public bool validateEventDate(DateTime eventDate, TimeSpan startTime, out string errMsg)
        {
            DateTime dateTime = eventDate.Date + startTime;
            if (dateTime < DateTime.Now.Add(new TimeSpan(1, 0, 0)))
            {
                errMsg = "Please choose a later time";
                return false;
            }

            errMsg = string.Empty;
            return true;
            
        }

      



        public Event createEvent()
        {
            Event result = new Event(0, "", null, null, null, 0, "", new DateTime(), new DateTime());
            return result;
        }





    }
}
