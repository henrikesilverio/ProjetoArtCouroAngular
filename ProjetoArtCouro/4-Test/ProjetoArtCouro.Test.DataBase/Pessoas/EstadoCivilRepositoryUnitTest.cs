using System.Linq;
using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Repositorios.PessoaRepository;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Test.DataBase.Infra;

namespace ProjetoArtCouro.Test.DataBase.Pessoas
{
    [TestClass]
    public class EstadoCivilRepositoryUnitTest
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
        public void CriarEstadoCivil()
        {
            using (var repositorio = new EstadoCivilRepository(_context))
            {
                repositorio.Criar(new EstadoCivil
                {
                    EstadoCivilNome = "Solteiro"
                });
                var estadosCivis = _context.EstadosCivis.ToList();
                Assert.IsTrue(estadosCivis.Any(), "Estado civil não foi incluído");
                Assert.IsTrue(estadosCivis.Any(x => x.EstadoCivilNome == "Solteiro"), "Estado civil não foi incluído");
            }
        }

        [TestMethod]
        public void ObterEstadoCivilPorId()
        {
            using (var repositorio = new EstadoCivilRepository(_context))
            {
                repositorio.Criar(new EstadoCivil
                {
                    EstadoCivilNome = "Solteiro"
                });
                var primeiro = _context.EstadosCivis.FirstOrDefault();
                Assert.IsNotNull(primeiro, "Estado civil não foi incluído");
                var estadoCivil = repositorio.ObterPorId(primeiro.EstadoCivilId);
                Assert.AreEqual(primeiro, estadoCivil, "O estado civil incluído e diferente do retornado pelo metodo");
            }
        }

        [TestMethod]
        public void ObterEstadoCivilPorCodigo()
        {
            using (var repositorio = new EstadoCivilRepository(_context))
            {
                repositorio.Criar(new EstadoCivil
                {
                    EstadoCivilNome = "Solteiro"
                });
                var primeiro = _context.EstadosCivis.FirstOrDefault();
                Assert.IsNotNull(primeiro, "Estado civil não foi incluído");
                var estadoCivil = repositorio.ObterPorCodigo(primeiro.EstadoCivilCodigo);
                Assert.AreEqual(primeiro, estadoCivil, "O estado civil incluído e diferente do retornado pelo metodo");
            }
        }

        [TestMethod]
        public void ObterListaEstadoCivil()
        {
            using (var repositorio = new EstadoCivilRepository(_context))
            {
                repositorio.Criar(new EstadoCivil
                {
                    EstadoCivilNome = "Solteiro"
                });
                repositorio.Criar(new EstadoCivil
                {
                    EstadoCivilNome = "Casado"
                });
                repositorio.Criar(new EstadoCivil
                {
                    EstadoCivilNome = "Viúvo"
                });
                var lista = repositorio.ObterLista();
                Assert.IsNotNull(lista, "Estado civil não foi incluído");
                Assert.AreEqual(lista.Count, 3, "Nen todos os estados civis foram incluídos");
            }
        }

        [TestMethod]
        public void AtualizarEstadoCivil()
        {
            using (var repositorio = new EstadoCivilRepository(_context))
            {
                repositorio.Criar(new EstadoCivil
                {
                    EstadoCivilNome = "Solteiro"
                });
                var antesAtualizado = _context.EstadosCivis.FirstOrDefault();
                Assert.IsNotNull(antesAtualizado, "Estado civil não foi incluído");
                antesAtualizado.EstadoCivilNome = "Casado";
                repositorio.Atualizar(antesAtualizado);
                var aposAtualizado = _context.EstadosCivis.FirstOrDefault();
                Assert.IsNotNull(aposAtualizado, "Estado civil não foi Atualizado");
                Assert.AreEqual(aposAtualizado.EstadoCivilNome, "Casado", "Estado civil não foi Atualizado");
            }
        }

        [TestMethod]
        public void DeletarEstadoCivil()
        {
            using (var repositorio = new EstadoCivilRepository(_context))
            {
                repositorio.Criar(new EstadoCivil
                {
                    EstadoCivilNome = "Solteiro"
                });
                var primeiro = _context.EstadosCivis.FirstOrDefault();
                Assert.IsNotNull(primeiro, "Estado civil não foi incluído");
                repositorio.Deletar(primeiro);
                var retorno = _context.EstadosCivis.FirstOrDefault();
                Assert.IsNull(retorno, "Estado civil não foi removido");
            }
        }
    }
}
