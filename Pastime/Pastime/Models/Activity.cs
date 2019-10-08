using System;
using System.Collections.Generic;
using System.Text;

namespace Pastime.Models
{
    public class Activity
    {
        private string name;
        private string iconImage;

        

        public Activity(string name, string iconImage)
        {
            this.name = name;
            this.iconImage = iconImage;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string IconImage
        {
            get => iconImage;
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
