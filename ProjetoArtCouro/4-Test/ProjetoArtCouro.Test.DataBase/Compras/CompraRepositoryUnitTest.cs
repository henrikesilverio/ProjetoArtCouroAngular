using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Repositorios.CompraRepository;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Models.Compra;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Resources.Validation;
using ProjetoArtCouro.Test.DataBase.Infra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoArtCouro.Test.DataBase.Compras
{
    [TestClass]
    public class CompraRepositoryUnitTest
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

        [TestInitialize]
        public void Inicializacao()
        {
            var dbConnection = DbConnectionFactory.CreateTransient();
            var modelBuilder = EntityFrameworkHelper.GetDbModelBuilder();
            _context = new DataBaseContext(dbConnection, modelBuilder);
        }

        [TestMethod]
        public void CriarCompra()
        {
            using (var repositorio = new CompraRepository(_context))
            {
                repositorio.Criar(new Compra
                {
                    CondicaoPagamento = new CondicaoPagamento { Descricao = "A vista", QuantidadeParcelas = 1, Ativo = true },
                    DataCadastro = DateTime.Now,
                    FormaPagamento = new FormaPagamento { Descricao = "Catão", Ativo = true },
                    Fornecedor = ObterPessoaBase(),
                    StatusCompra = StatusCompraEnum.Aberto,
                    Usuario = ObterUsuarioBase(),
                    ValorTotalBruto = 110,
                    ValorTotalFrete = 10,
                    ValorTotalLiquido = 100,
                    ItensCompra = new List<ItemCompra>
                    {
                        new ItemCompra { PrecoVenda = 100, ProdutoCodigo = 1, ProdutoNome = "Cinto", Quantidade = 1, ValorBruto = 100, ValorLiquido = 100 }
                    }
                });

                var compras = _context.Compras.ToList();
                Assert.IsTrue(compras.Any(), "Compra não foi incluído");
                Assert.IsTrue(compras.Any(x => x.StatusCompra == StatusCompraEnum.Aberto), "Compra não foi incluído");
                Assert.IsTrue(compras.Any(x => x.ValorTotalBruto == 110), "Compra não foi incluído");
                Assert.IsTrue(compras.Any(x => x.ValorTotalFrete == 10), "Compra não foi incluído");
                Assert.IsTrue(compras.Any(x => x.ValorTotalLiquido == 100), "Compra não foi incluído");
            }
        }

        [TestMethod]
        public void ObterCompraPorCodigo()
        {
            using (var repositorio = new CompraRepository(_context))
            {
                repositorio.Criar(new Compra
                {
                    CondicaoPagamento = new CondicaoPagamento { Descricao = "A vista", QuantidadeParcelas = 1, Ativo = true },
                    DataCadastro = DateTime.Now,
                    FormaPagamento = new FormaPagamento { Descricao = "Catão", Ativo = true },
                    Fornecedor = ObterPessoaBase(),
                    StatusCompra = StatusCompraEnum.Aberto,
                    Usuario = ObterUsuarioBase(),
                    ValorTotalBruto = 110,
                    ValorTotalFrete = 10,
                    ValorTotalLiquido = 100,
                    ItensCompra = new List<ItemCompra>
                    {
                        new ItemCompra { PrecoVenda = 100, ProdutoCodigo = 1, ProdutoNome = "Cinto", Quantidade = 1, ValorBruto = 100, ValorLiquido = 100 }
                    }
                });

                var primeiraCompra = _context.Compras.FirstOrDefault();
                Assert.IsNotNull(primeiraCompra, "Compra não foi incluído");

                var compra = repositorio.ObterPorCodigo(primeiraCompra.CompraCodigo);
                Assert.AreEqual(primeiraCompra, compra, "Compra não é igual");
            }
        }

        [TestMethod]
        public void ObterCompraPorCodigoComItensCompra()
        {
            using (var repositorio = new CompraRepository(_context))
            {
                repositorio.Criar(new Compra
                {
                    CondicaoPagamento = new CondicaoPagamento { Descricao = "A vista", QuantidadeParcelas = 1, Ativo = true },
                    DataCadastro = DateTime.Now,
                    FormaPagamento = new FormaPagamento { Descricao = "Catão", Ativo = true },
                    Fornecedor = ObterPessoaBase(),
                    StatusCompra = StatusCompraEnum.Aberto,
                    Usuario = ObterUsuarioBase(),
                    ValorTotalBruto = 110,
                    ValorTotalFrete = 10,
                    ValorTotalLiquido = 100,
                    ItensCompra = new List<ItemCompra>
                    {
                        new ItemCompra { PrecoVenda = 100, ProdutoCodigo = 1, ProdutoNome = "Cinto", Quantidade = 1, ValorBruto = 100, ValorLiquido = 100 }
                    }
                });

                var primeiraCompra = _context.Compras.FirstOrDefault();
                Assert.IsNotNull(primeiraCompra, "Compra não foi incluído");

                var compra = repositorio.ObterPorCodigoComItensCompra(primeiraCompra.CompraCodigo);
                Assert.AreEqual(primeiraCompra, compra, "Compra não é igual");
                Assert.IsNotNull(compra.ItensCompra, "Compra sem item");
            }
        }

        [TestMethod]
        public void ObterListaVendaPorFiltro()
        {
            using (var repositorio = new CompraRepository(_context))
            {
                var dataCadastro = DateTime.Now;
                repositorio.Criar(new Compra
                {
                    CondicaoPagamento = new CondicaoPagamento { Descricao = "A vista", QuantidadeParcelas = 1, Ativo = true },
                    DataCadastro = dataCadastro,
                    FormaPagamento = new FormaPagamento { Descricao = "Catão", Ativo = true },
                    Fornecedor = ObterPessoaBase(),
                    StatusCompra = StatusCompraEnum.Aberto,
                    Usuario = ObterUsuarioBase(),
                    ValorTotalBruto = 110,
                    ValorTotalFrete = 10,
                    ValorTotalLiquido = 100,
                    ItensCompra = new List<ItemCompra>
                    {
                        new ItemCompra { PrecoVenda = 100, ProdutoCodigo = 1, ProdutoNome = "Cinto", Quantidade = 1, ValorBruto = 100, ValorLiquido = 100 }
                    }
                });

                var filtro = new PesquisaCompra
                {
                    CodigoFornecedor = 1,
                    CodigoUsuario = 1,
                    CodigoCompra = 1,
                    CPFCNPJ = "12345678909",
                    DataCadastro = dataCadastro,
                    NomeFornecedor = "Henrique",
                    StatusCompra = StatusCompraEnum.Aberto
                };

                var compras = repositorio.ObterListaPorFiltro(filtro);

                Assert.IsTrue(compras.Any(), "Compra não incluido");
                Assert.IsTrue(compras.All(x => x.CompraCodigo == 1), "Compra não incluido");
                Assert.IsTrue(compras.All(x => x.DataCadastro == dataCadastro), "Compra não incluido");
                Assert.IsTrue(compras.All(x => x.StatusCompra == StatusCompraEnum.Aberto), "Compra não incluido");
                Assert.IsTrue(compras.All(x => x.Usuario != null), "Compra sem usuario");
                Assert.IsTrue(compras.All(x => x.Fornecedor != null), "Compra sem cliente");
                Assert.IsTrue(compras.All(x => x.Fornecedor.PessoaCodigo == 1), "Compra não incluido");
                Assert.IsTrue(compras.All(x => x.Fornecedor.Nome == "Henrique"), "Compra não incluido");
                Assert.IsTrue(compras.All(x => x.Fornecedor.PessoaFisica != null), "Cliente sem pessoa fisica");
                Assert.IsTrue(compras.All(x => x.Fornecedor.PessoaFisica.CPF == "12345678909"), "Cliente sem pessoa fisica");
            }
        }

        [TestMethod]
        public void AtualizarCompra()
        {
            using (var repositorio = new CompraRepository(_context))
            {
                repositorio.Criar(new Compra
                {
                    CondicaoPagamento = new CondicaoPagamento { Descricao = "A vista", QuantidadeParcelas = 1, Ativo = true },
                    DataCadastro = DateTime.Now,
                    FormaPagamento = new FormaPagamento { Descricao = "Catão", Ativo = true },
                    Fornecedor = ObterPessoaBase(),
                    StatusCompra = StatusCompraEnum.Aberto,
                    Usuario = ObterUsuarioBase(),
                    ValorTotalBruto = 110,
                    ValorTotalFrete = 10,
                    ValorTotalLiquido = 100,
                    ItensCompra = new List<ItemCompra>
                    {
                        new ItemCompra { PrecoVenda = 100, ProdutoCodigo = 1, ProdutoNome = "Cinto", Quantidade = 1, ValorBruto = 100, ValorLiquido = 100 }
                    }
                });

                var antesAtualizado = _context.Compras.FirstOrDefault();
                Assert.IsNotNull(antesAtualizado, "Compra não foi incluído");

                var dataCadastro = DateTime.Now;
                antesAtualizado.StatusCompra = StatusCompraEnum.Confirmado;
                antesAtualizado.DataCadastro = dataCadastro;
                antesAtualizado.ValorTotalBruto = 200;
                antesAtualizado.ValorTotalFrete = 100;
                antesAtualizado.ValorTotalLiquido = 100;

                repositorio.Atualizar(antesAtualizado);
                var aposAtualizado = _context.Compras.FirstOrDefault();

                Assert.IsNotNull(aposAtualizado, "Compra não foi Atualizado");
                Assert.AreEqual(aposAtualizado.DataCadastro, dataCadastro, "Compra não foi Atualizado");
                Assert.AreEqual(aposAtualizado.ValorTotalBruto, 200, "Compra não foi Atualizado");
                Assert.AreEqual(aposAtualizado.ValorTotalFrete, 100, "Compra não foi Atualizado");
                Assert.AreEqual(aposAtualizado.ValorTotalLiquido, 100, "Compra não foi Atualizado");
            }
        }

        [TestMethod]
        public void DeletarCompra()
        {
            using (var repositorio = new CompraRepository(_context))
            {
                repositorio.Criar(new Compra
                {
                    CondicaoPagamento = new CondicaoPagamento { Descricao = "A vista", QuantidadeParcelas = 1, Ativo = true },
                    DataCadastro = DateTime.Now,
                    FormaPagamento = new FormaPagamento { Descricao = "Catão", Ativo = true },
                    Fornecedor = ObterPessoaBase(),
                    StatusCompra = StatusCompraEnum.Aberto,
                    Usuario = ObterUsuarioBase(),
                    ValorTotalBruto = 110,
                    ValorTotalFrete = 10,
                    ValorTotalLiquido = 100,
                    ItensCompra = new List<ItemCompra>
                    {
                        new ItemCompra { PrecoVenda = 100, ProdutoCodigo = 1, ProdutoNome = "Cinto", Quantidade = 1, ValorBruto = 100, ValorLiquido = 100 }
                    }
                });

                var primeiro = _context.Compras.FirstOrDefault();
                Assert.IsNotNull(primeiro, "Compra não foi incluído");
                repositorio.Deletar(primeiro);
                var retorno = _context.Compras.FirstOrDefault();
                Assert.IsNull(retorno, "Compra não foi removido");
            }
        }
    }
}
