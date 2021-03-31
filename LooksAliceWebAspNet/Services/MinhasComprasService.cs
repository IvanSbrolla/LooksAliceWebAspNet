using LooksAliceWebAspNet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LooksAliceWebAspNet.Services
{
    public class MinhasComprasService
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public List<Compras_Pedidos> ListarComprasPedidos(int? IdCompra)
        {
            var result = from obj in context.compras_Pedidos select obj;
            return result
                .Where(x => x.ComprasId == IdCompra)
                .Include(x => x.Compras)
                .Include(x => x.Pedidos)
                .Include(x => x.Pedidos.Produto)
                .Include(x => x.Pedidos.Produto.Cor)
                
                .ToList();
        }
    }
}