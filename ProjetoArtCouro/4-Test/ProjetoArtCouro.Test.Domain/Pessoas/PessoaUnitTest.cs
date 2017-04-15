using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Resources.Resources;

namespace ProjetoArtCouro.Test.Domain.Pessoas
{
    [TestClass]
    public class PessoaUnitTest
    {
        [TestMethod]
        public void ValidarPessoaSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var pessoa = new Pessoa();
                pessoa.Validar();
            }
            catch (InvalidOperationException e)
            {
                Assert.AreNotEqual(e.Message, "", "Nao retornou mensagens");
                var mensagens = e.Message.Split('-');
                Assert.AreNotEqual(mensagens.Length, 0, "Nao retornou mensagens");
                Assert.AreEqual(mensagens.Length, 4, "Quantidade de mensagens invalida");
                mensagens = mensagens.Select(x => x.Trim()).ToArray();
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "Nome"))),
                    "Falta mensagem nome obrigatório");
                Assert.IsTrue(mensagens.Any(x => x.Contains(Erros.PaperEmptyPerson)),
                    "Falta mensagem papel pessoa obrigatório");
                Assert.IsTrue(mensagens.Any(x => x.Contains(Erros.MeansOfCommunicationEmpty)),
                    "Falta mensagem meio de comunicação obrigatório");
                Assert.IsTrue(mensagens.Any(x => x.Contains(Erros.EmptyAddress)),
                    "Falta mensagem endereço obrigatório");
            }
        }

        [TestMethod]
        public void ValidarPessoaComPropriedadesObrigatoriasPreenchidas()
        {
            var pessoa = new Pessoa
            {
                Nome = "23231",
                Papeis = new List<Papel>(),
                MeiosComunicacao = new List<MeioComunicacao>(),
                Enderecos = new List<Endereco>()
            };
            pessoa.Validar();
            Assert.AreEqual(pessoa.Notifications.Count, 0, "Existem mensagens de erros");
        }

        [TestMethod]
        public void ValidarPessoaSemNome()
        {
            try
            {
                var pessoa = new Pessoa
                {
                    Papeis = new List<Papel>(),
                    MeiosComunicacao = new List<MeioComunicacao>(),
                    Enderecos = new List<Endereco>()
                };
                pessoa.Validar();
            }
            catch (Exception e)
            {
                Assert.AreNotEqual(e.Message, "", "Nao retornou mensagens");
                var mensagens = e.Message.Split('-');
                Assert.AreNotEqual(mensagens.Length, 0, "Nao retornou mensagens");
                Assert.AreEqual(mensagens.Length, 1, "Quantidade de mensagens invalida");
                mensagens = mensagens.Select(x => x.Trim()).ToArray();
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "Nome"))),
                    "Falta mensagem Nome obrigatório");
            }
        }

        [TestMethod]
        public void ValidarPessoaComNomeComMaisDe150Caracteres()
        {
            try
            {
                var pessoa = new Pessoa
                {
                    Nome = new string('-', 151),
                    Papeis = new List<Papel>(),
                    MeiosComunicacao = new List<MeioComunicacao>(),
                    Enderecos = new List<Endereco>()
                };
                pessoa.Validar();
            }
            catch (Exception e)
            {
                Assert.AreNotEqual(e.Message, "", "Nao retornou mensagens");
                var mensagens = e.Message.Split('-');
                Assert.AreNotEqual(mensagens.Length, 0, "Nao retornou mensagens");
                Assert.AreEqual(mensagens.Length, 1, "Quantidade de mensagens invalida");
                mensagens = mensagens.Select(x => x.Trim()).ToArray();
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "Nome", 150))),
                    "Falta mensagem Nome com mais de 150 caracteres");
            }
        }

        [TestMethod]
        public void ValidarPessoaSemPapel()
        {
            try
            {
                var pessoa = new Pessoa
                {
                    Nome = "1",
                    MeiosComunicacao = new List<MeioComunicacao>(),
                    Enderecos = new List<Endereco>()
                };
                pessoa.Validar();
            }
            catch (Exception e)
            {
                Assert.AreNotEqual(e.Message, "", "Nao retornou mensagens");
                var mensagens = e.Message.Split('-');
                Assert.AreNotEqual(mensagens.Length, 0, "Nao retornou mensagens");
                Assert.AreEqual(mensagens.Length, 1, "Quantidade de mensagens invalida");
                mensagens = mensagens.Select(x => x.Trim()).ToArray();
                Assert.IsTrue(mensagens.Any(x => x.Contains(Erros.PaperEmptyPerson)),
                    "Falta mensagem papel pessoa obrigatório");
            }
        }

        [TestMethod]
        public void ValidarPessoaSemMeioDeComunicacao()
        {
            try
            {
                var pessoa = new Pessoa
                {
                    Nome = "1",
                    Papeis = new List<Papel>(),
                    Enderecos = new List<Endereco>()
                };
                pessoa.Validar();
            }
            catch (Exception e)
            {
                Assert.AreNotEqual(e.Message, "", "Nao retornou mensagens");
                var mensagens = e.Message.Split('-');
                Assert.AreNotEqual(mensagens.Length, 0, "Nao retornou mensagens");
                Assert.AreEqual(mensagens.Length, 1, "Quantidade de mensagens invalida");
                mensagens = mensagens.Select(x => x.Trim()).ToArray();
                Assert.IsTrue(mensagens.Any(x => x.Contains(Erros.MeansOfCommunicationEmpty)),
                    "Falta mensagem meio de comunicação obrigatório");
            }
        }

        [TestMethod]
        public void ValidarPessoaSemEndereco()
        {
            try
            {
                var pessoa = new Pessoa
                {
                    Nome = "1",
                    MeiosComunicacao = new List<MeioComunicacao>(),
                    Papeis = new List<Papel>()
                };
                pessoa.Validar();
            }
            catch (Exception e)
            {
                Assert.AreNotEqual(e.Message, "", "Nao retornou mensagens");
                var mensagens = e.Message.Split('-');
                Assert.AreNotEqual(mensagens.Length, 0, "Nao retornou mensagens");
                Assert.AreEqual(mensagens.Length, 1, "Quantidade de mensagens invalida");
                mensagens = mensagens.Select(x => x.Trim()).ToArray();
                Assert.IsTrue(mensagens.Any(x => x.Contains(Erros.EmptyAddress)),
                    "Falta mensagem endereço obrigatório");
            }
        }
    }
}
