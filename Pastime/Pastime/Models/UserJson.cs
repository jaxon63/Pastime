using System;
using System.Collections.Generic;
using System.Text;

namespace Pastime
{
    public class UserJsonObj
    {
        public string username { get; set; }
        public string email { get; set; }
        public string status { get; set; }
        public string password { get; set; }
    }

    public class UserJson
    {
        public List<UserJsonObj> user_json { get; set; }
    }
}
