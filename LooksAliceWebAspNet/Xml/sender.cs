using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LooksAliceWebAspNet.Xml
{
    public class sender
    {
        public string name { get; set; }
        public string email { get; set; }
        public phone phone { get; set; }
        public List<document> documents { get; set; }
        public string hash { get; set; }
    }
}