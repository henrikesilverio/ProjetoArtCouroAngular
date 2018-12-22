using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjetoArtCouro.Business.UsuarioService;
using ProjetoArtCouro.Domain.Contracts.IRepository.IUsuario;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.Usuario;
using ProjetoArtCouro.Mapping.Configs;
using ProjetoArtCouro.Resources.Validation;
using System.Collections.Generic;

namespace ProjetoArtCouro.Test.Business.Usuarios
{
    [TestClass]
    public class UsuarioServiceUnitTest
    {
        private UsuarioService _usuarioService;
        private Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private Mock<IPermissaoRepository> _permissaoRepositoryMock;
        private Mock<IGrupoPermissaoRepository> _grupoPermissaoRepositoryMock;

        [TestInitialize]
        public void Inicializacao()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _permissaoRepositoryMock = new Mock<IPermissaoRepository>();
            _grupoPermissaoRepositoryMock = new Mock<IGrupoPermissaoRepository>();
            _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object,
                _permissaoRepositoryMock.Object,
                _grupoPermissaoRepositoryMock.Object);
            MapperConfig.RegisterMappings();
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void CriarUsuario_DadosInvalidos_Excecao()
        {
            _usuarioService.CriarUsuario(new UsuarioModel());
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Codigo do grudo diferente de zero")]
        public void CriarUsuario_GrupoCodigoZero_Excecao()
        {
            _usuarioService.CriarUsuario(new UsuarioModel
            {
                Ativo = true,
                UsuarioNome = "Henrique",
                Senha = "123",
                ConfirmarSenha = "123",
                GrupoCodigo = 0
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Senha igual a confirmação de senha")]
        public void CriarUsuario_SenhaDiferenteDeConfirmacaoDeSenha_Excecao()
        {
            _usuarioService.CriarUsuario(new UsuarioModel
            {
                Ativo = true,
                UsuarioNome = "Henrique",
                Senha = "123",
                ConfirmarSenha = "321",
                GrupoCodigo = 1
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Usuario encontrado")]
        public void CriarUsuario_UsuarioExistente_Excecao()
        {
            _usuarioRepositoryMock
                .Setup(x => x.ObterPorUsuarioNome("Henrique"))
                .Returns(new Usuario
                {
                    Ativo = true,
                    Senha = PasswordAssertionConcern.Encrypt("123"),
                    UsuarioNome = "Henrique",
                    UsuarioCodigo = 1
                });

            _usuarioService.CriarUsuario(new UsuarioModel
            {
                Ativo = true,
                UsuarioNome = "Henrique",
                Senha = "123",
                ConfirmarSenha = "123",
                GrupoCodigo = 1
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Grupo encontrado")]
        public void CriarUsuario_GrupoInexistente_Excecao()
        {
            _grupoPermissaoRepositoryMock
                .Setup(x => x.ObterPorCodigoComPermissoes(1))
                .Returns((GrupoPermissao)null);

            _usuarioService.CriarUsuario(new UsuarioModel
            {
                Ativo = true,
                UsuarioNome = "Henrique",
                Senha = "123",
                ConfirmarSenha = "123",
                GrupoCodigo = 1
            });
        }

        [TestMethod]
        public void CriarUsuario_DadosValidos_RetornaObjeto()
        {
            _grupoPermissaoRepositoryMock
                .Setup(x => x.ObterPorCodigoComPermissoes(1))
                .Returns(new GrupoPermissao
                {
                    GrupoPermissaoCodigo = 1,
                    GrupoPermissaoNome = "Todos"
                });

            _usuarioService.CriarUsuario(new UsuarioModel
            {
                Ativo = true,
                UsuarioNome = "Henrique",
                Senha = "123",
                ConfirmarSenha = "123",
                GrupoCodigo = 1
            });
        }

        [TestMethod]
        public void ObterListaUsuario_DadosExistentes_RetornaLista()
        {
            _usuarioRepositoryMock
                .Setup(x => x.ObterListaComPermissoes())
                .Returns(new List<Usuario>
                {
                    new Usuario
                    {
                        Ativo = true,
                        Senha = "123",
                        UsuarioCodigo = 1,
                        UsuarioNome = "Henrique",
                    }
                });

            var usuarios = _usuarioService.ObterListaUsuario();

            Assert.IsNotNull(usuarios, "Usuarios não deveriam ser nulos");
            Assert.AreEqual(usuarios.Count, 1, "Quantidade de usuarios invalida");
        }

        [TestMethod]
        public void ObterPermissoesUsuarioLogado_DadosExistentes_RetornaLista()
        {
            _usuarioRepositoryMock
                .Setup(x => x.ObterPorUsuarioNomeComPermissoes("Henrique"))
                .Returns(new Usuario
                {
                    Ativo = true,
                    Senha = "123",
                    UsuarioCodigo = 1,
                    UsuarioNome = "Henrique",
                    Permissoes = new List<Permissao>
                    {
                        new Permissao
                        {
                            AcaoNome = "Editar",
                            PermissaoNome = "Editar",
                            PermissaoCodigo = 1
                        }
                    }
                });

            var permissoes = _usuarioService.ObterPermissoesUsuarioLogado("Henrique");

            Assert.IsNotNull(permissoes, "Permissões não deveriam ser nulos");
            Assert.AreEqual(permissoes.Count, 1, "Quantidade de permissão invalida");
        }

        [TestMethod]
        public void PesquisarUsuario_DadosExistentes_RetornaLista()
        {
            _usuarioRepositoryMock
                .Setup(x => x.ObterListaPorFiltro(It.IsAny<PesquisaUsuario>()))
                .Returns(new List<Usuario>
                {
                    new Usuario
                    {
                        Ativo = true,
                        Senha = PasswordAssertionConcern.Encrypt("123"),
                        UsuarioNome = "Henrique",
                        UsuarioCodigo = 1
                    }
                });

            var usuarios = _usuarioService.PesquisarUsuario(new PesquisaUsuarioModel());
            Assert.IsNotNull(usuarios, "Usuarios não devem ser nulos");
        }

        [TestMethod]
        public void PesquisarUsuarioPorCodigo_DadosExistentes_RetornaObjeto()
        {
            _usuarioRepositoryMock
                .Setup(x => x.ObterPorCodigoComPermissoesEGrupo(1))
                .Returns(new Usuario
                {
                    Ativo = true,
                    Senha = PasswordAssertionConcern.Encrypt("123"),
                    UsuarioNome = "Henrique",
                    UsuarioCodigo = 1
                });

            var usuario = _usuarioService.PesquisarUsuarioPorCodigo(1);
            Assert.IsNotNull(usuario, "Usuario não deve ser nulo");
            Assert.AreEqual(usuario.UsuarioCodigo, 1, "Usuario com código invalido");
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Nome do usuario correto")]
        public void AlterarSenha_NomeVazio_Excecao()
        {
            _usuarioService.AlterarSenha(string.Empty, string.Empty);
        }

        [TestMethod]
        public void AlterarSenha_DadosValidos_SenhaAlterada()
        {
            _usuarioRepositoryMock
                .Setup(x => x.ObterPorUsuarioNomeComPermissoes("Henrique"))
                .Returns(new Usuario
                {
                    Ativo = true,
                    Senha = PasswordAssertionConcern.Encrypt("123"),
                    UsuarioNome = "Henrique",
                    UsuarioCodigo = 1
                });

            _usuarioService.AlterarSenha("Henrique", string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Usuario encontrado")]
        public void EditarUsuario_UsuarioInexistente_Excecao()
        {
            _usuarioRepositoryMock
                .Setup(x => x.ObterPorCodigoComPermissoesEGrupo(1))
                .Returns((Usuario)null);

            _usuarioService.EditarUsuario(new UsuarioModel
            {
                Ativo = true,
                ConfirmarSenha = "123",
                Senha = "123",
                UsuarioCodigo = 1,
                UsuarioNome = "Nome"
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Grupo encontrado")]
        public void EditarUsuario_GrupoInexistente_Excecao()
        {
            _usuarioRepositoryMock
                .Setup(x => x.ObterPorCodigoComPermissoesEGrupo(1))
                .Returns(new Usuario
                {
                    Ativo = true,
                    Senha = "123",
                    UsuarioCodigo = 1,
                    UsuarioNome = "Nome",
                    GrupoPermissao = new GrupoPermissao
                    {
                        GrupoPermissaoCodigo = 1,
                    }
                });

            _grupoPermissaoRepositoryMock
                .Setup(x => x.ObterPorCodigoComPermissoes(2))
                .Returns((GrupoPermissao)null);

            _usuarioService.EditarUsuario(new UsuarioModel
            {
                Ativo = true,
                ConfirmarSenha = "123",
                Senha = "123",
                UsuarioCodigo = 1,
                UsuarioNome = "Nome",
                GrupoCodigo = 2
            });
        }

        [TestMethod]
        public void EditarUsuario_DadosValidos_UsuarioEditado()
        {
            _usuarioRepositoryMock
                .Setup(x => x.ObterPorCodigoComPermissoesEGrupo(1))
                .Returns(new Usuario
                {
                    Ativo = true,
                    Senha = "123",
                    UsuarioCodigo = 1,
                    UsuarioNome = "Nome",
                    GrupoPermissao = new GrupoPermissao
                    {
                        GrupoPermissaoCodigo = 1,
                    }
                });

            _usuarioService.EditarUsuario(new UsuarioModel
            {
                Ativo = true,
                ConfirmarSenha = "123",
                Senha = "123",
                UsuarioCodigo = 1,
                UsuarioNome = "Nome",
                GrupoCodigo = 1
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Codigo do usuario diferente de zero")]
        public void EditarPermissaoUsuario_UsuarioCodigoZero_Excecao()
        {
            _usuarioService.EditarPermissaoUsuario(new ConfiguracaoUsuarioModel());
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Codigo do usuario diferente de zero")]
        public void EditarPermissaoUsuario_SemPermissoes_Excecao()
        {
            _usuarioService.EditarPermissaoUsuario(new ConfiguracaoUsuarioModel
            {
                UsuarioCodigo = 1,
                Permissoes = new List<PermissaoModel>()
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Usuario encontrado")]
        public void EditarPermissaoUsuario_UsuarioInexistente_Excecao()
        {
            _usuarioRepositoryMock
                .Setup(x => x.ObterPorCodigoComPermissoes(1))
                .Returns((Usuario)null);

            _usuarioService.EditarPermissaoUsuario(new ConfiguracaoUsuarioModel
            {
                UsuarioCodigo = 1,
                Permissoes = new List<PermissaoModel>
                {
                    new PermissaoModel
                    {
                        AcaoNome = "Editar",
                        Codigo = 1,
                        Nome = "Editar"
                    }
                }
            });
        }

        [TestMethod]
        public void EditarPermissaoUsuario_DadosValidos_RetornaObjeto()
        {
            _usuarioRepositoryMock
                .Setup(x => x.ObterPorCodigoComPermissoes(1))
                .Returns(new Usuario
                {
                    Ativo = true,
                    Senha = "123",
                    UsuarioCodigo = 1,
                    UsuarioNome = "Nome",
                    Permissoes = new List<Permissao>
                    {
                        new Permissao
                        {
                            AcaoNome = "Editar",
                            PermissaoCodigo = 1,
                            PermissaoNome = "Editar"
                        }
                    }
                });

            _permissaoRepositoryMock
                .Setup(x => x.ObterLista())
                .Returns(new List<Permissao>
                {
                    new Permissao
                    {
                        AcaoNome = "Editar",
                        PermissaoCodigo = 1,
                        PermissaoNome = "Editar"
                    }
                });

            _usuarioService.EditarPermissaoUsuario(new ConfiguracaoUsuarioModel
            {
                UsuarioCodigo = 1,
                Permissoes = new List<PermissaoModel>
                {
                    new PermissaoModel
                    {
                        AcaoNome = "Editar",
                        Codigo = 1,
                        Nome = "Editar"
                    }
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Codigo do usuario diferente de zero")]
        public void ExcluirUsuario_CodigoUsuarioZero_Excecao()
        {
            _usuarioService.ExcluirUsuario(0);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Usuario encontrado")]
        public void ExcluirUsuario_UsuarioExistente_Excecao()
        {
            _usuarioRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns((Usuario)null);

            _usuarioService.ExcluirUsuario(1);
        }

        [TestMethod]
        public void ExcluirUsuario_DadosValidos_UsuarioExclido()
        {
            _usuarioRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new Usuario
                {
                    Ativo = true,
                    Senha = "123",
                    UsuarioCodigo = 1,
                    UsuarioNome = "Nome",
                    GrupoPermissao = new GrupoPermissao
                    {
                        GrupoPermissaoCodigo = 1,
                    }
                });

            _usuarioService.ExcluirUsuario(1);
        }

        [TestMethod]
        public void ObterListaPermissao_DadosExistentes_RetornaLista()
        {
            _permissaoRepositoryMock
                .Setup(x => x.ObterLista())
                .Returns(new List<Permissao>
                {
                    new Permissao
                    {
                        AcaoNome = "Editar",
                        PermissaoCodigo = 1,
                        PermissaoNome = "Editar"
                    }
                });

            var permissoes = _usuarioService.ObterListaPermissao();
            Assert.IsNotNull(permissoes, "Permissões não devem ser nulos");
        }

        [TestMethod]
        public void ObterGrupoPermissaoPorCodigo_DadosExistentes_RetornaLista()
        {
            _grupoPermissaoRepositoryMock
                .Setup(x => x.ObterPorCodigoComPermissoes(1))
                .Returns(new GrupoPermissao
                {
                    GrupoPermissaoCodigo = 1,
                    GrupoPermissaoNome = "Todos",
                    Permissoes = new List<Permissao>
                    {
                        new Permissao
                        {
                            AcaoNome = "Editar",
                            PermissaoCodigo = 1,
                            PermissaoNome = "Editar"
                        }
                    }
                });

            var permissoes = _usuarioService.ObterGrupoPermissaoPorCodigo(1);
            Assert.IsNotNull(permissoes, "Permissões não devem ser nulos");
        }

        [TestMethod]
        public void PesquisarGrupo_DadosExistentes_RetornaLista()
        {
            _grupoPermissaoRepositoryMock
                .Setup(x => x.ObterListaForFiltro(It.IsAny<PesquisaGrupoPermissao>()))
                .Returns(new List<GrupoPermissao>
                {
                    new GrupoPermissao
                    {
                        GrupoPermissaoCodigo = 1,
                        GrupoPermissaoNome = "Todos",
                        Permissoes = new List<Permissao>
                        {
                            new Permissao
                            {
                                AcaoNome = "Editar",
                                PermissaoCodigo = 1,
                                PermissaoNome = "Editar"
                            }
                        }
                    }
                });

            var grupos = _usuarioService.PesquisarGrupo(new PesquisaGrupoPermissaoModel());
            Assert.IsNotNull(grupos, "Grupos não devem ser nulos");
        }

        [TestMethod]
        public void ObterListaGrupoPermissao_DadosExistentes_RetornaLista()
        {
            _grupoPermissaoRepositoryMock
                .Setup(x => x.ObterLista())
                .Returns(new List<GrupoPermissao>
                {
                    new GrupoPermissao
                    {
                        GrupoPermissaoCodigo = 1,
                        GrupoPermissaoNome = "Todos",
                        Permissoes = new List<Permissao>
                        {
                            new Permissao
                            {
                                AcaoNome = "Editar",
                                PermissaoCodigo = 1,
                                PermissaoNome = "Editar"
                            }
                        }
                    }
                });

            var grupos = _usuarioService.ObterListaGrupoPermissao();
            Assert.IsNotNull(grupos, "Grupos não devem ser nulos");
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void CriarGrupoPermissao_DadosInvalidos_Excecao()
        {
            _usuarioService.CriarGrupoPermissao(new GrupoModel());
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Grupo não encontrado")]
        public void CriarGrupoPermissao_GrupoExistente_Excecao()
        {
            _grupoPermissaoRepositoryMock
                .Setup(x => x.ObterPorNome("todos"))
                .Returns(new GrupoPermissao
                {
                    GrupoPermissaoCodigo = 1,
                    GrupoPermissaoNome = "TODOS",
                    Permissoes = new List<Permissao>
                    {
                        new Permissao
                        {
                            AcaoNome = "Editar",
                            PermissaoCodigo = 1,
                            PermissaoNome = "Editar"
                        }
                    }
                });

            _usuarioService.CriarGrupoPermissao(new GrupoModel
            {
                GrupoCodigo = 1,
                GrupoNome = "TODOS",
                Permissoes = new List<PermissaoModel>
                {
                    new PermissaoModel
                    {
                        AcaoNome = "Editar",
                        Codigo = 1,
                        Nome = "Editar"
                    }
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Permissão encontrado")]
        public void CriarGrupoPermissao_PermissoesInexistente_Excecao()
        {
            _grupoPermissaoRepositoryMock
                .Setup(x => x.ObterPorNome(It.IsAny<string>()))
                .Returns((GrupoPermissao)null);

            _permissaoRepositoryMock
                .Setup(x => x.ObterLista())
                .Returns(new List<Permissao>());

            _usuarioService.CriarGrupoPermissao(new GrupoModel
            {
                GrupoCodigo = 1,
                GrupoNome = "TODOS",
                Permissoes = new List<PermissaoModel>
                {
                    new PermissaoModel
                    {
                        AcaoNome = "Editar",
                        Codigo = 1,
                        Nome = "Editar"
                    }
                }
            });
        }

        [TestMethod]
        public void CriarGrupoPermissao_DadosValidos_GrupoCriado()
        {
            _grupoPermissaoRepositoryMock
                .Setup(x => x.ObterPorNome(It.IsAny<string>()))
                .Returns((GrupoPermissao)null);

            _permissaoRepositoryMock
                .Setup(x => x.ObterLista())
                .Returns(new List<Permissao>
                {
                    new Permissao
                    {
                        AcaoNome = "Editar",
                        PermissaoCodigo = 1,
                        PermissaoNome = "Editar"
                    }
                });

            _usuarioService.CriarGrupoPermissao(new GrupoModel
            {
                GrupoCodigo = 1,
                GrupoNome = "TODOS",
                Permissoes = new List<PermissaoModel>
                {
                    new PermissaoModel
                    {
                        AcaoNome = "Editar",
                        Codigo = 1,
                        Nome = "Editar"
                    }
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void EditarGrupoPermissao_DadosInvalidos_Excecao()
        {
            _usuarioService.EditarGrupoPermissao(new GrupoModel());
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Grupo encontrado")]
        public void EditarGrupoPermissao_GrupoInexistente_Excecao()
        {
            _grupoPermissaoRepositoryMock
                .Setup(x => x.ObterPorCodigoComPermissoesEUsuarios(It.IsAny<int>()))
                .Returns((GrupoPermissao)null);

            _usuarioService.EditarGrupoPermissao(new GrupoModel
            {
                GrupoCodigo = 1,
                GrupoNome = "TODOS",
                Permissoes = new List<PermissaoModel>
                {
                    new PermissaoModel
                    {
                        AcaoNome = "Editar",
                        Codigo = 1,
                        Nome = "Editar"
                    }
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Permissão encontrada")]
        public void EditarGrupoPermissao_PermissoesInexistente_Excecao()
        {
            _grupoPermissaoRepositoryMock
                .Setup(x => x.ObterPorCodigoComPermissoesEUsuarios(It.IsAny<int>()))
                .Returns(new GrupoPermissao
                {
                    GrupoPermissaoCodigo = 1,
                    GrupoPermissaoNome = "TODOS",
                    Permissoes = new List<Permissao>
                    {
                        new Permissao
                        {
                            AcaoNome = "Editar",
                            PermissaoCodigo = 1,
                            PermissaoNome = "Editar"
                        }
                    }
                });

            _permissaoRepositoryMock
                .Setup(x => x.ObterLista())
                .Returns(new List<Permissao>());

            _usuarioService.EditarGrupoPermissao(new GrupoModel
            {
                GrupoCodigo = 1,
                GrupoNome = "TODOS",
                Permissoes = new List<PermissaoModel>
                {
                    new PermissaoModel
                    {
                        AcaoNome = "Editar",
                        Codigo = 1,
                        Nome = "Editar"
                    }
                }
            });
        }

        [TestMethod]
        public void EditarGrupoPermissao_DadosValidos_GrupoEditado()
        {
            _grupoPermissaoRepositoryMock
                .Setup(x => x.ObterPorCodigoComPermissoesEUsuarios(It.IsAny<int>()))
                .Returns(new GrupoPermissao
                {
                    GrupoPermissaoCodigo = 1,
                    GrupoPermissaoNome = "TODOS",
                    Permissoes = new List<Permissao>
                    {
                        new Permissao
                        {
                            AcaoNome = "Editar",
                            PermissaoCodigo = 1,
                            PermissaoNome = "Editar"
                        }
                    }
                });

            _permissaoRepositoryMock
                .Setup(x => x.ObterLista())
                .Returns(new List<Permissao>
                {
                    new Permissao
                    {
                        AcaoNome = "Editar",
                        PermissaoNome = "Editar",
                        PermissaoCodigo = 1
                    }
                });

            _usuarioService.EditarGrupoPermissao(new GrupoModel
            {
                GrupoCodigo = 1,
                GrupoNome = "TODOS",
                Permissoes = new List<PermissaoModel>
                {
                    new PermissaoModel
                    {
                        AcaoNome = "Editar",
                        Codigo = 1,
                        Nome = "Editar"
                    }
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Grupo encontrado")]
        public void ExcluirGrupoPermissao_GrupoInexistente_Excecao()
        {
            _grupoPermissaoRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns((GrupoPermissao)null);

            _usuarioService.ExcluirGrupoPermissao(1);
        }

        [TestMethod]
        public void ExcluirGrupoPermissao_DadosValidos_GrupoExcluido()
        {
            _grupoPermissaoRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new GrupoPermissao
                {
                    GrupoPermissaoCodigo = 1,
                    GrupoPermissaoNome = "TODOS",
                    Permissoes = new List<Permissao>
                    {
                        new Permissao
                        {
                            AcaoNome = "Editar",
                            PermissaoCodigo = 1,
                            PermissaoNome = "Editar"
                        }
                    }
                });

            _usuarioService.ExcluirGrupoPermissao(1);
        }
    }
}
