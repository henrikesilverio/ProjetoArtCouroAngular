using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Test.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoArtCouro.Test.Domain.Vendas
{
    [TestClass]
    public class VendaUnitTest
    {
        [TestMethod]
        public void ValidarVendaSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var venda = new Venda();
                venda.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 5);
                Assert.IsTrue(mensagens.Any(x => x.Contains(
                    string.Format(Erros.FieldCannotBe, "DataCadastro", new DateTime()))),
                    "Falta mensagem data de cadastro não pode ser 01/01/1900");

                Assert.IsTrue(mensagens.Any(x => x.Contains(
                    string.Format(Erros.FieldCannotBe, "StatusVenda", StatusVendaEnum.None))),
                   "Falta mensagem status da venda não pode ser Nome");

                Assert.IsTrue(mensagens.Any(x => x.Contains(
                    string.Format(Erros.FieldCannotBeZero, "ValorTotalBruto"))),
                    "Falta mensagem valor do total bruto não pode ser zero");

                Assert.IsTrue(mensagens.Any(x => x.Contains(
                    string.Format(Erros.FieldCannotBeZero, "ValorTotalLiquido"))),
                    "Falta mensagem valor do total liquido não pode ser zero");

                Assert.IsTrue(mensagens.Any(x => x.Contains(Erros.SaleItemsNotSet)),
                   "Falta mensagem itens de venda não pode ser vazio");
            }
        }

        [TestMethod]
        public void ValidarVendaComPropriedadesObrigatoriasPreenchidas()
        {
            var venda = new Venda()
            {
                DataCadastro = DateTime.Now,
                StatusVenda = StatusVendaEnum.Aberto,
                ValorTotalBruto = 1.0M,
                ValorTotalLiquido = 1.0M,
                ItensVenda = new List<ItemVenda>()
            };
            venda.Validar();
        }
    }
}
