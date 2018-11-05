using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Produtos;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Test.Domain.Helpers;
using System.Linq;

namespace ProjetoArtCouro.Test.Domain.Produtos
{
    [TestClass]
    public class ProdutoUnitTest
    {
        [TestMethod]
        public void ValidarProdutoSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var produto = new Produto();
                produto.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 4);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "ProdutoNome"))),
                    "Falta mensagem nome do produto obrigatório");

                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldCannotBeZero, "PrecoVenda"))),
                    "Falta mensagem preço da venda não pode ser zero");

                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldCannotBeZero, "PrecoCusto"))),
                    "Falta mensagem preço da custo não pode ser zero");

                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldCannotBeNull, "Unidade"))),
                    "Falta mensagem unidade não pode ser nulo");
            }
        }

        [TestMethod]
        public void ValidarProdutoComPropriedadesObrigatoriasPreenchidas()
        {
            var produto = new Produto
            {
                ProdutoNome = "Cinto",
                PrecoCusto = 10.00M,
                PrecoVenda = 12.00M,
                Unidade = new Unidade { UnidadeNome = "UN" }
            };
            produto.Validar();
        }

        [TestMethod]
        public void ValidarProdutoComDescricaoComMaisDe200Caracteres()
        {
            try
            {
                var produto = new Produto
                {
                    ProdutoNome = new string('A', 201),
                    PrecoCusto = 10.00M,
                    PrecoVenda = 12.00M,
                    Unidade = new Unidade { UnidadeNome = "UN" }
                };
                produto.Validar();
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
