using System;
using System.Collections.Generic;
using System.Text;

namespace Pastime
{
    public class RegisterObj
    {
        public string status { get; set; }
        public string current_user { get; set; }
        public string reason { get; set; }
    }

    //The login JSON response may include multiple objects
    public class RegisterJSON
    {
        public List<RegisterObj> register { get; set; }
    }
}
