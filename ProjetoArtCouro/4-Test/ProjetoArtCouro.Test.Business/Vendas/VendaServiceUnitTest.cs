using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjetoArtCouro.Business.VendaService;
using ProjetoArtCouro.Domain.Contracts.IRepository.IEstoque;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPagamento;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPessoa;
using ProjetoArtCouro.Domain.Contracts.IRepository.IProduto;
using ProjetoArtCouro.Domain.Contracts.IRepository.IUsuario;
using ProjetoArtCouro.Domain.Contracts.IRepository.IVenda;
using ProjetoArtCouro.Domain.Entities.Estoques;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Entities.Produtos;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Models.Venda;
using ProjetoArtCouro.Mapping.Configs;
using System;
using System.Collections.Generic;

namespace ProjetoArtCouro.Test.Business.Vendas
{
    [TestClass]
    public class VendaServiceUnitTest
    {
        private VendaService _vendaService;
        private Mock<IVendaRepository> _vendaRepositoryMock;
        private Mock<IItemVendaRepository> _itemVendaRepositoryMock;
        private Mock<IPessoaRepository> _pessoaRepositoryMock;
        private Mock<IFormaPagamentoRepository> _formaPagamentoRepositoryMock;
        private Mock<ICondicaoPagamentoRepository> _condicaoPagamentoRepositoryMock;
        private Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private Mock<IContaReceberRepository> _contaReceberRepositoryMock;
        private Mock<IEstoqueRepository> _estoqueRepositoryMock;
        private Mock<IProdutoRepository> _produtoRepositoryMock;

