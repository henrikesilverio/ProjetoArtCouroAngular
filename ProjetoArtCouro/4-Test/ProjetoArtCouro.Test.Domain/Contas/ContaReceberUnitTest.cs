using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Test.Domain.Helpers;
using System;
using System.Linq;

namespace ProjetoArtCouro.Test.Domain.Contas
{
    [TestClass]
    public class ContaReceberUnitTest
    {
        [TestMethod]
        public void ValidarContaReceberSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var contaReceber = new ContaReceber();
                contaReceber.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 4);
                Assert.IsTrue(mensagens.Any(x => x.Contains(
                    string.Format(Erros.FieldCannotBe, "DataVencimento", new DateTime()))),
                    "Falta mensagem data de vencimento não pode ser 01/01/1900");

                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldCannotBeZero, "ValorDocumento"))),
                    "Falta mensagem valor do documento não pode ser zero");

                Assert.IsTrue(mensagens.Any(x => x.Contains(
                    string.Format(Erros.FieldCannotBe, "StatusContaReceber", StatusContaReceberEnum.None))),
                   "Falta mensagem status da conta a receber não pode ser Nome");

                Assert.IsTrue(mensagens.Any(x => x.Contains(Erros.SaleNotSet)),
                   "Falta mensagem venda não pode ser vazia");
            }
        }

        [TestMethod]
        public void ValidarContaReceberComPropriedadesObrigatoriasPreenchidas()
        {
            var contaReceber = new ContaReceber()
            {
                DataVencimento = DateTime.Now,
                ValorDocumento = 1.0M,
                StatusContaReceber = StatusContaReceberEnum.Aberto,
                Venda = new Venda { DataCadastro = DateTime.Now }
            };
            contaReceber.Validar();
        }
    }
}
