using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Repositorios.EstoqueRepository;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Entities.Estoques;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Entities.Produtos;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Models.Estoque;
using ProjetoArtCouro.Resources.Validation;
using ProjetoArtCouro.Test.DataBase.Infra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoArtCouro.Test.DataBase.Estoques
{
    [TestClass]
    public class EstoqueRepositoryUnitTest
    {
        private DataBaseContext _context;

        private Pessoa ObterPessoaBase()
        {
            return new Pessoa
            {
                Nome = "Henrique",
                Papeis = new List<Papel>
                {
                    _context.Papeis
                        .FirstOrDefault(x => x.PapelCodigo == (int)TipoPapelPessoaEnum.Fornecedor)
                },
                MeiosComunicacao = new List<MeioComunicacao>
                {
                    new MeioComunicacao
                    {
                        MeioComunicacaoNome = "henrikesilverio@gmail.com",
                        TipoComunicacao = TipoComunicacaoEnum.Email,
                        Principal = true
                    }
                },
                Enderecos = new List<Endereco>
                {
                    new Endereco
                    {
                        Logradouro = "Rua A",
                        Numero = "100",
                        CEP = "88100566",
                        Bairro = "Alfabeto",
                        Cidade = "Maringá",
                        Estado = new Estado { EstadoNome = "PR" },
                        Principal = true
                    }
                },
                PessoaFisica = new PessoaFisica
                {
                    CPF = "12345678909",
                    RG = "203004009",
                    Sexo = "M",
                    EstadoCivil = new EstadoCivil { EstadoCivilNome = "Solteiro" }
                }
            };
        }

        private List<Permissao> ObterPermissoes()
        {
            return new List<Permissao>
            {
                new Permissao
                {
                    AcaoNome = "Pessoas",
                    PermissaoNome = "Pessoas"
                }
            };
        }

        private GrupoPermissao ObterGrupoPermissao()
        {
            return new GrupoPermissao
            {
                GrupoPermissaoNome = "TODOS",
                Permissoes = ObterPermissoes()
            };
        }

        private Usuario ObterUsuarioBase()
        {
            return new Usuario
            {
                UsuarioNome = "Henrique",
                Senha = PasswordAssertionConcern.Encrypt("123456"),
                Ativo = true,
                GrupoPermissao = ObterGrupoPermissao()
            };
        }

        private Compra ObterCompra()
        {
            return new Compra
            {
                CondicaoPagamento = new CondicaoPagamento
                {
                    Descricao = "A vista",
                    QuantidadeParcelas = 1,
                    Ativo = true
                },
                DataCadastro = DateTime.Now,
                FormaPagamento = new FormaPagamento
                {
                    Descricao = "Catão",
                    Ativo = true
                },
                Fornecedor = ObterPessoaBase(),
                StatusCompra = StatusCompraEnum.Aberto,
                Usuario = ObterUsuarioBase(),
                ValorTotalBruto = 110,
                ValorTotalFrete = 10,
                ValorTotalLiquido = 100
            };
        }

        [TestInitialize]
        public void Inicializacao()
        {
            var dbConnection = DbConnectionFactory.CreateTransient();
            var modelBuilder = EntityFrameworkHelper.GetDbModelBuilder();
            _context = new DataBaseContext(dbConnection, modelBuilder);
        }

        [TestMethod]
        public void CriarEstoque()
        {
            using (var repositorio = new EstoqueRepository(_context))
            {
                var data = DateTime.Now;
                repositorio.Criar(new Estoque
                {
                    DataUltimaEntrada = data,
                    Produto = new Produto
                    {
                        PrecoCusto = 1,
                        PrecoVenda = 2,
                        ProdutoNome = "Cinto",
                        Unidade = new Unidade
                        {
                            UnidadeNome = "UN"
                        }
                    },
                    Quantidade = 10
                });

                var estoques = _context.Estoques.ToList();
                Assert.IsTrue(estoques.Any(), "Estoque não foi incluído");
                Assert.IsTrue(estoques.Any(x => x.Quantidade == 10), "Estoque não foi incluído");
                Assert.IsTrue(estoques.Any(x => x.DataUltimaEntrada == data), "Estoque não foi incluído");
                Assert.IsTrue(estoques.All(x => x.Produto != null), "Estoque não foi incluído");
            }
        }

        [TestMethod]
        public void ObterEstoquePorCodigoProduto()
        {
            using (var repositorio = new EstoqueRepository(_context))
            {
                var data = DateTime.Now;
                repositorio.Criar(new Estoque
                {
                    DataUltimaEntrada = data,
                    Produto = new Produto
                    {
                        PrecoCusto = 1,
                        PrecoVenda = 2,
                        ProdutoNome = "Cinto",
                        Unidade = new Unidade
                        {
                            UnidadeNome = "UN"
                        }
                    },
                    Quantidade = 10
                });

                var primeiroEstoque = _context.Estoques.FirstOrDefault();
                Assert.IsNotNull(primeiroEstoque, "Estoque não foi incluído");

                var estoque = repositorio.ObterPorCodigoProduto(1);
                Assert.AreEqual(primeiroEstoque, estoque, "Estoque de pagamento não é igual");
                Assert.IsNotNull(primeiroEstoque.Produto, "Estoque sem produto");
            }
        }

        [TestMethod]
        public void ObterEstoqueListaPorFiltro()
        {
            using (var repositorio = new EstoqueRepository(_context))
            {
                var data = DateTime.Now;
                repositorio.Criar(new Estoque
                {
                    DataUltimaEntrada = data,
                    Produto = new Produto
                    {
                        PrecoCusto = 1,
                        PrecoVenda = 2,
                        ProdutoNome = "Cinto",
                        Unidade = new Unidade
                        {
                            UnidadeNome = "UN"
                        }
                    },
                    Quantidade = 10,
                    Compra = ObterCompra()
                });

                var filtro = new PesquisaEstoque
                {
                    CodigoProduto = 1,
                    DescricaoProduto = "Cinto",
                    CodigoFornecedor = 1,
                    NomeFornecedor = "Henrique",
                    QuantidadeEstoque = 10
                };

                var estoques = repositorio.ObterListaPorFiltro(filtro);
                Assert.IsTrue(estoques.Any(), "Estoque não foi incluída");
                Assert.IsTrue(estoques.Any(x => x.Quantidade == 10), "Estoque não foi incluída");
                Assert.IsTrue(estoques.All(x => x.Compra != null), "Estoque sem compra");
                Assert.IsTrue(estoques.All(x => x.Compra.Fornecedor != null), "Compra sem fornecedor");
                Assert.IsTrue(estoques.All(x => x.Compra.Fornecedor.Nome == "Henrique"), "Estoque não foi incluída");
                Assert.IsTrue(estoques.All(x => x.Compra.Fornecedor.PessoaCodigo == 1), "Estoque não foi incluída");
                Assert.IsTrue(estoques.All(x => x.Produto != null), "Esquete sem produto");
                Assert.IsTrue(estoques.All(x => x.Produto.ProdutoCodigo == 1), "Esquete não foi incluída");
                Assert.IsTrue(estoques.All(x => x.Produto.ProdutoNome == "Cinto"), "Esquete não foi incluída");
            }
        }

        [TestMethod]
        public void AtualizarEstoque()
        {
            using (var repositorio = new EstoqueRepository(_context))
            {
                var data = DateTime.Now;
                repositorio.Criar(new Estoque
                {
                    DataUltimaEntrada = data,
                    Produto = new Produto
                    {
                        PrecoCusto = 1,
                        PrecoVenda = 2,
                        ProdutoNome = "Cinto",
                        Unidade = new Unidade
                        {
                            UnidadeNome = "UN"
                        }
                    },
                    Quantidade = 10
                });
            
                var antesAtualizado = _context.Estoques.FirstOrDefault();
                Assert.IsNotNull(antesAtualizado, "Conta não foi incluído");
                var dataCadastro = DateTime.Now;
                antesAtualizado.DataUltimaEntrada = DateTime.Now;
                antesAtualizado.Quantidade = 12;

                repositorio.Atualizar(antesAtualizado);
                var aposAtualizado = _context.Estoques.FirstOrDefault();
                Assert.IsNotNull(aposAtualizado, "Conta não foi Atualizado");
                Assert.AreEqual(aposAtualizado.DataUltimaEntrada, dataCadastro, "Conta não foi Atualizado");
                Assert.AreEqual(aposAtualizado.Quantidade, 12, "Conta não foi Atualizado");
            }
        }
    }
}
