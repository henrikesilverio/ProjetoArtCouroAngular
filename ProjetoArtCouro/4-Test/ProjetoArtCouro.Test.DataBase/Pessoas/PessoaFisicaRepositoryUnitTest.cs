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
    public class PessoaFisicaRepositoryUnitTest
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

        private PessoaFisica ObterPessoaFisicaBase()
        {
            return new PessoaFisica
            {
                CPF = "12345678909",
                RG = "303004005",
                Sexo = "Masculino",
                EstadoCivil = new EstadoCivil
                {
                    EstadoCivilNome = "Solteiro"
                },
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
        public void CriarPessoaFisica()
        {
            using (var repositorio = new PessoaFisicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaFisicaBase());

                var primeiraPessoaFisica = _context.PessoasFisicas.FirstOrDefault();
                Assert.IsNotNull(primeiraPessoaFisica, "Pessoa não foi incluído");

                Assert.IsNotNull(primeiraPessoaFisica.CPF, "Pessoa fisica sem CPF");
                Assert.IsNotNull(primeiraPessoaFisica.RG, "Pessoa fisica sem RG");
                Assert.IsNotNull(primeiraPessoaFisica.Sexo, "Pessoa fisica sem sexo");
                Assert.IsNotNull(primeiraPessoaFisica.EstadoCivil, "Pessoa fisica sem estado civil");
                Assert.IsNotNull(primeiraPessoaFisica.Pessoa, "Pessoa fisica sem pessoa base");
            }
        }

        [TestMethod]
        public void ObterPessoaFisicaPorId()
        {
            using (var repositorio = new PessoaFisicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaFisicaBase());

                var primeiroPessoaFisica = _context.PessoasFisicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaFisica, "Pessoa não foi incluído");

                var pessoa = repositorio.ObterPorId(primeiroPessoaFisica.PessoaId);
                Assert.AreEqual(primeiroPessoaFisica, pessoa, "Pessoa não é igual");
            }
        }

        [TestMethod]
        public void ObterPessoaFisicaPorCPF()
        {
            using (var repositorio = new PessoaFisicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaFisicaBase());

                var primeiroPessoaFisica = _context.PessoasFisicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaFisica, "Pessoa fisica não foi incluído");

                var pessoa = repositorio.ObterPorCPF(primeiroPessoaFisica.CPF);
                Assert.AreEqual(primeiroPessoaFisica, pessoa, "Pessoa fisica não é igual");
            }
        }

        [TestMethod]
        public void ObterListaDePessoaFisica()
        {
            using (var repositorio = new PessoaFisicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaFisicaBase());

                var primeiroPessoaFisica = _context.PessoasFisicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaFisica, "Pessoa fisica não foi incluído");

                var pessoas = repositorio.ObterLista();
                Assert.IsTrue(pessoas.Any(), "Pessoa fisica não é igual");
            }
        }

        [TestMethod]
        public void ObterListaDePessoaFisicaPorCodigo()
        {
            using (var repositorio = new PessoaFisicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaFisicaBase());

                var primeiroPessoaFisica = _context.PessoasFisicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaFisica, "Pessoa fisica não foi incluído");

                var filtro = new PesquisaPessoaFisica { Codigo = primeiroPessoaFisica.PessoaFisicaCodigo };
                var pessoas = repositorio.ObterListaPorFiltro(filtro);
                Assert.IsTrue(pessoas.Any(), "Pessoa fisica não é igual");
            }
        }

        [TestMethod]
        public void ObterListaDePessoaFisicaPorNome()
        {
            using (var repositorio = new PessoaFisicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaFisicaBase());

                var primeiroPessoaFisica = _context.PessoasFisicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaFisica, "Pessoa fisica não foi incluído");

                var filtro = new PesquisaPessoaFisica { Nome = primeiroPessoaFisica.Pessoa.Nome };
                var pessoas = repositorio.ObterListaPorFiltro(filtro);
                Assert.IsTrue(pessoas.Any(), "Pessoa fisica não é igual");
            }
        }

        [TestMethod]
        public void ObterListaDePessoaFisicaPorCPF()
        {
            using (var repositorio = new PessoaFisicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaFisicaBase());

                var primeiroPessoaFisica = _context.PessoasFisicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaFisica, "Pessoa fisica não foi incluído");

                var filtro = new PesquisaPessoaFisica { CPF = primeiroPessoaFisica.CPF };
                var pessoas = repositorio.ObterListaPorFiltro(filtro);
                Assert.IsTrue(pessoas.Any(), "Pessoa fisica não é igual");
            }
        }

        [TestMethod]
        public void ObterListaDePessoaFisicaPorEmail()
        {
            using (var repositorio = new PessoaFisicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaFisicaBase());

                var primeiroPessoaFisica = _context.PessoasFisicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaFisica, "Pessoa fisica não foi incluído");

                var filtro = new PesquisaPessoaFisica { Email = "henrikesilverio@gmail.com" };
                var pessoas = repositorio.ObterListaPorFiltro(filtro);
                Assert.IsTrue(pessoas.Any(), "Pessoa fisica não é igual");
            }
        }

        [TestMethod]
        public void ObterListaDePessoaFisicaPorTipoPapel()
        {
            using (var repositorio = new PessoaFisicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaFisicaBase());

                var primeiroPessoaFisica = _context.PessoasFisicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaFisica, "Pessoa fisica não foi incluído");

                var filtro = new PesquisaPessoaFisica { TipoPapelPessoa = TipoPapelPessoaEnum.Funcionario };
                var pessoas = repositorio.ObterListaPorFiltro(filtro);
                Assert.IsTrue(pessoas.Any(), "Pessoa fisica não é igual");
            }
        }

        [TestMethod]
        public void AtualizarPessoa()
        {
            using (var repositorio = new PessoaFisicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaFisicaBase());

                var primeiroPessoaFisicaIncluido = _context.PessoasFisicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaFisicaIncluido, "Pessoa fisica não foi incluído");

                primeiroPessoaFisicaIncluido.RG = "9999999";
                primeiroPessoaFisicaIncluido.Sexo = "Feminino";
                repositorio.Atualizar(primeiroPessoaFisicaIncluido);

                var primeiroPessoaFisicaAtualizado = _context.PessoasFisicas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaFisicaAtualizado, "Pessoa fisica não foi Atualizado");
                Assert.AreEqual(primeiroPessoaFisicaAtualizado.RG, "9999999", "Pessoa não foi Atualizado");
                Assert.AreEqual(primeiroPessoaFisicaAtualizado.Sexo, "Feminino", "Pessoa não foi Atualizado");
            }
        }

        [TestMethod]
        public void DeletarPessoa()
        {
            using (var repositorio = new PessoaFisicaRepository(_context))
            {
                repositorio.Criar(ObterPessoaFisicaBase());

                var primeiraPessoaFisica = _context.PessoasFisicas.FirstOrDefault();

                Assert.IsNotNull(primeiraPessoaFisica, "Pessoa fisica não foi incluído");
                repositorio.Deletar(primeiraPessoaFisica);

                var retorno = _context.PessoasFisicas.FirstOrDefault();
                Assert.IsNull(retorno, "Pessoa fisica não foi removido");
            }
        }
    }
}
