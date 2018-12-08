using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Repositorios.PagamentoRepository;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Test.DataBase.Infra;
using System.Linq;

namespace ProjetoArtCouro.Test.DataBase.Pagamentos
{
    [TestClass]
    public class CondicaoPagamentoRepositoryUnitTest
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
        public void CriarCondicaoPagamento()
        {
            using (var repositorio = new CondicaoPagamentoRepository(_context))
            {
                repositorio.Criar(new CondicaoPagamento
                {
                    Descricao = "A Vista",
                    Ativo = true,
                    QuantidadeParcelas = 1
                });

                var condicoesPagamento = _context.CondicoesPagamento.ToList();
                Assert.IsTrue(condicoesPagamento.Any(), "Condição de pagamento não foi incluído");
                Assert.IsTrue(condicoesPagamento.Any(x => x.Descricao == "A Vista"), "Condição de pagamento não foi incluído");
                Assert.IsTrue(condicoesPagamento.Any(x => x.Ativo == true), "Condição de pagamento não foi incluído");
                Assert.IsTrue(condicoesPagamento.Any(x => x.QuantidadeParcelas == 1), "Condição de pagamento não foi incluído");
            }
        }

        [TestMethod]
        public void ObterCondicaoPagamentoPorCodigo()
        {
            using (var repositorio = new CondicaoPagamentoRepository(_context))
            {
                repositorio.Criar(new CondicaoPagamento
                {
                    Descricao = "A Vista",
                    Ativo = true,
                    QuantidadeParcelas = 1
                });

                var primeiroCondicaoPagamento = _context.CondicoesPagamento.FirstOrDefault();
                Assert.IsNotNull(primeiroCondicaoPagamento, "Condição de pagamento não foi incluído");

                var CondicaoPagamento = repositorio.ObterPorCodigo(primeiroCondicaoPagamento.CondicaoPagamentoCodigo);
                Assert.AreEqual(primeiroCondicaoPagamento, CondicaoPagamento, "Condição de pagamento não é igual");
            }
        }

        [TestMethod]
        public void ObterListaCondicaoPagamento()
        {
            using (var repositorio = new CondicaoPagamentoRepository(_context))
            {
                repositorio.Criar(new CondicaoPagamento
                {
                    Descricao = "A Vista",
                    Ativo = true,
                    QuantidadeParcelas = 1
                });

                var condicoesPagamento = repositorio.ObterLista();
                Assert.IsTrue(condicoesPagamento.Any(), "Condição de pagamento não incluido");
            }
        }

        [TestMethod]
        public void AtualizarCondicaoPagamento()
        {
            using (var repositorio = new CondicaoPagamentoRepository(_context))
            {
                repositorio.Criar(new CondicaoPagamento
                {
                    Descricao = "A Vista",
                    Ativo = true,
                    QuantidadeParcelas = 1
                });

                var antesAtualizado = _context.CondicoesPagamento.FirstOrDefault();
                Assert.IsNotNull(antesAtualizado, "Condição de pagamento não foi incluído");
                antesAtualizado.Descricao = "1 + 1";
                antesAtualizado.Ativo = false;
                antesAtualizado.QuantidadeParcelas = 2;

                repositorio.Atualizar(antesAtualizado);
                var aposAtualizado = _context.CondicoesPagamento.FirstOrDefault();
                Assert.IsNotNull(aposAtualizado, "Condição de pagamento não foi Atualizado");
                Assert.AreEqual(aposAtualizado.Descricao, "1 + 1", "Condição de pagamento não foi Atualizado");
                Assert.AreEqual(aposAtualizado.Ativo, false, "Condição de pagamento não foi Atualizado");
                Assert.AreEqual(aposAtualizado.QuantidadeParcelas, 2, "Condição de pagamento não foi Atualizado");
            }
        }

        [TestMethod]
        public void DeletarCondicaoPagamento()
        {
            using (var repositorio = new CondicaoPagamentoRepository(_context))
            {
                repositorio.Criar(new CondicaoPagamento
                {
                    Descricao = "A Vista",
                    Ativo = true,
                    QuantidadeParcelas = 1
                });

                var primeiro = _context.CondicoesPagamento.FirstOrDefault();
                Assert.IsNotNull(primeiro, "Condição de pagamento não foi incluído");
                repositorio.Deletar(primeiro);
                var retorno = _context.CondicoesPagamento.FirstOrDefault();
                Assert.IsNull(retorno, "Condição de pagamento não foi removido");
            }
        }
    }
}
