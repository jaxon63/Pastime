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

       // private Image displayPicture;
        

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

        public User()
        {

        }

        public int UserId
        {
            get
            {
                return userId;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public int YearJoined
        {
            get
            {
                return yearJoined;
            }
        }

        public string Locality
        {
            get
            {
                return locality;
            }
        }

        public string FaveSport
        {
            get
            {
                return faveSport;
            }
            set
            {
                faveSport = value;
            }
        }

        public string Bio
        {
            get
            {
                return bio;
            }
            set
            {
                bio = value;
            }
        }

        public Uri DpUri
        {
            get
            {
                return dpUri;
            }
            set
            {
                dpUri = value;
            }
        }

        /*public Image DisplayPicture
        {
            get
            {
                return displayPicture;
            }
        }*/

        public int Rating
        {
            get
            {
                return rating;
            }
            set
            {
                rating = value;
            }
        }

        /// <summary>
        /// DOESN'T WORK. Sets the user's display picture as a Xamarin Forms Image object. Propbably not needed as you can just use
        /// the URI as the image source to the image on a form.
        /// </summary>
        /*public void getPictureFromUri()
        {
            try
            {
                if (ImageSource.FromUri(dpUri) != null)
                    displayPicture.Source = ImageSource.FromUri(dpUri);
            } catch (NullReferenceException nEx)
            {
                Console.WriteLine(nEx.StackTrace);
            }
        }*/


        //TODO
        public bool validateBio()
        {
            return true;
        }

        //TODO
        public bool validateEmail() { 
            return true;

        }

        //TODO
        public bool validatePass() {
            return true;
        }

        /// <summary>
        /// Gets the user's current geographical location and sets it to a Location object.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Finds the locality of the user based off their current location. Only needs to be done when showing a user profile.
        /// Could not get it to work on emulator but works on physical device
        /// </summary>
        /// <returns></returns>
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

        async Task SetLocations()
        {
            await SetCurrentLocation();
            await SetCurrentLocality();
        }
        public double CalculateDistance (Location loc)
        {
            return Location.CalculateDistance(location, loc, DistanceUnits.Kilometers);
        }
    }
}
