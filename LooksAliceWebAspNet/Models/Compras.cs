using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace LooksAliceWebAspNet.Models
{
    public class Compras
    {
        [Key]
        public int CompraId { get; set; }
        public string CodTransacao { get; set; }
        public double Preco { get; set; }
        public string Forma_Pagamento { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        public string UserName { get; set; }
        public string Logradouro { get; set; }
        public string Cep { get; set; }
        public string NumeroResidencial { get; set; }
        public string Uf { get; set; }
        public string StatusEnvio { get; set; }
    }
}