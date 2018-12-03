using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Repositorios.VendaRepository;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Resources.Validation;
using ProjetoArtCouro.Test.DataBase.Infra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoArtCouro.Test.DataBase.Vendas
{
    [TestClass]
    public class ItemVendaRepositoryUnitTest
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
                        .FirstOrDefault(x => x.PapelCodigo == (int)TipoPapelPessoaEnum.Cliente)
                },
                MeiosComunicacao = new List<MeioComunicacao>
                {
                    new MeioComunicacao
                    {
                        MeioComunicacaoNome = "henrikesilverio@gmail.com",
                        TipoComunicacao = TipoComunicacaoEnum.Email,
                        Principal = true
                    }
                },
                Enderecos = new List<Endereco>
                {
                    new Endereco
                    {
                        Logradouro = "Rua A",
                        Numero = "100",
                        CEP = "88100566",
                        Bairro = "Alfabeto",
                        Cidade = "Maringá",
                        Estado = new Estado { EstadoNome = "PR" },
                        Principal = true
                    }
                },
                PessoaFisica = new PessoaFisica
                {
                    CPF = "12345678909",
                    RG = "203004009",
                    Sexo = "M",
                    EstadoCivil = new EstadoCivil { EstadoCivilNome = "Solteiro" }
                }
            };
        }

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

        private Usuario ObterUsuarioBase()
        {
            return new Usuario
            {
                UsuarioNome = "Henrique",
                Senha = PasswordAssertionConcern.Encrypt("123456"),
                Ativo = true,
                GrupoPermissao = ObterGrupoPermissao()
            };
        }

        private Venda ObterVenda()
        {
            return new Venda
            {
                DataCadastro = DateTime.Now,
                StatusVenda = StatusVendaEnum.Aberto,
                ValorTotalBruto = 100,
                ValorTotalDesconto = 0,
                ValorTotalLiquido = 100,
                Cliente = ObterPessoaBase(),
                Usuario = ObterUsuarioBase(),
                CondicaoPagamento = new CondicaoPagamento
                {
                    Ativo = true,
                    Descricao = "A vista",
                    QuantidadeParcelas = 1
                },
                FormaPagamento = new FormaPagamento
                {
                    Ativo = true,
                    Descricao = "Cartão"
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
        public void CriarItemVenda()
        {
            using (var repositorio = new ItemVendaRepository(_context))
            {
                repositorio.Criar(new ItemVenda
                {
                    PrecoVenda = 100,
                    ProdutoCodigo = 1,
                    ProdutoNome = "Cinto",
                    Quantidade = 1,
                    ValorBruto = 100,
                    ValorDesconto = 0,
                    ValorLiquido = 100,
                    Venda = ObterVenda()
                });

                var itens = _context.ItensVenda.ToList();
                Assert.IsTrue(itens.Any(), "Item não foi incluído");
                Assert.IsTrue(itens.Any(x => x.PrecoVenda == 100), "Item não foi incluído");
                Assert.IsTrue(itens.Any(x => x.ProdutoCodigo == 1), "Item não foi incluído");
                Assert.IsTrue(itens.Any(x => x.ProdutoNome == "Cinto"), "Item não foi incluído");
                Assert.IsTrue(itens.Any(x => x.Quantidade == 1), "Item não foi incluído");
                Assert.IsTrue(itens.Any(x => x.ValorBruto == 100), "Item não foi incluído");
                Assert.IsTrue(itens.Any(x => x.ValorDesconto == 0), "Item não foi incluído");
                Assert.IsTrue(itens.Any(x => x.ValorLiquido == 100), "Item não foi incluído");
            }
        }

        [TestMethod]
        public void DeletarItemVenda()
        {
            using (var repositorio = new ItemVendaRepository(_context))
            {
                repositorio.Criar(new ItemVenda
                {
                    PrecoVenda = 100,
                    ProdutoCodigo = 1,
                    ProdutoNome = "Cinto",
                    Quantidade = 1,
                    ValorBruto = 100,
                    ValorDesconto = 0,
                    ValorLiquido = 100,
                    Venda = ObterVenda()
                });

                var primeiro = _context.ItensVenda.FirstOrDefault();
                Assert.IsNotNull(primeiro, "Item não foi incluído");
                repositorio.Deletar(primeiro);
                var retorno = _context.ItensVenda.FirstOrDefault();
                Assert.IsNull(retorno, "Item não foi removido");
            }
        }
    }
}
