using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace LooksAliceWebAspNet.Models
{
    public class Cores
    {
        [Key]
        public int CorId { get; set; }
        public string Cor_Nome { get; set; }
        public string CodRgba { get; set; }
    }
}