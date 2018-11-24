using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Repositorios.ProdutoRepository;
using ProjetoArtCouro.Domain.Entities.Produtos;
using ProjetoArtCouro.Test.DataBase.Infra;
using System.Linq;

namespace ProjetoArtCouro.Test.DataBase.Produtos
{
    [TestClass]
    public class UnidadeRepositoryUnitTest
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
        public void CriarUnidade()
        {
            using (var repositorio = new UnidadeRepository(_context))
            {
                repositorio.Criar(new Unidade
                {
                    UnidadeNome = "UN"
                });

                var unidades = _context.Unidades.ToList();
                Assert.IsTrue(unidades.Any(), "Unidade não foi incluída");
                Assert.IsTrue(unidades.Any(x => x.UnidadeNome == "UN"), "Unidade não foi incluído");
            }
        }

        [TestMethod]
        public void ObterUnidadePorId()
        {
            using (var repositorio = new UnidadeRepository(_context))
            {
                repositorio.Criar(new Unidade
                {
                    UnidadeNome = "UN"
                });

                var primeiraUnidade = _context.Unidades.FirstOrDefault();
                Assert.IsNotNull(primeiraUnidade, "Unidade não foi incluído");

                var unidade = repositorio.ObterPorId(primeiraUnidade.UnidadeId);
                Assert.AreEqual(primeiraUnidade, unidade, "Unidade não é igual");
            }
        }

        [TestMethod]
        public void ObterUnidadePorCodigo()
        {
            using (var repositorio = new UnidadeRepository(_context))
            {
                repositorio.Criar(new Unidade
                {
                    UnidadeNome = "UN"
                });

                var primeiraUnidade = _context.Unidades.FirstOrDefault();
                Assert.IsNotNull(primeiraUnidade, "Unidade não foi incluído");

                var unidade = repositorio.ObterPorCodigo(primeiraUnidade.UnidadeCodigo);
                Assert.AreEqual(primeiraUnidade, unidade, "Unidade não é igual");
            }
        }

        [TestMethod]
        public void ObterListaUnidade()
        {
            using (var repositorio = new UnidadeRepository(_context))
            {
                repositorio.Criar(new Unidade
                {
                    UnidadeNome = "UN"
                });

                var unidades = repositorio.ObterLista();
                Assert.IsTrue(unidades.Any(), "Unidades não incluido");
            }
        }

        [TestMethod]
        public void AtualizarUnidade()
        {
            using (var repositorio = new UnidadeRepository(_context))
            {
                repositorio.Criar(new Unidade
                {
                    UnidadeNome = "UN"
                });

                var antesAtualizado = _context.Unidades.FirstOrDefault();
                Assert.IsNotNull(antesAtualizado, "Unidade não foi incluída");
                antesAtualizado.UnidadeNome = "KG";

                repositorio.Atualizar(antesAtualizado);
                var aposAtualizado = _context.Unidades.FirstOrDefault();
                Assert.IsNotNull(aposAtualizado, "Unidade não foi Atualizada");
                Assert.AreEqual(aposAtualizado.UnidadeNome, "KG", "Unidade não foi Atualizada");
            }
        }

        [TestMethod]
        public void DeletarUnidade()
        {
            using (var repositorio = new UnidadeRepository(_context))
            {
                repositorio.Criar(new Unidade
                {
                    UnidadeNome = "UN"
                });

                var primeiro = _context.Unidades.FirstOrDefault();
                Assert.IsNotNull(primeiro, "Unidade não foi incluído");
                repositorio.Deletar(primeiro);
                var retorno = _context.Unidades.FirstOrDefault();
                Assert.IsNull(retorno, "Unidade não foi removido");
            }
        }
    }
}
