using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Test.Domain.Helpers;
using System;
using System.Linq;

namespace ProjetoArtCouro.Test.Domain.Contas
{
    [TestClass]
    public class ContaPagarUnitTest
    {
        [TestMethod]
        public void ValidarContaPagarSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var contaPagar = new ContaPagar();
                contaPagar.Validar();
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 4);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldCannotBe, "DataVencimento", new DateTime()))),
                    "Falta mensagem data de vencimento não pode ser 01/01/1900");

                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldCannotBeZero, "ValorDocumento"))),
                    "Falta mensagem valor do documento não pode ser zero");

                Assert.IsTrue(mensagens.Any(x => x.Contains(
                    string.Format(Erros.FieldCannotBe, "StatusContaPagar", StatusContaPagarEnum.None))),
                   "Falta mensagem status da conta a pagar não pode ser Nome");

                Assert.IsTrue(mensagens.Any(x => x.Contains(Erros.PurchaseNotSet)),
                   "Falta mensagem compra não pode ser vazia");
            }
        }

        [TestMethod]
        public void ValidarContaPagarComPropriedadesObrigatoriasPreenchidas()
        {
            var contaPagar = new ContaPagar
            {
                DataVencimento = DateTime.Now,
                ValorDocumento = 1.0M,
                StatusContaPagar = StatusContaPagarEnum.Aberto,
                Compra = new Compra { DataCadastro = DateTime.Now }
            };
            contaPagar.Validar();
        }
    }
}
