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
        private DateTime startTime;
        private DateTime endTime;
        private bool active;

        public EventModel()
        {
           
        }

        //validation methods.
        //output parameter will be the error message displayed on the UI
        public bool validateName(string name, out string errMsg)
        {
            if (string.IsNullOrEmpty(name))
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
            if (string.IsNullOrEmpty(desc))
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

        public Event createEvent()
        {
            Event result = null;

            return result;
        }





    }
}
