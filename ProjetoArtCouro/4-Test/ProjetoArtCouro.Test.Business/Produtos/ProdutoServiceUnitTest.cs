using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjetoArtCouro.Business.ProdutoService;
using ProjetoArtCouro.Domain.Contracts.IRepository.IProduto;
using ProjetoArtCouro.Domain.Entities.Produtos;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.Produto;
using ProjetoArtCouro.Mapping.Configs;
using System.Collections.Generic;

namespace ProjetoArtCouro.Test.Business.Produtos
{
    [TestClass]
    public class ProdutoServiceUnitTest
    {
        private ProdutoService _produtoService;
        private Mock<IProdutoRepository> _produtoRepositoryMock;
        private Mock<IUnidadeRepository> _unidadeRepositoryMock;

        [TestInitialize]
        public void Inicializacao()
        {
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _unidadeRepositoryMock = new Mock<IUnidadeRepository>();
            _produtoService = new ProdutoService(_produtoRepositoryMock.Object, 
                _unidadeRepositoryMock.Object);
            MapperConfig.RegisterMappings();
        }

        [TestMethod]
        public void ObterListaProduto_DadosExistentes_RetornaLista()
        {
            _produtoRepositoryMock
                .Setup(x => x.ObterListaComUnidade())
                .Returns(new List<Produto>
                {
                    new Produto
                    {
                        PrecoCusto = 1,
                        PrecoVenda = 2,
                        ProdutoCodigo = 1,
                        ProdutoNome = "Cinto",
                        Unidade = new Unidade { UnidadeCodigo = 1, UnidadeNome = "UN" }
                    }
                });
        
            var produtos = _produtoService.ObterListaProduto();

            Assert.IsNotNull(produtos, "Produtos não deveriam ser nulos");
            Assert.AreEqual(produtos.Count, 1, "Quantidade de produtos invalida");
        }

        [TestMethod]
        public void ObterListaUnidade_DadosExistentes_RetornaLista()
        {
            _unidadeRepositoryMock
                .Setup(x => x.ObterLista())
                .Returns(new List<Unidade>
                {
                    new Unidade { UnidadeCodigo = 1, UnidadeNome = "UN" },
                    new Unidade { UnidadeCodigo = 1, UnidadeNome = "KG" }
                });

            var unidades = _produtoService.ObterListaUnidade();

            Assert.IsNotNull(unidades, "Unidades não deveriam ser nulos");
            Assert.AreEqual(unidades.Count, 2, "Quantidade de unidades invalida");
        }

        [TestMethod]
        public void ObterProdutoPorCodigo_DadosExistentes_RetornaObjeto()
        {
            _produtoRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new Produto
                {
                    PrecoCusto = 1,
                    PrecoVenda = 2,
                    ProdutoCodigo = 1,
                    ProdutoNome = "Cinto"
                });

            var produto = _produtoService.ObterProdutoPorCodigo(1);

