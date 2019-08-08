using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Pastime
{
    class User
    {
        private int userId;
        private string email;
        private string password;
        private string username;
        private int yearJoined;
        private string location;
        private string faveSport;
        private string bio;
        private Uri dpUri;
        private int rating;

        private Image displayPicture; //Don't include in database
        

        public User(int userId, string email, string password, string username, int yearJoined, string location, string faveSport, string bio, Uri dpUri, int rating)
        {
            this.userId = userId;
            this.email = email;
            this.password = password;
            this.username = username;
            this.yearJoined = yearJoined;
            this.location = location;
            this.faveSport = faveSport;
            this.bio = bio;
            this.dpUri = dpUri;
            this.rating = rating;

            getPictureFromUri();
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

        public string Location
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

        public Uri DisplayPicture
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

        //TODO
        public void getPictureFromUri()
        {
          
        }

        //TODO
        public void getCurrentLocation()
        {

        }

        //TODO
        public bool validateBio()
        {
            return true;
        }

        //TODO
        public bool validateEmail()
        {
            return true;
        }

        //TODO
        public bool validatePassword()
        {
            return true;
        }
    }
}
