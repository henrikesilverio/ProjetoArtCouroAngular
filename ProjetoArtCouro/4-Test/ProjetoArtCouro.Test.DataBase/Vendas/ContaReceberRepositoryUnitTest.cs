using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Repositorios.VendaRepository;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.ContaReceber;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Resources.Validation;
using ProjetoArtCouro.Test.DataBase.Infra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoArtCouro.Test.DataBase.Vendas
{
    [TestClass]
    public class ContaReceberRepositoryUnitTest
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

        private Venda ObterVenda()
        {
            return new Venda
            {
                DataCadastro = DateTime.Now,
                StatusVenda = StatusVendaEnum.Aberto,
                ValorTotalBruto = 100,
                ValorTotalDesconto = 0,
                ValorTotalLiquido = 100,
                Cliente = ObterPessoaBase(),
                Usuario = ObterUsuarioBase(),
                CondicaoPagamento = new CondicaoPagamento
                {
                    Ativo = true,
                    Descricao = "A vista",
                    QuantidadeParcelas = 1
                },
                FormaPagamento = new FormaPagamento
                {
                    Ativo = true,
                    Descricao = "Cartão"
                }
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
        public void CriarContaReceber()
        {
            using (var repositorio = new ContaReceberRepository(_context))
            {
                var dataVencimento = DateTime.Now;
                repositorio.Criar(new ContaReceber
                {
                    DataVencimento = dataVencimento,
                    Recebido = true,
                    StatusContaReceber = StatusContaReceberEnum.Recebido,
                    ValorDocumento = 100,
                    Venda = ObterVenda()
                });

                var contas = _context.ContasReceber.ToList();
                Assert.IsTrue(contas.Any(), "Conta não foi incluída");
                Assert.IsTrue(contas.Any(x => x.DataVencimento == dataVencimento), "Conta não foi incluída");
                Assert.IsTrue(contas.Any(x => x.Recebido == true), "Conta não foi incluída");
                Assert.IsTrue(contas.Any(x => x.StatusContaReceber == StatusContaReceberEnum.Recebido), "Conta não foi incluída");
                Assert.IsTrue(contas.Any(x => x.ValorDocumento == 100), "Conta não foi incluída");
                Assert.IsTrue(contas.All(x => x.Venda != null), "Conta sem venda");
            }
        }

        [TestMethod]
        public void ObterContaReceberPorCodigoComVenda()
        {
            using (var repositorio = new ContaReceberRepository(_context))
            {
                repositorio.Criar(new ContaReceber
                {
                    DataVencimento = DateTime.Now,
                    Recebido = true,
                    StatusContaReceber = StatusContaReceberEnum.Recebido,
                    ValorDocumento = 100,
                    Venda = ObterVenda()
                });

                var primeiraContaReceber = _context.ContasReceber.FirstOrDefault();
                Assert.IsNotNull(primeiraContaReceber, "Conta não foi incluída");

                var contaReceber = repositorio.ObterPorCodigoComVenda(primeiraContaReceber.ContaReceberCodigo);
                Assert.AreEqual(primeiraContaReceber, contaReceber, "Conta não é igual");
                Assert.IsNotNull(primeiraContaReceber.Venda, "Conta sem venda");
            }
        }

        [TestMethod]
        public void ObterListaContaReceberPorCodigoVenda()
        {
            using (var repositorio = new ContaReceberRepository(_context))
            {
                var venda = ObterVenda();
                repositorio.Criar(new ContaReceber
                {
                    DataVencimento = DateTime.Now,
                    Recebido = true,
                    StatusContaReceber = StatusContaReceberEnum.Recebido,
                    ValorDocumento = 100,
                    Venda = venda
                });

                var contasReceber = repositorio.ObterListaPorVendaCodigo(venda.VendaCodigo);
                Assert.IsTrue(contasReceber.Any(), "Conta não foi incluída");
                Assert.IsTrue(contasReceber.All(x => x.Venda != null), "Conta sem venda");
            }
        }

        [TestMethod]
        public void ObterListaContaReceberPorFiltro()
        {
            using (var repositorio = new ContaReceberRepository(_context))
            {
                var venda = ObterVenda();
                var dataVencimento = DateTime.Now;
                repositorio.Criar(new ContaReceber
                {
                    DataVencimento = dataVencimento,
                    Recebido = true,
                    StatusContaReceber = StatusContaReceberEnum.Recebido,
                    ValorDocumento = 100,
                    Venda = venda
                });

                var filtro = new PesquisaContaReceber
                {
                    CodigoCliente = 1,
                    CodigoUsuario = 1,
                    CodigoVenda = 1,
                    CPFCNPJ = "12345678909",
                    DataEmissao = dataVencimento,
                    DataVencimento = dataVencimento,
                    NomeCliente = "Henrique",
                    StatusContaReceber = StatusContaReceberEnum.Recebido
                };

                var contasReceber = repositorio.ObterListaPorFiltro(filtro);
                Assert.IsTrue(contasReceber.Any(), "Conta não foi incluída");
                Assert.IsTrue(contasReceber.Any(x => x.StatusContaReceber == StatusContaReceberEnum.Recebido), "Conta não foi incluída");
                Assert.IsTrue(contasReceber.Any(x => x.DataVencimento == dataVencimento), "Conta não foi incluída");
                Assert.IsTrue(contasReceber.All(x => x.Venda != null), "Conta sem venda");
                Assert.IsTrue(contasReceber.All(x => x.Venda.DataCadastro.Date == dataVencimento.Date), "Conta não foi incluída");
                Assert.IsTrue(contasReceber.All(x => x.Venda.VendaCodigo == 1), "Conta não foi incluída");
                Assert.IsTrue(contasReceber.All(x => x.Venda.Cliente != null), "Venda sem cliente");
                Assert.IsTrue(contasReceber.All(x => x.Venda.Cliente.PessoaCodigo == 1), "Conta não foi incluída");
                Assert.IsTrue(contasReceber.All(x => x.Venda.Cliente.Nome == "Henrique"), "Conta não foi incluída");
                Assert.IsTrue(contasReceber.All(x => x.Venda.Cliente.PessoaFisica.CPF == "12345678909"), "Conta não foi incluída");
                Assert.IsTrue(contasReceber.All(x => x.Venda.Usuario != null), "Conta sem usuario");
                Assert.IsTrue(contasReceber.All(x => x.Venda.Usuario.UsuarioCodigo == 1), "Conta não foi incluída");
            }
        }

        [TestMethod]
        public void AtualizarContaReceber()
        {
            using (var repositorio = new ContaReceberRepository(_context))
            {
                var dataVencimento = DateTime.Now;
                repositorio.Criar(new ContaReceber
                {
                    DataVencimento = DateTime.Now,
                    Recebido = true,
                    StatusContaReceber = StatusContaReceberEnum.Recebido,
                    ValorDocumento = 100,
                    Venda = ObterVenda()
                });

                var antesAtualizado = _context.ContasReceber.FirstOrDefault();
                Assert.IsNotNull(antesAtualizado, "Conta não foi incluído");
                var dataCadastro = DateTime.Now;
                antesAtualizado.DataVencimento = DateTime.Now;
                antesAtualizado.Recebido = false;
                antesAtualizado.StatusContaReceber = StatusContaReceberEnum.Aberto;
                antesAtualizado.ValorDocumento = 600;

                repositorio.Atualizar(antesAtualizado);
                var aposAtualizado = _context.ContasReceber.FirstOrDefault();
                Assert.IsNotNull(aposAtualizado, "Conta não foi Atualizado");
                Assert.AreEqual(aposAtualizado.DataVencimento, dataCadastro, "Conta não foi Atualizado");
                Assert.AreEqual(aposAtualizado.Recebido, false, "Conta não foi Atualizado");
                Assert.AreEqual(aposAtualizado.StatusContaReceber, StatusContaReceberEnum.Aberto, "Conta não foi Atualizado");
                Assert.AreEqual(aposAtualizado.ValorDocumento, 600, "Conta não foi Atualizado");
            }
        }

        [TestMethod]
        public void DeletarContaReceber()
        {
            using (var repositorio = new ContaReceberRepository(_context))
            {
                var dataVencimento = DateTime.Now;
                repositorio.Criar(new ContaReceber
                {
                    DataVencimento = DateTime.Now,
                    Recebido = true,
                    StatusContaReceber = StatusContaReceberEnum.Recebido,
                    ValorDocumento = 100,
                    Venda = ObterVenda()
                });

                var primeiro = _context.ContasReceber.FirstOrDefault();
                Assert.IsNotNull(primeiro, "Conta não foi incluído");
                repositorio.Deletar(primeiro);
                var retorno = _context.ContasReceber.FirstOrDefault();
                Assert.IsNull(retorno, "Conta não foi removido");
            }
        }
    }
}