        [TestInitialize]
        public void Inicializacao()
        {
            _vendaRepositoryMock = new Mock<IVendaRepository>();
            _itemVendaRepositoryMock = new Mock<IItemVendaRepository>();
            _pessoaRepositoryMock = new Mock<IPessoaRepository>();
            _formaPagamentoRepositoryMock = new Mock<IFormaPagamentoRepository>();
            _condicaoPagamentoRepositoryMock = new Mock<ICondicaoPagamentoRepository>();
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _contaReceberRepositoryMock = new Mock<IContaReceberRepository>();
            _estoqueRepositoryMock = new Mock<IEstoqueRepository>();
            _produtoRepositoryMock = new Mock<IProdutoRepository>();

            _vendaService = new VendaService(
                _vendaRepositoryMock.Object,
                _itemVendaRepositoryMock.Object,
                _pessoaRepositoryMock.Object,
                _formaPagamentoRepositoryMock.Object,
                _condicaoPagamentoRepositoryMock.Object,
                _usuarioRepositoryMock.Object,
                _contaReceberRepositoryMock.Object,
                _estoqueRepositoryMock.Object,
                _produtoRepositoryMock.Object);

            MapperConfig.RegisterMappings();
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void CriarVenda_DadosInvalidos_Excecao()
        {
            _vendaService.CriarVenda(1, new VendaModel());
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Item preenchido")]
        public void CriarVenda_ItemDeVendaVazio_Excecao()
        {
            _vendaService.CriarVenda(1, new VendaModel
            {
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusVenda = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ItemVendaModel = new List<ItemVendaModel>()
            });
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void CriarVenda_ItensDeVendaInvalidos_Excecao()
        {
            _vendaService.CriarVenda(1, new VendaModel
            {
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusVenda = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ItemVendaModel = new List<ItemVendaModel>
                {
                    new ItemVendaModel()
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Venda com status aberto")]
        public void CriarVenda_VendaDiferenteDeAberto_Excecao()
        {
            _vendaService.CriarVenda(1, new VendaModel
            {
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusVenda = "Confirmado",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ItemVendaModel = new List<ItemVendaModel>
                {
                    new ItemVendaModel
                    {
                        Codigo = 1,
                        Descricao = "Cinto",
                        PrecoVenda = "1",
                        Quantidade = 1,
                        ValorBruto = "1",
                        ValorLiquido = "1",
                        ValorDesconto = "0"
                    }
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "A soma do valor total bruto esta correta")]
        public void CriarVenda_SomaDoValorTotalBrutoDivergente_Excecao()
        {
            _vendaService.CriarVenda(1, new VendaModel
            {
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusVenda = "Aberto",
                ValorTotalBruto = "2",
                ValorTotalLiquido = "1",
                ValorTotalDesconto = "0",
                ItemVendaModel = new List<ItemVendaModel>
                {
                    new ItemVendaModel
                    {
                        Codigo = 1,
                        Descricao = "Cinto",
                        PrecoVenda = "1",
                        Quantidade = 1,
                        ValorBruto = "1",
                        ValorLiquido = "1",
                        ValorDesconto = "0"
                    }
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "A soma do valor total desconto esta correta")]
        public void CriarVenda_SomaDoValorTotalDescontoDivergente_Excecao()
        {
            _vendaService.CriarVenda(1, new VendaModel
            {
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusVenda = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ValorTotalDesconto = "1",
                ItemVendaModel = new List<ItemVendaModel>
                {
                    new ItemVendaModel
                    {
                        Codigo = 1,
                        Descricao = "Cinto",
                        PrecoVenda = "1",
                        Quantidade = 1,
                        ValorBruto = "1",
                        ValorLiquido = "1",
                        ValorDesconto = "0"
                    }
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "A soma do valor total desconto esta correta")]
        public void CriarVenda_SomaDoValorTotalLiquidoDivergente_Excecao()
        {
            _vendaService.CriarVenda(1, new VendaModel
            {
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusVenda = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ValorTotalDesconto = "0",
                ItemVendaModel = new List<ItemVendaModel>
                {
                    new ItemVendaModel
                    {
                        Codigo = 1,
                        Descricao = "Cinto",
                        PrecoVenda = "1",
                        Quantidade = 1,
                        ValorBruto = "1",
                        ValorLiquido = "2",
                        ValorDesconto = "0"
                    }
                }
            });
        }

        [TestMethod]
        public void CriarVenda_DadosValidos_VendaCriada()
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

            _vendaService.CriarVenda(1, new VendaModel
            {
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusVenda = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ValorTotalDesconto = "0",
                ItemVendaModel = new List<ItemVendaModel>
                {
                    new ItemVendaModel
                    {
                        Codigo = 1,
                        Descricao = "Cinto",
                        PrecoVenda = "1",
                        Quantidade = 1,
                        ValorBruto = "1",
                        ValorLiquido = "1",
                        ValorDesconto = "0"
                    }
                }
            });
        }

        [TestMethod]
        public void PesquisarVenda_DadosValidos_RetornaLista()
        {
            _vendaRepositoryMock
                .Setup(x => x.ObterListaPorFiltro(It.IsAny<PesquisaVenda>()))
                .Returns(new List<Venda>()
                {
                    new Venda
                    {
                        VendaCodigo = 1,
                        DataCadastro = DateTime.Now,
                        StatusVenda = StatusVendaEnum.Aberto,
                        ValorTotalBruto = 1,
                        ValorTotalDesconto = 1,
                        ValorTotalLiquido = 1
                    }
                });

            var vendas = _vendaService.PesquisarVenda(1, new PesquisaVendaModel
            {
                CodigoVenda = 1,
                CodigoCliente = 1,
                CPFCNPJ = "12345678909",
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy"),
                NomeCliente = "Henrique"
            });
        
            Assert.IsNotNull(vendas, "Vendas não devem ser nulas");
            Assert.AreEqual(vendas.Count, 1, "Quantidade de vendas invalidas");
        }

        [TestMethod]
        public void ObterVendaPorCodigo_DadosValidos_RetornaObjeto()
        {
            _vendaRepositoryMock
                .Setup(x => x.ObterPorCodigoComItensVenda(1))
                .Returns(new Venda
                {
                    VendaCodigo = 1,
                    DataCadastro = DateTime.Now,
                    StatusVenda = StatusVendaEnum.Aberto,
                    ValorTotalBruto = 3,
                    ValorTotalDesconto = 3,
                    ValorTotalLiquido = 3
                });

            var venda = _vendaService.ObterVendaPorCodigo(1);
        
            Assert.IsNotNull(venda, "Venda não deve ser nula");
            Assert.AreEqual(venda.CodigoVenda, 1, "Venda com código invalido");
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void AtualizarVenda_DadosInvalidos_Excecao()
        {
            _vendaService.AtualizarVenda(new VendaModel());
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Item preenchido")]
        public void AtualizarVenda_ItemDeVendaVazio_Excecao()
        {
            _vendaService.AtualizarVenda(new VendaModel
            {
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusVenda = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ValorTotalDesconto = "0",
                ItemVendaModel = new List<ItemVendaModel>()
            });
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void AtualizarVenda_ItensDeVendaInvalidos_Excecao()
        {
            _vendaService.AtualizarVenda(new VendaModel
            {
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusVenda = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ValorTotalDesconto = "0",
                ItemVendaModel = new List<ItemVendaModel>
                {
                    new ItemVendaModel()
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Venda encontrada")]
        public void AtualizarVenda_VendaNaoEncontrada_Excecao()
        {
            _vendaService.AtualizarVenda(new VendaModel
            {
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusVenda = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ValorTotalDesconto = "0",
                ItemVendaModel = new List<ItemVendaModel>
                {
                    new ItemVendaModel
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
        [ExpectedException(typeof(BusinessException), "Estoque valido")]
        public void AtualizarVenda_EstoqueInsuficiente_Excecao()
        {
            _vendaRepositoryMock
                .Setup(x => x.ObterPorCodigoComItensVenda(1))
                .Returns(new Venda());

            _estoqueRepositoryMock
                .Setup(x => x.ObterPorCodigoProduto(1))
                .Returns(new Estoque
                {
                    EstoqueCodigo = 1,
                    Quantidade = 0,
                    Produto = new Produto
                    {
                        ProdutoCodigo = 1
                    }
                });

            _vendaService.AtualizarVenda(new VendaModel
            {
                CodigoVenda = 1,
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusVenda = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ValorTotalDesconto = "0",
                ItemVendaModel = new List<ItemVendaModel>
                {
                    new ItemVendaModel
                    {
                        Codigo = 1,
                        Descricao = "Cinto",
                        PrecoVenda = "1",
                        Quantidade = 1,
                        ValorBruto = "1",
                        ValorLiquido = "1",
                        ValorDesconto = "0"
                    }
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Cliente encontrado")]
        public void AtualizarVenda_ClienteNaoEncontrada_Excecao()
        {
            _vendaRepositoryMock
                .Setup(x => x.ObterPorCodigoComItensVenda(1))
                .Returns(new Venda());

            _estoqueRepositoryMock
                .Setup(x => x.ObterPorCodigoProduto(1))
                .Returns(new Estoque
                {
                    EstoqueCodigo = 1,
                    Quantidade = 1,
                    Produto = new Produto
                    {
                        ProdutoCodigo = 1
                    }
                });

            _vendaService.AtualizarVenda(new VendaModel
            {
                CodigoVenda = 1,
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusVenda = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ValorTotalDesconto = "0",
                ItemVendaModel = new List<ItemVendaModel>
                {
                    new ItemVendaModel
                    {
                        Codigo = 1,
                        Descricao = "Cinto",
                        PrecoVenda = "1",
                        Quantidade = 1,
                        ValorBruto = "1",
                        ValorLiquido = "1",
                        ValorDesconto = "0"
                    }
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Forma de pagamento encontrado")]
        public void AtualizarVenda_FormaDePagamentoNaoEncontrada_Excecao()
        {
            _pessoaRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new Pessoa
                {
                    Nome = "Cliente",
                    PessoaCodigo = 1
                });

            _vendaRepositoryMock
                .Setup(x => x.ObterPorCodigoComItensVenda(1))
                .Returns(new Venda());

            _estoqueRepositoryMock
                .Setup(x => x.ObterPorCodigoProduto(1))
                .Returns(new Estoque
                {
                    EstoqueCodigo = 1,
                    Quantidade = 1,
                    Produto = new Produto
                    {
                        ProdutoCodigo = 1
                    }
                });

            _vendaService.AtualizarVenda(new VendaModel
            {
                CodigoVenda = 1,
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusVenda = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ValorTotalDesconto = "0",
                ItemVendaModel = new List<ItemVendaModel>
                {
                    new ItemVendaModel
                    {
                        Codigo = 1,
                        Descricao = "Cinto",
                        PrecoVenda = "1",
                        Quantidade = 1,
                        ValorBruto = "1",
                        ValorLiquido = "1",
                        ValorDesconto = "0"
                    }
                },
                ClienteId = 1
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Condição de pagamento encontrado")]
        public void AtualizarVenda_CondicaoDePagamentoNaoEncontrada_Excecao()
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

            _vendaRepositoryMock
                .Setup(x => x.ObterPorCodigoComItensVenda(1))
                .Returns(new Venda());

            _estoqueRepositoryMock
                .Setup(x => x.ObterPorCodigoProduto(1))
                .Returns(new Estoque
                {
                    EstoqueCodigo = 1,
                    Quantidade = 1,
                    Produto = new Produto
                    {
                        ProdutoCodigo = 1
                    }
                });

            _vendaService.AtualizarVenda(new VendaModel
            {
                CodigoVenda = 1,
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusVenda = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ValorTotalDesconto = "0",
                ItemVendaModel = new List<ItemVendaModel>
                {
                    new ItemVendaModel
                    {
                        Codigo = 1,
                        Descricao = "Cinto",
                        PrecoVenda = "1",
                        Quantidade = 1,
                        ValorBruto = "1",
                        ValorLiquido = "1",
                        ValorDesconto = "0"
                    }
                },
                ClienteId = 1,
                FormaPagamentoId = 1
            });
        }

        [TestMethod]
        public void AtualizarVenda_DadosValidos_VendaAtualizada()
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

            _estoqueRepositoryMock
                .Setup(x => x.ObterPorCodigoProduto(1))
                .Returns(new Estoque
                {
                    EstoqueCodigo = 1,
                    Quantidade = 1,
                    Produto = new Produto
                    {
                        ProdutoCodigo = 1
                    }
                });

            _itemVendaRepositoryMock
                .Setup(x => x.Criar(It.IsAny<ItemVenda>()))
                .Returns(new ItemVenda
                {
                    ProdutoCodigo = 1,
                    ProdutoNome = "Cinto",
                    ItemVendaCodigo = 1,
                    PrecoVenda = 1,
                    Quantidade = 1,
                    ValorBruto = 1,
                    ValorLiquido = 1
                });

            _vendaRepositoryMock
                .Setup(x => x.ObterPorCodigoComItensVenda(1))
                .Returns(new Venda
                {
                    VendaCodigo = 1,
                    DataCadastro = DateTime.Now,
                    ValorTotalLiquido = 1,
                    ItensVenda = new List<ItemVenda>
                    {
                        new ItemVenda
                        {
                            ProdutoCodigo = 1,
                            ProdutoNome = "Cinto",
                            ItemVendaCodigo = 1,
                            PrecoVenda = 1,
                            Quantidade = 1,
                            ValorBruto = 1,
                            ValorLiquido = 1
                        }
                    }
                });

            _vendaService.AtualizarVenda(new VendaModel
            {
                CodigoVenda = 1,
                DataCadastro = DateTime.Now.ToString("dd/MM/yyyy H:mm"),
                StatusVenda = "Aberto",
                ValorTotalBruto = "1",
                ValorTotalLiquido = "1",
                ValorTotalDesconto = "0",
                ItemVendaModel = new List<ItemVendaModel>
                {
                    new ItemVendaModel
                    {
                        Codigo = 1,
                        Descricao = "Cinto",
                        PrecoVenda = "1",
                        Quantidade = 1,
                        ValorBruto = "1",
                        ValorLiquido = "1",
                        ValorDesconto = "0"
                    }
                },
                ClienteId = 1,
                FormaPagamentoId = 1,
                CondicaoPagamentoId = 1
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Venda encontrada")]
        public void ExcluirVenda_VendaNaoEncontrada_Excecao()
        {
            _vendaService.ExcluirVenda(1);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Venda não confirmada")]
        public void ExcluirVenda_VendaConfirmada_Excecao()
        {
            _vendaRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new Venda
                {
                    VendaCodigo = 1,
                    DataCadastro = DateTime.Now,
                    StatusVenda = StatusVendaEnum.Confirmado
                });

            _vendaService.ExcluirVenda(1);
        }

        [TestMethod]
        public void ExcluirVenda_DadosValidos_VendaExcluida()
        {
            _vendaRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new Venda
                {
                    VendaCodigo = 1,
                    DataCadastro = DateTime.Now,
                    StatusVenda = StatusVendaEnum.Aberto
                });

            _vendaService.ExcluirVenda(1);
        }
    }
}
