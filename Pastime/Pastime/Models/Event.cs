using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace Pastime.Models
{
    public class Event
    {
        private int eventId;
        private string name;
        private User host;
        private List<User> guests;
        private Activity activity;
        private List<string> equipmentNeeded;
        private Location location;
        private int numOfGuests;
        private int maxGuests;
        private string description;
        private DateTime startTime;
        private TimeSpan endTime;
        private bool active;

        //TODO: add user to list of guests

        public Event()
        {
        }

        public Event( string name, User host, Activity activity, Location location, int maxGuests, string description, DateTime startTime, TimeSpan endTime)
        {
            this.name = name;
            this.host = host;
            this.activity = activity;
            this.location = location;
            this.maxGuests = maxGuests;
            this.description = description;
            this.startTime = startTime;
            this.endTime = endTime;
            active = true;
            //Automatically add the host as a guest to the event
            this.guests.Add(host);
        }

        public int EventId
        {
            get
            {
                return eventId;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public User Host
        {
            get
            {
                return host;
            }
        }


        public List<User> Guests
        {
            get
            {
                return guests;
            }
        }

        public List<string> EquipmentNeeded
        {
            get
            {
                return equipmentNeeded;
            }
            set
            {
                equipmentNeeded = value;
            }
        }

        public Activity Activity
        {
            get
            {
                return activity;
            }
        }

        public Location Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
            }
        }

        public int MaxGuests
        {
            get
            {
                return maxGuests;
            }

            set
            {
                maxGuests = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return startTime;
            }

            set
            {
                startTime = value;
            }
        }

        public TimeSpan EndTime
        {
            get
            {
                return endTime;
            }

            set
            {
                endTime = value;
            }
        }

        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }

        public bool AddGuest(User guest)
        {
            if (guests.Count < maxGuests && !guests.Contains(guest) && active)
            {
                guests.Add(guest);
                return true;
            }

            return false;
        }

        public bool RemoveGuest(User guest)
        {
            if (guests.Contains(guest))
            {
                guests.Remove(guest);
                return true;
            }
            return false;
        }

        public int getGuestCount()
        {
            return guests.Count();
        }

       /* public bool CheckIfActive()
        {
            if (DateTime.Now > endTime)
            {
                active = false;
            }
            else
            {
                active = true;
            }

            return active;
        } */
    }
}