using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Repositorios.PessoaRepository;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Models.Pessoa;
using ProjetoArtCouro.Test.DataBase.Infra;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ProjetoArtCouro.Test.DataBase.Pessoas
{
    [TestClass]
    public class PessoaJuridicaRepositoryUnitTest
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
                        .FirstOrDefault(x => x.PapelCodigo == (int)TipoPapelPessoaEnum.Funcionario)
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
                }
            };
        }

        private PessoaJuridica ObterPessoaJuridicaBase()
        {
            return new PessoaJuridica
            {
                CNPJ = "123456789090001",
                Contato = "Henrique",
                Pessoa = ObterPessoaBase()
            };
        }

        [TestInitialize]
        public void Inicializacao()
        {
            var dbConnection = DbConnectionFactory.CreateTransient();
            var modelBuilder = EntityFrameworkHelper.GetDbModelBuilder();
            _context = new DataBaseContext(dbConnection, modelBuilder);

            _context.Papeis.AddOrUpdate(
                p => p.PapelNome,
                new Papel { PapelNome = "Pessoa Fisica" },
                new Papel { PapelNome = "Pessoa Juridica" },
                new Papel { PapelNome = "Funcionario" },
                new Papel { PapelNome = "Cliente" },
                new Papel { PapelNome = "Fornecedor" });

            _context.SaveChanges();
        }

        [TestMethod]
        public void CriarPessoaJuridica()
        {
            using (var repositorio = new PessoaJuridicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaJuridicaBase());

                var primeiraPessoaJuridica = _context.PessoasJuridicas.FirstOrDefault();
                Assert.IsNotNull(primeiraPessoaJuridica, "Pessoa não foi incluído");

                Assert.IsNotNull(primeiraPessoaJuridica.CNPJ, "Pessoa Juridica sem CNPJ");
                Assert.IsNotNull(primeiraPessoaJuridica.Contato, "Pessoa Juridica sem Contato");
                Assert.IsNotNull(primeiraPessoaJuridica.Pessoa, "Pessoa Juridica sem pessoa base");
            }
        }

        [TestMethod]
        public void ObterPessoaJuridicaPorId()
        {
            using (var repositorio = new PessoaJuridicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaJuridicaBase());

                var primeiroPessoaJuridica = _context.PessoasJuridicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaJuridica, "Pessoa não foi incluído");

                var pessoa = repositorio.ObterPorId(primeiroPessoaJuridica.PessoaId);
                Assert.AreEqual(primeiroPessoaJuridica, pessoa, "Pessoa não é igual");
            }
        }

        [TestMethod]
        public void ObterPessoaJuridicaPorCNPJ()
        {
            using (var repositorio = new PessoaJuridicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaJuridicaBase());

                var primeiroPessoaJuridica = _context.PessoasJuridicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaJuridica, "Pessoa Juridica não foi incluído");

                var pessoa = repositorio.ObterPorCNPJ(primeiroPessoaJuridica.CNPJ);
                Assert.AreEqual(primeiroPessoaJuridica, pessoa, "Pessoa Juridica não é igual");
            }
        }

        [TestMethod]
        public void ObterListaDePessoaJuridica()
        {
            using (var repositorio = new PessoaJuridicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaJuridicaBase());

                var primeiroPessoaJuridica = _context.PessoasJuridicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaJuridica, "Pessoa Juridica não foi incluído");

                var pessoas = repositorio.ObterLista();
                Assert.IsTrue(pessoas.Any(), "Pessoa Juridica não é igual");
            }
        }

        [TestMethod]
        public void ObterListaDePessoaJuridicaPorCodigo()
        {
            using (var repositorio = new PessoaJuridicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaJuridicaBase());

                var primeiroPessoaJuridica = _context.PessoasJuridicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaJuridica, "Pessoa Juridica não foi incluído");

                var filtro = new PesquisaPessoaJuridica { Codigo = primeiroPessoaJuridica.PessoaJuridicaCodigo };
                var pessoas = repositorio.ObterListaPorFiltro(filtro);
                Assert.IsTrue(pessoas.Any(), "Pessoa Juridica não é igual");
            }
        }

        [TestMethod]
        public void ObterListaDePessoaJuridicaPorNome()
        {
            using (var repositorio = new PessoaJuridicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaJuridicaBase());

                var primeiroPessoaJuridica = _context.PessoasJuridicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaJuridica, "Pessoa Juridica não foi incluído");

                var filtro = new PesquisaPessoaJuridica { Nome = primeiroPessoaJuridica.Pessoa.Nome };
                var pessoas = repositorio.ObterListaPorFiltro(filtro);
                Assert.IsTrue(pessoas.Any(), "Pessoa Juridica não é igual");
            }
        }

        [TestMethod]
        public void ObterListaDePessoaJuridicaPorCPF()
        {
            using (var repositorio = new PessoaJuridicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaJuridicaBase());

                var primeiroPessoaJuridica = _context.PessoasJuridicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaJuridica, "Pessoa Juridica não foi incluído");

                var filtro = new PesquisaPessoaJuridica { CNPJ = primeiroPessoaJuridica.CNPJ };
                var pessoas = repositorio.ObterListaPorFiltro(filtro);
                Assert.IsTrue(pessoas.Any(), "Pessoa Juridica não é igual");
            }
        }

        [TestMethod]
        public void ObterListaDePessoaJuridicaPorEmail()
        {
            using (var repositorio = new PessoaJuridicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaJuridicaBase());

                var primeiroPessoaJuridica = _context.PessoasJuridicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaJuridica, "Pessoa Juridica não foi incluído");

                var filtro = new PesquisaPessoaJuridica { Email = "henrikesilverio@gmail.com" };
                var pessoas = repositorio.ObterListaPorFiltro(filtro);
                Assert.IsTrue(pessoas.Any(), "Pessoa Juridica não é igual");
            }
        }

        [TestMethod]
        public void ObterListaDePessoaJuridicaPorTipoPapel()
        {
            using (var repositorio = new PessoaJuridicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaJuridicaBase());

                var primeiroPessoaJuridica = _context.PessoasJuridicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaJuridica, "Pessoa Juridica não foi incluído");

                var filtro = new PesquisaPessoaJuridica { TipoPapelPessoa = TipoPapelPessoaEnum.Funcionario };
                var pessoas = repositorio.ObterListaPorFiltro(filtro);
                Assert.IsTrue(pessoas.Any(), "Pessoa Juridica não é igual");
            }
        }

        [TestMethod]
        public void AtualizarPessoa()
        {
            using (var repositorio = new PessoaJuridicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaJuridicaBase());

                var primeiroPessoaJuridicaIncluido = _context.PessoasJuridicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaJuridicaIncluido, "Pessoa Juridica não foi incluído");

                primeiroPessoaJuridicaIncluido.Contato = "Fernando";
                repositorio.Atualizar(primeiroPessoaJuridicaIncluido);

                var primeiroPessoaJuridicaAtualizado = _context.PessoasJuridicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaJuridicaAtualizado, "Pessoa Juridica não foi Atualizado");
                Assert.AreEqual(primeiroPessoaJuridicaAtualizado.Contato, "Fernando", "Pessoa não foi Atualizado");
            }
        }

        [TestMethod]
        public void DeletarPessoa()
        {
            using (var repositorio = new PessoaJuridicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaJuridicaBase());

                var primeiraPessoaJuridica = _context.PessoasJuridicas.FirstOrDefault();

                Assert.IsNotNull(primeiraPessoaJuridica, "Pessoa Juridica não foi incluído");
                repositorio.Deletar(primeiraPessoaJuridica);

                var retorno = _context.PessoasJuridicas.FirstOrDefault();
                Assert.IsNull(retorno, "Pessoa Juridica não foi removido");
            }
        }
    }
}
