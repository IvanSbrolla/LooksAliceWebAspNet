using LooksAliceWebAspNet.Models.ViewModels;
using LooksAliceWebAspNet.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LooksAliceWebAspNet.Controllers
{
    public class MinhasComprasController : Controller
    {
        private MinhasComprasService minhasComprasService = new MinhasComprasService();

        // GET: MinhasCompras
        public ActionResult Index()
        {
            return RedirectToAction("Detalhes");
        }

        [Authorize]
        public ActionResult Detalhes(int? IdCompra, int? pagina)
        {
            if (IdCompra == 0 || IdCompra == null)
            {
                return RedirectToAction("MinhasCompras", "Compras");
            }
            else
            {
                int tamanhoPagina = 12;
                int numeroPagina = pagina ?? 1;
                ViewData["IdCompra"] = IdCompra;
                var Model = minhasComprasService.ListarComprasPedidos(IdCompra).ToPagedList(numeroPagina, tamanhoPagina);
                DetalhesViewModel model = new DetalhesViewModel
                {
                    Compras_Pedidos = Model
                };
                return View(model);
            }
        }
    }
}