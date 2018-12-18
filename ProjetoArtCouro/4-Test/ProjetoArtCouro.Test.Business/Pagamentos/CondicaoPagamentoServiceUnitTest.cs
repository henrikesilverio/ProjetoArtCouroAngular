using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjetoArtCouro.Business.PagamentoService;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPagamento;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.CondicaoPagamento;
using ProjetoArtCouro.Mapping.Configs;
using System.Collections.Generic;

namespace ProjetoArtCouro.Test.Business.Pagamentos
{
    [TestClass]
    public class CondicaoPagamentoServiceUnitTest
    {
        private CondicaoPagamentoService _condicaoPagamentoService;
        private Mock<ICondicaoPagamentoRepository> _condicaoPagamentoRepositoryMock;

        [TestInitialize]
        public void Inicializacao()
        {
            _condicaoPagamentoRepositoryMock = new Mock<ICondicaoPagamentoRepository>();
            _condicaoPagamentoService = new CondicaoPagamentoService(_condicaoPagamentoRepositoryMock.Object);
            MapperConfig.RegisterMappings();
        }

        [TestMethod]
        public void ObterListaCondicaoPagamento_DadosExistentes_RetornaLista()
        {
            _condicaoPagamentoRepositoryMock
                .Setup(x => x.ObterLista())
                .Returns(new List<CondicaoPagamento>
                {
                    new CondicaoPagamento
                    {
                        CondicaoPagamentoCodigo = 1,
                        Descricao = "A vista",
                        QuantidadeParcelas = 1
                    }
                });

            var condicoesPagamento = _condicaoPagamentoService.ObterListaCondicaoPagamento();

            Assert.IsNotNull(condicoesPagamento, "Condições de pagamento não deveriam ser nulas");
            Assert.AreEqual(condicoesPagamento.Count, 1, "Quantidade de Condições de pagamento invalidas");
        }

        [TestMethod]
        public void ObterCondicaoPagamentoPorCodigo_DadosExistentes_RetornaObjeto()
        {
            _condicaoPagamentoRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new CondicaoPagamento
                {
                    CondicaoPagamentoCodigo = 1,
                    Descricao = "A vista",
                    QuantidadeParcelas = 1
                });

            var condicaoPagamento = _condicaoPagamentoService.ObterCondicaoPagamentoPorCodigo(1);

            Assert.IsNotNull(condicaoPagamento, "Condiçao de pagamento não deve ser nula");
            Assert.AreEqual(condicaoPagamento.CondicaoPagamentoCodigo, 1, "Condiçao de pagamento com código invalido");
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void CriarCondicaoPagamento_DadosInvalidos_Excecao()
        {
            _condicaoPagamentoRepositoryMock
                .Setup(x => x.Criar(It.IsAny<CondicaoPagamento>()))
                .Returns((CondicaoPagamento)null);

            var condicaoPagamento = _condicaoPagamentoService
                .CriarCondicaoPagamento(new CondicaoPagamentoModel());
        }

        [TestMethod]
        public void CriarCondicaoPagamento_DadosValidos_RetornaObjeto()
        {
            _condicaoPagamentoRepositoryMock
                .Setup(x => x.Criar(It.IsAny<CondicaoPagamento>()))
                .Returns(new CondicaoPagamento
                {
                    CondicaoPagamentoCodigo = 1,
                    Descricao = "A vista",
                    QuantidadeParcelas = 1
                });

            var condicaoPagamento = _condicaoPagamentoService
                .CriarCondicaoPagamento(new CondicaoPagamentoModel
                {
                    Ativo = true,
                    Descricao = "A vista",
                    QuantidadeParcelas = 1
                });

            Assert.IsNotNull(condicaoPagamento, "Condiçao de pagamento não deve ser nula");
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void AtualizarCondicaoPagamento_DadosInvalidos_Excecao()
        {
            _condicaoPagamentoRepositoryMock
                .Setup(x => x.Atualizar(It.IsAny<CondicaoPagamento>()))
                .Returns((CondicaoPagamento)null);

            var condicaoPagamento = _condicaoPagamentoService
                .AtualizarCondicaoPagamento(new CondicaoPagamentoModel());
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Código valido")]
        public void AtualizarCondicaoPagamento_SemCodigo_Excecao()
        {
            _condicaoPagamentoRepositoryMock
                .Setup(x => x.Atualizar(It.IsAny<CondicaoPagamento>()))
                .Returns((CondicaoPagamento)null);

            var condicaoPagamento = _condicaoPagamentoService
                .AtualizarCondicaoPagamento(new CondicaoPagamentoModel
                {
                    Ativo = true,
                    Descricao = "A Vista",
                    QuantidadeParcelas = 1
                });
        }

        [TestMethod]
        public void AtualizarCondicaoPagamento_DadosValidos_RetornaObjeto()
        {
            _condicaoPagamentoRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new CondicaoPagamento
                {
                    Ativo = true,
                    CondicaoPagamentoCodigo = 1,
                    Descricao = "A vista",
                    QuantidadeParcelas = 1
                });

            _condicaoPagamentoRepositoryMock
                .Setup(x => x.Atualizar(It.IsAny<CondicaoPagamento>()))
                .Returns(new CondicaoPagamento
                {
                    Ativo = false,
                    CondicaoPagamentoCodigo = 1,
                    Descricao = "A vista",
                    QuantidadeParcelas = 1
                });

            var condicaoPagamento = _condicaoPagamentoService
                .AtualizarCondicaoPagamento(new CondicaoPagamentoModel
                {
                    Ativo = false,
                    CondicaoPagamentoCodigo = 1,
                    Descricao = "A Vista",
                    QuantidadeParcelas = 1
                });

            Assert.IsNotNull(condicaoPagamento, "Condiçao de pagamento não deve ser nula");
            Assert.AreEqual(condicaoPagamento.Ativo, false, "Condiçao de pagamento não foi atualizado");
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Código valido")]
        public void ExcluirCondicaoPagamento_SemCodigo_Excecao()
        {
            _condicaoPagamentoRepositoryMock
                .Setup(x => x.Deletar(It.IsAny<CondicaoPagamento>()));

            _condicaoPagamentoService.ExcluirCondicaoPagamento(0);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Entidade encontrada")]
        public void ExcluirCondicaoPagamento_Inexistente_Excecao()
        {
            _condicaoPagamentoRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new CondicaoPagamento
                {
                    Ativo = true,
                    CondicaoPagamentoCodigo = 1,
                    Descricao = "A vista",
                    QuantidadeParcelas = 1
                });

            _condicaoPagamentoService.ExcluirCondicaoPagamento(2);
        }

        [TestMethod]
        public void ExcluirCondicaoPagamento_Valido_SeraRemovido()
        {
            _condicaoPagamentoRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new CondicaoPagamento
                {
                    Ativo = true,
                    CondicaoPagamentoCodigo = 1,
                    Descricao = "A vista",
                    QuantidadeParcelas = 1
                });

            _condicaoPagamentoService.ExcluirCondicaoPagamento(1);
        }
    }
}
