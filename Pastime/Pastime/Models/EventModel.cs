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







        public EventModel(string name, Activity activity, string description)
        {
            this.name = name;
            this.activity = activity;
            this.description = description;






        }

        public bool validateName()
        {
            if (string.IsNullOrEmpty(name))
                return false;
            else
                return true;
        }

        public bool validateDesc()
        {
            if (description.Length < 20 || description.Length > 300)
                return false;
            else
                return true;
        }

        public Event createEvent()
        {
            Event result = null;

            return result;
        }





    }
}
