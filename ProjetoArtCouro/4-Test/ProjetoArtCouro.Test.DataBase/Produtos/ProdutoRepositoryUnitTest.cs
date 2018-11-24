using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Repositorios.ProdutoRepository;
using ProjetoArtCouro.Domain.Entities.Produtos;
using ProjetoArtCouro.Test.DataBase.Infra;
using System.Linq;

namespace ProjetoArtCouro.Test.DataBase.Produtos
{
    [TestClass]
    public class ProdutoRepositoryUnitTest
    {
        private DataBaseContext _context;

        [TestInitialize]
        public void Inicializacao()
        {
            var dbConnection = DbConnectionFactory.CreateTransient();
            var modelBuilder = EntityFrameworkHelper.GetDbModelBuilder();
            _context = new DataBaseContext(dbConnection, modelBuilder);
        }

        [TestMethod]
        public void CriarProduto()
        {
            using (var repositorio = new ProdutoRepository(_context))
            {
                repositorio.Criar(new Produto
                {
                    PrecoCusto = 10.0M,
                    PrecoVenda = 12.0M,
                    ProdutoNome = "Cinto",
                    Unidade = new Unidade { UnidadeNome = "UN" }
                });

                var produtos = _context.Produtos.ToList();
                Assert.IsTrue(produtos.Any(), "Produto não foi incluído");
                Assert.IsTrue(produtos.Any(x => x.PrecoCusto == 10.0M), "Produto não foi incluído");
                Assert.IsTrue(produtos.Any(x => x.PrecoVenda == 12.0M), "Produto não foi incluído");
                Assert.IsTrue(produtos.Any(x => x.ProdutoNome == "Cinto"), "Produto não foi incluído");
                Assert.IsTrue(produtos.All(x => x.Unidade != null), "Produto não foi incluído");
            }
        }

        [TestMethod]
        public void ObterProdutoPorId()
        {
            using (var repositorio = new ProdutoRepository(_context))
            {
                repositorio.Criar(new Produto
                {
                    PrecoCusto = 10.0M,
                    PrecoVenda = 12.0M,
                    ProdutoNome = "Cinto",
                    Unidade = new Unidade { UnidadeNome = "UN" }
                });

                var primeiroProduto = _context.Produtos.FirstOrDefault();
                Assert.IsNotNull(primeiroProduto, "Produto não foi incluído");

                var produto = repositorio.ObterPorId(primeiroProduto.ProdutoId);
                Assert.AreEqual(primeiroProduto, produto, "Produto não é igual");
            }
        }

        [TestMethod]
        public void ObterProdutoPorCodigo()
        {
            using (var repositorio = new ProdutoRepository(_context))
            {
                repositorio.Criar(new Produto
                {
                    PrecoCusto = 10.0M,
                    PrecoVenda = 12.0M,
                    ProdutoNome = "Cinto",
                    Unidade = new Unidade { UnidadeNome = "UN" }
                });

                var primeiroProduto = _context.Produtos.FirstOrDefault();
                Assert.IsNotNull(primeiroProduto, "Produto não foi incluído");

                var produto = repositorio.ObterPorCodigo(primeiroProduto.ProdutoCodigo);
                Assert.AreEqual(primeiroProduto, produto, "Produto não é igual");
            }
        }

        [TestMethod]
        public void ObterProdutoPorCodigoComUnidade()
        {
            using (var repositorio = new ProdutoRepository(_context))
            {
                repositorio.Criar(new Produto
                {
                    PrecoCusto = 10.0M,
                    PrecoVenda = 12.0M,
                    ProdutoNome = "Cinto",
                    Unidade = new Unidade { UnidadeNome = "UN" }
                });

                var primeiroProduto = _context.Produtos.FirstOrDefault();
                Assert.IsNotNull(primeiroProduto, "Produto não foi incluído");

                var produto = repositorio.ObterComUnidadePorCodigo(primeiroProduto.ProdutoCodigo);
                Assert.AreEqual(primeiroProduto, produto, "Produto não é igual");
                Assert.IsNotNull(primeiroProduto.Unidade, "Produto não tem unidade");
            }
        }

        [TestMethod]
        public void ObterListaProduto()
        {
            using (var repositorio = new ProdutoRepository(_context))
            {
                repositorio.Criar(new Produto
                {
                    PrecoCusto = 10.0M,
                    PrecoVenda = 12.0M,
                    ProdutoNome = "Cinto",
                    Unidade = new Unidade { UnidadeNome = "UN" }
                });

                var produtos = repositorio.ObterLista();
                Assert.IsTrue(produtos.Any(), "Produto não incluido");
            }
        }

        [TestMethod]
        public void ObterListaProdutoComUnidade()
        {
            using (var repositorio = new ProdutoRepository(_context))
            {
                repositorio.Criar(new Produto
                {
                    PrecoCusto = 10.0M,
                    PrecoVenda = 12.0M,
                    ProdutoNome = "Cinto",
                    Unidade = new Unidade { UnidadeNome = "UN" }
                });

                var produtos = repositorio.ObterLista();
                Assert.IsTrue(produtos.Any(), "Produto não incluido");
                Assert.IsTrue(produtos.All(x => x.Unidade != null), "Produto não incluido");
            }
        }

        [TestMethod]
        public void AtualizarProduto()
        {
            using (var repositorio = new ProdutoRepository(_context))
            {
                repositorio.Criar(new Produto
                {
                    PrecoCusto = 10.0M,
                    PrecoVenda = 12.0M,
                    ProdutoNome = "Cinto",
                    Unidade = new Unidade { UnidadeNome = "UN" }
                });

                var antesAtualizado = _context.Produtos.FirstOrDefault();
                Assert.IsNotNull(antesAtualizado, "Produto não foi incluído");
                antesAtualizado.PrecoCusto = 20.1M;
                antesAtualizado.PrecoVenda = 26.96M;
                antesAtualizado.ProdutoNome = "Sapato";

                repositorio.Atualizar(antesAtualizado);
                var aposAtualizado = _context.Produtos.FirstOrDefault();
                Assert.IsNotNull(aposAtualizado, "Produto não foi Atualizado");
                Assert.AreEqual(aposAtualizado.PrecoCusto, 20.1M, "Produto não foi Atualizado");
                Assert.AreEqual(aposAtualizado.PrecoVenda, 26.96M, "Produto não foi Atualizado");
                Assert.AreEqual(aposAtualizado.ProdutoNome, "Sapato", "Produto não foi Atualizado");
            }
        }

        [TestMethod]
        public void DeletarProduto()
        {
            using (var repositorio = new ProdutoRepository(_context))
            {
                repositorio.Criar(new Produto
                {
                    PrecoCusto = 10.0M,
                    PrecoVenda = 12.0M,
                    ProdutoNome = "Cinto",
                    Unidade = new Unidade { UnidadeNome = "UN" }
                });

                var primeiro = _context.Produtos.FirstOrDefault();
                Assert.IsNotNull(primeiro, "Produto não foi incluído");
                repositorio.Deletar(primeiro);
                var retorno = _context.Produtos.FirstOrDefault();
                Assert.IsNull(retorno, "Produto não foi removido");
            }
        }
    }
}
