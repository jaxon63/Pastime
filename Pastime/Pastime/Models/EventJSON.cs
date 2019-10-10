using System;
using System.Collections.Generic;

namespace Pastime.Models
{
    public class EventJSONObj
    {
        public string status { get; set; }
        public string reason { get; set; }
    }
​
    //The login JSON response may include multiple objects
    public class EventJSON
    {
        public List<EventJSONObj> event_json { get; set; }
    }
}


