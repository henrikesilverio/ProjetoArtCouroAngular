using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Resources.Resources;

namespace ProjetoArtCouro.Test.Domain.Pessoas
{
    [TestClass]
    public class EnderecoUnitTest
    {
        [TestMethod]
        public void ValidarEnderecoSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var endereco = new Endereco();
                endereco.Validar();
            }
            catch (InvalidOperationException e)
            {
                var mensagens = ObterMensagensValidas(e, 6);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "CEP"))),
                    "Falta mensagem CEP obrigatório");
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "Logradouro"))),
                    "Falta mensagem Logradouro obrigatório");
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "Numero"))),
                    "Falta mensagem Numero obrigatório");
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "Bairro"))),
                   "Falta mensagem Bairro obrigatório");
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "Cidade"))),
                  "Falta mensagem Cidade obrigatório");
                Assert.IsTrue(mensagens.Any(x => x.Contains(Erros.EmptyState)),
                  "Falta mensagem Estado obrigatório");
            }
        }

        [TestMethod]
        public void ValidarEnderecoComPropriedadesObrigatoriasPreenchidas()
        {
            var endereco = new Endereco
            {
                CEP = "sdasd",
                Logradouro = "sdads",
                Numero = "dsads",
                Bairro = "sdasd",
                Cidade = "dsasda",
                Estado = new Estado()
            };
            endereco.Validar();
        }

        [TestMethod]
        public void ValidarEnderecoSemCEP()
        {
            try
            {
                var endereco = new Endereco
                {
                    Logradouro = "sdads",
                    Numero = "dsads",
                    Bairro = "sdasd",
                    Cidade = "dsasda",
                    Estado = new Estado()
                };
                endereco.Validar();
            }
            catch (InvalidOperationException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "CEP"))),
                    "Falta mensagem CEP obrigatório");
            }
        }

        [TestMethod]
        public void ValidarEnderecoComCEPComMaisDe9Caracteres()
        {
            try
            {
                var endereco = new Endereco
                {
                    CEP = new string('A', 10),
                    Logradouro = "sdads",
                    Numero = "dsads",
                    Bairro = "sdasd",
                    Cidade = "dsasda",
                    Estado = new Estado()
                };
                endereco.Validar();
            }
            catch (InvalidOperationException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "CEP", 9))),
                    "Falta mensagem CEP com mais de 9 caracteres");
            }
        }

        [TestMethod]
        public void ValidarEnderecoSemLogradouro()
        {
            try
            {
                var endereco = new Endereco
                {
                    CEP = "sdas",
                    Numero = "dsads",
                    Bairro = "sdasd",
                    Cidade = "dsasda",
                    Estado = new Estado()
                };
                endereco.Validar();
            }
            catch (InvalidOperationException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "Logradouro"))),
                   "Falta mensagem Logradouro obrigatório");
            }
        }

        [TestMethod]
        public void ValidarEnderecoComLogradouroComMaisDe200Caracteres()
        {
            try
            {
                var endereco = new Endereco
                {
                    CEP = "sadsd",
                    Logradouro = new string('A', 201),
                    Numero = "dsads",
                    Bairro = "sdasd",
                    Cidade = "dsasda",
                    Estado = new Estado()
                };
                endereco.Validar();
            }
            catch (InvalidOperationException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "Logradouro", 200))),
                    "Falta mensagem Logradouro com mais de 200 caracteres");
            }
        }

        [TestMethod]
        public void ValidarEnderecoSemBairro()
        {
            try
            {
                var endereco = new Endereco
                {
                    CEP = "sdas",
                    Logradouro = "sdads",
                    Numero = "dsads",
                    Cidade = "dsasda",
                    Estado = new Estado()
                };
                endereco.Validar();
            }
            catch (InvalidOperationException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "Bairro"))),
                   "Falta mensagem Bairro obrigatório");
            }
        }

        [TestMethod]
        public void ValidarEnderecoComBairroComMaisDe50Caracteres()
        {
            try
            {
                var endereco = new Endereco
                {
                    CEP = "sadsd",
                    Logradouro = "sdad",
                    Numero = "dsads",
                    Bairro = new string('A', 51),
                    Cidade = "dsasda",
                    Estado = new Estado()
                };
                endereco.Validar();
            }
            catch (InvalidOperationException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "Bairro", 50))),
                    "Falta mensagem Bairro com mais de 50 caracteres");
            }
        }

        [TestMethod]
        public void ValidarEnderecoSemNumero()
        {
            try
            {
                var endereco = new Endereco
                {
                    CEP = "sdas",
                    Logradouro = "sdads",
                    Bairro = "sdasd",
                    Cidade = "dsasda",
                    Estado = new Estado()
                };
                endereco.Validar();
            }
            catch (InvalidOperationException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "Numero"))),
                   "Falta mensagem Numero obrigatório");
            }
        }

        [TestMethod]
        public void ValidarEnderecoComNumeroComMaisDe6Caracteres()
        {
            try
            {
                var endereco = new Endereco
                {
                    CEP = "sadsd",
                    Logradouro = "sdad",
                    Numero = new string('A', 7),
                    Bairro = "sdadsad",
                    Cidade = "dsasda",
                    Estado = new Estado()
                };
                endereco.Validar();
            }
            catch (InvalidOperationException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "Numero", 6))),
                    "Falta mensagem Numero com mais de 6 caracteres");
            }
        }

        [TestMethod]
        public void ValidarEnderecoComComplementoComMaisDe50Caracteres()
        {
            try
            {
                var endereco = new Endereco
                {
                    CEP = "sadsd",
                    Logradouro = "sdad",
                    Numero = "sdasd",
                    Bairro = "sdadsad",
                    Cidade = "dsasda",
                    Complemento = new string('A', 51),
                    Estado = new Estado()
                };
                endereco.Validar();
            }
            catch (InvalidOperationException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "Complemento", 50))),
                    "Falta mensagem Complemento com mais de 50 caracteres");
            }
        }

        [TestMethod]
        public void ValidarEnderecoSemCidade()
        {
            try
            {
                var endereco = new Endereco
                {
                    CEP = "sdas",
                    Logradouro = "sdads",
                    Numero = "sdasd",
                    Bairro = "sdasd",
                    Estado = new Estado()
                };
                endereco.Validar();
            }
            catch (InvalidOperationException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "Cidade"))),
                   "Falta mensagem Cidade obrigatório");
            }
        }

        [TestMethod]
        public void ValidarEnderecoComCidadeComMaisDe50Caracteres()
        {
            try
            {
                var endereco = new Endereco
                {
                    CEP = "sadsd",
                    Logradouro = "sdad",
                    Numero = "sads",
                    Bairro = "sdadsad",
                    Cidade = new string('A', 51),
                    Estado = new Estado()
                };
                endereco.Validar();
            }
            catch (InvalidOperationException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "Cidade", 50))),
                    "Falta mensagem Cidade com mais de 50 caracteres");
            }
        }

        [TestMethod]
        public void ValidarEnderecoSemEstado()
        {
            try
            {
                var endereco = new Endereco
                {
                    CEP = "sdas",
                    Logradouro = "sdads",
                    Numero = "sdasd",
                    Bairro = "sdasd",
                    Cidade = "sdads"
                };
                endereco.Validar();
            }
            catch (InvalidOperationException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(Erros.EmptyState)),
                  "Falta mensagem Estado obrigatório");
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
