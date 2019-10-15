using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Linq;
using System.Threading.Tasks;

namespace Pastime.Models
{
    public class User
    {
        private int userId;
        private string email;
        private string password;
        private string username;
        private int yearJoined;
        private Location location;
        private string locality;
        private string faveSport;
        private string bio;
        private Uri dpUri;
        private int rating;

        public User(int userId, string email, string password, string username, int yearJoined, string faveSport, string bio, Uri dpUri, int rating)
        {
            this.userId = userId;
            this.email = email;
            this.password = password;
            this.username = username;
            this.yearJoined = yearJoined;
            this.faveSport = faveSport;
            this.bio = bio;
            this.dpUri = dpUri;
            this.rating = rating;
        }

        public int UserId
        {
            get => userId;
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
            }
        }

        public string Username
        {
            get => username;
            set
            {
                username = value;
            }
        }

        public int YearJoined
        {
            get => yearJoined;
        }

        public string Locality
        {
            get => locality;
        }

        public string FaveSport
        {
            get => faveSport;
            set
            {
                faveSport = value;
            }
        }

        public string Bio
        {
            get => bio;
            set
            {
                bio = value;
            }
        }

        // Gets the user's current geographical location and sets it to a Location object.
        public async Task SetCurrentLocation()
        {
            try
            {
                Location loc = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium)); //Can set to be more accurate if necessary
                if (location != null)
                {
                    this.location = loc;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                throw fnsEx;
            }
            catch (FeatureNotEnabledException fneEx)
            {
                throw fneEx;
            }
            catch (PermissionException pEx)
            {
                throw pEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Finds the locality of the user based off their current location. Only needs to be done when showing a user profile.
        public async Task SetCurrentLocality()
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(location);
                Placemark placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    this.locality = placemark.Locality;
                }
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

        public double CalculateDistance(Location loc)
        {
            return Location.CalculateDistance(location, loc, DistanceUnits.Kilometers);
        }
    }
}
