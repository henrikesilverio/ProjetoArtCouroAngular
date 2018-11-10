using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Test.Domain.Helpers;
using System.Linq;

namespace ProjetoArtCouro.Test.Domain.Pagamentos
{
    [TestClass]
    public class CondicaoPagamentoUnitTest
    {
        [TestMethod]
        public void ValidarCondicaoPagamentoSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var condicaoPagamento = new CondicaoPagamento();
                condicaoPagamento.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 2);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "Descricao"))),
                    "Falta mensagem descrição obrigatório");

                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldCannotBeZero, "QuantidadeParcelas"))),
                    "Falta mensagem quantidade de parcelas não pode ser zero");
            }
        }

        [TestMethod]
        public void ValidarCondicaoPagamentoComPropriedadesObrigatoriasPreenchidas()
        {
            var condicaoPagamento = new CondicaoPagamento
            {
                Descricao = "1 + 1",
                QuantidadeParcelas = 1
            };
            condicaoPagamento.Validar();
        }

        [TestMethod]
        public void ValidarCondicaoPagamentoComDescricaoComMaisDe30Caracteres()
        {
            try
            {
                var condicaoPagamento = new CondicaoPagamento
                {
                    Descricao = new string('A', 31),
                    QuantidadeParcelas = 1
                };
                condicaoPagamento.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "Descricao", 30))),
                    "Falta mensagem descricao com mais de 30 caracteres");
            }
        }
    }
}
