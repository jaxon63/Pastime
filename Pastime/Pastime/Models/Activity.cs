using System;
using System.Collections.Generic;
using System.Text;

namespace Pastime.Models
{
    public class Activity
    {
        private string name;
        private string iconImage;

        

        public Activity(string name, string icon)
        {
            this.name = name;
            this.iconImage = icon;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        /*
        public Uri IconImage
        public string IconImage
        {
            get
            {
                return iconImage;
            }
        } */

            /*
        public List<string> Equipment
        {
            get
            {
                return equipment;
            }
        }
        */
    }
}
