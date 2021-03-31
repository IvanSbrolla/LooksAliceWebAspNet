using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LooksAliceWebAspNet.Xml
{
    public class creditCard
    {
        public string token { get; set; }
        public installment installment { get; set; }
        public holder holder { get; set; }
        public billingAddress billingAddress { get; set; }
    }
}