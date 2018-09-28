using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Resources;

namespace ProjetoArtCouro.Test.Domain.Pessoas
{
    [TestClass]
    public class PessoaFisicaUnitTest
    {
        [TestMethod]
        public void ValidarPessoaFisicaSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var pessoaFisica = new PessoaFisica();
                pessoaFisica.Validar();
            }
            catch (DomainException e)
            {
                var mensagens = ObterMensagensValidas(e, 5);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "CPF"))),
                    "Falta mensagem nome obrigatório");
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "RG"))),
                    "Falta mensagem nome obrigatório");
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "Sexo"))),
                    "Falta mensagem nome obrigatório");
                Assert.IsTrue(mensagens.Any(x => x.Contains(Erros.EmptyPerson)),
                    "Falta mensagem pessoa obrigatória");
                Assert.IsTrue(mensagens.Any(x => x.Contains(Erros.EmptyMaritalStatus)),
                    "Falta mensagem estado civil obrigatório");
            }
        }

        [TestMethod]
        public void ValidarPessoaFisicaComPropriedadesObrigatoriasPreenchidas()
        {
            var pessoaFisica = new PessoaFisica
            {
                CPF = "13321232",
                RG = "sdasd321",
                Sexo = "sadsads",
                Pessoa = new Pessoa(),
                EstadoCivil = new EstadoCivil()
            };
            pessoaFisica.Validar();
            Assert.AreEqual(pessoaFisica.Notifications.Count, 0, "Existem mensagens de erros");
        }

        [TestMethod]
        public void ValidarPessoaFisicaSemCPF()
        {
            try
            {
                var pessoaFisica = new PessoaFisica
                {
                    RG = "sdasd321",
                    Sexo = "sadsads",
                    Pessoa = new Pessoa(),
                    EstadoCivil = new EstadoCivil()
                };
                pessoaFisica.Validar();
            }
            catch (DomainException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "CPF"))),
                    "Falta mensagem CPF obrigatório");
            }
        }

        [TestMethod]
        public void ValidarPessoaFisicaSemRG()
        {
            try
            {
                var pessoaFisica = new PessoaFisica
                {
                    CPF = "13321232",
                    Sexo = "sadsads",
                    Pessoa = new Pessoa(),
                    EstadoCivil = new EstadoCivil()
                };
                pessoaFisica.Validar();
            }
            catch (DomainException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "RG"))),
                    "Falta mensagem RG obrigatório");
            }
        }

        [TestMethod]
        public void ValidarPessoaFisicaComRGComMaisDe15Caracteres()
        {
            try
            {
                var pessoaFisica = new PessoaFisica
                {
                    CPF = "13321232",
                    RG = new string('A', 16),
                    Sexo = "sadsads",
                    Pessoa = new Pessoa(),
                    EstadoCivil = new EstadoCivil()
                };
                pessoaFisica.Validar();
            }
            catch (DomainException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "RG", 15))),
                    "Falta mensagem RG com mais de 15 caracteres");
            }
        }

        [TestMethod]
        public void ValidarPessoaFisicaSemSexo()
        {
            try
            {
                var pessoaFisica = new PessoaFisica
                {
                    CPF = "13321232",
                    RG = "sdad",
                    Pessoa = new Pessoa(),
                    EstadoCivil = new EstadoCivil()
                };
                pessoaFisica.Validar();
            }
            catch (DomainException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "Sexo"))),
                    "Falta mensagem Sexo obrigatório");
            }
        }

        [TestMethod]
        public void ValidarPessoaFisicaComSexoComMaisDe10Caracteres()
        {
            try
            {
                var pessoaFisica = new PessoaFisica
                {
                    CPF = "13321232",
                    RG = "sdad",
                    Sexo = new string('A', 11),
                    Pessoa = new Pessoa(),
                    EstadoCivil = new EstadoCivil()
                };
                pessoaFisica.Validar();
            }
            catch (DomainException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "Sexo", 10))),
                    "Falta mensagem Sexo com mais de 10 caracteres");
            }
        }

        [TestMethod]
        public void ValidarPessoaFisicaSemPessoa()
        {
            try
            {
                var pessoaFisica = new PessoaFisica
                {
                    CPF = "13321232",
                    RG = new string('A', 15),
                    Sexo = new string('A', 10),
                    EstadoCivil = new EstadoCivil()
                };
                pessoaFisica.Validar();
            }
            catch (DomainException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(Erros.EmptyPerson)),
                    "Falta mensagem pessoa obrigatório");
            }
        }

        [TestMethod]
        public void ValidarPessoaSemEstadoCivil()
        {
            try
            {
                var pessoaFisica = new PessoaFisica
                {
                    CPF = "13321232",
                    RG = new string('A', 15),
                    Sexo = new string('A', 10),
                    Pessoa = new Pessoa()
                };
                pessoaFisica.Validar();
            }
            catch (DomainException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(Erros.EmptyMaritalStatus)),
                    "Falta mensagem estado civil obrigatório");
            }
        }

        private static string[] ObterMensagensValidas(Exception e, int quantidadeDeMensagens)
        {
            Assert.AreNotEqual(e.Message, "", "Nao retornou mensagens");
            var mensagens = e.Message.Split('-');
            Assert.AreNotEqual(mensagens.Length, 0, "Nao retornou mensagens");
            Assert.AreEqual(mensagens.Length, quantidadeDeMensagens, "Quantidade de mensagens invalida");
            mensagens = mensagens.Select(x => x.Trim()).ToArray();
            return mensagens;
        }
    }
}
