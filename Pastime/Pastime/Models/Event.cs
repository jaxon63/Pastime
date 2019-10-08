using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using System.Linq;
using System.Threading.Tasks;

namespace Pastime.Models
{
   public class Event
    {
        private int eventId;
        private string name;
        private User host;
        private List<User> guests;
        private Activity activity;
        private ObservableCollection<string> equipmentNeeded;
        private Location location;
        private string locality;
        private int numOfGuests;
        private int maxGuests;
        private string description;
        private DateTime startTime;
        private DateTime endTime;
        private bool active;

        //TODO: add user to list of guests

       

        public Event( string name, User host, Activity activity, ObservableCollection<string> equipment, Location location, int maxGuests, string description, DateTime startTime, DateTime endTime)
        {
            this.name = name;
            this.host = host;
            this.activity = activity;
            this.equipmentNeeded = equipment;
            this.location = location;
            this.maxGuests = maxGuests;
            this.description = description;
            this.startTime = startTime;
            this.endTime = endTime;
            this.active = true;
            this.guests = new List<User>();
            //Automatically add the host as a guest to the event
            this.guests.Add(host);
        }

        //For testing location of event
        public Event (double lat, double lon)
        {
            location = new Location(lat, lon);
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

        public ObservableCollection<string> EquipmentNeeded
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

        public string Locality
        {
            get
            {
                return locality;
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

        public DateTime EndTime
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
            return guests.Count;
        }

        public async Task<string> getLocationLocality()
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(location);
                Placemark placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    locality = placemark.Locality;
                }
                else
                {
                    locality = "Unknown Location";
                }
                return locality;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                throw fnsEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool CheckIfActive()
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
        } 
    }
}