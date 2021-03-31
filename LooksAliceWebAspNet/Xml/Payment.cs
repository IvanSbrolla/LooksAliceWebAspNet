using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LooksAliceWebAspNet.Xml
{
    public class payment
    {
        public string mode { get; set; }
        public string method { get; set; }
        public sender sender { get; set; }
        public string currency { get; set; }
        public string notificationURL { get; set; }
        public List<item> items { get; set; }
        public creditCard creditCard { get; set; }
        public string extraAmount { get; set; }
        public string reference { get; set; }
        public shipping shipping { get; set; }
        public string receiverEmail { get; set; }
    }
}