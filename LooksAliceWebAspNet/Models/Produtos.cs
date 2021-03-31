using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace LooksAliceWebAspNet.Models
{
    public class Produtos
    {
        [Key]
        public int ProdutoId { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public Tecidos Tecido { get; set; }
        public int TecidoId { get; set; }
        public string Tamanho { get; set; }
        public Cores Cor { get; set; }
        public int CorId { get; set; }
        public byte[] Imagem { get; set; }
        public bool Promocao { get; set; }
        public bool DestaqueHome { get; set; }
        public double Preco_Promocao { get; set; }
        public Categorias Categoria { get; set; }
        public int CategoriaId { get; set; }
        public bool RecomendadoHome { get; set; }
        public bool RecomendadoCarrinho { get; set; }
    }
}