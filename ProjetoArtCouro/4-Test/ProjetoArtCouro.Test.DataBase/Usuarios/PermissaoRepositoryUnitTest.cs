using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Repositorios.UsuarioRepository;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Test.DataBase.Infra;
using System.Linq;

namespace ProjetoArtCouro.Test.DataBase.Usuarios
{
    [TestClass]
    public class PermissaoRepositoryUnitTest
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
        public void ObterPermissaoPorCodigo()
        {
            using (var repositorio = new PermissaoRepository(_context))
            {
                _context.Permissoes.Add(new Permissao
                {
                    AcaoNome = "Atualizar",
                    PermissaoNome = "Atualizar"
                });
                _context.SaveChanges();

                var primeiraPermissao = _context.Permissoes.FirstOrDefault();
                Assert.IsNotNull(primeiraPermissao, "Permissão não foi incluído");

                var permissao = repositorio.ObterPermissaoPorCodigo(primeiraPermissao.PermissaoCodigo);
                Assert.AreEqual(primeiraPermissao, permissao, "Permissão não é igual");
            }
        }

        [TestMethod]
        public void ObterListaPermissao()
        {
            using (var repositorio = new PermissaoRepository(_context))
            {
                _context.Permissoes.Add(new Permissao
                {
                    AcaoNome = "Atualizar",
                    PermissaoNome = "Atualizar"
                });
                _context.SaveChanges();

                var permissoes = repositorio.ObterLista();
                Assert.IsTrue(permissoes.Any(), "Permissão não incluido");
            }
        }
    }
}
