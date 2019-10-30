using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace Pastime.Models
{
    public class Event
    {
        private string eventId;
        private string name;
        private string host;
        private List<string> guests;
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

        public Event(string eventId, string name, string host, Activity activity, ObservableCollection<string> equipment, Location location, int maxGuests, int numOfGuests, List<string> guests, string description, DateTime startTime, DateTime endTime)
        {
            this.eventId = eventId;
            this.name = name;
            this.host = host;
            this.activity = activity;
            this.equipmentNeeded = equipment;
            this.location = location;
            this.numOfGuests = numOfGuests;
            this.maxGuests = maxGuests;
            this.description = description;
            this.startTime = startTime;
            this.endTime = endTime;
            this.active = true;
            this.guests = guests;

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

        public string Host
        {
            get => host;
        }


        public List<string> Guests
        {
            get => guests;
        }

        public int NumOfGuests
        {
            get => numOfGuests;
            set
            {
                numOfGuests = value;
            }
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

        public string DisplayStartDate
        {
            get
            {
                string monthName = StartTime.ToString("MMMM", CultureInfo.InvariantCulture);
                string suffix = string.Empty;
                string time = StartTime.ToString("h:mm tt");

                if (StartTime.Day == 1 || StartTime.Day == 21 || StartTime.Day == 31)
                {
                    suffix = "st";
                }
                else if (StartTime.Day == 2 || StartTime.Day == 22)
                {
                    suffix = "nd";
                }
                else
                {
                    suffix = "th";
                }

                return String.Format($"{StartTime.DayOfWeek.ToString()} the {StartTime.Day}{suffix} of {monthName} at {time}");
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

        public bool AddGuest(string guest)
        {
            if (guests.Count < maxGuests && !guests.Contains(guest) && active)
            {
                guests.Add(guest);
                return true;
            }
            return false;
        }

        public bool RemoveGuest(string guest)
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

        public string GuestCount
        {
            get => getGuestCount().ToString();
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