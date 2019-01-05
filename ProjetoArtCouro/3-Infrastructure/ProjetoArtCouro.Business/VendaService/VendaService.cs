using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using ProjetoArtCouro.Domain.Contracts.IRepository.IEstoque;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPagamento;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPessoa;
using ProjetoArtCouro.Domain.Contracts.IRepository.IProduto;
using ProjetoArtCouro.Domain.Contracts.IRepository.IUsuario;
using ProjetoArtCouro.Domain.Contracts.IRepository.IVenda;
using ProjetoArtCouro.Domain.Contracts.IService.IVenda;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resource.Validation;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.Venda;
using ProjetoArtCouro.Mapping;

namespace ProjetoArtCouro.Business.VendaService
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IItemVendaRepository _itemVendaRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IFormaPagamentoRepository _formaPagamentoRepository;
        private readonly ICondicaoPagamentoRepository _condicaoPagamentoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IContaReceberRepository _contaReceberRepository;
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly IProdutoRepository _produtoRepository;

        public VendaService(IVendaRepository vendaRepository, IItemVendaRepository itemVendaRepository,
            IPessoaRepository pessoaRepository, IFormaPagamentoRepository formaPagamentoRepository,
            ICondicaoPagamentoRepository condicaoPagamentoRepository, IUsuarioRepository usuarioRepository,
            IContaReceberRepository contaReceberRepository, IEstoqueRepository estoqueRepository,
            IProdutoRepository produtoRepository)
        {
            _vendaRepository = vendaRepository;
            _itemVendaRepository = itemVendaRepository;
            _pessoaRepository = pessoaRepository;
            _formaPagamentoRepository = formaPagamentoRepository;
            _condicaoPagamentoRepository = condicaoPagamentoRepository;
            _usuarioRepository = usuarioRepository;
            _contaReceberRepository = contaReceberRepository;
            _estoqueRepository = estoqueRepository;
            _produtoRepository = produtoRepository;
        }

        public void CriarVenda(int usuarioCodigo, VendaModel model)
        {
            var venda = Map<Venda>.MapperTo(model);
            venda.Validar();

            AssertionConcern<BusinessException>
                .AssertArgumentTrue(venda.ItensVenda.Any(), Erros.SaleItemsNotSet);

            venda.ItensVenda.ForEach(x => x.Validar());

            AssertionConcern<BusinessException>
                .AssertArgumentEquals(venda.StatusVenda, StatusVendaEnum.Aberto, 
                Erros.StatusOfDifferentSalesOpen);

            AplicaValidacoesPadrao(venda);
            var usuario = _usuarioRepository.ObterPorCodigo(usuarioCodigo);
            venda.Usuario = usuario;
            venda.Cliente = null;
            venda.FormaPagamento = null;
            venda.CondicaoPagamento = null;
            _vendaRepository.Criar(venda);
        }

        public List<VendaModel> PesquisarVenda(int codigoUsuario, PesquisaVendaModel model)
        {
            var filtro = Map<PesquisaVenda>.MapperTo(model);
            filtro.CodigoUsuario = codigoUsuario;
            var compras = _vendaRepository.ObterListaPorFiltro(filtro);
            return Map<List<VendaModel>>.MapperTo(compras);
        }

        public VendaModel ObterVendaPorCodigo(int codigoVenda)
        {
            var venda = _vendaRepository.ObterPorCodigoComItensVenda(codigoVenda);
            return Map<VendaModel>.MapperTo(venda);
        }

        public void AtualizarVenda(VendaModel model)
        {
            var venda = Map<Venda>.MapperTo(model);
            venda.Validar();

            AssertionConcern<BusinessException>
                .AssertArgumentTrue(venda.ItensVenda.Any(), Erros.SaleItemsNotSet);

            venda.ItensVenda.ForEach(x => x.Validar());
            
            var vendaAtual = _vendaRepository
                .ObterPorCodigoComItensVenda(venda.VendaCodigo);

            AssertionConcern<BusinessException>
                .AssertArgumentNotNull(vendaAtual, Erros.SaleDoesNotExist);

            if (venda.StatusVenda == StatusVendaEnum.Aberto)
            {
                VerificarEstoque(venda.ItensVenda);
                AplicaValidacoesPadrao(venda);
                AdicionaClienteFormaECondicaoDePagamento(venda, vendaAtual);
                AtualizaItensVenda(vendaAtual, venda.ItensVenda);
                vendaAtual.StatusVenda = StatusVendaEnum.Confirmado;
                AdicionaContaReceberNaVenda(vendaAtual);
                vendaAtual.ContasReceber.ForEach(x => x.Validar());
                BaixarDoEstoque(vendaAtual.ItensVenda);
            }
            else
            {
                vendaAtual.StatusVenda = StatusVendaEnum.Cancelado;
                RemoveContaReceberDaVenda(vendaAtual);
                DevolverNoEstoque(vendaAtual.ItensVenda);
            }
            _vendaRepository.Atualizar(vendaAtual);
        }

        public void ExcluirVenda(int codigoVenda)
        {
            var venda = _vendaRepository.ObterPorCodigo(codigoVenda);

            AssertionConcern<BusinessException>
                .AssertArgumentNotNull(venda, Erros.SaleDoesNotExist);

            AssertionConcern<BusinessException>
                .AssertArgumentNotEquals(venda.StatusVenda, StatusVendaEnum.Confirmado, 
                Erros.SaleConfirmedCanNotBeExcluded);

            _vendaRepository.Deletar(venda);
        }

        private void AtualizaItensVenda(Venda vendaAtual, IEnumerable<ItemVenda> itensVenda)
        {
            var itensVendaAtual = vendaAtual.ItensVenda.ToList();
            itensVendaAtual.ForEach(item =>
            {
                _itemVendaRepository.Deletar(item);
            });
            vendaAtual.ItensVenda.Clear();
            itensVenda.ForEach(item =>
            {
                item.Venda = vendaAtual;
                var novoItem = _itemVendaRepository.Criar(item);
                vendaAtual.ItensVenda.Add(novoItem);
            });
        }

        private static void AplicaValidacoesPadrao(Venda venda)
        {
            AssertionConcern<BusinessException>
                .AssertArgumentEquals(venda.ItensVenda.Sum(x => x.ValorBruto), 
                venda.ValorTotalBruto,
                Erros.SumDoNotMatchTotalCrudeValue);

            AssertionConcern<BusinessException>
                .AssertArgumentEquals(venda.ItensVenda.Sum(x => x.ValorDesconto),
                venda.ValorTotalDesconto,
                Erros.SumDoNotMatchTotalValueDiscount);

            AssertionConcern<BusinessException>
                .AssertArgumentEquals(venda.ItensVenda.Sum(x => x.ValorLiquido), 
                venda.ValorTotalLiquido,
                Erros.SumDoNotMatchTotalValueLiquid);
        }

        private void AdicionaClienteFormaECondicaoDePagamento(Venda venda, Venda vendaAtual)
        {
            var cliente = _pessoaRepository
                .ObterPorCodigo(venda.Cliente.PessoaCodigo);

            AssertionConcern<BusinessException>
                .AssertArgumentNotNull(cliente, Erros.ClientNotFound);

            var formaPagamento = _formaPagamentoRepository
                .ObterPorCodigo(venda.FormaPagamento.FormaPagamentoCodigo);

            AssertionConcern<BusinessException>
                .AssertArgumentNotNull(formaPagamento, Erros.FormOfPaymentDoesNotExist);

            var condicaoPagamento = _condicaoPagamentoRepository
                .ObterPorCodigo(venda.CondicaoPagamento.CondicaoPagamentoCodigo);

            AssertionConcern<BusinessException>
                .AssertArgumentNotNull(condicaoPagamento, Erros.PaymentConditionDoesNotExist);

            vendaAtual.Cliente = cliente;
            vendaAtual.FormaPagamento = formaPagamento;
            vendaAtual.CondicaoPagamento = condicaoPagamento;
        }

        private static void AdicionaContaReceberNaVenda(Venda venda)
        {
            venda.ContasReceber = new List<ContaReceber>();
            for (var i = 0; i < venda.CondicaoPagamento.QuantidadeParcelas; i++)
            {
                venda.ContasReceber.Add(new ContaReceber
                {
                    DataVencimento = DateTime.Now.AddDays(1).AddMonths(i),
                    Recebido = false,
                    StatusContaReceber = StatusContaReceberEnum.Aberto,
                    ValorDocumento = venda.ValorTotalLiquido / venda.CondicaoPagamento.QuantidadeParcelas,
                    Venda = venda
                });
            }
        }

        private void RemoveContaReceberDaVenda(Venda venda)
        {
            var contasReceber = _contaReceberRepository.ObterListaPorVendaCodigo(venda.VendaCodigo);
            contasReceber.ForEach(x =>
            {
                _contaReceberRepository.Deletar(x);
            });
        }

        private void BaixarDoEstoque(IEnumerable<ItemVenda> itensVenda)
        {
            itensVenda.ForEach(x =>
            {
                var estoque = _estoqueRepository.ObterPorCodigoProduto(x.ProdutoCodigo);
                estoque.Quantidade = estoque.Quantidade - x.Quantidade;
                _estoqueRepository.Atualizar(estoque);
            });
        }

        private void DevolverNoEstoque(IEnumerable<ItemVenda> itensVenda)
        {
            itensVenda.ForEach(x =>
            {
                var estoque = _estoqueRepository.ObterPorCodigoProduto(x.ProdutoCodigo);
                estoque.Quantidade = estoque.Quantidade + x.Quantidade;
                _estoqueRepository.Atualizar(estoque);
            });
        }

        private void VerificarEstoque(IEnumerable<ItemVenda> itensVenda)
        {
            var produtosQueFaltamNoEstoque = new List<Tuple<string, int, int>>();
            itensVenda.ForEach(itemVenda =>
            {
                var estoque = _estoqueRepository.ObterPorCodigoProduto(itemVenda.ProdutoCodigo);
                if (estoque.Quantidade - itemVenda.Quantidade < 0)
                {
                    produtosQueFaltamNoEstoque.Add(new Tuple<string, int, int>(
                        itemVenda.ProdutoNome, 
                        itemVenda.Quantidade,
                        estoque.Quantidade));
                }
            });
            AssertionConcern<BusinessException>
                .AssertArgumentFalse(produtosQueFaltamNoEstoque.Any(),
                MensagemDeEstoqueInsuficiente(produtosQueFaltamNoEstoque));
        }

        private static string MensagemDeEstoqueInsuficiente(List<Tuple<string, int, int>> produtosQueFaltamNoEstoque)
        {
            var mensagen = Erros.ProductsThatAreMissingInStock + "</br>";
            produtosQueFaltamNoEstoque.ForEach(x =>
            {
                mensagen += string.Format(Erros.ProductInsufficient, x.Item1, x.Item2, x.Item1) + "</br>";
            });
            return mensagen;
        }

        public void Dispose()
        {
            _vendaRepository.Dispose();
            _itemVendaRepository.Dispose();
            _pessoaRepository.Dispose();
            _formaPagamentoRepository.Dispose();
            _condicaoPagamentoRepository.Dispose();
            _usuarioRepository.Dispose();
            _contaReceberRepository.Dispose();
            _estoqueRepository.Dispose();
            _produtoRepository.Dispose();
        }
    }
}
