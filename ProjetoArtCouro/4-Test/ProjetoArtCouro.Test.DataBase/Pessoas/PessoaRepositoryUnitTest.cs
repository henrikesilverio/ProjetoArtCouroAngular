using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Repositorios.PessoaRepository;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Test.DataBase.Infra;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoArtCouro.Test.DataBase.Pessoas
{
    [TestClass]
    public class PessoaRepositoryUnitTest
    {
        private DataBaseContext _context;

        private Pessoa ObterPessoaBase()
        {
            return new Pessoa
            {
                Nome = "Henrique",
                Papeis = new List<Papel>
                {
                    new Papel { PapelNome = "Funcionario" }
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

        [TestInitialize]
        public void Inicializacao()
        {
            var dbConnection = DbConnectionFactory.CreateTransient();
            var modelBuilder = EntityFrameworkHelper.GetDbModelBuilder();
            _context = new DataBaseContext(dbConnection, modelBuilder);
        }

        [TestMethod]
        public void CriarPessoa()
        {
            using (var repositorio = new PessoaRepository(_context))
            {
                repositorio.Criar(ObterPessoaBase());

                var primeiraPessoa = _context.Pessoas.FirstOrDefault();
                Assert.IsNotNull(primeiraPessoa, "Pessoa não foi incluído");
                Assert.AreEqual("Henrique", primeiraPessoa.Nome, "Pessoa sem nome");
                Assert.IsTrue(primeiraPessoa.Papeis.Any(), "Pessoa sem papel");
                Assert.IsTrue(primeiraPessoa.MeiosComunicacao.Any(), "Pessoa sem meio de comunicação");
                Assert.IsTrue(primeiraPessoa.Enderecos.Any(), "Pessoa sem endereço");
            }
        }

        [TestMethod]
        public void ObterPessoaPorId()
        {
            using (var repositorio = new PessoaRepository(_context))
            {
                repositorio.Criar(ObterPessoaBase());

                var primeiroPessoa = _context.Pessoas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoa, "Pessoa não foi incluído");

                var pessoa = repositorio.ObterPorId(primeiroPessoa.PessoaId);
                Assert.AreEqual(primeiroPessoa, pessoa, "Pessoa não é igual");
            }
        }

        [TestMethod]
        public void ObterPessoaPorCodigo()
        {
            using (var repositorio = new PessoaRepository(_context))
            {
                repositorio.Criar(ObterPessoaBase());

                var primeiroPessoa = _context.Pessoas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoa, "Pessoa não foi incluído");

                var pessoa = repositorio.ObterPorCodigo(primeiroPessoa.PessoaCodigo);
                Assert.AreEqual(primeiroPessoa, pessoa, "Pessoa não é igual");
            }
        }

        [TestMethod]
        public void ObterPessoaPorCodigoComPessoaCompleta()
        {
            using (var repositorio = new PessoaRepository(_context))
            {
                var pessoaBase = ObterPessoaBase();
                pessoaBase.PessoaFisica = new PessoaFisica
                {
                    CPF = "12345678909",
                    RG = "123456795",
                    Sexo = "Masculino",
                    EstadoCivil = new EstadoCivil { EstadoCivilNome = "Solteiro" }
                };
                pessoaBase.PessoaJuridica = new PessoaJuridica
                {
                    CNPJ = "123456789090001"
                };
                repositorio.Criar(pessoaBase);

                var primeiroPessoa = _context.Pessoas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoa, "Pessoa não foi incluído");

                var pessoa = repositorio.ObterPorCodigoComPessoaCompleta(primeiroPessoa.PessoaCodigo);
                Assert.IsNotNull(pessoa, "Pessoa não foi incluído");
                Assert.AreEqual(primeiroPessoa, pessoa, "Pessoa não é igual");
                Assert.IsNotNull(pessoa.PessoaFisica, "Pessoa sem pessoa fisica");
                Assert.IsNotNull(pessoa.PessoaFisica.EstadoCivil, "Pessoa fisica sem estado civil");
                Assert.IsNotNull(pessoa.PessoaJuridica, "Pessoa sem pessoa juridica");
                Assert.IsTrue(pessoa.Papeis.Any(), "Pessoa sem papel");
                Assert.IsTrue(pessoa.MeiosComunicacao.Any(), "Pessoa sem papel");
                Assert.IsTrue(pessoa.Enderecos.Any(), "Pessoa sem papel");
                Assert.IsTrue(pessoa.Enderecos.All(x => x.Estado != null), "Endereço sem estado");
            }
        }

        [TestMethod]
        public void ObterPessoaPorCPFComPessoaCompleta()
        {
            using (var repositorio = new PessoaRepository(_context))
            {
                var pessoaBase = ObterPessoaBase();
                pessoaBase.PessoaFisica = new PessoaFisica
                {
                    CPF = "12345678909",
                    RG = "123456795",
                    Sexo = "Masculino",
                    EstadoCivil = new EstadoCivil { EstadoCivilNome = "Solteiro" }
                };
                repositorio.Criar(pessoaBase);

                var primeiroPessoa = _context.Pessoas
                    .Include("PessoaFisica")
                    .FirstOrDefault();

                Assert.IsNotNull(primeiroPessoa, "Pessoa não foi incluído");

                var pessoa = repositorio.ObterPorCPFComPessoaCompleta(primeiroPessoa.PessoaFisica.CPF);
                Assert.IsNotNull(pessoa, "Pessoa não foi incluído");
                Assert.AreEqual(primeiroPessoa, pessoa, "Pessoa não é igual");
                Assert.IsNotNull(pessoa.PessoaFisica, "Pessoa sem pessoa fisica");
                Assert.IsNotNull(pessoa.PessoaFisica.EstadoCivil, "Pessoa fisica sem estado civil");
                Assert.IsTrue(pessoa.Papeis.Any(), "Pessoa sem papel");
                Assert.IsTrue(pessoa.MeiosComunicacao.Any(), "Pessoa sem papel");
                Assert.IsTrue(pessoa.Enderecos.Any(), "Pessoa sem papel");
                Assert.IsTrue(pessoa.Enderecos.All(x => x.Estado != null), "Endereço sem estado");
            }
        }

        [TestMethod]
        public void ObterPessoaPorCNPJComPessoaCompleta()
        {
            using (var repositorio = new PessoaRepository(_context))
            {
                var pessoaBase = ObterPessoaBase();
                pessoaBase.PessoaJuridica = new PessoaJuridica
                {
                    CNPJ = "123456789090001"
                };
                repositorio.Criar(pessoaBase);

                var primeiroPessoa = _context.Pessoas
                    .Include("PessoaJuridica")

                    .FirstOrDefault();

                Assert.IsNotNull(primeiroPessoa, "Pessoa não foi incluído");

                var pessoa = repositorio.ObterPorCNPJComPessoaCompleta(primeiroPessoa.PessoaJuridica.CNPJ);
                Assert.IsNotNull(pessoa, "Pessoa não foi incluído");
                Assert.AreEqual(primeiroPessoa, pessoa, "Pessoa não é igual");
                Assert.IsNotNull(pessoa.PessoaJuridica, "Pessoa sem pessoa juridica");
                Assert.IsTrue(pessoa.Papeis.Any(), "Pessoa sem papel");
                Assert.IsTrue(pessoa.MeiosComunicacao.Any(), "Pessoa sem papel");
                Assert.IsTrue(pessoa.Enderecos.Any(), "Pessoa sem papel");
                Assert.IsTrue(pessoa.Enderecos.All(x => x.Estado != null), "Endereço sem estado");
            }
        }

        [TestMethod]
        public void ObterListaDePessoaComPessoaFisicaEJuridica()
        {
            using (var repositorio = new PessoaRepository(_context))
            {
                var pessoaBase = ObterPessoaBase();
                pessoaBase.PessoaFisica = new PessoaFisica
                {
                    CPF = "12345678909",
                    RG = "123456795",
                    Sexo = "Masculino",
                    EstadoCivil = new EstadoCivil { EstadoCivilNome = "Solteiro" }
                };
                pessoaBase.PessoaJuridica = new PessoaJuridica
                {
                    CNPJ = "123456789090001"
                };
                repositorio.Criar(pessoaBase);

                var pessoas = repositorio.ObterListaComPessoaFisicaEJuridica();
                Assert.IsNotNull(pessoas, "Pessoas não foram incluídas");

                Assert.IsNotNull(pessoas.All(x => x.PessoaFisica != null), "Pessoa sem pessoa fisica");
                Assert.IsNotNull(pessoas.All(x => x.PessoaJuridica != null), "Pessoa sem pessoa juridica");
            }
        }

        [TestMethod]
        public void ObterListaDePessoaComPessoaFisicaEJuridicaPorPapel()
        {
            using (var repositorio = new PessoaRepository(_context))
            {
                var pessoaBase = ObterPessoaBase();
                pessoaBase.PessoaFisica = new PessoaFisica
                {
                    CPF = "12345678909",
                    RG = "123456795",
                    Sexo = "Masculino",
                    EstadoCivil = new EstadoCivil { EstadoCivilNome = "Solteiro" }
                };
                pessoaBase.PessoaJuridica = new PessoaJuridica
                {
                    CNPJ = "123456789090001"
                };
                repositorio.Criar(pessoaBase);

                var pessoas = repositorio
                    .ObterListaComPessoaFisicaEJuridicaPorPapel(TipoPapelPessoaEnum.Funcionario);
                Assert.IsNotNull(pessoas, "Pessoas não foram incluídas");

                Assert.IsNotNull(pessoas.All(x => x.PessoaFisica != null), "Pessoa sem pessoa fisica");
                Assert.IsNotNull(pessoas.All(x => x.PessoaJuridica != null), "Pessoa sem pessoa juridica");
            }
        }

        [TestMethod]
        public void AtualizarPessoa()
        {
            using (var repositorio = new PessoaRepository(_context))
            {
                var pessoaBase = ObterPessoaBase();
                repositorio.Criar(pessoaBase);

                var primeiroPessoaIncluido = _context.Pessoas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaIncluido, "Estado não foi incluído");

                primeiroPessoaIncluido.Nome = "Erica";
                repositorio.Atualizar(primeiroPessoaIncluido);

                var primeiroPessoaAtualizado = _context.Pessoas.FirstOrDefault();
                Assert.IsNotNull(primeiroPessoaAtualizado, "Pessoa não foi Atualizado");
                Assert.AreEqual(primeiroPessoaAtualizado.Nome, "Erica", "Pessoa não foi Atualizado");
            }
        }

        [TestMethod]
        public void DeletarPessoa()
        {
            using (var repositorio = new PessoaRepository(_context))
            {
                var pessoaBase = ObterPessoaBase();
                repositorio.Criar(pessoaBase);

                var primeiraPessoa = _context.Pessoas.FirstOrDefault();

                Assert.IsNotNull(primeiraPessoa, "Pessoa não foi incluído");
                repositorio.Deletar(primeiraPessoa);

                var retorno = _context.Pessoas.FirstOrDefault();
                Assert.IsNull(retorno, "Pessoa não foi removido");
            }
        }
    }
}
