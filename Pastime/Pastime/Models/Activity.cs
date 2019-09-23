using System;
using System.Collections.Generic;
using System.Text;

namespace Pastime.Models
{
    public class Activity
    {
        private string name;

        //Add this stuff later
        //private Uri iconImage;
        //private List<string> equipment;


        public Activity(string name)
        {
            this.name = name;
            //this.iconImage = icon;
            //this.equipment = equipment;
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
