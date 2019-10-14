using System;
using System.Collections.Generic;
using System.Text;

namespace Pastime
{
    public class EventJSONObj
    {
        public string eventID { get; set; }
        public string name { get; set; }
        public string activity { get; set; }
        public string equipment { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string max_guests { get; set; }
        public string description { get; set; }
        public string date { get; set; }
        public string end_time { get; set; }
    }

    //The login JSON response may include multiple objects
    public class EventJSON
    {
        public List<EventJSONObj> Event { get; set; }
    }

}