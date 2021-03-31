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
    public class HomeController : Controller
    {
        private readonly ProdutoService _produtoService = new ProdutoService();
        private readonly LooksAliceWebAspNet.Services.EmailService emailService = new LooksAliceWebAspNet.Services.EmailService();
        private readonly AppUserService appService = new AppUserService();
        private readonly ProdutosEmailViewModel _produtoEmailViewModel = new ProdutosEmailViewModel();
        private readonly LayoutService _layoutService = new LayoutService();
        ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index()
        {

            var listRecomendados = _produtoService.ProdutosRecomendadosHome();
            var listDestaque = _produtoService.ProdutosDestaque();
            var ViewModel = new ProdutosEmailViewModel
            {
                Produtos = listRecomendados,
                ProdutosDestaque = listDestaque
            };
            if (User.Identity.IsAuthenticated)
            {

                var PrimeiroNome = appService.GetPrimeiroNome(User.Identity.Name);
                ViewData["PrimeiroNome"] = PrimeiroNome;
            }
            Email email = new Email();
            return View(ViewModel);
        }
        public ActionResult Roupas(int? pagina, string categoria)
        {
            int tamanhoPagina = 12;
            int numeroPagina = pagina ?? 1;
            if (string.IsNullOrEmpty(categoria))
            {
                return View(_produtoService.ListagemRoupa().ToPagedList(numeroPagina, tamanhoPagina));
            }
            else
            {
                return View(_produtoService.ListagemRoupaCategoria(categoria).ToPagedList(numeroPagina, tamanhoPagina));
            }

        }
        public ActionResult BuscarRoupa(int? pagina)
        {
            int tamanhoPagina = 12;
            int numeroPagina = pagina ?? 1;
            string Prod = Request.Form["txtBuscarProduto"].ToString();
            var list = _produtoService.BuscarProduto(Prod);
            return View("Roupas", list.ToPagedList(numeroPagina, tamanhoPagina));
        }

        public ActionResult EnviarEmail(string Assunto, string EmailRemetente,string NomeRemetente, string Menssagem, string TelefoneRemetente)
        {

            if (emailService.Verificacao(Assunto, NomeRemetente, Menssagem, EmailRemetente, TelefoneRemetente) == true)
            {
                if(emailService.EnviarEmail(Assunto, NomeRemetente, Menssagem, EmailRemetente, TelefoneRemetente) == true)
                {
                    var result = new
                    {
                        result = "sucesso"
                    };
                    return Json(result,JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new
                    {
                        result = "erro"
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var result = new
                {
                    result = "erro"
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetCategorias()
        {
            var result = _layoutService.GetCategorias();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}