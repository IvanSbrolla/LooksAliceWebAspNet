using LooksAliceWebAspNet.Models;
using LooksAliceWebAspNet.Services;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LooksAliceWebAspNet.Models.ViewModels;

namespace LooksAliceWebAspNet.Controllers
{
    public class RoupasController : Controller
    {
        private ProdutoService _produtoService = new ProdutoService();
        private ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CalcFrete(string cep)
        {
            try
            {
                CorreioService correioService = new CorreioService(cep, "0.200", 30, 1, 30, 30);
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
            catch (Exception e)
            {
                var result = new { errorMessage = e.Message };
                return Json(result);
            }

        }
        public ActionResult Roupa(int Id, string Descricao)
        {
            var dbProduto = from obj in context.produtos select obj;
            var aux = from obj in context.produtos select obj;
            var list = _produtoService.GetInfoProduto(Id);
            var listCores = _produtoService.GetCoresProduto(Descricao);
            var listTamanhos = _produtoService.GetTamanhosProduto(Descricao);
            var modelCorreio = new Correio();
            SelectList selectListTamanhos = new SelectList(dbProduto.Where(x => x.Descricao == Descricao).DistinctBy(x => x.Tamanho), "Tamanho", "Tamanho");
            ViewBag.listTamanho = selectListTamanhos;

            SelectList selectListCores = new SelectList(dbProduto.Include(x => x.Cor).Where(x => x.Descricao == Descricao).DistinctBy(x => x.Cor.Cor_Nome), "CorId", "Cor.Cor_Nome");
            ViewBag.listCores = selectListCores;
            var ViewModel = new EscolhaDeProdutoViewModel { Produtos = list, ProdutosCores = listCores, ProdutosTamanhos = listTamanhos, Correio = modelCorreio };
            if (list != null)
            {
                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("RoupaError");
            }
        }
        public ActionResult RoupaError()
        {
            return View();
        }
       
    }
}