using LooksAliceWebAspNet.Models;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LooksAliceWebAspNet.Services
{
    public class ProdutoService
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public List<Produtos> ListagemRoupa()
        {
            var result = (from obj in context.produtos select obj);

            return result.Include(x => x.Categoria)
                .Include(x => x.Cor)
                .DistinctBy(x => x.Descricao)
                .OrderByDescending(x => x.Promocao).ToList();
        }
        public List<Produtos> ListagemRoupaCategoria(string categoria)
        {
            var result = from obj in context.produtos select obj;
            return result
                .Where(x => x.Categoria.Categoria_Nome == categoria)
                .Include(x => x.Categoria)
                .Include(x => x.Cor)
                .DistinctBy(x => x.Descricao)
                .ToList();
        }
        public List<Produtos> BuscarProduto(string Prod)
        {
            var result = from obj in context.produtos select obj;
            return result
                .Where(x => x.Descricao.Contains(Prod))
                .Include(x => x.Categoria)
                .Include(x => x.Cor)
                .DistinctBy(x => x.Descricao)
                .ToList();
        }
        public List<Produtos> GetInfoProduto(int Id)
        {
            if (string.IsNullOrEmpty(Id.ToString())) { return null; }
            else
            {
                var result = from obj in context.produtos select obj;

                return result.Where(x => x.ProdutoId == Id)
                    .Include(x => x.Categoria)
                    .Include(x => x.Cor)
                    .ToList();
            }
        }
        public List<Produtos> GetCoresProduto(string Descricao)
        {
            var result = from obj in context.produtos select obj;
            return result.Where(x => x.Descricao == Descricao).Include(x => x.Cor).DistinctBy(x => x.Cor.Cor_Nome).ToList();
        }
        public List<Produtos> GetTamanhosProduto(string Descricao)
        {
            var result = from obj in context.produtos select obj;
            return result.Where(x => x.Descricao == Descricao).DistinctBy(x => x.Tamanho).ToList();
        }
        public List<Produtos> ProdutosRecomendadosHome()
        {
            var result = context.produtos;
            return result.Where(x => x.RecomendadoHome == true).DistinctBy(x => x.Descricao).ToList();
        }
        public List<Produtos> ProdutosDestaque()
        {
            var result = from obj in context.produtos select obj;
            return result
                .Where(x => x.DestaqueHome == true)
                .ToList();
        }
        public List<Produtos> ProdutosRecomendadosCarrinho()
        {
            var result = context.produtos;
            return result.Where(x => x.RecomendadoCarrinho == true).DistinctBy(x => x.Descricao).ToList();
        }
    }
}