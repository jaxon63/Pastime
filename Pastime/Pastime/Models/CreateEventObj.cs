using System;
using System.Collections.Generic;
using System.Text;

namespace Pastime
{
    public class CreateEventJSONObj
    {
        public string status { get; set; }
        public string event_code { get; set; }
        public string reason { get; set; }
    }

    //The login JSON response may include multiple objects
    public class CreateEventJSON
    {
        public List<CreateEventJSONObj> create_event { get; set; }
    }
}


