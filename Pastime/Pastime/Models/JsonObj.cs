using System;
using System.Collections.Generic;
using System.Text;

namespace Pastime
{
    //Any functions that use the APIs for JSON responses from the Server
    //This class is necessary to hold that JSON object

    //For example: this class holds JSON object returned from the Login API
    //each object inludes "status" and "http_code" value
    public class LoginObj
    { 
        public string status { get; set; }
        public string http_code { get; set; }
    }

    //The login JSON response may include multiple objects
    public class LoginJSON
    {
        public List<LoginObj> login { get; set; }
    }
}
