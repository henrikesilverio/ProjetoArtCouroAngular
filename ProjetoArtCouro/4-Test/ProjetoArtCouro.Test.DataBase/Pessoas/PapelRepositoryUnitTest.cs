using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Repositorios.PessoaRepository;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Test.DataBase.Infra;
using System.Linq;

namespace ProjetoArtCouro.Test.DataBase.Pessoas
{
    [TestClass]
    public class PapelRepositoryUnitTest
    {
        private DataBaseContext _context;

        [TestInitialize]
        public void Inicializacao()
        {
            var dbConnection = DbConnectionFactory.CreateTransient();
            var modelBuilder = EntityFrameworkHelper.GetDbModelBuilder();
            _context = new DataBaseContext(dbConnection, modelBuilder);
        }

        [TestMethod]
        public void CriarPapelPessoa()
        {
            using (var repositorio = new PapelRepository(_context))
            {
                repositorio.Criar(new Papel
                {
                    PapelNome = "Funcionario"
                });

                var papeis = _context.Papeis.ToList();
                Assert.IsTrue(papeis.Any(), "Papel não foi incluído");
                Assert.IsTrue(papeis.Any(x => x.PapelNome == "Funcionario"), "Papel não foi incluído");
            }
        }

        [TestMethod]
        public void ObterPapelPorId()
        {
            using (var repositorio = new PapelRepository(_context))
            {
                repositorio.Criar(new Papel
                {
                    PapelNome = "Funcionario"
                });

                var primeiroPapel = _context.Papeis.FirstOrDefault();
                Assert.IsNotNull(primeiroPapel, "Papel não foi incluído");

                var papel = repositorio.ObterPorId(primeiroPapel.PapelId);
                Assert.AreEqual(primeiroPapel, papel, "Papel não é igual");
            }
        }

        [TestMethod]
        public void ObterPapelPorCodigo()
        {
            using (var repositorio = new PapelRepository(_context))
            {
                repositorio.Criar(new Papel
                {
                    PapelNome = "Funcionario"
                });

                var primeiroPapel = _context.Papeis.FirstOrDefault();
                Assert.IsNotNull(primeiroPapel, "Papel não foi incluído");

                var papel = repositorio.ObterPorCodigo(primeiroPapel.PapelCodigo);
                Assert.AreEqual(primeiroPapel, papel, "Papel não é igual");
            }
        }

        [TestMethod]
        public void ObterListaDePapel()
        {
            using (var repositorio = new PapelRepository(_context))
            {
                repositorio.Criar(new Papel
                {
                    PapelNome = "Funcionario"
                });

                var Papels = _context.Papeis.ToList();
                Assert.IsTrue(Papels.Any(), "Papel não foi incluído");
            }
        }

        [TestMethod]
        public void AtualizarPapel()
        {
            using (var repositorio = new PapelRepository(_context))
            {
                repositorio.Criar(new Papel
                {
                    PapelNome = "Funcionario"
                });

                var primeiroPapelIncluido = _context.Papeis.FirstOrDefault();
                Assert.IsNotNull(primeiroPapelIncluido, "Papel não foi incluído");

                primeiroPapelIncluido.PapelNome = "Cliente";
                repositorio.Atualizar(primeiroPapelIncluido);


                var primeiroPapelAtualizado = _context.Papeis.FirstOrDefault();
                Assert.IsNotNull(primeiroPapelAtualizado, "Papel não foi Atualizado");
                Assert.AreEqual(primeiroPapelAtualizado.PapelNome, "Cliente", "Papel não foi Atualizado");
            }
        }

        [TestMethod]
        public void DeletarPapel()
        {
            using (var repositorio = new PapelRepository(_context))
            {
                repositorio.Criar(new Papel
                {
                    PapelNome = "Funcionario"
                });

                var primeiroPapel = _context.Papeis.FirstOrDefault();

                Assert.IsNotNull(primeiroPapel, "Papel não foi incluído");
                repositorio.Deletar(primeiroPapel);

                var retorno = _context.Papeis.FirstOrDefault();
                Assert.IsNull(retorno, "Papel não foi removido");
            }
        }
    }
}
