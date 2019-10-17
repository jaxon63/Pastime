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
        private string eventId;
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

        public Event(string eventId, string name, User host, Activity activity, ObservableCollection<string> equipment, Location location, int maxGuests, string description, DateTime startTime, DateTime endTime)
        {
            this.eventId = eventId;
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
        }

        public string EventId
        {
            get => eventId;
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
            }
        }

        public User Host
        {
            get => host;
        }


        public List<User> Guests
        {
            get => guests;
        }

        public ObservableCollection<string> EquipmentNeeded
        {
            get => equipmentNeeded;
            set
            {
                equipmentNeeded = value;
            }
        }

        public Activity Activity
        {
            get => activity;
        }

        public Location Location
        {
            get => location;
            set
            {
                location = value;
            }
        }

        public string Locality
        {
            get => locality;
            set
            {
                locality = value;
            }
        }

        public int MaxGuests
        {
            get => maxGuests;
            set
            {
                maxGuests = value;
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
            }
        }

        public DateTime StartTime
        {
            get => startTime;
            set
            {
                startTime = value;
            }
        }

        public DateTime EndTime
        {
            get => endTime;
            set
            {
                endTime = value;
            }
        }

        public bool Active
        {
            get => active;
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
                    if (string.IsNullOrWhiteSpace(placemark.Locality))
                    {
                        locality = "Unknown Location";
                    }
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
            if (DateTime.Compare(DateTime.Now,  endTime) > 0)
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