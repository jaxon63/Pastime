using System;
using System.Collections.Generic;
using System.Text;

namespace Pastime
{
    public class RegisterObj
    { 
        public string status { get; set; }
        public string reason { get; set; }
    }
    
    public class RegisterJSON
    {
        public List<RegisterObj> Register { get; set; }
    }
}
