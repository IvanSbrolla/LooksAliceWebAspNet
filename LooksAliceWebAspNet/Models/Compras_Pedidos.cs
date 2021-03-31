using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace LooksAliceWebAspNet.Models
{
    public class Compras_Pedidos
    {
        [Key]
        public int Compras_PedidosId { get; set; }
        public Pedidos Pedidos { get; set; }
        [ForeignKey("Pedidos")]
        public int PedidosId { get; set; }
        public Compras Compras { get; set; }
        [ForeignKey("Compras")]
        public int ComprasId { get; set; }
    }
}