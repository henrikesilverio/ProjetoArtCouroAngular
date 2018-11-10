using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Test.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoArtCouro.Test.Domain.Compras
{
    [TestClass]
    public class CompraUnitTest
    {
        [TestMethod]
        public void ValidarCompraSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var compra = new Compra();
                compra.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 6);
                Assert.IsTrue(mensagens.Any(x => x.Contains(
                    string.Format(Erros.FieldCannotBe, "DataCadastro", new DateTime()))),
                    "Falta mensagem data de cadastro não pode ser 01/01/1900");

                Assert.IsTrue(mensagens.Any(x => x.Contains(
                    string.Format(Erros.FieldCannotBe, "StatusCompra", StatusCompraEnum.None))),
                   "Falta mensagem status da compra não pode ser Nome");

                Assert.IsTrue(mensagens.Any(x => x.Contains(
                    string.Format(Erros.FieldCannotBeZero, "ValorTotalBruto"))),
                    "Falta mensagem valor do documento não pode ser zero");

                Assert.IsTrue(mensagens.Any(x => x.Contains(
                    string.Format(Erros.FieldCannotBeZero, "ValorTotalLiquido"))),
                    "Falta mensagem valor do documento não pode ser zero");

                Assert.IsTrue(mensagens.Any(x => x.Contains(Erros.PurchaseItemsNotInformed)),
                   "Falta mensagem itens de compra não pode ser vazia");
            }
        }

        [TestMethod]
        public void ValidarCompraComPropriedadesObrigatoriasPreenchidas()
        {
            var compra = new Compra()
            {
                DataCadastro = DateTime.Now,
                StatusCompra = StatusCompraEnum.Aberto,
                ValorTotalBruto = 1.0M,
                ValorTotalLiquido = 1.0M,
                ItensCompra = new List<ItemCompra>()
            };
            compra.Validar();
        }
    }
}
