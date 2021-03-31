using LooksAliceWebAspNet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LooksAliceWebAspNet.Services
{
    public class CarrinhoService
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public List<Pedidos> ListarProdutosDoCarrinho(string UserName)
        {
            var result = from obj in context.pedidos select obj;
            return result.Include(x => x.Produto)
                .Where(x => x.AppUserName == UserName && x.Concluido == false)
                .ToList();
        }
        public double SomaTotal(string UserName)
        {
            var result = from obj in context.pedidos select obj;
            return result
                .Where(x => x.AppUserName == UserName && x.Concluido == false)
                .Sum(x => x.Preco);
        }
        public int ContarPedidosDoCarrinho(string UserName)
        {
            var result = from obj in context.pedidos select obj;
            var verificacaoResult = result.Where(x => x.Concluido == false && x.AppUserName == UserName).Count();
            if (verificacaoResult != 0)
            {
                return result
                    .Where(x => x.AppUserName == UserName && x.Concluido == false)
                    .Sum(x => x.Produto_Qntd);
            }
            else
            {
                return 0;
            }
        }
        public void AddProduto(Pedidos pedidos)
        {
            context.pedidos.Add(pedidos);
            context.SaveChanges();
        }
        public void RemoverProduto(int pedidoId)
        {
            var result = from obj in context.pedidos select obj;
            context.pedidos.Remove(result.Where(x => x.IdPedido == pedidoId).FirstOrDefault());
            context.SaveChanges();
        }
        public Produtos GetProdutoId(string Descricao, string Cor, string Tamanho)
        {
            var result = from obj in context.produtos select obj;
            return result
                .Include(x => x.Cor)
                .Where(x => x.Descricao == Descricao && x.Cor.Cor_Nome == Cor && x.Tamanho == Tamanho)
                .FirstOrDefault();

        }
        public List<ApplicationUser> GetUserId(string UserName)
        {
            var result = from obj in context.Users select obj;
            return result
                .Where(x => x.UserName == UserName)
                .ToList();
        }
    }
}