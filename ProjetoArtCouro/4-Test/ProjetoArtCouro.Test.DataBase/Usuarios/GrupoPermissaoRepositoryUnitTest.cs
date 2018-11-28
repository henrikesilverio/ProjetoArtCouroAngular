using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Repositorios.UsuarioRepository;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Resources.Validation;
using ProjetoArtCouro.Test.DataBase.Infra;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoArtCouro.Test.DataBase.Usuarios
{
    [TestClass]
    public class GrupoPermissaoRepositoryUnitTest
    {
        private DataBaseContext _context;

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

        [TestInitialize]
        public void Inicializacao()
        {
            var dbConnection = DbConnectionFactory.CreateTransient();
            var modelBuilder = EntityFrameworkHelper.GetDbModelBuilder();
            _context = new DataBaseContext(dbConnection, modelBuilder);
        }

        [TestMethod]
        public void CriarGrupoPermissao()
        {
            using (var repositorio = new GrupoPermissaoRepository(_context))
            {
                repositorio.Criar(new GrupoPermissao
                {
                    GrupoPermissaoNome = "Novo",
                    Permissoes = ObterPermissoes()
                });

                var grupos = _context.GruposPermissao.ToList();
                Assert.IsTrue(grupos.Any(), "Grupo não foi incluído");
                Assert.IsTrue(grupos.Any(x => x.GrupoPermissaoNome == "Novo"), "Grupo não foi incluído");
                Assert.IsTrue(grupos.All(x => x.Permissoes != null), "Grupo não foi incluído");
            }
        }

        [TestMethod]
        public void ObterGrupoPermissaoPorId()
        {
            using (var repositorio = new GrupoPermissaoRepository(_context))
            {
                repositorio.Criar(new GrupoPermissao
                {
                    GrupoPermissaoNome = "Novo",
                    Permissoes = ObterPermissoes()
                });

                var primeiroGrupo = _context.GruposPermissao.FirstOrDefault();
                Assert.IsNotNull(primeiroGrupo, "Grupo não foi incluído");

                var grupo = repositorio.ObterPorId(primeiroGrupo.GrupoPermissaoId);
                Assert.AreEqual(primeiroGrupo, grupo, "Grupo não é igual");
            }
        }

        [TestMethod]
        public void ObterGrupoPermissaoPorCodigo()
        {
            using (var repositorio = new GrupoPermissaoRepository(_context))
            {
                repositorio.Criar(new GrupoPermissao
                {
                    GrupoPermissaoNome = "Novo",
                    Permissoes = ObterPermissoes()
                });

                var primeiroGrupo = _context.GruposPermissao.FirstOrDefault();
                Assert.IsNotNull(primeiroGrupo, "Grupo não foi incluído");

                var grupo = repositorio.ObterPorCodigo(primeiroGrupo.GrupoPermissaoCodigo);
                Assert.AreEqual(primeiroGrupo, grupo, "Grupo não é igual");
            }
        }

        [TestMethod]
        public void ObterGrupoPermissaoPorCodigoComPermissoes()
        {
            using (var repositorio = new GrupoPermissaoRepository(_context))
            {
                repositorio.Criar(new GrupoPermissao
                {
                    GrupoPermissaoNome = "Novo",
                    Permissoes = ObterPermissoes()
                });

                var primeiroGrupo = _context.GruposPermissao.FirstOrDefault();
                Assert.IsNotNull(primeiroGrupo, "Grupo não foi incluído");

                var grupo = repositorio.ObterPorCodigoComPermissoes(primeiroGrupo.GrupoPermissaoCodigo);
                Assert.AreEqual(primeiroGrupo, grupo, "Grupo não é igual");
                Assert.IsNotNull(primeiroGrupo.Permissoes, "Grupo sem permissões");
            }
        }

        [TestMethod]
        public void ObterGrupoPermissaoPorCodigoComPermissoesEUsuarios()
        {
            using (var repositorio = new GrupoPermissaoRepository(_context))
            {
                repositorio.Criar(new GrupoPermissao
                {
                    GrupoPermissaoNome = "Novo",
                    Permissoes = ObterPermissoes(),
                    Usuarios = new List<Usuario>
                    {
                        new Usuario
                        {
                            UsuarioNome = "Henrique",
                            Senha = PasswordAssertionConcern.Encrypt("123456"),
                            Ativo = true,
                        }
                    }
                });

                var primeiroGrupo = _context.GruposPermissao.FirstOrDefault();
                Assert.IsNotNull(primeiroGrupo, "Grupo não foi incluído");

                var grupo = repositorio.ObterPorCodigoComPermissoesEUsuarios(primeiroGrupo.GrupoPermissaoCodigo);
                Assert.AreEqual(primeiroGrupo, grupo, "Grupo não é igual");
                Assert.IsNotNull(primeiroGrupo.Permissoes, "Grupo sem permissões");
                Assert.IsNotNull(primeiroGrupo.Usuarios, "Grupo sem usuarios");
            }
        }

        [TestMethod]
        public void ObterGrupoPermissaoPorNome()
        {
            using (var repositorio = new GrupoPermissaoRepository(_context))
            {
                repositorio.Criar(new GrupoPermissao
                {
                    GrupoPermissaoNome = "Novo",
                    Permissoes = ObterPermissoes()
                });

                var primeiroGrupo = _context.GruposPermissao.FirstOrDefault();
                Assert.IsNotNull(primeiroGrupo, "Grupo não foi incluído");

                var grupo = repositorio.ObterPorNome(primeiroGrupo.GrupoPermissaoNome);
                Assert.AreEqual(primeiroGrupo, grupo, "Grupo não é igual");
            }
        }

        [TestMethod]
        public void ObterListaGrupoPermissao()
        {
            using (var repositorio = new GrupoPermissaoRepository(_context))
            {
                repositorio.Criar(new GrupoPermissao
                {
                    GrupoPermissaoNome = "Novo",
                    Permissoes = ObterPermissoes()
                });

                var grupos = repositorio.ObterLista();
                Assert.IsTrue(grupos.Any(), "Grupo não incluido");
            }
        }

        [TestMethod]
        public void AtualizarGrupoPermissao()
        {
            using (var repositorio = new GrupoPermissaoRepository(_context))
            {
                repositorio.Criar(new GrupoPermissao
                {
                    GrupoPermissaoNome = "Novo",
                    Permissoes = ObterPermissoes()
                });

                var antesAtualizado = _context.GruposPermissao.FirstOrDefault();
                Assert.IsNotNull(antesAtualizado, "Grupo não foi incluído");
                antesAtualizado.GrupoPermissaoNome = "Velho";

                repositorio.Atualizar(antesAtualizado);
                var aposAtualizado = _context.GruposPermissao.FirstOrDefault();
                Assert.IsNotNull(aposAtualizado, "Grupo não foi Atualizado");
                Assert.AreEqual(aposAtualizado.GrupoPermissaoNome, "Velho", "Grupo não foi Atualizado");
            }
        }

        [TestMethod]
        public void DeletarGrupoPermissao()
        {
            using (var repositorio = new GrupoPermissaoRepository(_context))
            {
                repositorio.Criar(new GrupoPermissao
                {
                    GrupoPermissaoNome = "Novo",
                    Permissoes = ObterPermissoes()
                });

                var primeiro = _context.GruposPermissao.FirstOrDefault();
                Assert.IsNotNull(primeiro, "Grupo não foi incluído");
                repositorio.Deletar(primeiro);
                var retorno = _context.GruposPermissao.FirstOrDefault();
                Assert.IsNull(retorno, "Grupo não foi removido");
            }
        }
    }
}
