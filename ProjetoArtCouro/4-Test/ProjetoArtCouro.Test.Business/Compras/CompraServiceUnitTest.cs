using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjetoArtCouro.Business.CompraService;
using ProjetoArtCouro.Domain.Contracts.IRepository.ICompra;
using ProjetoArtCouro.Domain.Contracts.IRepository.IEstoque;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPagamento;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPessoa;
using ProjetoArtCouro.Domain.Contracts.IRepository.IProduto;
using ProjetoArtCouro.Domain.Contracts.IRepository.IUsuario;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.Compra;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Mapping.Configs;
using System;
using System.Collections.Generic;

namespace ProjetoArtCouro.Test.Business.Compras
{
    [TestClass]
    public class CompraServiceUnitTest
    {
        private CompraService _compraService;
        private Mock<ICompraRepository> _compraRepositoryMock;
        private Mock<IItemCompraRepository> _itemCompraRepositoryMock;
        private Mock<IPessoaRepository> _pessoaRepositoryMock;
        private Mock<IFormaPagamentoRepository> _formaPagamentoRepositoryMock;
        private Mock<ICondicaoPagamentoRepository> _condicaoPagamentoRepositoryMock;
        private Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private Mock<IContaPagarRepository> _contaPagarRepositoryMock;
        private Mock<IEstoqueRepository> _estoqueRepositoryMock;
        private Mock<IProdutoRepository> _produtoRepositoryMock;

