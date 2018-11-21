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
    public class MeioComunicacaoRepositoryUnitTest
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
        public void CriarMeioComunicacao()
        {
            using (var repositorio = new MeioComunicacaoRepository(_context))
            {
                repositorio.Criar(new MeioComunicacao
                {
                    MeioComunicacaoNome = "henrikesilverio@gmail.com",
                    TipoComunicacao = TipoComunicacaoEnum.Email,
                    Principal = true,
                    Pessoa = ObterPessoaBase()
                });

                var meiosComunicacao = _context.MeiosComunicacao.ToList();
                Assert.IsTrue(meiosComunicacao.Any(), "Meio de comunicação não foi incluído");
                Assert.IsTrue(meiosComunicacao
                    .Any(x => x.MeioComunicacaoNome == "henrikesilverio@gmail.com"), "Meio de comunicação não foi incluído");
            }
        }

        [TestMethod]
        public void ObterMeioComunicacaoPorId()
        {
            using (var repositorio = new MeioComunicacaoRepository(_context))
            {
                repositorio.Criar(new MeioComunicacao
                {
                    MeioComunicacaoNome = "henrikesilverio@gmail.com",
                    TipoComunicacao = TipoComunicacaoEnum.Email,
                    Principal = true,
                    Pessoa = ObterPessoaBase()
                });

                var primeiroMeioComunicacao = _context.MeiosComunicacao.FirstOrDefault();
                Assert.IsNotNull(primeiroMeioComunicacao, "Meio de comunicação não foi incluído");

                var meioComunicacao = repositorio.ObterPorId(primeiroMeioComunicacao.MeioComunicacaoId);
                Assert.AreEqual(primeiroMeioComunicacao, meioComunicacao, "Meio de comunicação não é igual");
            }
        }

        [TestMethod]
        public void ObterMeioComunicacaoPorCodigo()
        {
            using (var repositorio = new MeioComunicacaoRepository(_context))
            {
                repositorio.Criar(new MeioComunicacao
                {
                    MeioComunicacaoNome = "henrikesilverio@gmail.com",
                    TipoComunicacao = TipoComunicacaoEnum.Email,
                    Principal = true,
                    Pessoa = ObterPessoaBase()
                });

                var primeiroMeioComunicacao = _context.MeiosComunicacao.FirstOrDefault();
                Assert.IsNotNull(primeiroMeioComunicacao, "Meio de comunicação não foi incluído");

                var meioComunicacao = repositorio.ObterPorCodigo(primeiroMeioComunicacao.MeioComunicacaoCodigo);
                Assert.AreEqual(primeiroMeioComunicacao, meioComunicacao, "Meio de comunicação não é igual");
            }
        }

        [TestMethod]
        public void ObterListaDeMeioComunicacao()
        {
            using (var repositorio = new MeioComunicacaoRepository(_context))
            {
                repositorio.Criar(new MeioComunicacao
                {
                    MeioComunicacaoNome = "henrikesilverio@gmail.com",
                    TipoComunicacao = TipoComunicacaoEnum.Email,
                    Principal = true,

                    Pessoa = ObterPessoaBase()
                });

                var meiosComunicacao = repositorio.ObterLista();
                Assert.IsTrue(meiosComunicacao.Any(), "Meio de comunicação não foi incluído");
            }
        }

        [TestMethod]
        public void AtualizarMeioComunicacao()
        {
            using (var repositorio = new MeioComunicacaoRepository(_context))
            {
                repositorio.Criar(new MeioComunicacao
                {
                    MeioComunicacaoNome = "henrikesilverio@gmail.com",
                    TipoComunicacao = TipoComunicacaoEnum.Email,
                    Principal = true,

                    Pessoa = ObterPessoaBase()
                });

                var antesAtualizado = _context.MeiosComunicacao.FirstOrDefault();
                Assert.IsNotNull(antesAtualizado, "Meio de comunicação não foi incluído");
                antesAtualizado.MeioComunicacaoNome = "hpsilverio@gmail.com";
                antesAtualizado.Principal = false;

                repositorio.Atualizar(antesAtualizado);
                var aposAtualizado = _context.MeiosComunicacao.FirstOrDefault();
                Assert.IsNotNull(aposAtualizado, "Meio de comunicação não foi Atualizado");
                Assert.AreEqual(aposAtualizado.MeioComunicacaoNome, "hpsilverio@gmail.com", "Meio de comunicação não foi Atualizado");
                Assert.AreEqual(aposAtualizado.Principal, false, "Meio de comunicação não foi Atualizado");
            }
        }

        [TestMethod]
        public void DeletarMeioComunicacao()
        {
            using (var repositorio = new MeioComunicacaoRepository(_context))
            {
                repositorio.Criar(new MeioComunicacao
                {
                    MeioComunicacaoNome = "henrikesilverio@gmail.com",
                    TipoComunicacao = TipoComunicacaoEnum.Email,
                    Principal = true,

                    Pessoa = ObterPessoaBase()
                });

                var primeiro = _context.MeiosComunicacao.FirstOrDefault();
                Assert.IsNotNull(primeiro, "Meio de comunicação não foi incluído");
                repositorio.Deletar(primeiro);
                var retorno = _context.MeiosComunicacao.FirstOrDefault();
                Assert.IsNull(retorno, "Meio de comunicação não foi removido");
            }
        }
    }
}
