using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjetoArtCouro.Business.EstoqueService;
using ProjetoArtCouro.Domain.Contracts.IRepository.IEstoque;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Entities.Estoques;
using ProjetoArtCouro.Domain.Entities.Produtos;
using ProjetoArtCouro.Domain.Models.Estoque;
using ProjetoArtCouro.Mapping.Configs;
using System;
using System.Collections.Generic;

namespace ProjetoArtCouro.Test.Business.Estoques
{
    [TestClass]
    public class EstoqueServiceUnitTest
    {
        private EstoqueService _estoqueService;
        private Mock<IEstoqueRepository> _estoqueRepositoryMock;

        [TestInitialize]
        public void Inicializacao()
        {
            _estoqueRepositoryMock = new Mock<IEstoqueRepository>();

            _estoqueService = new EstoqueService(_estoqueRepositoryMock.Object);

            MapperConfig.RegisterMappings();
        }

        [TestMethod]
        public void PesquisarEstoque_DadosValidos_RetornaLista()
        {
            _estoqueRepositoryMock
                .Setup(x => x.ObterListaPorFiltro(It.IsAny<PesquisaEstoque>()))
                .Returns(new List<Estoque>
                {
                    new Estoque
                    {
                        DataUltimaEntrada = DateTime.Now,
                        EstoqueCodigo = 1,
                        Produto = new Produto
                        {
                            PrecoCusto = 1.23M,
                            PrecoVenda = 1.23M,
                            ProdutoCodigo = 1,
                            ProdutoNome = "Cinto",
                        },
                        Compra = new Compra
                        {
                            CompraCodigo = 1,
                            DataCadastro = DateTime.Now
                        }
                    }
                });

            var estoques = _estoqueService.PesquisarEstoque(new PesquisaEstoqueModel
            {
                CodigoFornecedor = 1,
                CodigoProduto = 1,
                DescricaoProduto = "Cinto",
                NomeFornecedor = "Henrique",
                QuantidaEstoque = 1
            });

            Assert.IsNotNull(estoques, "Estoques não devem ser nulas");
            Assert.AreEqual(estoques.Count, 1, "Quantidade de estoques invalidas");
        }
    }
}
