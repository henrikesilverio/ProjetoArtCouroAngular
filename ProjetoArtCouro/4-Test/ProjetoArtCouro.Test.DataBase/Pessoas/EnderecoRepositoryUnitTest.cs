using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Repositorios.PessoaRepository;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Test.DataBase.Infra;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoArtCouro.Test.DataBase.Pessoas
{
    [TestClass]
    public class EnderecoRepositoryUnitTest
    {
        private DataBaseContext _context;

        private Pessoa ObterPessoaBase()
        {
            return new Pessoa
            {
                Nome = "Henrique",
                Papeis = new List<Papel>
                {
                    _context.Papeis
                        .FirstOrDefault(x => x.PapelCodigo == (int)TipoPapelPessoaEnum.Funcionario)
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
        public void CriarEndereco()
        {
            using (var repositorio = new EnderecoRepository(_context))
            {
                repositorio.Criar(new Endereco
                {
                    Logradouro = "Rua A",
                    Numero = "100",
                    CEP = "88100566",
                    Bairro = "Alfabeto",
                    Cidade = "Maringá",
                    Estado = new Estado { EstadoNome = "PR" },
                    Principal = true,
                    Pessoa = ObterPessoaBase()
                });

                var enderecos = _context.Enderecos.ToList();
                Assert.IsTrue(enderecos.Any(), "Endereço não foi incluído");
                Assert.IsTrue(enderecos.Any(x => x.Logradouro == "Rua A"), "Endereço não foi incluído");
                Assert.IsTrue(enderecos.Any(x => x.Numero == "100"), "Endereço não foi incluído");
                Assert.IsTrue(enderecos.Any(x => x.Bairro == "Alfabeto"), "Endereço não foi incluído");
                Assert.IsTrue(enderecos.Any(x => x.Cidade == "Maringá"), "Endereço não foi incluído");
                Assert.IsTrue(enderecos.Any(x => x.CEP == "88100566"), "Endereço não foi incluído");
                Assert.IsTrue(enderecos.Any(x => x.Principal == true), "Endereço não foi incluído");
                Assert.IsTrue(enderecos.All(x => x.Estado != null), "Endereço não foi incluído");
            }
        }

        [TestMethod]
        public void ObterEnderecoPorId()
        {
            using (var repositorio = new EnderecoRepository(_context))
            {
                repositorio.Criar(new Endereco
                {
                    Logradouro = "Rua A",
                    Numero = "100",
                    CEP = "88100566",
                    Bairro = "Alfabeto",
                    Cidade = "Maringá",
                    Estado = new Estado { EstadoNome = "PR" },
                    Principal = true,
                    Pessoa = ObterPessoaBase()
                });

                var primeiroEndereco = _context.Enderecos.FirstOrDefault();
                Assert.IsNotNull(primeiroEndereco, "Endereço não foi incluído");

                var endereco = repositorio.ObterPorId(primeiroEndereco.EnderecoId);
                Assert.AreEqual(primeiroEndereco, endereco, "Endereço não é igual");
            }
        }

        [TestMethod]
        public void ObterListaDeEndereco()
        {
            using (var repositorio = new EnderecoRepository(_context))
            {
                repositorio.Criar(new Endereco
                {
                    Logradouro = "Rua A",
                    Numero = "100",
                    CEP = "88100566",
                    Bairro = "Alfabeto",
                    Cidade = "Maringá",
                    Estado = new Estado { EstadoNome = "PR" },
                    Principal = true,
                    Pessoa = ObterPessoaBase()
                });

                var enderecos = repositorio.ObterLista();
                Assert.IsTrue(enderecos.Any(), "Endereço não foi incluído");
            }
        }

        [TestMethod]
        public void AtualizarEndereco()
        {
            using (var repositorio = new EnderecoRepository(_context))
            {
                repositorio.Criar(new Endereco
                {
                    Logradouro = "Rua A",
                    Numero = "100",
                    CEP = "88100566",
                    Bairro = "Alfabeto",
                    Cidade = "Maringá",
                    Estado = new Estado { EstadoNome = "PR" },
                    Principal = true,
                    Pessoa = ObterPessoaBase()
                });

                var antesAtualizado = _context.Enderecos.FirstOrDefault();
                Assert.IsNotNull(antesAtualizado, "Endereço não foi incluído");
                antesAtualizado.Logradouro = "Rua B";
                antesAtualizado.Numero = "200";
                antesAtualizado.CEP = "00303130";
                antesAtualizado.Bairro = "Lll";
                antesAtualizado.Cidade = "Londrina";
                antesAtualizado.Principal = false;

                repositorio.Atualizar(antesAtualizado);
                var aposAtualizado = _context.Enderecos.FirstOrDefault();
                Assert.IsNotNull(aposAtualizado, "Endereço não foi Atualizado");
                Assert.AreEqual(aposAtualizado.Logradouro, "Rua B", "Endereço não foi Atualizado");
                Assert.AreEqual(aposAtualizado.Numero, "200", "Endereço não foi Atualizado");
                Assert.AreEqual(aposAtualizado.CEP, "00303130", "Endereço não foi Atualizado");
                Assert.AreEqual(aposAtualizado.Bairro, "Lll", "Endereço não foi Atualizado");
                Assert.AreEqual(aposAtualizado.Cidade, "Londrina", "Endereço não foi Atualizado");
                Assert.AreEqual(aposAtualizado.Principal, false, "Endereço não foi Atualizado");
            }
        }

        [TestMethod]
        public void DeletarEndereco()
        {
            using (var repositorio = new EnderecoRepository(_context))
            {
                repositorio.Criar(new Endereco
                {
                    Logradouro = "Rua A",
                    Numero = "100",
                    CEP = "88100566",
                    Bairro = "Alfabeto",
                    Cidade = "Maringá",
                    Estado = new Estado { EstadoNome = "PR" },
                    Principal = true,
                    Pessoa = ObterPessoaBase()
                });

                var primeiro = _context.Enderecos.FirstOrDefault();
                Assert.IsNotNull(primeiro, "Endereço não foi incluído");
                repositorio.Deletar(primeiro);
                var retorno = _context.Enderecos.FirstOrDefault();
                Assert.IsNull(retorno, "Endereço não foi removido");
            }
        }
    }
}
