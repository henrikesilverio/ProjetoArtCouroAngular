using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Produtos;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Test.Domain.Helpers;
using System.Linq;

namespace ProjetoArtCouro.Test.Domain.Produtos
{
    [TestClass]
    public class UnidadeUnitTest
    {
        [TestMethod]
        public void ValidarUnidadeSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var unidade = new Unidade();
                unidade.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 2);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "UnidadeNome"))),
                    "Falta mensagem nome da unidade obrigatório");

                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldCannotBeZero, "UnidadeCodigo"))),
                    "Falta mensagem codigo da unidade não pode ser zero");
            }
        }

        [TestMethod]
        public void ValidarUnidadeComPropriedadesObrigatoriasPreenchidas()
        {
            var unidade = new Unidade
            {
                UnidadeCodigo = 1,
                UnidadeNome = "KG"
            };
            unidade.Validar();
        }

        [TestMethod]
        public void ValidarUnidadeComDescricaoComMaisDe30Caracteres()
        {
            try
            {
                var unidade = new Unidade
                {
                    UnidadeCodigo = 1,
                    UnidadeNome = new string('A', 31)
                };
                unidade.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "UnidadeNome", 30))),
                    "Falta mensagem nome da unidade com mais de 30 caracteres");
            }
        }
    }
}
