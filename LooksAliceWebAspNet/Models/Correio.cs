using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace LooksAliceWebAspNet.Models
{
    public class Correio
    {
        [Required]
        [MinLength(5, ErrorMessage = "Número inválido de caracteres!")]
        [MaxLength(8, ErrorMessage = "Número de caracteres excedido!")]
        public string CepDestino { get; set; }
        public string Comprimento { get; set; }
        public string Peso { get; set; }
        public decimal Largura { get; set; }
        public decimal Diametro { get; set; }
        public decimal Altura { get; set; }
        public string Preco { get; set; }
        public string PrazoMin { get; set; }
        public string PrazoMax { get; set; }
    }
}