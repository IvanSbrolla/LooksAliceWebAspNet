using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LooksAliceWebAspNet.Models.ViewModels
{

    public class ProdutosEmailViewModel
    {
        public Email Email { get; set; }
        public IEnumerable<Produtos> Produtos { get; set; }
        public IEnumerable<Produtos> ProdutosDestaque { get; set; }
    }
    public class EscolhaDeProdutoViewModel
    {
        public Correio Correio { get; set; }
        public Pedidos Pedidos { get; set; }
        public IEnumerable<Produtos> Produtos { get; set; }
        public IEnumerable<Produtos> ProdutosCores { get; set; }
        public IEnumerable<Produtos> ProdutosTamanhos { get; set; }
    }
    public class CarrinhoViewModel
    {
        public IPagedList<Pedidos> Pedidos { get; set; }
        public Correio Correio { get; set; }
        public IEnumerable<Produtos> Produtos { get; set; }

    }

    public class DetalhesViewModel
    {
        public IPagedList<Compras_Pedidos> Compras_Pedidos { get; set; }
    }

    public class PaymentViewModel
    {
        public IEnumerable<Pedidos> Pedidos { get; set; }
        public IEnumerable<Produtos> Produtos { get; set; }
        public Correio Correio { get; set; }
        public Card Card { get; set; }
        public IEnumerable<Ano> Anos { get; set; }
        public IEnumerable<Mes> Meses { get; set; }
        public double Total { get; set; }

    }

    public class MinhasComprasViewModel
    {
        public IPagedList<Compras> Compras { get; set; }
    }

}