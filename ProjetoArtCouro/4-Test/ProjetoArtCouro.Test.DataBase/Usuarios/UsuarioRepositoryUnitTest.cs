using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Repositorios.UsuarioRepository;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Models.Usuario;
using ProjetoArtCouro.Resources.Validation;
using ProjetoArtCouro.Test.DataBase.Infra;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoArtCouro.Test.DataBase.Usuarios
{
    [TestClass]
    public class UsuarioRepositoryUnitTest
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

        private GrupoPermissao ObterGrupoPermissao()
        {
            return new GrupoPermissao
            {
                GrupoPermissaoNome = "TODOS",
                Permissoes = ObterPermissoes()
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
        public void CriarUsuario()
        {
            using (var repositorio = new UsuarioRepository(_context))
            {
                repositorio.Criar(new Usuario
                {
                    UsuarioNome = "Henrique",
                    Senha = PasswordAssertionConcern.Encrypt("123456"),
                    Ativo = true,
                    GrupoPermissao = ObterGrupoPermissao()
                });

                var usuarios = _context.Usuarios.ToList();
                Assert.IsTrue(usuarios.Any(), "Usuario não foi incluído");
                Assert.IsTrue(usuarios.Any(x => x.UsuarioNome == "Henrique"), "Usuario não foi incluído");
                Assert.IsTrue(usuarios.Any(x => x.Senha == PasswordAssertionConcern.Encrypt("123456")), "Usuario não foi incluído");
                Assert.IsTrue(usuarios.All(x => x.Ativo), "Usuario não foi incluído");
                Assert.IsTrue(usuarios.All(x => x.GrupoPermissao != null), "Usuario não foi incluído");
            }
        }

        [TestMethod]
        public void ObterUsuarioPorId()
        {
            using (var repositorio = new UsuarioRepository(_context))
            {
                repositorio.Criar(new Usuario
                {
                    UsuarioNome = "Henrique",
                    Senha = PasswordAssertionConcern.Encrypt("123456"),
                    Ativo = true,
                    GrupoPermissao = ObterGrupoPermissao()
                });

                var primeiroUsuario = _context.Usuarios.FirstOrDefault();
                Assert.IsNotNull(primeiroUsuario, "Usuario não foi incluído");

                var usuario = repositorio.ObterPorId(primeiroUsuario.UsuarioId);
                Assert.AreEqual(primeiroUsuario, usuario, "Usuario não é igual");
            }
        }

        [TestMethod]
        public void ObterUsuarioPorCodigo()
        {
            using (var repositorio = new UsuarioRepository(_context))
            {
                repositorio.Criar(new Usuario
                {
                    UsuarioNome = "Henrique",
                    Senha = PasswordAssertionConcern.Encrypt("123456"),
                    Ativo = true,
                    GrupoPermissao = ObterGrupoPermissao()
                });
        
                var primeiroUsuario = _context.Usuarios.FirstOrDefault();
                Assert.IsNotNull(primeiroUsuario, "Usuario não foi incluído");

                var usuario = repositorio.ObterPorCodigo(primeiroUsuario.UsuarioCodigo);
                Assert.AreEqual(primeiroUsuario, usuario, "Usuario não é igual");
            }
        }

        [TestMethod]
        public void ObterUsuarioPorCodigoComPermissoes()
        {
            using (var repositorio = new UsuarioRepository(_context))
            {
                repositorio.Criar(new Usuario
                {
                    UsuarioNome = "Henrique",
                    Senha = PasswordAssertionConcern.Encrypt("123456"),
                    Ativo = true,
                    GrupoPermissao = ObterGrupoPermissao()
                });

                var primeiroUsuario = _context.Usuarios.FirstOrDefault();
                Assert.IsNotNull(primeiroUsuario, "Usuario não foi incluído");

                var usuario = repositorio.ObterPorCodigoComPermissoes(primeiroUsuario.UsuarioCodigo);
                Assert.AreEqual(primeiroUsuario, usuario, "Usuario não é igual");
                Assert.IsNotNull(primeiroUsuario.Permissoes, "Usuario sem permissoes");
            }
        }

        [TestMethod]
        public void ObterUsuarioPorCodigoComPermissoesEGrupo()
        {
            using (var repositorio = new UsuarioRepository(_context))
            {
                repositorio.Criar(new Usuario
                {
                    UsuarioNome = "Henrique",
                    Senha = PasswordAssertionConcern.Encrypt("123456"),
                    Ativo = true,
                    GrupoPermissao = ObterGrupoPermissao()
                });

                var primeiroUsuario = _context.Usuarios.FirstOrDefault();
                Assert.IsNotNull(primeiroUsuario, "Usuario não foi incluído");

                var usuario = repositorio.ObterPorCodigoComPermissoesEGrupo(primeiroUsuario.UsuarioCodigo);
                Assert.AreEqual(primeiroUsuario, usuario, "Usuario não é igual");
                Assert.IsNotNull(primeiroUsuario.Permissoes, "Usuario sem permissoes");
                Assert.IsNotNull(primeiroUsuario.GrupoPermissao, "Usuario sem grupo");
            }
        }

        [TestMethod]
        public void ObterUsuarioPorUsuarioNome()
        {
            using (var repositorio = new UsuarioRepository(_context))
            {
                repositorio.Criar(new Usuario
                {
                    UsuarioNome = "Henrique",
                    Senha = PasswordAssertionConcern.Encrypt("123456"),
                    Ativo = true,
                    GrupoPermissao = ObterGrupoPermissao()
                });

                var primeiroUsuario = _context.Usuarios.FirstOrDefault();
                Assert.IsNotNull(primeiroUsuario, "Usuario não foi incluído");

                var usuario = repositorio.ObterPorUsuarioNome(primeiroUsuario.UsuarioNome);
                Assert.AreEqual(primeiroUsuario, usuario, "Usuario não é igual");
            }
        }

        [TestMethod]
        public void ObterUsuarioPorUsuarioNomeComPermissoes()
        {
            using (var repositorio = new UsuarioRepository(_context))
            {
                repositorio.Criar(new Usuario
                {
                    UsuarioNome = "Henrique",
                    Senha = PasswordAssertionConcern.Encrypt("123456"),
                    Ativo = true,
                    GrupoPermissao = ObterGrupoPermissao()
                });

                var primeiroUsuario = _context.Usuarios.FirstOrDefault();
                Assert.IsNotNull(primeiroUsuario, "Usuario não foi incluído");

                var usuario = repositorio.ObterPorUsuarioNomeComPermissoes(primeiroUsuario.UsuarioNome);
                Assert.AreEqual(primeiroUsuario, usuario, "Usuario não é igual");
                Assert.IsNotNull(primeiroUsuario.Permissoes, "Usuario sem permissoes");
            }
        }

        [TestMethod]
        public void ObterUsuarioPorUsuarioNomeComPermissoesEGrupo()
        {
            using (var repositorio = new UsuarioRepository(_context))
            {
                repositorio.Criar(new Usuario
                {
                    UsuarioNome = "Henrique",
                    Senha = PasswordAssertionConcern.Encrypt("123456"),
                    Ativo = true,
                    GrupoPermissao = ObterGrupoPermissao()
                });

                var primeiroUsuario = _context.Usuarios.FirstOrDefault();
                Assert.IsNotNull(primeiroUsuario, "Usuario não foi incluído");

                var usuario = repositorio.ObterPorUsuarioNomeComPermissoesEGrupo(primeiroUsuario.UsuarioNome);
                Assert.AreEqual(primeiroUsuario, usuario, "Usuario não é igual");
                Assert.IsNotNull(primeiroUsuario.Permissoes, "Usuario sem permissões");
                Assert.IsNotNull(primeiroUsuario.GrupoPermissao, "Usuario sem grupo");
                Assert.IsNotNull(primeiroUsuario.GrupoPermissao.Permissoes, "Grupo sem permissões");
            }
        }

        [TestMethod]
        public void ObterListaUsuario()
        {
            using (var repositorio = new UsuarioRepository(_context))
            {
                repositorio.Criar(new Usuario
                {
                    UsuarioNome = "Henrique",
                    Senha = PasswordAssertionConcern.Encrypt("123456"),
                    Ativo = true,
                    GrupoPermissao = ObterGrupoPermissao()
                });

                var usuarios = repositorio.ObterLista();
                Assert.IsTrue(usuarios.Any(), "Usuario não incluido");
            }
        }

        [TestMethod]
        public void ObterListaUsuarioComPermissoes()
        {
            using (var repositorio = new UsuarioRepository(_context))
            {
                repositorio.Criar(new Usuario
                {
                    UsuarioNome = "Henrique",
                    Senha = PasswordAssertionConcern.Encrypt("123456"),
                    Ativo = true,
                    GrupoPermissao = ObterGrupoPermissao()
                });

                var usuarios = repositorio.ObterListaComPermissoes();
                Assert.IsTrue(usuarios.Any(), "Usuario não incluido");
                Assert.IsTrue(usuarios.All(x => x.Permissoes != null), "Usuario não incluido");
            }
        }

        [TestMethod]
        public void ObterListaUsuarioPorFiltro()
        {
            using (var repositorio = new UsuarioRepository(_context))
            {
                repositorio.Criar(new Usuario
                {
                    UsuarioNome = "Henrique",
                    Senha = PasswordAssertionConcern.Encrypt("123456"),
                    Ativo = true,
                    GrupoPermissao = ObterGrupoPermissao()
                });

                var usuarios = repositorio.ObterListaPorFiltro(new PesquisaUsuario
                {
                    UsuarioNome = "Henrique",
                    Ativo = true,
                    GrupoPermissaoCodigo = 1
                });

                Assert.IsTrue(usuarios.Any(), "Usuario não incluido");
                Assert.IsTrue(usuarios.Any(x => x.UsuarioNome == "Henrique"), "Usuario sem nome");
                Assert.IsTrue(usuarios.All(x => x.Ativo), "Usuario inativo");
                Assert.IsTrue(usuarios.All(x => x.GrupoPermissao != null), "Usuario sem grupo");
            }
        }

        [TestMethod]
        public void AtualizarUsuario()
        {
            using (var repositorio = new UsuarioRepository(_context))
            {
                repositorio.Criar(new Usuario
                {
                    UsuarioNome = "Henrique",
                    Senha = PasswordAssertionConcern.Encrypt("123456"),
                    Ativo = true,
                    GrupoPermissao = ObterGrupoPermissao()
                });

                var antesAtualizado = _context.Usuarios.FirstOrDefault();
                Assert.IsNotNull(antesAtualizado, "Usuario não foi incluído");
                antesAtualizado.UsuarioNome = "Erica";
                antesAtualizado.Senha = PasswordAssertionConcern.Encrypt("654321");
                antesAtualizado.Ativo = false;

                repositorio.Atualizar(antesAtualizado);
                var aposAtualizado = _context.Usuarios.FirstOrDefault();
                Assert.IsNotNull(aposAtualizado, "Usuario não foi Atualizado");
                Assert.AreEqual(aposAtualizado.UsuarioNome, "Erica", "Usuario não foi Atualizado");
                Assert.AreEqual(aposAtualizado.Senha, PasswordAssertionConcern.Encrypt("654321"), "Usuario não foi Atualizado");
                Assert.AreEqual(aposAtualizado.Ativo, false, "Usuario não foi Atualizado");
            }
        }

        [TestMethod]
        public void DeletarUsuario()
        {
            using (var repositorio = new UsuarioRepository(_context))
            {
                repositorio.Criar(new Usuario
                {
                    UsuarioNome = "Henrique",
                    Senha = PasswordAssertionConcern.Encrypt("123456"),
                    Ativo = true,
                    GrupoPermissao = ObterGrupoPermissao()
                });

                var primeiro = _context.Usuarios.FirstOrDefault();
                Assert.IsNotNull(primeiro, "Usuario não foi incluído");
                repositorio.Deletar(primeiro);
                var retorno = _context.Usuarios.FirstOrDefault();
                Assert.IsNull(retorno, "Usuario não foi removido");
            }
        }
    }
}
