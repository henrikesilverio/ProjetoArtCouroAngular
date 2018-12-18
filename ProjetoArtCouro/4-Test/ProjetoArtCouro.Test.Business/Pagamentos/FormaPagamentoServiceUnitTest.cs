using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjetoArtCouro.Business.PagamentoService;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPagamento;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.FormaPagamento;
using ProjetoArtCouro.Mapping.Configs;
using System.Collections.Generic;

namespace ProjetoArtCouro.Test.Business.Pagamentos
{
    [TestClass]
    public class FormaPagamentoServiceUnitTest
    {
        private FormaPagamentoService _formaPagamentoService;
        private Mock<IFormaPagamentoRepository> _formaPagamentoRepositoryMock;

        [TestInitialize]
        public void Inicializacao()
        {
            _formaPagamentoRepositoryMock = new Mock<IFormaPagamentoRepository>();
            _formaPagamentoService = new FormaPagamentoService(_formaPagamentoRepositoryMock.Object);
            MapperConfig.RegisterMappings();
        }

        [TestMethod]
        public void ObterListaFormaPagamento_DadosExistentes_RetornaLista()
        {
            _formaPagamentoRepositoryMock
                .Setup(x => x.ObterLista())
                .Returns(new List<FormaPagamento>
                {
                    new FormaPagamento
                    {
                        FormaPagamentoCodigo = 1,
                        Descricao = "Cartão"
                    }
                });

            var formasPagamento = _formaPagamentoService.ObterListaFormaPagamento();

            Assert.IsNotNull(formasPagamento, "Formas de pagamento não deveriam ser nulas");
            Assert.AreEqual(formasPagamento.Count, 1, "Quantidade de Formas de pagamento invalidas");
        }

        [TestMethod]
        public void ObterFormaPagamentoPorCodigo_DadosExistentes_RetornaObjeto()
        {
            _formaPagamentoRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new FormaPagamento
                {
                    FormaPagamentoCodigo = 1,
                    Descricao = "Cartão"
                });

            var formaPagamento = _formaPagamentoService.ObterFormaPagamentoPorCodigo(1);

            Assert.IsNotNull(formaPagamento, "Forma de pagamento não deve ser nula");
            Assert.AreEqual(formaPagamento.FormaPagamentoCodigo, 1, "Forma de pagamento com código invalido");
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void CriarFormaPagamento_DadosInvalidos_Excecao()
        {
            _formaPagamentoRepositoryMock
                .Setup(x => x.Criar(It.IsAny<FormaPagamento>()))
                .Returns((FormaPagamento)null);

            var condicaoPagamento = _formaPagamentoService
                .CriarFormaPagamento(new FormaPagamentoModel());
        }

        [TestMethod]
        public void CriarFormaPagamento_DadosValidos_RetornaObjeto()
        {
            _formaPagamentoRepositoryMock
                .Setup(x => x.Criar(It.IsAny<FormaPagamento>()))
                .Returns(new FormaPagamento
                {
                    FormaPagamentoCodigo = 1,
                    Descricao = "Cartão"
                });

            var formaPagamento = _formaPagamentoService
                .CriarFormaPagamento(new FormaPagamentoModel
                {
                    Ativo = true,
                    Descricao = "Cartão"
                });

            Assert.IsNotNull(formaPagamento, "Forma de pagamento não deve ser nula");
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void AtualizarFormaPagamento_DadosInvalidos_Excecao()
        {
            _formaPagamentoRepositoryMock
                .Setup(x => x.Atualizar(It.IsAny<FormaPagamento>()))
                .Returns((FormaPagamento)null);

            _formaPagamentoService
                .AtualizarFormaPagamento(new FormaPagamentoModel());
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Código valido")]
        public void AtualizarFormaPagamento_SemCodigo_Excecao()
        {
            _formaPagamentoRepositoryMock
                .Setup(x => x.Atualizar(It.IsAny<FormaPagamento>()))
                .Returns((FormaPagamento)null);

            _formaPagamentoService
                .AtualizarFormaPagamento(new FormaPagamentoModel
                {
                    Ativo = true,
                    Descricao = "Cartão"
                });
        }

        [TestMethod]
        public void AtualizarFormaPagamento_DadosValidos_RetornaObjeto()
        {
            _formaPagamentoRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new FormaPagamento
                {
                    Ativo = true,
                    FormaPagamentoCodigo = 1,
                    Descricao = "Cartão"
                });

            _formaPagamentoRepositoryMock
                .Setup(x => x.Atualizar(It.IsAny<FormaPagamento>()))
                .Returns(new FormaPagamento
                {
                    Ativo = false,
                    FormaPagamentoCodigo = 1,
                    Descricao = "Cartão"
                });

            var formaPagamento = _formaPagamentoService
                .AtualizarFormaPagamento(new FormaPagamentoModel
                {
                    Ativo = false,
                    FormaPagamentoCodigo = 1,
                    Descricao = "Cartão"
                });

            Assert.IsNotNull(formaPagamento, "Forma de pagamento não deve ser nula");
            Assert.AreEqual(formaPagamento.Ativo, false, "Forma de pagamento não foi atualizado");
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Código valido")]
        public void ExcluirFormaPagamento_SemCodigo_Excecao()
        {
            _formaPagamentoRepositoryMock
                .Setup(x => x.Deletar(It.IsAny<FormaPagamento>()));

            _formaPagamentoService.ExcluirFormaPagamento(0);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Entidade encontrada")]
        public void ExcluirFormaPagamento_Inexistente_Excecao()
        {
            _formaPagamentoRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new FormaPagamento
                {
                    Ativo = true,
                    FormaPagamentoCodigo = 1,
                    Descricao = "Cartão"
                });

            _formaPagamentoService.ExcluirFormaPagamento(2);
        }

        [TestMethod]
        public void ExcluirFormaPagamento_Valido_SeraRemovido()
        {
            _formaPagamentoRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new FormaPagamento
                {
                    Ativo = true,
                    FormaPagamentoCodigo = 1,
                    Descricao = "Cartão"
                });

            _formaPagamentoService.ExcluirFormaPagamento(1);
        }
    }
}
