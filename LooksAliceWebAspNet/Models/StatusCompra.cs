using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace LooksAliceWebAspNet.Models
{
    public class StatusCompra
    {
        [Key]
        public int StatusCompraId { get; set; }
        public string Status { get; set; }
    }
}