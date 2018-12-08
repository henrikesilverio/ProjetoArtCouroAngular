using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Repositorios.CompraRepository;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Models.ContaPagar;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Resources.Validation;
using ProjetoArtCouro.Test.DataBase.Infra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoArtCouro.Test.DataBase.Compras
{
    [TestClass]
    public class ContaPagarRepositoryUnitTest
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
        public void CriarContaPagar()
        {
            using (var repositorio = new ContaPagarRepository(_context))
            {
                var dataVencimento = DateTime.Now;
                repositorio.Criar(new ContaPagar
                {
                    DataVencimento = dataVencimento,
                    Pago = false,
                    StatusContaPagar = StatusContaPagarEnum.Aberto,
                    ValorDocumento = 100,
                    Compra = ObterCompra()
                });

                var contas = _context.ContasPagar.ToList();
                Assert.IsTrue(contas.Any(), "Conta não foi incluída");
                Assert.IsTrue(contas.Any(x => x.DataVencimento == dataVencimento), "Conta não foi incluída");
                Assert.IsTrue(contas.Any(x => x.Pago == false), "Conta não foi incluída");
                Assert.IsTrue(contas.Any(x => x.StatusContaPagar == StatusContaPagarEnum.Aberto), "Conta não foi incluída");
                Assert.IsTrue(contas.Any(x => x.ValorDocumento == 100), "Conta não foi incluída");
                Assert.IsTrue(contas.All(x => x.Compra != null), "Conta sem compra");
            }
        }

        [TestMethod]
        public void ObterContaPagarPorCodigoComVenda()
        {
            using (var repositorio = new ContaPagarRepository(_context))
            {
                repositorio.Criar(new ContaPagar
                {
                    DataVencimento = DateTime.Now,
                    Pago = false,
                    StatusContaPagar = StatusContaPagarEnum.Aberto,
                    ValorDocumento = 100,
                    Compra = ObterCompra()
                });

                var primeiraContaPagar = _context.ContasPagar.FirstOrDefault();
                Assert.IsNotNull(primeiraContaPagar, "Conta não foi incluída");

                var contaPagar = repositorio.ObterPorCodigoComCompra(primeiraContaPagar.ContaPagarCodigo);
                Assert.AreEqual(primeiraContaPagar, contaPagar, "Conta não é igual");
                Assert.IsNotNull(primeiraContaPagar.Compra, "Conta sem compra");
            }
        }

        [TestMethod]
        public void ObterListaContaPagarPorCodigoVenda()
        {
            using (var repositorio = new ContaPagarRepository(_context))
            {
                var compra = ObterCompra();
                repositorio.Criar(new ContaPagar
                {
                    DataVencimento = DateTime.Now,
                    Pago = false,
                    StatusContaPagar = StatusContaPagarEnum.Aberto,
                    ValorDocumento = 100,
                    Compra = compra
                });

                var contasPagar = repositorio.ObterListaPorCompraCodigo(compra.CompraCodigo);
                Assert.IsTrue(contasPagar.Any(), "Conta não foi incluída");
                Assert.IsTrue(contasPagar.All(x => x.Compra != null), "Conta sem compra");
            }
        }

        [TestMethod]
        public void ObterListaContaPagarPorFiltro()
        {
            using (var repositorio = new ContaPagarRepository(_context))
            {
                var compra = ObterCompra();
                var dataVencimento = DateTime.Now;
                repositorio.Criar(new ContaPagar
                {
                    DataVencimento = dataVencimento,
                    Pago = false,
                    StatusContaPagar = StatusContaPagarEnum.Aberto,
                    ValorDocumento = 100,
                    Compra = compra
                });

                var filtro = new PesquisaContaPagar
                {
                    CodigoFornecedor = 1,
                    CodigoUsuario = 1,
                    CodigoCompra = 1,
                    CPFCNPJ = "12345678909",
                    DataEmissao = dataVencimento,
                    DataVencimento = dataVencimento,
                    NomeFornecedor = "Henrique",
                    StatusContaPagar = StatusContaPagarEnum.Aberto
                };

                var contasPagar = repositorio.ObterListaPorFiltro(filtro);
                Assert.IsTrue(contasPagar.Any(), "Conta não foi incluída");
                Assert.IsTrue(contasPagar.Any(x => x.StatusContaPagar == StatusContaPagarEnum.Aberto), "Conta não foi incluída");
                Assert.IsTrue(contasPagar.Any(x => x.DataVencimento == dataVencimento), "Conta não foi incluída");
                Assert.IsTrue(contasPagar.All(x => x.Compra != null), "Conta sem compra");
                Assert.IsTrue(contasPagar.All(x => x.Compra.DataCadastro.Date == dataVencimento.Date), "Conta não foi incluída");
                Assert.IsTrue(contasPagar.All(x => x.Compra.CompraCodigo == 1), "Conta não foi incluída");
                Assert.IsTrue(contasPagar.All(x => x.Compra.Fornecedor != null), "Compra sem fornecedor");
                Assert.IsTrue(contasPagar.All(x => x.Compra.Fornecedor.PessoaCodigo == 1), "Conta não foi incluída");
                Assert.IsTrue(contasPagar.All(x => x.Compra.Fornecedor.Nome == "Henrique"), "Conta não foi incluída");
                Assert.IsTrue(contasPagar.All(x => x.Compra.Fornecedor.PessoaFisica.CPF == "12345678909"), "Conta não foi incluída");
                Assert.IsTrue(contasPagar.All(x => x.Compra.Usuario != null), "Conta sem usuario");
                Assert.IsTrue(contasPagar.All(x => x.Compra.Usuario.UsuarioCodigo == 1), "Conta não foi incluída");
            }
        }

        [TestMethod]
        public void AtualizarContaPagar()
        {
            using (var repositorio = new ContaPagarRepository(_context))
            {
                var dataVencimento = DateTime.Now;
                repositorio.Criar(new ContaPagar
                {
                    DataVencimento = dataVencimento,
                    Pago = false,
                    StatusContaPagar = StatusContaPagarEnum.Aberto,
                    ValorDocumento = 100,
                    Compra = ObterCompra()
                });

                var antesAtualizado = _context.ContasPagar.FirstOrDefault();
                Assert.IsNotNull(antesAtualizado, "Conta não foi incluído");
                var dataCadastro = DateTime.Now;
                antesAtualizado.DataVencimento = DateTime.Now;
                antesAtualizado.Pago = true;
                antesAtualizado.StatusContaPagar = StatusContaPagarEnum.Pago;
                antesAtualizado.ValorDocumento = 600;

                repositorio.Atualizar(antesAtualizado);
                var aposAtualizado = _context.ContasPagar.FirstOrDefault();
                Assert.IsNotNull(aposAtualizado, "Conta não foi Atualizado");
                Assert.AreEqual(aposAtualizado.DataVencimento, dataCadastro, "Conta não foi Atualizado");
                Assert.AreEqual(aposAtualizado.Pago, true, "Conta não foi Atualizado");
                Assert.AreEqual(aposAtualizado.StatusContaPagar, StatusContaPagarEnum.Pago, "Conta não foi Atualizado");
                Assert.AreEqual(aposAtualizado.ValorDocumento, 600, "Conta não foi Atualizado");
            }
        }

        [TestMethod]
        public void DeletarContaPagar()
        {
            using (var repositorio = new ContaPagarRepository(_context))
            {
                var dataVencimento = DateTime.Now;
                repositorio.Criar(new ContaPagar
                {
                    DataVencimento = dataVencimento,
                    Pago = false,
                    StatusContaPagar = StatusContaPagarEnum.Aberto,
                    ValorDocumento = 100,
                    Compra = ObterCompra()
                });

                var primeiro = _context.ContasPagar.FirstOrDefault();
                Assert.IsNotNull(primeiro, "Conta não foi incluído");
                repositorio.Deletar(primeiro);
                var retorno = _context.ContasPagar.FirstOrDefault();
                Assert.IsNull(retorno, "Conta não foi removido");
            }
        }
    }
}
