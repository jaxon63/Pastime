using System;
using System.Collections;

namespace Pastime.Models
{
    public class EventModel
    {
        private Event eventData;

        private ArrayList eventList;

        private int eventId;
        private string eventName, eventSport, eventDesc, eventLocation;
        private DateTime startDate;
        
        public EventModel(int eventId, string eventName, string eventSport, string eventDesc, string eventLocation, DateTime startDate)
        {
            this.eventId = eventId;
            this.eventName = eventName;
            this.eventSport = eventSport;
            this.eventDesc = eventDesc;
            this.eventLocation = eventLocation;
            this.startDate = startDate;
        }




    }
}