        [TestInitialize]
        public void Inicializacao()
        {
            _compraRepositoryMock = new Mock<ICompraRepository>();
            _itemCompraRepositoryMock = new Mock<IItemCompraRepository>();
            _pessoaRepositoryMock = new Mock<IPessoaRepository>();
            _formaPagamentoRepositoryMock = new Mock<IFormaPagamentoRepository>();
            _condicaoPagamentoRepositoryMock = new Mock<ICondicaoPagamentoRepository>();
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _contaPagarRepositoryMock = new Mock<IContaPagarRepository>();
            _estoqueRepositoryMock = new Mock<IEstoqueRepository>();
            _produtoRepositoryMock = new Mock<IProdutoRepository>();

            _compraService = new CompraService(
                _compraRepositoryMock.Object,
                _itemCompraRepositoryMock.Object,
                _pessoaRepositoryMock.Object,
                _formaPagamentoRepositoryMock.Object,
                _condicaoPagamentoRepositoryMock.Object,
                _usuarioRepositoryMock.Object,
                _contaPagarRepositoryMock.Object,
                _estoqueRepositoryMock.Object,
                _produtoRepositoryMock.Object);

            MapperConfig.RegisterMappings();
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void CriarCompra_DadosInvalidos_Excecao()
        {
            _compraService.CriarCompra(1, new CompraModel());
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Item preenchido")]
        public void CriarCompra_ItemDeCompraVazio_Excecao()
        {
            _compraService.CriarCompra(1, new CompraModel
            {
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusCompra = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ItemCompraModel = new List<ItemCompraModel>()
            });
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void CriarCompra_ItensDeCompraInvalidos_Excecao()
        {
            _compraService.CriarCompra(1, new CompraModel
            {
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusCompra = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ItemCompraModel = new List<ItemCompraModel>
                {
                    new ItemCompraModel()
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Compra com status aberto")]
        public void CriarCompra_CompraDiferenteDeAberto_Excecao()
        {
            _compraService.CriarCompra(1, new CompraModel
            {
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusCompra = "Confirmado",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ItemCompraModel = new List<ItemCompraModel>
                {
                    new ItemCompraModel
                    {
                        Codigo = 1,
                        Descricao = "Cinto",
                        PrecoVenda = "1",
                        Quantidade = 1,
                        ValorBruto = "1",
                        ValorLiquido = "1"
                    }
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Compra com status aberto")]
        public void CriarCompra_SomaDoValorTotalBrutoDivergente_Excecao()
        {
            _compraService.CriarCompra(1, new CompraModel
            {
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusCompra = "Aberto",
                ValorTotalBruto = "2",
                ValorTotalLiquido = "1",
                ItemCompraModel = new List<ItemCompraModel>
                {
                    new ItemCompraModel
                    {
                        Codigo = 1,
                        Descricao = "Cinto",
                        PrecoVenda = "1",
                        Quantidade = 1,
                        ValorBruto = "1",
                        ValorLiquido = "1"
                    }
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Compra com status aberto")]
        public void CriarCompra_SomaDoValorTotalLiquidoDivergente_Excecao()
        {
            _compraService.CriarCompra(1, new CompraModel
            {
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusCompra = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ValorTotalFrete = "1",
                ItemCompraModel = new List<ItemCompraModel>
                {
                    new ItemCompraModel
                    {
                        Codigo = 1,
                        Descricao = "Cinto",
                        PrecoVenda = "1",
                        Quantidade = 1,
                        ValorBruto = "1",
                        ValorLiquido = "1"
                    }
                }
            });
        }

        [TestMethod]
        public void CriarCompra_DadosValidos_CompraCriada()
        {
            _usuarioRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new Usuario
                {
                    Ativo = true,
                    Senha = "sdasdas",
                    UsuarioCodigo = 1,
                    UsuarioNome = "Henrique"
                });

            _compraService.CriarCompra(1, new CompraModel
            {
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusCompra = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ValorTotalFrete = "0",
                ItemCompraModel = new List<ItemCompraModel>
                {
                    new ItemCompraModel
                    {
                        Codigo = 1,
                        Descricao = "Cinto",
                        PrecoVenda = "1",
                        Quantidade = 1,
                        ValorBruto = "1",
                        ValorLiquido = "1"
                    }
                }
            });
        }

        [TestMethod]
        public void PesquisarCompra_DadosValidos_RetornaLista()
        {
            _compraRepositoryMock
                .Setup(x => x.ObterListaPorFiltro(It.IsAny<PesquisaCompra>()))
                .Returns(new List<Compra>()
                {
                    new Compra
                    {
                        CompraCodigo = 1,
                        DataCadastro = DateTime.Now,
                        StatusCompra = StatusCompraEnum.Aberto,
                        ValorTotalBruto = 1,
                        ValorTotalFrete = 1,
                        ValorTotalLiquido = 1
                    }
                });

            var compras = _compraService.PesquisarCompra(1, new PesquisaCompraModel
            {
                CodigoCompra = 1,
                CodigoFornecedor = 1,
                CPFCNPJ = "12345678909",
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy"),
                NomeFornecedor = "Henrique"
            });

            Assert.IsNotNull(compras, "compras não devem ser nulas");
            Assert.AreEqual(compras.Count, 1, "Quantidade de compras de pagamento invalidas");
        }

        [TestMethod]
        public void ObterCompraPorCodigo_DadosValidos_RetornaObjeto()
        {
            _compraRepositoryMock
                .Setup(x => x.ObterPorCodigoComItensCompra(1))
                .Returns(new Compra
                {
                    CompraCodigo = 1,
                    DataCadastro = DateTime.Now,
                    StatusCompra = StatusCompraEnum.Aberto,
                    ValorTotalBruto = 3,
                    ValorTotalFrete = 3,
                    ValorTotalLiquido = 3
                });

             var compra = _compraService.ObterCompraPorCodigo(1);

            Assert.IsNotNull(compra, "Compra não deve ser nula");
            Assert.AreEqual(compra.CodigoCompra, 1, "Compra com código invalido");
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void AtualizarCompra_DadosInvalidos_Excecao()
        {
            _compraService.AtualizarCompra(new CompraModel());
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Item preenchido")]
        public void AtualizarCompra_ItemDeCompraVazio_Excecao()
        {
            _compraService.AtualizarCompra(new CompraModel
            {
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusCompra = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ItemCompraModel = new List<ItemCompraModel>()
            });
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void AtualizarCompra_ItensDeCompraInvalidos_Excecao()
        {
            _compraService.AtualizarCompra(new CompraModel
            {
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusCompra = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ItemCompraModel = new List<ItemCompraModel>
                {
                    new ItemCompraModel()
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Compra encontrada")]
        public void AtualizarCompra_CompraNaoEncontrada_Excecao()
        {
            _compraService.AtualizarCompra(new CompraModel
            {
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusCompra = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ItemCompraModel = new List<ItemCompraModel>
                {
                    new ItemCompraModel
                    {
                        Codigo = 1,
                        Descricao = "Cinto",
                        PrecoVenda = "1",
                        Quantidade = 1,
                        ValorBruto = "1",
                        ValorLiquido = "1"
                    }
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Fornecedor encontrado")]
        public void AtualizarCompra_FornecedorNaoEncontrada_Excecao()
        {
            _compraRepositoryMock
                .Setup(x => x.ObterPorCodigoComItensCompra(1))
                .Returns(new Compra
                {
                    CompraCodigo = 1,
                    DataCadastro = DateTime.Now
                });

            _compraService.AtualizarCompra(new CompraModel
            {
                CodigoCompra = 1,
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusCompra = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ItemCompraModel = new List<ItemCompraModel>
                {
                    new ItemCompraModel
                    {
                        Codigo = 1,
                        Descricao = "Cinto",
                        PrecoVenda = "1",
                        Quantidade = 1,
                        ValorBruto = "1",
                        ValorLiquido = "1"
                    }
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Forma de pagamento encontrado")]
        public void AtualizarCompra_FormaDePagamentoNaoEncontrada_Excecao()
        {
            _pessoaRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new Pessoa
                {
                    Nome = "Fornecedor",
                    PessoaCodigo = 1
                });

            _compraRepositoryMock
                .Setup(x => x.ObterPorCodigoComItensCompra(1))
                .Returns(new Compra
                {
                    CompraCodigo = 1,
                    DataCadastro = DateTime.Now
                });

            _compraService.AtualizarCompra(new CompraModel
            {
                CodigoCompra = 1,
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusCompra = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ItemCompraModel = new List<ItemCompraModel>
                {
                    new ItemCompraModel
                    {
                        Codigo = 1,
                        Descricao = "Cinto",
                        PrecoVenda = "1",
                        Quantidade = 1,
                        ValorBruto = "1",
                        ValorLiquido = "1"
                    }
                },
                FornecedorId = 1
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Condição de pagamento encontrado")]
        public void AtualizarCompra_CondicaoDePagamentoNaoEncontrada_Excecao()
        {
            _pessoaRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new Pessoa
                {
                    Nome = "Fornecedor",
                    PessoaCodigo = 1
                });

            _formaPagamentoRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new FormaPagamento
                {
                    FormaPagamentoCodigo = 1,
                    Descricao = "Cartão",
                    Ativo = true
                });

            _compraRepositoryMock
                .Setup(x => x.ObterPorCodigoComItensCompra(1))
                .Returns(new Compra
                {
                    CompraCodigo = 1,
                    DataCadastro = DateTime.Now
                });

            _compraService.AtualizarCompra(new CompraModel
            {
                CodigoCompra = 1,
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusCompra = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ItemCompraModel = new List<ItemCompraModel>
                {
                    new ItemCompraModel
                    {
                        Codigo = 1,
                        Descricao = "Cinto",
                        PrecoVenda = "1",
                        Quantidade = 1,
                        ValorBruto = "1",
                        ValorLiquido = "1"
                    }
                },
                FornecedorId = 1,
                FormaPagamentoId = 1
            });
        }

        [TestMethod]
        public void AtualizarCompra_DadosValidos_CompraAtualizada()
        {
            _pessoaRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new Pessoa
                {
                    Nome = "Fornecedor",
                    PessoaCodigo = 1
                });

            _formaPagamentoRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new FormaPagamento
                {
                    FormaPagamentoCodigo = 1,
                    Descricao = "Cartão",
                    Ativo = true
                });

            _condicaoPagamentoRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new CondicaoPagamento
                {
                    CondicaoPagamentoCodigo = 1,
                    Descricao = "A vista",
                    Ativo = true,
                    QuantidadeParcelas = 1
                });

            _compraRepositoryMock
                .Setup(x => x.ObterPorCodigoComItensCompra(1))
                .Returns(new Compra
                {
                    CompraCodigo = 1,
                    DataCadastro = DateTime.Now,
                    ValorTotalLiquido = 1,
                    ItensCompra = new List<ItemCompra>
                    {
                        new ItemCompra
                        {
                            ProdutoCodigo = 1,
                            ProdutoNome = "Cinto",
                            ItemCompraCodigo = 1,
                            PrecoVenda = 1,
                            Quantidade = 1,
                            ValorBruto = 1,
                            ValorLiquido = 1
                        }
                    }
                });

            _compraService.AtualizarCompra(new CompraModel
            {
                CodigoCompra = 1,
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusCompra = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ItemCompraModel = new List<ItemCompraModel>
                {
                    new ItemCompraModel
                    {
                        Codigo = 1,
                        Descricao = "Cinto",
                        PrecoVenda = "1",
                        Quantidade = 1,
                        ValorBruto = "1",
                        ValorLiquido = "1"
                    }
                },
                FornecedorId = 1,
                FormaPagamentoId = 1,
                CondicaoPagamentoId = 1
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Compra encontrada")]
        public void ExcluirCompra_CompraNaoEncontrada_Excecao()
        {
            _compraService.ExcluirCompra(1);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Compra não confirmada")]
        public void ExcluirCompra_CompraConfirmada_Excecao()
        {
            _compraRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new Compra
                {
                    CompraCodigo = 1,
                    DataCadastro = DateTime.Now,
                    StatusCompra = StatusCompraEnum.Confirmado
                });

            _compraService.ExcluirCompra(1);
        }

        [TestMethod]
        public void ExcluirCompra_DadosValidos_CompraExcluida()
        {
            _compraRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new Compra
                {
                    CompraCodigo = 1,
                    DataCadastro = DateTime.Now,
                    StatusCompra = StatusCompraEnum.Aberto
                });

            _compraService.ExcluirCompra(1);
        }
    }
}
