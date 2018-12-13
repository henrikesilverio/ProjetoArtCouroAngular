using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjetoArtCouro.Business.Services.AutenticacaoService;
using ProjetoArtCouro.Domain.Contracts.IRepository.IUsuario;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Resources.Validation;
using System.Collections.Generic;

namespace ProjetoArtCouro.Test.Business.Autenticacoes
{
    [TestClass]
    public class AutenticacaoServiceUnitTest
    {
        private AutenticacaoService _autenticacaoService;
        private Mock<IUsuarioRepository> _usuarioRepositoryMock;

        [TestInitialize]
        public void Inicializacao()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _autenticacaoService = new AutenticacaoService(_usuarioRepositoryMock.Object);
        }

        [TestMethod]
        public void AutenticarUsuario_Inexistente_RetornoNulo()
        {
            _usuarioRepositoryMock
                .Setup(x => x.ObterPorUsuarioNome("Henrique"))
                .Returns((Usuario)null);

            var usuario = _autenticacaoService
                .AutenticarUsuario("Henrique", string.Empty);

            Assert.IsNull(usuario, "Usuário deveria ser nulo");
        }

        [TestMethod]
        public void AutenticarUsuario_SenhaInvalida_RetornoNulo()
        {
            _usuarioRepositoryMock
                .Setup(x => x.ObterPorUsuarioNome("Henrique"))
                .Returns(new Usuario
                {
                    UsuarioNome = "Henrique",
                    Senha = PasswordAssertionConcern.Encrypt("123")
                });

            var usuario = _autenticacaoService
                .AutenticarUsuario("Henrique", "321");

            Assert.IsNull(usuario, "Usuário deveria ser nulo");
        }

        [TestMethod]
        public void AutenticarUsuario_UsuarioValido_RetornoUsuario()
        {
            _usuarioRepositoryMock
                .Setup(x => x.ObterPorUsuarioNome("Henrique"))
                .Returns(new Usuario
                {
                    UsuarioNome = "Henrique",
                    Senha = PasswordAssertionConcern.Encrypt("123")
                });

            var usuario = _autenticacaoService
                .AutenticarUsuario("Henrique", "123");

            Assert.IsNotNull(usuario, "Usuário não deveria ser nulo");
            Assert.AreEqual(usuario.UsuarioNome, "Henrique", "Nome do usuário invalido");
            Assert.AreEqual(usuario.Senha, PasswordAssertionConcern.Encrypt("123"), "Senha do usuário invalida");
        }

        [TestMethod]
        public void ObterPermissoes_UsuarioValido_RetornaLista()
        {
            _usuarioRepositoryMock
                .Setup(x => x.ObterPorUsuarioNomeComPermissoesEGrupo("Henrique"))
                .Returns(new Usuario
                {
                    UsuarioNome = "Henrique",
                    Senha = PasswordAssertionConcern.Encrypt("123"),
                    Permissoes = new List<Permissao>
                    {
                        new Permissao
                        {
                            AcaoNome = "Criar",
                            PermissaoNome = "Criar"
                        }
                    },
                    GrupoPermissao = new GrupoPermissao
                    {
                        Permissoes = new List<Permissao>
                        {
                            new Permissao
                            {
                                AcaoNome = "Deletar",
                                PermissaoNome = "Deletar"
                            }
                        }
                    }
                });

            var permissoes = _autenticacaoService
                .ObterPermissoes("Henrique");

            Assert.IsNotNull(permissoes, "Permissões não deveriam ser nulas");
            Assert.AreEqual(permissoes.Count, 2, "Quantidade de permissões invalidas");
        }
    }
}
