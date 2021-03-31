using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LooksAliceWebAspNet.Models
{
    public class Pedidos
    {
        [Key]
        public int IdPedido { get; set; }
        public string AppUserName { get; set; }
        public Produtos Produto { get; set; }
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "Insira uma quantidade!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Insira um numero válido!")]
        [Display(Name = "Quantidade")]
        [Range(1, 1000, ErrorMessage = "Insira uma quantidade válida!")]
        public int Produto_Qntd { get; set; }
        public bool Concluido { get; set; }
        public double Preco { get; set; }
    }
}