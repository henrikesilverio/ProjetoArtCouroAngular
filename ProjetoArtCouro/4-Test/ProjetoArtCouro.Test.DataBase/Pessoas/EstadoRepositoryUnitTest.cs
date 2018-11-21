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
    public class EstadoRepositoryUnitTest
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
        public void CriarEstado()
        {
            using (var repositorio = new EstadoRepository(_context))
            {
                repositorio.Criar(new Estado
                {
                    EstadoNome = "Paraná"
                });

                var estados = _context.Estados.ToList();
                Assert.IsTrue(estados.Any(), "Estado não foi incluído");
                Assert.IsTrue(estados.Any(x => x.EstadoNome == "Paraná"), "Estado não foi incluído");
            }
        }

        [TestMethod]
        public void ObterEstadoPorId()
        {
            using (var repositorio = new EstadoRepository(_context))
            {
                repositorio.Criar(new Estado
                {
                    EstadoNome = "Paraná"
                });

                var primeiroEstado = _context.Estados.FirstOrDefault();
                Assert.IsNotNull(primeiroEstado, "Estado não foi incluído");

                var estado = repositorio.ObterPorId(primeiroEstado.EstadoId);
                Assert.AreEqual(primeiroEstado, estado, "Estado não é igual");
            }
        }

        [TestMethod]
        public void ObterEstadoPorCodigo()
        {
            using (var repositorio = new EstadoRepository(_context))
            {
                repositorio.Criar(new Estado
                {
                    EstadoNome = "Paraná"
                });

                var primeiroEstado = _context.Estados.FirstOrDefault();
                Assert.IsNotNull(primeiroEstado, "Estado não foi incluído");

                var estado = repositorio.ObterPorCodigo(primeiroEstado.EstadoCodigo);
                Assert.AreEqual(primeiroEstado, estado, "Estado não é igual");
            }
        }

        [TestMethod]
        public void ObterListaDeEstado()
        {
            using (var repositorio = new EstadoRepository(_context))
            {
                repositorio.Criar(new Estado
                {
                    EstadoNome = "Paraná"
                });

                var estados = _context.Estados.ToList();
                Assert.IsTrue(estados.Any(), "Estado não foi incluído");
            }
        }

        [TestMethod]
        public void AtualizarEstado()
        {
            using (var repositorio = new EstadoRepository(_context))
            {
                repositorio.Criar(new Estado
                {
                    EstadoNome = "Paraná"
                });

                var primeiroEstadoIncluido = _context.Estados.FirstOrDefault();
                Assert.IsNotNull(primeiroEstadoIncluido, "Estado não foi incluído");

                primeiroEstadoIncluido.EstadoNome = "São Paulo";
                repositorio.Atualizar(primeiroEstadoIncluido);


                var primeiroEstadoAtualizado = _context.Estados.FirstOrDefault();
                Assert.IsNotNull(primeiroEstadoAtualizado, "Estado não foi Atualizado");
                Assert.AreEqual(primeiroEstadoAtualizado.EstadoNome, "São Paulo", "Estado não foi Atualizado");
            }
        }

        [TestMethod]
        public void DeletarEstado()
        {
            using (var repositorio = new EstadoRepository(_context))
            {
                repositorio.Criar(new Estado
                {
                    EstadoNome = "Paraná"
                });

                var primeiroEstado = _context.Estados.FirstOrDefault();

                Assert.IsNotNull(primeiroEstado, "Estado não foi incluído");
                repositorio.Deletar(primeiroEstado);

                var retorno = _context.Estados.FirstOrDefault();
                Assert.IsNull(retorno, "Estado não foi removido");
            }
        }
    }
}
