using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LooksAliceWebAspNet.Xml
{
    public class billingAddress
    {
        public string street { get; set; }
        public string number { get; set; }
        public string complement { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }
    }
}