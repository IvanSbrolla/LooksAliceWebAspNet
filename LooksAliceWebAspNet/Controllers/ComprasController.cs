using LooksAliceWebAspNet.Json;
using LooksAliceWebAspNet.Models;
using LooksAliceWebAspNet.Services;
using LooksAliceWebAspNet.Xml;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using LooksAliceWebAspNet.Models.ViewModels;

namespace LooksAliceWebAspNet.Controllers
{
    public class ComprasController : Controller
    {

        private ComprasService compraService = new ComprasService();
        private CarrinhoService carrinhoService = new CarrinhoService();
        private CarrinhoService _carrinhoService = new CarrinhoService();

        // GET: Compras
        public ActionResult Index()
        {
            return RedirectToAction("MinhasCompras");
        }

        [Authorize]
        public ActionResult MinhasCompras(int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            var listCompras = compraService.ListarComprasDeUsuario(User.Identity.Name).ToPagedList(numeroPagina, tamanhoPagina);
            MinhasComprasViewModel model = new MinhasComprasViewModel
            {
                Compras = listCompras
            };
            ViewData["ContagemDeComprasUsuario"] = compraService.ContarComprasDeUsuario(User.Identity.Name).ToString();
            return View(model);
        }
        [Authorize]
        public ActionResult Payment(string paymentMethod)
        {
            if (!string.IsNullOrEmpty(paymentMethod))
            {
                ViewData["PaymentMethodSelected"] = paymentMethod.ToString();
                var resultPedidos = carrinhoService.ListarProdutosDoCarrinho(User.Identity.Name);
                var PrecoTotal = carrinhoService.SomaTotal(User.Identity.Name);
                List<Ano> ListAnos = new List<Ano>();
                for (int i = 1; i <= 12; i++)
                {
                    ListAnos.Add(new Ano() { AnoNum = i });
                };
                List<Mes> ListMes = new List<Mes>();
                int AnoAtual = DateTime.Today.Year;
                for (int i = AnoAtual; i <= (AnoAtual + 10); i++)
                {
                    ListMes.Add(new Mes() { MesNum = i });
                };
                PaymentViewModel ViewModel = new PaymentViewModel()
                {
                    Pedidos = resultPedidos,
                    Total = PrecoTotal,
                    Anos = ListAnos,
                    Meses = ListMes
                };
                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("ErrorSession");
            }
        }
        [Authorize]
        public ActionResult PaymentSuccess()
        {
            return View();
        }
        public ActionResult SumTotal(string PrecoDaCompraSemFrete, string PrecoFrete)
        {
            PrecoFrete = PrecoFrete.Replace(",",".");
            var total = (Convert.ToDouble(PrecoDaCompraSemFrete) + Convert.ToDouble(PrecoFrete)).ToString("F2");
            var result = new
            {
                Result = total
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ConsultarCep(string Cep)
        {
            try
            {
                WebRequest request = WebRequest.Create("https://viacep.com.br/ws/" + Cep + "/json/");
                request.ContentType = "application/json; charset-UTF-8";
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream receiveResponse = response.GetResponseStream();
                StreamReader reader = new StreamReader(receiveResponse, Encoding.UTF8);
                var jsonResult = JsonConvert.DeserializeObject<ViaCepJsonAux>(reader.ReadToEnd());
                var result = new
                {
                    Status = "Sucesso",
                    Cep = jsonResult.cep,
                    Logradouro = jsonResult.logradouro,
                    Bairro = jsonResult.bairro,
                    Localidade = jsonResult.localidade,
                    Uf = jsonResult.uf
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                var result = new
                {
                    Status = "Erro"
                };

                return Json(result);
            }


        }
        public ActionResult CalcFrete(string CepDestino)
        {
            try
            {
                var resultContagemProdutos = _carrinhoService.ContarPedidosDoCarrinho(User.Identity.Name);
                CorreioService correioService = new CorreioService(CepDestino, ( 0.200 * resultContagemProdutos).ToString().Replace(",", "."), 30, 1 * resultContagemProdutos, 30, 30);
                correioService.CalcPrecoPrazo();
                var result = new
                {
                    Preco = correioService.Preco,
                    PrazoMin = correioService.PrazoEntrega,
                    PrazoMax = Convert.ToString(Convert.ToInt32(correioService.PrazoEntrega) + 7),
                    Result = "sucesso"

                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                var result = new
                {
                    Status = "erro"
                };
                return Json(result);

            }

        }
        public ActionResult GetSomaSemFrete()
        {
            var PrecoTotal = Convert.ToDouble(carrinhoService.SomaTotal(User.Identity.Name)).ToString("F2");

            var result = new
            {
                Result = PrecoTotal
            };
            return Json(result);
        }
        [Authorize]
        public ActionResult PaymentMethods()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GerarSessao()
        {
            try
            {
                WebRequest request = WebRequest.CreateHttp("https://ws.pagseguro.uol.com.br/v2/sessions?email=looksalice.adm@gmail.com&token=062392d8-2fdc-452b-ac1d-df7dbae6ad43220b81ee47f29c2eea94ff46991bec27c092-1640-4c3c-be51-eb15ea22f18a");
                request.Method = "POST";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveResponse = response.GetResponseStream();
                    StreamReader reader = new StreamReader(receiveResponse, Encoding.UTF8);
                    XmlDocument xml = new XmlDocument();
                    xml.Load(reader);
                    XmlNode session = xml.SelectSingleNode("//session");
                    var id = session.SelectNodes("//id");
                    var result = new
                    {
                        Result = "sucesso",
                        Id = id[0].InnerText
                    };
                    return Json(result);
                }
                else
                {
                    var result = new
                    {
                        Result = "erro"
                    };
                    return Json(result);
                }
            }
            catch (Exception)
            {
                var result = new
                {
                    Result = "erro"
                };

                return Json(result);
            }
        }
        public ActionResult ErrorSession()
        {
            return View();
        }
        public JsonResult VerificarEndereco(string NomeDestinatario, string Cep, string Endereco, string NumeroResidencial, string Bairro, string Cidade, string Uf, string ddd, string telefone)
        {
            if (!(string.IsNullOrEmpty(NomeDestinatario)) &&
                !(string.IsNullOrEmpty(Cep)) &&
                !(string.IsNullOrEmpty(Endereco)) &&
                !(string.IsNullOrEmpty(NumeroResidencial)) &&
                !(string.IsNullOrEmpty(Bairro)) &&
                !(string.IsNullOrEmpty(Cidade)) &&
                !(string.IsNullOrEmpty(Uf)) &&
                !(string.IsNullOrEmpty(ddd)) &&
                !(string.IsNullOrEmpty(telefone)))
            {
                var jsonResult2 = new
                {
                    Result = "sucesso"
                };
                return Json(jsonResult2, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var jsonResult2 = new
                {
                    Result = "error"
                };
                return Json(jsonResult2, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult VerificarEnderecoCobranca(string Cep, string Endereco, string NumeroResidencial, string Bairro, string Cidade, string Uf, string Ddd, string Telefone, string Email, string DataNascimento)
        {
            if (!(string.IsNullOrEmpty(Cep)) &&
                !(string.IsNullOrEmpty(Endereco)) &&
                !(string.IsNullOrEmpty(NumeroResidencial)) &&
                !(string.IsNullOrEmpty(Bairro)) &&
                !(string.IsNullOrEmpty(Cidade)) &&
                !(string.IsNullOrEmpty(Uf)) &&
                !(string.IsNullOrEmpty(Ddd)) &&
                !(string.IsNullOrEmpty(Telefone)) &&
                !(string.IsNullOrEmpty(Email)) &&
                !(string.IsNullOrEmpty(DataNascimento)))
            {
                var date = DateTime.ParseExact(DataNascimento, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var stringDate = date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                var jsonResult2 = new
                {
                    Result = "sucesso",
                    dataNascimento = stringDate
                };
                return Json(jsonResult2, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var jsonResult2 = new
                {
                    Result = "error"
                };
                return Json(jsonResult2, JsonRequestBehavior.AllowGet);
            }
        }
        //alterar url
        [HttpPost]
        public ActionResult Checkout(string senderName, string senderEmail, string senderHash, string phoneNumber, string phoneDDD, string documentCpfNumber, string fretePreco,
            string ruaEndereco, string numeroEndereco, string complementoEndereco, string distritoEndereco, string cidadeEndereco, string ufEndereco, string cepEndereco, string tokenCard,
            string qntdParcela, string valorParcela, string maxInstallmentNoInterest, string nomeCartao, string cpfCartao, string dataNascimentoCartao, string phoneCartao, string dddPhoneCartao,
            string ruaCobranca, string numeroCobranca, string complementoCobranca, string distritoCobranca, string cidadeCobranca, string estadoCobranca, string cepCobranca, string precoTotal)
        {

            try
            {
                string xmlString;
                MemoryStream stream = new MemoryStream();
                ComprasService _serviceCompra = new ComprasService();
                var idCompra = _serviceCompra.GetIdVenda();
                var paymentModel = new payment()
                {
                    mode = "default",
                    method = "creditCard",
                    sender = new sender
                    {
                        name = senderName,
                        email = senderEmail,
                        phone = new phone
                        {
                            areaCode = phoneDDD,
                            number = phoneNumber
                        },
                        documents = new List<document>
                        {

                            new document
                            {
                                type="CPF",
                                value=documentCpfNumber
                            }

                        },
                        hash = senderHash
                    },
                    currency = "BRL",
                    notificationURL = "https://looks-alice.com/Compras/GetStatus/",
                    items = _serviceCompra.PopularItems(User.Identity.Name),
                    extraAmount = "0.00",
                    reference = idCompra.ToString(),
                    shipping = new shipping
                    {
                        addressRequired = "true",
                        address = new address
                        {
                            street = ruaEndereco,
                            number = numeroEndereco,
                            complement = complementoEndereco,
                            district = distritoEndereco,
                            city = cidadeEndereco,
                            state = ufEndereco,
                            country = "BRA",
                            postalCode = cepEndereco
                        },
                        type = 2,
                        cost = fretePreco
                    },
                    creditCard = new creditCard
                    {
                        token = tokenCard,
                        installment = new installment
                        {
                            quantity = qntdParcela,
                            value = valorParcela,
                            noInterestInstallmentQuantity = maxInstallmentNoInterest
                        },
                        holder = new holder
                        {
                            name = nomeCartao,
                            documents = new List<document>
                            {
                                new document
                                {
                                    type = "CPF",
                                    value = cpfCartao
                                }
                            },
                            birthDate = dataNascimentoCartao,
                            phone = new phone
                            {
                                areaCode = dddPhoneCartao,
                                number = phoneCartao
                            }
                        },
                        billingAddress = new billingAddress
                        {
                            street = ruaCobranca,
                            number = numeroCobranca,
                            complement = complementoCobranca,
                            district = distritoCobranca,
                            city = cidadeCobranca,
                            state = estadoCobranca,
                            country = "BRA",
                            postalCode = cepCobranca
                        }
                    }
                };

                var xml = new XmlSerializer(paymentModel.GetType());
                XmlTextWriter xmlTextWriter = new XmlTextWriter(stream, Encoding.UTF8);
                xml.Serialize(xmlTextWriter, paymentModel);
                stream = (MemoryStream)xmlTextWriter.BaseStream;
                xmlString = Encoding.UTF8.GetString(stream.ToArray());
                byte[] byteArray = Encoding.UTF8.GetBytes(xmlString);

                WebRequest request = WebRequest.Create("https://ws.pagseguro.uol.com.br/v2/transactions/?email=looksalice.adm@gmail.com&token=062392d8-2fdc-452b-ac1d-df7dbae6ad43220b81ee47f29c2eea94ff46991bec27c092-1640-4c3c-be51-eb15ea22f18a");
                request.Method = "POST";
                request.ContentLength = byteArray.Length;
                request.ContentType = "application/xml; charset=UTF-8";
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveResponse = response.GetResponseStream();
                    StreamReader reader = new StreamReader(receiveResponse, Encoding.UTF8);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(reader);
                    XmlNode session = xmlDoc.SelectSingleNode("//transaction");
                    var codeT = session.SelectNodes("//code");
                    var codeResult = codeT[0].InnerText.ToString();
                    compraService.checkoutCred(User.Identity.Name, Convert.ToDouble(precoTotal), ruaEndereco, cepEndereco, ufEndereco, numeroEndereco, codeResult);
                    LooksAliceWebAspNet.Services.EmailService _emailService = new LooksAliceWebAspNet.Services.EmailService();
                }
                return Json(response);
            }
            catch (Exception e)
            {

                return Json(e);
            }



        }
        //alterar url
        [HttpPost]
        public ActionResult GetStatus(string notificationCode, string notificationType)
        {

            WebRequest request = WebRequest.Create("https://ws.pagseguro.uol.com.br/v3/transactions/notifications/" + notificationCode + "/?email=looksalice.adm@gmail.com&token=062392d8-2fdc-452b-ac1d-df7dbae6ad43220b81ee47f29c2eea94ff46991bec27c092-1640-4c3c-be51-eb15ea22f18a");
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveResponse = response.GetResponseStream();
                StreamReader reader = new StreamReader(receiveResponse, Encoding.UTF8);
                XmlDocument xml = new XmlDocument();
                xml.Load(reader);
                XmlNode session = xml.SelectSingleNode("//transaction");
                var reference = session.SelectNodes("//reference");
                var status = session.SelectNodes("//status");
                var result = new
                {
                    result = "sucesso",
                    reference = reference[0].InnerText,
                    status = status[0].InnerText
                };
                compraService.UpdateCompra_StatusCompra(Convert.ToInt32(result.reference), Convert.ToInt32(result.status));
                return null;
            }
            else
            {
                var result = new
                {
                    result = "sucesso"
                };
                return null;
            }
        }
        public ActionResult Termos()
        {
            return View();
        }
    }
}