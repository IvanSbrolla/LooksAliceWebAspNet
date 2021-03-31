using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LooksAliceWebAspNet.Models
{
    public class Tecidos
    {
        [Key]
        public int TecidoId { get; set; }
        public string Tecido_Nome { get; set; }
    }
}