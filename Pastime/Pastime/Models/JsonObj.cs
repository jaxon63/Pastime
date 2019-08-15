using System;
using System.Collections.Generic;
using System.Text;

namespace Pastime
{
    public class LoginObj
    { 
        public string status { get; set; }
        public string http_code { get; set; }
    }

    public class LoginJSON
    {
        public List<LoginObj> login { get; set; }
    }
}
