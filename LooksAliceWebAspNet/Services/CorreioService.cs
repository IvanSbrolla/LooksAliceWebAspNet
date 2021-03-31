using LooksAliceWebAspNet.ServiceCorreios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LooksAliceWebAspNet.Services
{
    public class CorreioService
    {
        public string Preco { get; set; }
        public string PrazoEntrega { get; set; }
        private string _CepDestino { get; set; }
        private string _Peso { get; set; }
        private decimal _Comprimento { get; set; }
        private decimal _Altura { get; set; }
        private decimal _Largura { get; set; }
        private decimal _Diametro { get; set; }

        public int MyProperty { get; set; }
        public CorreioService(string CepDestino, string Peso, decimal Comprimento, decimal Altura, decimal Largura, decimal Diametro)
        {
            _Comprimento = Comprimento;
            _Peso = Peso;
            _CepDestino = CepDestino;
            _Altura = Altura;
            _Largura = Largura;
            _Diametro = Diametro;
        }
        public void CalcPrecoPrazo()
        {
            string nCdEmpresa = string.Empty;
            string sDsSenha = string.Empty;
            string nCdServico = "04014";
            string sCepOrigem = "03057010";
            string sCepDestino = _CepDestino;
            string nVlPeso = _Peso;
            int nCdFormato = 1;
            decimal nVlComprimento = _Comprimento;
            decimal nVlAltura = _Altura;
            decimal nVlLargura = _Largura;
            decimal nVlDiametro = _Diametro;
            string sCdMaoPropria = "N";
            decimal nVlValorDeclarado = 0;
            string sCdAvisoRecebimento = "N";

            CalcPrecoPrazoWSSoapClient SRCorreios = new CalcPrecoPrazoWSSoapClient();

            
            cResultado returnCorreios = SRCorreios.CalcPrecoPrazo(nCdEmpresa, sDsSenha, nCdServico, sCepOrigem, sCepDestino, nVlPeso, nCdFormato, nVlComprimento,
                nVlAltura, nVlLargura, nVlDiametro, sCdMaoPropria, nVlValorDeclarado, sCdAvisoRecebimento);


            PrazoEntrega = returnCorreios.Servicos[0].PrazoEntrega;

            Preco = returnCorreios.Servicos[0].Valor;
        }
    }
}