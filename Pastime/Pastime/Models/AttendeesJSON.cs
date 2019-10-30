using System;
using System.Collections.Generic;

namespace Pastime.Models
{
    public class AttendeesJSONObj
    {
        public int Total { get; set; }
        public Array Attendees { get; set; }
    }

    public class AttendeesJSON
    {
        public List<AttendeesJSONObj> Attendees { get; set; }
    }
}



