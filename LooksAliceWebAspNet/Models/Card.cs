using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LooksAliceWebAspNet.Models
{
    public class Card
    {
        [DataType(DataType.CreditCard)]
        [Required(ErrorMessage = "*Campo Obrigatorio!*")]
        public string NumCard { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "*Campo Obrigatorio!*")]
        public string NomeNoCartao { get; set; }
        [Required(ErrorMessage = "*Campo Obrigatorio!*")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "*Campo Obrigatorio!*")]
        public int MesValidade { get; set; }
        [Required(ErrorMessage = "*Campo Obrigatorio!*")]
        public int AnoValidade { get; set; }
        [Required(ErrorMessage = "*Campo Obrigatorio!*")]
        public int Cvv { get; set; }
        [Required(ErrorMessage = "*Campo Obrigatorio!*")]
        public string Parcelamento { get; set; }
    }
}