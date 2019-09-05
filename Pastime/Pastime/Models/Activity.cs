using System;
using System.Collections.Generic;
using System.Text;

namespace Pastime.Models
{
    public class Activity
    {
        private string name;
        private Uri iconImage;
        private List<string> equipment;


        public Activity(string name, Uri icon, List<string> equipment)
        {
            this.name = name;
            this.iconImage = icon;
            this.equipment = equipment;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public Uri IconImage
        {
            get
            {
                return iconImage;
            }
        }

        public List<string> Equipment
        {
            get
            {
                return equipment;
            }
        }
    }
}
