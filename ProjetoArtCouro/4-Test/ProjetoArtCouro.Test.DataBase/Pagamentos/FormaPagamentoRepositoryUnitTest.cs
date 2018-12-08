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
    public class FormaPagamentoRepositoryUnitTest
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
        public void CriarFormaPagamento()
        {
            using (var repositorio = new FormaPagamentoRepository(_context))
            {
                repositorio.Criar(new FormaPagamento
                {
                    Descricao = "Cartão",
                    Ativo = true
                });

                var formasPagamento = _context.FormasPagamento.ToList();
                Assert.IsTrue(formasPagamento.Any(), "Forma de pagamento não foi incluído");
                Assert.IsTrue(formasPagamento.Any(x => x.Descricao == "Cartão"), "Forma de pagamento não foi incluído");
                Assert.IsTrue(formasPagamento.Any(x => x.Ativo == true), "Forma de pagamento não foi incluído");
            }
        }

        [TestMethod]
        public void ObterFormaPagamentoPorCodigo()
        {
            using (var repositorio = new FormaPagamentoRepository(_context))
            {
                repositorio.Criar(new FormaPagamento
                {
                    Descricao = "Cartão",
                    Ativo = true
                });

                var primeiroFormaPagamento = _context.FormasPagamento.FirstOrDefault();
                Assert.IsNotNull(primeiroFormaPagamento, "Forma de pagamento não foi incluído");

                var formaPagamento = repositorio.ObterPorCodigo(primeiroFormaPagamento.FormaPagamentoCodigo);
                Assert.AreEqual(primeiroFormaPagamento, formaPagamento, "Forma de pagamento não é igual");
            }
        }

        [TestMethod]
        public void ObterListaFormaPagamento()
        {
            using (var repositorio = new FormaPagamentoRepository(_context))
            {
                repositorio.Criar(new FormaPagamento
                {
                    Descricao = "Cartão",
                    Ativo = true
                });

                var formasPagamento = repositorio.ObterLista();
                Assert.IsTrue(formasPagamento.Any(), "Produto não incluido");
            }
        }

        [TestMethod]
        public void AtualizarFormaPagamento()
        {
            using (var repositorio = new FormaPagamentoRepository(_context))
            {
                repositorio.Criar(new FormaPagamento
                {
                    Descricao = "Cartão",
                    Ativo = true
                });

                var antesAtualizado = _context.FormasPagamento.FirstOrDefault();
                Assert.IsNotNull(antesAtualizado, "Forma de pagamento não foi incluído");
                antesAtualizado.Descricao = "Cheque";
                antesAtualizado.Ativo = false;

                repositorio.Atualizar(antesAtualizado);
                var aposAtualizado = _context.FormasPagamento.FirstOrDefault();
                Assert.IsNotNull(aposAtualizado, "Forma de pagamento não foi Atualizado");
                Assert.AreEqual(aposAtualizado.Descricao, "Cheque", "Forma de pagamento não foi Atualizado");
                Assert.AreEqual(aposAtualizado.Ativo, false, "Forma de pagamento não foi Atualizado");
            }
        }

        [TestMethod]
        public void DeletarFormaPagamento()
        {
            using (var repositorio = new FormaPagamentoRepository(_context))
            {
                repositorio.Criar(new FormaPagamento
                {
                    Descricao = "A Vista",
                    Ativo = true
                });

                var primeiro = _context.FormasPagamento.FirstOrDefault();
                Assert.IsNotNull(primeiro, "Forma de pagamento não foi incluído");
                repositorio.Deletar(primeiro);
                var retorno = _context.FormasPagamento.FirstOrDefault();
                Assert.IsNull(retorno, "Forma de pagamento não foi removido");
            }
        }
    }
}
