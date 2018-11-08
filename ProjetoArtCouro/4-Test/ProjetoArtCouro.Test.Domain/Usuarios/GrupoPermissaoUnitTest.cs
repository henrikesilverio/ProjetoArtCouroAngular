using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Test.Domain.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoArtCouro.Test.Domain.Usuarios
{
    [TestClass]
    public class GrupoPermissaoUnitTest
    {
        [TestMethod]
        public void ValidarGrupoPermissaoSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var grupoPermissao = new GrupoPermissao();
                grupoPermissao.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 2);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "GrupoPermissaoNome"))),
                    "Falta mensagem nome do grupo obrigatório");

                Assert.IsTrue(mensagens.Any(x => x.Contains(Erros.EmptyAllowList)),
                    "Falta mensagem lista de permissão vazia");
            }
        }

        [TestMethod]
        public void ValidarGrupoPermissaoComPropriedadesObrigatoriasPreenchidas()
        {
            var grupoPermissao = new GrupoPermissao
            {
                GrupoPermissaoNome = "NOVO",
                Permissoes = new List<Permissao> {
                    new Permissao { AcaoNome = "Salvar" }
                }
            };
            grupoPermissao.Validar();
        }

        [TestMethod]
        public void ValidarGrupoPermissaoComDescricaoComMaisDe50Caracteres()
        {
            try
            {
                var grupoPermissao = new GrupoPermissao
                {
                    GrupoPermissaoNome = new string('A', 51),
                    Permissoes = new List<Permissao> {
                        new Permissao { AcaoNome = "Salvar" }
                    }
                };
                grupoPermissao.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "GrupoPermissaoNome", 50))),
                    "Falta mensagem nome do grupo com mais de 50 caracteres");
            }
        }
    }
}
