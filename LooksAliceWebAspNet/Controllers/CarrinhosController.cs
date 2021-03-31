using LooksAliceWebAspNet.Models;
using LooksAliceWebAspNet.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LooksAliceWebAspNet.Models.ViewModels;

namespace LooksAliceWebAspNet.Controllers
{
    public class CarrinhosController : Controller
    {
        private ProdutoService _produtoService = new ProdutoService();
        private CarrinhoService _carrinhoService = new CarrinhoService();
        private ApplicationDbContext context = new ApplicationDbContext();
        // GET: Pedidos
        [Authorize]
        public ActionResult Index(int? pagina)
        {
            int tamanhoPagina = 12;
            int numeroPagina = pagina ?? 1;
            var list = _produtoService.ProdutosRecomendadosCarrinho();
            var result = _carrinhoService.ListarProdutosDoCarrinho(User.Identity.Name).ToPagedList(numeroPagina, tamanhoPagina);
            var resultContagemProdutos = _carrinhoService.ContarPedidosDoCarrinho(User.Identity.Name);
            ViewData["resultContagemProdutos"] = resultContagemProdutos.ToString();
            var ViewModel = new CarrinhoViewModel { Pedidos = result, Produtos = list };
            return View(ViewModel);
        }
        public ActionResult CalcFrete(string cep)
        {
            var resultContagemProdutos = _carrinhoService.ContarPedidosDoCarrinho(User.Identity.Name);
            CorreioService correioService = new CorreioService(cep, (0.200 * resultContagemProdutos).ToString().Replace(",", "."), 30, 1 * resultContagemProdutos, 30, 30);
            correioService.CalcPrecoPrazo();
            if (correioService.Preco == "0,00")
            {
                var result = new
                {
                    Preco = Convert.ToDouble(correioService.Preco).ToString("F2"),
                    PrazoMin = correioService.PrazoEntrega,
                    PrazoMax = Convert.ToString(Convert.ToInt32(correioService.PrazoEntrega) + 3),
                    Result = "erro"

                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new
                {
                    Preco = correioService.Preco,
                    PrazoMin = correioService.PrazoEntrega,
                    PrazoMax = Convert.ToString(Convert.ToInt32(correioService.PrazoEntrega) + 7),
                    Result = "sucesso"

                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduto(string Descricao, bool Promocao, double Preco_Promocao, double Preco)
        {
            if (User.Identity.IsAuthenticated)
            {
                Pedidos obj = new Pedidos();
                string tamProduto = Request.Form["ProdutosTamanhos"].ToString();
                string corProduto = Request.Form["ProdutosCores"].ToString();
                obj.AppUserName = User.Identity.Name.ToString();
                obj.Produto = _carrinhoService.GetProdutoId(Descricao, corProduto, tamProduto);
                string qntd = Request.Form["Pedidos.Produto_Qntd"].ToString();
                obj.Produto_Qntd = Convert.ToInt32(qntd);
                obj.ProdutoId = obj.Produto.ProdutoId;
                obj.Concluido = false;
                double preco;
                if (Promocao == true)
                {
                    preco = Preco_Promocao;
                }
                else
                {
                    preco = Preco;
                }
                obj.Preco = obj.Produto_Qntd * preco;


                _carrinhoService.AddProduto(obj);
                return RedirectToAction("Index", "Carrinhos");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoverProduto(string pedidoId)
        {
            _carrinhoService.RemoverProduto(Convert.ToInt32(pedidoId));
            return RedirectToAction("Index");
        }
    }
}