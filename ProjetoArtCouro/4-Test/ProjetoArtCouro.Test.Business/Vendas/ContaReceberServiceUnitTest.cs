using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjetoArtCouro.Business.VendaService;
using ProjetoArtCouro.Domain.Contracts.IRepository.IVenda;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.ContaReceber;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Mapping.Configs;
using System;
using System.Collections.Generic;

namespace ProjetoArtCouro.Test.Business.Vendas
{
    [TestClass]
    public class ContaReceberServiceUnitTest
    {
        private ContaReceberService _contaReceberService;
        private Mock<IContaReceberRepository> _contaReceberRepositoryMock;

        [TestInitialize]
        public void Inicializacao()
        {
            _contaReceberRepositoryMock = new Mock<IContaReceberRepository>();

            _contaReceberService = new ContaReceberService(_contaReceberRepositoryMock.Object);

            MapperConfig.RegisterMappings();
        }

        [TestMethod]
        public void PesquisarContaReceber_DadosValidos_RetornaLista()
        {
            _contaReceberRepositoryMock
                .Setup(x => x.ObterListaPorFiltro(It.IsAny<PesquisaContaReceber>()))
                .Returns(new List<ContaReceber>
                {
                    new ContaReceber
                    {
                        ContaReceberCodigo = 1,
                        DataVencimento = DateTime.Now,
                        Recebido = true,
                        StatusContaReceber = StatusContaReceberEnum.Recebido,
                        ValorDocumento = 1.23M,
                        Venda = new Venda
                        {
                            VendaCodigo = 1,
                            Cliente = new Pessoa
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

            var contas = _contaReceberService.PesquisarContaReceber(1, new PesquisaContaReceberModel
            {
                CodigoVenda = 1,
                CodigoCliente = 1,
                CPFCNPJ = "12345678909",
                DataEmissao = DateTime.Now.ToShortDateString(),
                DataVencimento = DateTime.Now.ToShortDateString(),
                NomeCliente = "Henrique",
                StatusId = 1
            });

            Assert.IsNotNull(contas, "Contas não devem ser nulas");
            Assert.AreEqual(contas.Count, 1, "Quantidade de contas invalidas");
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Lista preenchida")]
        public void ReceberContas_ListaVazia_Excecao()
        {
            _contaReceberService.ReceberContas(new List<ContaReceberModel>());
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void ReceberContas_DadosInvalidos_Excecao()
        {
            _contaReceberService.ReceberContas(new List<ContaReceberModel>
            {
                new ContaReceberModel()
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Conta encontrada")]
        public void ReceberContas_ContaNaoEncontrada_Excecao()
        {
            _contaReceberService.ReceberContas(new List<ContaReceberModel>
            {
                new ContaReceberModel
                {
                    CodigoVenda = 1,
                    CodigoContaReceber = 1,
                    CodigoCliente = 1,
                    CPFCNPJ = "12345678909",
                    DataEmissao = DateTime.Now.ToShortTimeString(),
                    DataVencimento = DateTime.Now.ToShortTimeString(),
                    NomeCliente = "Henrique",
                    Recebido = true,
                    Status = "Aberto",
                    ValorDocumento = "1,23"
                }
            });
        }

        [TestMethod]
        public void ReceberContas_DadosValidos_ContasRecebidas()
        {
            _contaReceberRepositoryMock
                .Setup(x => x.ObterPorCodigoComVenda(1))
                .Returns(new ContaReceber
                {
                    ContaReceberCodigo = 1,
                    Recebido = false,
                    StatusContaReceber = StatusContaReceberEnum.Recebido
                });

            _contaReceberService.ReceberContas(new List<ContaReceberModel>
            {
                new ContaReceberModel
                {
                    CodigoVenda = 1,
                    CodigoContaReceber = 1,
                    CodigoCliente = 1,
                    CPFCNPJ = "12345678909",
                    DataEmissao = DateTime.Now.ToShortTimeString(),
                    DataVencimento = DateTime.Now.ToShortTimeString(),
                    NomeCliente = "Henrique",
                    Recebido = true,
                    Status = "Aberto",
                    ValorDocumento = "1,23"
                }
            });
        }
    }
}
