using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjetoArtCouro.Business.CompraService;
using ProjetoArtCouro.Domain.Contracts.IRepository.ICompra;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.ContaPagar;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Mapping.Configs;
using System;
using System.Collections.Generic;

namespace ProjetoArtCouro.Test.Business.Compras
{
    [TestClass]
    public class ContaPagarServiceUnitTest
    {
        private ContaPagarService _contaPagarService;
        private Mock<IContaPagarRepository> _contaPagarRepositoryMock;

        [TestInitialize]
        public void Inicializacao()
        {
            _contaPagarRepositoryMock = new Mock<IContaPagarRepository>();

            _contaPagarService = new ContaPagarService(_contaPagarRepositoryMock.Object);

            MapperConfig.RegisterMappings();
        }

        [TestMethod]
        public void PesquisarContaPagar_DadosValidos_RetornaLista()
        {
            _contaPagarRepositoryMock
                .Setup(x => x.ObterListaPorFiltro(It.IsAny<PesquisaContaPagar>()))
                .Returns(new List<ContaPagar>
                {
                    new ContaPagar
                    {
                        ContaPagarCodigo = 1,
                        DataVencimento = DateTime.Now,
                        Pago = true,
                        StatusContaPagar = StatusContaPagarEnum.Pago,
                        ValorDocumento = 1.23M,
                        Compra = new Compra
                        {
                            CompraCodigo = 1,
                            Fornecedor = new Pessoa
                            {
                                Nome = "12345678909",
                                PessoaFisica = new PessoaFisica
                                {
                                    CPF = "12345678909"
                                }
                            }
                        }
                    }
                });

            var contas = _contaPagarService.PesquisarContaPagar(1, new PesquisaContaPagarModel
            {
                CodigoCompra = 1,
                CodigoFornecedor = 1,
                CPFCNPJ = "12345678909",
                DataEmissao = DateTime.Now.ToShortDateString(),
                DataVencimento = DateTime.Now.ToShortDateString(),
                NomeFornecedor = "Henrique",
                StatusId = 1
            });
        
            Assert.IsNotNull(contas, "Contas não devem ser nulas");
            Assert.AreEqual(contas.Count, 1, "Quantidade de contas invalidas");
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Lista preenchida")]
        public void PagarContas_ListaVazia_Excecao()
        {
            _contaPagarService.PagarContas(new List<ContaPagarModel>());
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void PagarContas_DadosInvalidos_Excecao()
        {
            _contaPagarService.PagarContas(new List<ContaPagarModel>
            {
                new ContaPagarModel()
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Conta encontrada")]
        public void PagarContas_ContaNaoEncontrada_Excecao()
        {
            _contaPagarService.PagarContas(new List<ContaPagarModel>
            {
                new ContaPagarModel
                {
                    CodigoCompra = 1,
                    CodigoContaPagar = 1,
                    CodigoFornecedor = 1,
                    CPFCNPJ = "12345678909",
                    DataEmissao = DateTime.Now.ToShortTimeString(),
                    DataVencimento = DateTime.Now.ToShortTimeString(),
                    NomeFornecedor = "Henrique",
                    Pago = true,
                    Status = "Aberto",
                    ValorDocumento = "1,23"
                }
            });
        }

        [TestMethod]
        public void PagarContas_DadosValidos_ContasPagas()
        {
            _contaPagarRepositoryMock
                .Setup(x => x.ObterPorCodigoComCompra(1))
                .Returns(new ContaPagar
                {
                    ContaPagarCodigo = 1,
                    Pago = false,
                    StatusContaPagar = StatusContaPagarEnum.Pago
                });

            _contaPagarService.PagarContas(new List<ContaPagarModel>
            {
                new ContaPagarModel
                {
                    CodigoCompra = 1,
                    CodigoContaPagar = 1,
                    CodigoFornecedor = 1,
                    CPFCNPJ = "12345678909",
                    DataEmissao = DateTime.Now.ToShortTimeString(),
                    DataVencimento = DateTime.Now.ToShortTimeString(),
                    NomeFornecedor = "Henrique",
                    Pago = true,
                    Status = "Aberto",
                    ValorDocumento = "1,23"
                }
            });
        }
    }
}
