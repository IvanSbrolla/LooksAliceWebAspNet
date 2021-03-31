using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LooksAliceWebAspNet.Xml
{
    public class shipping
    {
        public string addressRequired { get; set; }
        public address address { get; set; }
        public int type { get; set; }
        public string cost { get; set; }
    }
}