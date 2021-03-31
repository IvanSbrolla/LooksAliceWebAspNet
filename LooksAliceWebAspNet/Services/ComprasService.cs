using LooksAliceWebAspNet.Models;
using LooksAliceWebAspNet.Xml;
using MoreLinq;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LooksAliceWebAspNet.Services
{
    public class ComprasService
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        public int GetIdVenda()
        {
            var result = from obj in context.compras select obj;
            int resultContagem = result.Count();
            if (resultContagem > 0)
            {
                int resultId = result.OrderByDescending(x => x.CompraId).Select(x => x.CompraId).First();
                return resultId + 1;
            }
            else return 0 + 1;
        }
        public List<Compras> ListarComprasDeUsuario(string UserName)
        {
            var result = from obj in context.compras select obj;
            return result
                .Where(x => x.UserName == UserName)
                .ToList();
        }
        private List<Pedidos> ListarPedidos(string UserName)
        {
            var result = from obj in context.pedidos select obj;
            return result.Include(x => x.Produto)
                .Where(x => x.AppUserName == UserName && x.Concluido == false)
                .ToList();
        }
        public List<item> PopularItems(string UserName)
        {
            var resultProdutos = ListarPedidos(UserName);
            List<item> items = new List<item>();
            try
            {
                foreach (var x in resultProdutos)
                {
                    if (x.Produto.Promocao == false)
                    {
                        items.Add(new item
                        {
                            id = x.Produto.ProdutoId.ToString(),
                            amount = x.Produto.Preco.ToString("F2").Replace(",", "."),
                            description = x.Produto.Descricao,
                            quantity = x.Produto_Qntd.ToString()
                        });
                    }
                    else
                    {
                        items.Add(new item
                        {
                            id = x.Produto.ProdutoId.ToString(),
                            amount = x.Produto.Preco_Promocao.ToString("F2").Replace(",", "."),
                            description = x.Produto.Descricao,
                            quantity = x.Produto_Qntd.ToString()
                        });
                    }
                }
            }
            catch (Exception e)
            {


            }


            return items;
        }
        public void AddNovaCompra(string username, double preco, string logradouro, string cep, string uf, string numeroResidencial, string codeTransacao)
        {
            //add nova compra
            context.compras.Add(
                new Compras
                {
                    Data = DateTime.Now,
                    Forma_Pagamento = "CRED",
                    Preco = preco,
                    Logradouro = logradouro,
                    Cep = cep,
                    Uf = uf,
                    NumeroResidencial = numeroResidencial,
                    UserName = username,
                    CodTransacao = codeTransacao,
                    StatusEnvio = "Em analise" 
                });
            context.SaveChanges();
        }
        public int GetLastCompraId()
        {
            var result = from obj in context.compras select obj;

            return result.OrderByDescending(x => x.CompraId).Select(x => x.CompraId).FirstOrDefault(); ;
        }
        public List<Compras_Pedidos> GetCompra(int compraId)
        {
            var result = from obj in context.compras_Pedidos select obj;
            return result
                .Where(x => x.ComprasId == compraId)
                .Include(x => x.Compras)
                .Include(x => x.Pedidos)
                .Include(x => x.Pedidos.Produto)
                .ToList();
        }
        public void checkoutCred(string username, double preco, string logradouro, string cep, string uf, string numeroResidencial, string codeTransacao)
        
        {
            AddNovaCompra(username, preco, logradouro, cep, uf, numeroResidencial, codeTransacao); 
            //obj compras
            var _compras = from obj in context.compras select obj;
            //get ultimo id de compra
            int GET_CompraId = GetLastCompraId();
            //obj pedidos
            var _pedidos = from obj in context.pedidos select obj;
            //get pedidos de usuario
            var GET_PedidosUsuario = _pedidos.Where(x => x.AppUserName == username && x.Concluido == false).ToList();
            foreach (var item in GET_PedidosUsuario)
            {
                //adiciona pedidoid e compraid na tabela compras_pedidos
                context.compras_Pedidos.Add(new Compras_Pedidos
                {
                    ComprasId = GET_CompraId,
                    PedidosId = item.IdPedido
                });
                context.SaveChanges();
                //atualiza tabela de pedidos
                item.Concluido = true;
                context.SaveChanges();
            }
            //add compraid e statusid na Compra_statusCompra
            context.compra_statusCompra.Add(new Compra_StatusCompra
            {
                ComprasId = GET_CompraId,
                StatusCompraId = 1
            });
            context.SaveChanges();
        }
        public void UpdateCompra_StatusCompra(int idCompra, int statusCompra)
        {
            var result = from obj in context.compra_statusCompra select obj;
            var Obj = result.Include(x => x.Compras).Include(x => x.StatusCompra).Where(x => x.ComprasId == idCompra).FirstOrDefault();
            Obj.StatusCompraId = statusCompra;
            context.SaveChanges();
        }
        public int ContarComprasDeUsuario(string username)
        {
            var result = from obj in context.compras select obj;
            return result
                .Where(x => x.UserName == username)
                .Count();
        }
    }
}