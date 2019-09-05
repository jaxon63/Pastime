using System;
namespace Pastime.Models
{
    public class Event
    {
        private int eventId;
        private string eventName;
        //TODO: sport probably won't stay a string
        private string eventSport;
        private string eventDesc;
        private string eventLocation;
        private DateTime startDate;


        public Event(int eventId, string eventName, string sport,  string eventDesc, string eventLocation, DateTime startDate)
        {
            this.eventId = eventId;
            this.eventName = eventName;
            this.eventSport = sport;
            this.eventDesc = eventDesc;
            this.eventLocation = eventLocation;
            this.startDate = startDate;
        }

        public int EventId {get => eventId; }
        public string EventName { get => eventName; set => eventName = value; }
        public string EventSport { get => eventSport; set => eventSport = value; }
        public string EventDesc { get => eventDesc; set => eventDesc = value; }
        public string EventLocation { get => eventLocation; set => eventLocation = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
    }
}
