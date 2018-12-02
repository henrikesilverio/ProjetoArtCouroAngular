using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Repositorios.VendaRepository;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Models.Venda;
using ProjetoArtCouro.Resources.Validation;
using ProjetoArtCouro.Test.DataBase.Infra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoArtCouro.Test.DataBase.Vendas
{
    [TestClass]
    public class VendaRepositoryUnitTest
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
                        .FirstOrDefault(x => x.PapelCodigo == (int)TipoPapelPessoaEnum.Cliente)
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
        public void CriarVenda()
        {
            using (var repositorio = new VendaRepository(_context))
            {
                repositorio.Criar(new Venda
                {
                    DataCadastro = DateTime.Now,
                    StatusVenda = StatusVendaEnum.Aberto,
                    ValorTotalBruto = 100,
                    ValorTotalDesconto = 0,
                    ValorTotalLiquido = 100,
                    Cliente = ObterPessoaBase(),
                    Usuario = ObterUsuarioBase(),
                    CondicaoPagamento = new CondicaoPagamento { Ativo = true, Descricao = "A vista", QuantidadeParcelas = 1 },
                    FormaPagamento = new FormaPagamento { Ativo = true, Descricao = "Cartão" },
                    ItensVenda = new List<ItemVenda>
                    {
                        new ItemVenda { PrecoVenda = 100, ProdutoNome = "Cinto", Quantidade = 1, ValorBruto = 100, ValorDesconto = 0, ValorLiquido = 100}
                    }
                });

                var vendas = _context.Vendas.ToList();
                Assert.IsTrue(vendas.Any(), "Venda não foi incluído");
                Assert.IsTrue(vendas.Any(x => x.StatusVenda == StatusVendaEnum.Aberto), "Venda não foi incluído");
                Assert.IsTrue(vendas.Any(x => x.ValorTotalBruto == 100), "Venda não foi incluído");
                Assert.IsTrue(vendas.Any(x => x.ValorTotalDesconto == 0), "Venda não foi incluído");
                Assert.IsTrue(vendas.Any(x => x.ValorTotalLiquido == 100), "Venda não foi incluído");
            }
        }

        [TestMethod]
        public void ObterVendaPorId()
        {
            using (var repositorio = new VendaRepository(_context))
            {
                repositorio.Criar(new Venda
                {
                    DataCadastro = DateTime.Now,
                    StatusVenda = StatusVendaEnum.Aberto,
                    ValorTotalBruto = 100,
                    ValorTotalDesconto = 0,
                    ValorTotalLiquido = 100,
                    Cliente = ObterPessoaBase(),
                    Usuario = ObterUsuarioBase(),
                    CondicaoPagamento = new CondicaoPagamento { Ativo = true, Descricao = "A vista", QuantidadeParcelas = 1 },
                    FormaPagamento = new FormaPagamento { Ativo = true, Descricao = "Cartão" },
                    ItensVenda = new List<ItemVenda>
                    {
                        new ItemVenda { PrecoVenda = 100, ProdutoNome = "Cinto", Quantidade = 1, ValorBruto = 100, ValorDesconto = 0, ValorLiquido = 100}
                    }
                });

                var primeiraVenda = _context.Vendas.FirstOrDefault();
                Assert.IsNotNull(primeiraVenda, "Venda não foi incluído");

                var venda = repositorio.ObterPorId(primeiraVenda.VendaId);
                Assert.AreEqual(primeiraVenda, venda, "Venda não é igual");
            }
        }

        [TestMethod]
        public void ObterVendaPorCodigo()
        {
            using (var repositorio = new VendaRepository(_context))
            {
                repositorio.Criar(new Venda
                {
                    DataCadastro = DateTime.Now,
                    StatusVenda = StatusVendaEnum.Aberto,
                    ValorTotalBruto = 100,
                    ValorTotalDesconto = 0,
                    ValorTotalLiquido = 100,
                    Cliente = ObterPessoaBase(),
                    Usuario = ObterUsuarioBase(),
                    CondicaoPagamento = new CondicaoPagamento { Ativo = true, Descricao = "A vista", QuantidadeParcelas = 1 },
                    FormaPagamento = new FormaPagamento { Ativo = true, Descricao = "Cartão" },
                    ItensVenda = new List<ItemVenda>
                    {
                        new ItemVenda { PrecoVenda = 100, ProdutoNome = "Cinto", Quantidade = 1, ValorBruto = 100, ValorDesconto = 0, ValorLiquido = 100}
                    }
                });

                var primeiraVenda = _context.Vendas.FirstOrDefault();
                Assert.IsNotNull(primeiraVenda, "Venda não foi incluído");

                var venda = repositorio.ObterPorCodigo(primeiraVenda.VendaCodigo);
                Assert.AreEqual(primeiraVenda, venda, "Venda não é igual");
            }
        }

        [TestMethod]
        public void ObterVendaPorCodigoComItensVenda()
        {
            using (var repositorio = new VendaRepository(_context))
            {
                repositorio.Criar(new Venda
                {
                    DataCadastro = DateTime.Now,
                    StatusVenda = StatusVendaEnum.Aberto,
                    ValorTotalBruto = 100,
                    ValorTotalDesconto = 0,
                    ValorTotalLiquido = 100,
                    Cliente = ObterPessoaBase(),
                    Usuario = ObterUsuarioBase(),
                    CondicaoPagamento = new CondicaoPagamento { Ativo = true, Descricao = "A vista", QuantidadeParcelas = 1 },
                    FormaPagamento = new FormaPagamento { Ativo = true, Descricao = "Cartão" },
                    ItensVenda = new List<ItemVenda>
                    {
                        new ItemVenda { PrecoVenda = 100, ProdutoNome = "Cinto", Quantidade = 1, ValorBruto = 100, ValorDesconto = 0, ValorLiquido = 100}
                    }
                });

                var primeiraVenda = _context.Vendas.FirstOrDefault();
                Assert.IsNotNull(primeiraVenda, "Venda não foi incluído");
            
                var venda = repositorio.ObterPorCodigoComItensVenda(primeiraVenda.VendaCodigo);
                Assert.AreEqual(primeiraVenda, venda, "Venda não é igual");
                Assert.IsNotNull(primeiraVenda.ItensVenda, "Venda sem itens");
            }
        }

        [TestMethod]
        public void ObterListaVenda()
        {
            using (var repositorio = new VendaRepository(_context))
            {
                repositorio.Criar(new Venda
                {
                    DataCadastro = DateTime.Now,
                    StatusVenda = StatusVendaEnum.Aberto,
                    ValorTotalBruto = 100,
                    ValorTotalDesconto = 0,
                    ValorTotalLiquido = 100,
                    Cliente = ObterPessoaBase(),
                    Usuario = ObterUsuarioBase(),
                    CondicaoPagamento = new CondicaoPagamento { Ativo = true, Descricao = "A vista", QuantidadeParcelas = 1 },
                    FormaPagamento = new FormaPagamento { Ativo = true, Descricao = "Cartão" },
                    ItensVenda = new List<ItemVenda>
                    {
                        new ItemVenda { PrecoVenda = 100, ProdutoNome = "Cinto", Quantidade = 1, ValorBruto = 100, ValorDesconto = 0, ValorLiquido = 100}
                    }
                });

                var vendas = repositorio.ObterLista();
                Assert.IsTrue(vendas.Any(), "Venda não incluido");
            }
        }

        [TestMethod]
        public void ObterListaVendaPorFiltro()
        {
            using (var repositorio = new VendaRepository(_context))
            {
                var dataCadastro = DateTime.Now;
                repositorio.Criar(new Venda
                {
                    DataCadastro = dataCadastro,
                    StatusVenda = StatusVendaEnum.Aberto,
                    ValorTotalBruto = 100,
                    ValorTotalDesconto = 0,
                    ValorTotalLiquido = 100,
                    Cliente = ObterPessoaBase(),
                    Usuario = ObterUsuarioBase(),
                    CondicaoPagamento = new CondicaoPagamento { Ativo = true, Descricao = "A vista", QuantidadeParcelas = 1 },
                    FormaPagamento = new FormaPagamento { Ativo = true, Descricao = "Cartão" },
                    ItensVenda = new List<ItemVenda>
                    {
                        new ItemVenda { PrecoVenda = 100, ProdutoNome = "Cinto", Quantidade = 1, ValorBruto = 100, ValorDesconto = 0, ValorLiquido = 100}
                    }
                });

                var filtro = new PesquisaVenda
                {
                    CodigoCliente = 1,
                    CodigoUsuario = 1,
                    CodigoVenda = 1,
                    CPFCNPJ = "12345678909",
                    DataCadastro = dataCadastro,
                    NomeCliente = "Henrique",
                    StatusVenda = StatusVendaEnum.Aberto
                };
                var vendas = repositorio.ObterListaPorFiltro(filtro);
                Assert.IsTrue(vendas.Any(), "Venda não incluido");
                Assert.IsTrue(vendas.All(x => x.VendaCodigo == 1), "Venda não incluido");
                Assert.IsTrue(vendas.All(x => x.DataCadastro == dataCadastro), "Venda não incluido");
                Assert.IsTrue(vendas.All(x => x.StatusVenda == StatusVendaEnum.Aberto), "Venda não incluido");
                Assert.IsTrue(vendas.All(x => x.Usuario != null), "Venda sem usuario");
                Assert.IsTrue(vendas.All(x => x.Cliente != null), "Venda sem cliente");
                Assert.IsTrue(vendas.All(x => x.Cliente.PessoaCodigo == 1), "Venda não incluido");
                Assert.IsTrue(vendas.All(x => x.Cliente.Nome == "Henrique"), "Venda não incluido");
                Assert.IsTrue(vendas.All(x => x.Cliente.PessoaFisica != null), "Cliente sem pessoa fisica");
                Assert.IsTrue(vendas.All(x => x.Cliente.PessoaFisica.CPF == "12345678909"), "Cliente sem pessoa fisica");
            }
        }

        [TestMethod]
        public void AtualizarVenda()
        {
            using (var repositorio = new VendaRepository(_context))
            {
                repositorio.Criar(new Venda
                {
                    DataCadastro = DateTime.Now,
                    StatusVenda = StatusVendaEnum.Aberto,
                    ValorTotalBruto = 100,
                    ValorTotalDesconto = 0,
                    ValorTotalLiquido = 100,
                    Cliente = ObterPessoaBase(),
                    Usuario = ObterUsuarioBase(),
                    CondicaoPagamento = new CondicaoPagamento { Ativo = true, Descricao = "A vista", QuantidadeParcelas = 1 },
                    FormaPagamento = new FormaPagamento { Ativo = true, Descricao = "Cartão" },
                    ItensVenda = new List<ItemVenda>
                    {
                        new ItemVenda { PrecoVenda = 100, ProdutoNome = "Cinto", Quantidade = 1, ValorBruto = 100, ValorDesconto = 0, ValorLiquido = 100}
                    }
                });

                var antesAtualizado = _context.Vendas.FirstOrDefault();
                Assert.IsNotNull(antesAtualizado, "Venda não foi incluído");
                var dataCadastro = DateTime.Now;
                antesAtualizado.StatusVenda = StatusVendaEnum.Confirmado;
                antesAtualizado.DataCadastro = dataCadastro;
                antesAtualizado.ValorTotalBruto = 200;
                antesAtualizado.ValorTotalDesconto = 100;
                antesAtualizado.ValorTotalLiquido = 100;

                repositorio.Atualizar(antesAtualizado);
                var aposAtualizado = _context.Vendas.FirstOrDefault();
                Assert.IsNotNull(aposAtualizado, "Venda não foi Atualizado");
                Assert.AreEqual(aposAtualizado.DataCadastro, dataCadastro, "Venda não foi Atualizado");
                Assert.AreEqual(aposAtualizado.ValorTotalBruto, 200, "Venda não foi Atualizado");
                Assert.AreEqual(aposAtualizado.ValorTotalDesconto, 100, "Venda não foi Atualizado");
                Assert.AreEqual(aposAtualizado.ValorTotalLiquido, 100, "Venda não foi Atualizado");
            }
        }

        [TestMethod]
        public void DeletarVenda()
        {
            using (var repositorio = new VendaRepository(_context))
            {
                repositorio.Criar(new Venda
                {
                    DataCadastro = DateTime.Now,
                    StatusVenda = StatusVendaEnum.Aberto,
                    ValorTotalBruto = 100,
                    ValorTotalDesconto = 0,
                    ValorTotalLiquido = 100,
                    Cliente = ObterPessoaBase(),
                    Usuario = ObterUsuarioBase(),
                    CondicaoPagamento = new CondicaoPagamento { Ativo = true, Descricao = "A vista", QuantidadeParcelas = 1 },
                    FormaPagamento = new FormaPagamento { Ativo = true, Descricao = "Cartão" },
                    ItensVenda = new List<ItemVenda>
                    {
                        new ItemVenda { PrecoVenda = 100, ProdutoNome = "Cinto", Quantidade = 1, ValorBruto = 100, ValorDesconto = 0, ValorLiquido = 100}
                    }
                });

                var primeiro = _context.Vendas.FirstOrDefault();
                Assert.IsNotNull(primeiro, "Venda não foi incluído");
                repositorio.Deletar(primeiro);
                var retorno = _context.Vendas.FirstOrDefault();
                Assert.IsNull(retorno, "Venda não foi removido");
            }
        }
    }
}
