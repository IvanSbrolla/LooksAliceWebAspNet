using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace LooksAliceWebAspNet.Models
{
    public class Compra_StatusCompra
    {
        [Key]
        public int Id { get; set; }
        public Compras Compras { get; set; }
        [ForeignKey("Compras")]
        public int ComprasId { get; set; }
        public StatusCompra StatusCompra { get; set; }
        [ForeignKey("StatusCompra")]
        public int StatusCompraId { get; set; }
    }
}