            Assert.IsNotNull(produto, "Produto não deve ser nulo");
            Assert.AreEqual(produto.ProdutoCodigo, 1, "Produto com código invalido");
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void CriarProduto_DadosInvalidos_Excecao()
        {
            var produto = _produtoService.CriarProduto(new ProdutoModel());
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Unidade encontrada")]
        public void CriarProduto_UnidadeNaoEncontrada_Excecao()
        {
            _unidadeRepositoryMock
               .Setup(x => x.ObterPorCodigo(2))
               .Returns(new Unidade { UnidadeCodigo = 2, UnidadeNome = "UN" });

            var produto = _produtoService
                .CriarProduto(new ProdutoModel
                {
                    Descricao = "Cinto",
                    PrecoCusto = "1",
                    PrecoVenda = "2",
                    UnidadeCodigo = 1,
                    UnidadeNome = "UN"
                });
        }

        [TestMethod]
        public void CriarProduto_DadosValidos_RetornaObjeto()
        {
            _produtoRepositoryMock
                .Setup(x => x.Criar(It.IsAny<Produto>()))
                .Returns(new Produto
                {
                    PrecoCusto = 1,
                    PrecoVenda = 2,
                    ProdutoCodigo = 1,
                    ProdutoNome = "Cinto",
                    Unidade = new Unidade { UnidadeCodigo = 1, UnidadeNome = "UN" }
                });

            _unidadeRepositoryMock
               .Setup(x => x.ObterPorCodigo(1))
               .Returns(new Unidade { UnidadeCodigo = 1, UnidadeNome = "UN" });

            var produto = _produtoService
                .CriarProduto(new ProdutoModel
                {
                    Descricao = "Cinto",
                    PrecoCusto = "1",
                    PrecoVenda = "2",
                    UnidadeCodigo = 1,
                    UnidadeNome = "UN"
                });

            Assert.IsNotNull(produto, "Produto não deve ser nulo");
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void AtualizarProduto_DadosInvalidos_Excecao()
        {
            var produto = _produtoService.AtualizarProduto(new ProdutoModel());
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Codigo do produto difirente de 0")]
        public void AtualizarProduto_ProdutoCodigoZero_Excecao()
        {
            var produto = _produtoService
                .AtualizarProduto(new ProdutoModel
                {
                    Descricao = "Cinto",
                    PrecoCusto = "1",
                    PrecoVenda = "2",
                    UnidadeCodigo = 1,
                    UnidadeNome = "UN"
                });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Codigo da unidade difirente de 0")]
        public void AtualizarProduto_UnidadeCodigoZero_Excecao()
        {
            var produto = _produtoService
                .AtualizarProduto(new ProdutoModel
                {
                    ProdutoCodigo = 1,
                    Descricao = "Cinto",
                    PrecoCusto = "1",
                    PrecoVenda = "2",
                    UnidadeCodigo = 0,
                    UnidadeNome = "UN"
                });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Unidade encontrada")]
        public void AtualizarProduto_UnidadeNaoEncontrada_Excecao()
        {
            _unidadeRepositoryMock
               .Setup(x => x.ObterPorCodigo(2))
               .Returns(new Unidade { UnidadeCodigo = 2, UnidadeNome = "UN" });

            var produto = _produtoService
                .AtualizarProduto(new ProdutoModel
                {
                    ProdutoCodigo = 1,
                    Descricao = "Cinto",
                    PrecoCusto = "1",
                    PrecoVenda = "2",
                    UnidadeCodigo = 1,
                    UnidadeNome = "UN"
                });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Produto encontrado")]
        public void AtualizarProduto_ProdutoNaoEncontrada_Excecao()
        {
            _produtoRepositoryMock
                .Setup(x => x.ObterComUnidadePorCodigo(2))
                .Returns(new Produto
                {
                    PrecoCusto = 1,
                    PrecoVenda = 2,
                    ProdutoCodigo = 1,
                    ProdutoNome = "Cinto",
                    Unidade = new Unidade { UnidadeCodigo = 1, UnidadeNome = "UN" }
                });

            _unidadeRepositoryMock
               .Setup(x => x.ObterPorCodigo(1))
               .Returns(new Unidade { UnidadeCodigo = 1, UnidadeNome = "UN" });

            var produto = _produtoService
                .AtualizarProduto(new ProdutoModel
                {
                    ProdutoCodigo = 1,
                    Descricao = "Cinto",
                    PrecoCusto = "1",
                    PrecoVenda = "2",
                    UnidadeCodigo = 1,
                    UnidadeNome = "UN"
                });
        }

        [TestMethod]
        public void AtualizarProduto_DadosValidos_RetornaObjeto()
        {
            _produtoRepositoryMock
                .Setup(x => x.ObterComUnidadePorCodigo(1))
                .Returns(new Produto
                {
                    PrecoCusto = 1,
                    PrecoVenda = 2,
                    ProdutoCodigo = 1,
                    ProdutoNome = "Cinto",
                    Unidade = new Unidade { UnidadeCodigo = 1, UnidadeNome = "UN" }
                });

            _produtoRepositoryMock
                .Setup(x => x.Atualizar(It.IsAny<Produto>()))
                .Returns(new Produto
                {
                    PrecoCusto = 1,
                    PrecoVenda = 2,
                    ProdutoCodigo = 1,
                    ProdutoNome = "Cinto",
                    Unidade = new Unidade { UnidadeCodigo = 1, UnidadeNome = "UN" }
                });

            _unidadeRepositoryMock
               .Setup(x => x.ObterPorCodigo(1))
               .Returns(new Unidade { UnidadeCodigo = 1, UnidadeNome = "UN" });

            var produto = _produtoService
                .AtualizarProduto(new ProdutoModel
                {
                    ProdutoCodigo = 1,
                    Descricao = "Cinto",
                    PrecoCusto = "1",
                    PrecoVenda = "2",
                    UnidadeCodigo = 1,
                    UnidadeNome = "UN"
                });

            Assert.IsNotNull(produto, "Produto não deve ser nulo");
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Entidade encontrada")]
        public void ExcluirProduto_Inexistente_Excecao()
        {
            _produtoRepositoryMock
               .Setup(x => x.ObterPorCodigo(1))
               .Returns(new Produto
               {
                   PrecoCusto = 1,
                   PrecoVenda = 2,
                   ProdutoCodigo = 1,
                   ProdutoNome = "Cinto",
                   Unidade = new Unidade { UnidadeCodigo = 1, UnidadeNome = "UN" }
               });

            _produtoService.ExcluirProduto(0);
        }

        [TestMethod]
        public void ExcluirProduto_Valido_SeraRemovido()
        {
            _produtoRepositoryMock
               .Setup(x => x.ObterPorCodigo(1))
               .Returns(new Produto
               {
                   PrecoCusto = 1,
                   PrecoVenda = 2,
                   ProdutoCodigo = 1,
                   ProdutoNome = "Cinto",
                   Unidade = new Unidade { UnidadeCodigo = 1, UnidadeNome = "UN" }
               });

            _produtoService.ExcluirProduto(1);
        }
    }
}
