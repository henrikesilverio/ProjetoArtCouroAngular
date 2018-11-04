using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Test.Domain.Helpers;
using System.Linq;

namespace ProjetoArtCouro.Test.Domain.Pagamentos
{
    [TestClass]
    public class FormaPagamentoUnitTest
    {
        [TestMethod]
        public void ValidarFormaPagamentoSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var formaPagamento = new FormaPagamento();
                formaPagamento.Validar();
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "Descricao"))),
                    "Falta mensagem descrição obrigatório");
            }
        }

        [TestMethod]
        public void ValidarFormaPagamentoComPropriedadesObrigatoriasPreenchidas()
        {
            var formaPagamento = new FormaPagamento
            {
                Descricao = "Cartão",
            };
            formaPagamento.Validar();
        }

        [TestMethod]
        public void ValidarFormaPagamentoComDescricaoComMaisDe30Caracteres()
        {
            try
            {
                var formaPagamento = new FormaPagamento
                {
                    Descricao = new string('A', 31),
                };
                formaPagamento.Validar();
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
