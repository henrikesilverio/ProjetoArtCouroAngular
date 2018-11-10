using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Test.Domain.Helpers;
using System.Linq;

namespace ProjetoArtCouro.Test.Domain.Compras
{
    [TestClass]
    public class ItemCompraUnitTest
    {
        [TestMethod]
        public void ValidarItemCompraSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var itemCompra = new ItemCompra();
                itemCompra.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 6);
                Assert.IsTrue(mensagens.Any(x => x.Contains(
                    string.Format(Erros.FieldCannotBeZero, "ProdutoCodigo"))),
                    "Falta mensagem codigo do produto não pode ser zero");

                Assert.IsTrue(mensagens.Any(x => x.Contains(
                    string.Format(Erros.FieldIsRequired, "ProdutoNome"))),
                   "Falta mensagem nome do produto é obrigatório");

                Assert.IsTrue(mensagens.Any(x => x.Contains(
                   string.Format(Erros.FieldCannotBeZero, "Quantidade"))),
                   "Falta mensagem quantidade não pode ser zero");

                Assert.IsTrue(mensagens.Any(x => x.Contains(
                   string.Format(Erros.FieldCannotBeZero, "PrecoVenda"))),
                   "Falta mensagem preço de venda não pode ser zero");

                Assert.IsTrue(mensagens.Any(x => x.Contains(
                   string.Format(Erros.FieldCannotBeZero, "ValorBruto"))),
                   "Falta mensagem valor bruto não pode ser zero");

                Assert.IsTrue(mensagens.Any(x => x.Contains(
                   string.Format(Erros.FieldCannotBeZero, "ValorLiquido"))),
                   "Falta mensagem valor liquido não pode ser zero");
            }
        }

        [TestMethod]
        public void ValidarItemCompraComPropriedadesObrigatoriasPreenchidas()
        {
            var itemCompra = new ItemCompra()
            {
                ProdutoCodigo = 1,
                ProdutoNome = "Cinto",
                Quantidade = 1,
                PrecoVenda = 1.0M,
                ValorBruto = 1.0M,
                ValorLiquido = 1.0M
            };
            itemCompra.Validar();
        }

        [TestMethod]
        public void ValidarItemCompraComNomeDoProdutoComMaisDe200Caracteres()
        {
            try
            {
                var itemCompra = new ItemCompra()
                {
                    ProdutoCodigo = 1,
                    ProdutoNome = new string('A', 201),
                    Quantidade = 1,
                    PrecoVenda = 1.0M,
                    ValorBruto = 1.0M,
                    ValorLiquido = 1.0M
                };
                itemCompra.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "ProdutoNome", 200))),
                    "Falta mensagem nome do produto com mais de 200 caracteres");
            }
        }
    }
}
