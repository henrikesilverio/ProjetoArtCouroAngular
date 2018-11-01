using System.Collections.Generic;
using ProjetoArtCouro.Domain.Contracts.IRepository.IProduto;
using ProjetoArtCouro.Domain.Contracts.IService.IProduto;
using ProjetoArtCouro.Domain.Entities.Produtos;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resource.Validation;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.Produto;
using ProjetoArtCouro.Domain.Models.Common;
using ProjetoArtCouro.Mapping;

namespace ProjetoArtCouro.Business.Services.ProdutoService
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUnidadeRepository _unidadeRepository;

        public ProdutoService(IProdutoRepository produtoRepository, IUnidadeRepository unidadeRepository)
        {
            _produtoRepository = produtoRepository;
            _unidadeRepository = unidadeRepository;
        }

        public List<ProdutoModel> ObterListaProduto()
        {
            var produtos = _produtoRepository.ObterListaComUnidade();
            return Map<List<ProdutoModel>>.MapperTo(produtos);
        }

        public List<LookupModel> ObterListaUnidade()
        {
            var unidades = _unidadeRepository.ObterLista();
            return Map<List<LookupModel>>.MapperTo(unidades);
        }

        public Produto ObterProdutoPorCodigo(int codigo)
        {
            return _produtoRepository.ObterPorCodigo(codigo);
        }

        public ProdutoModel CriarProduto(ProdutoModel model)
        {
            var produto = Map<Produto>.MapperTo(model);
            produto.Validar();

            var unidade = _unidadeRepository.ObterPorCodigo(produto.Unidade.UnidadeCodigo);
            AssertionConcern<BusinessException>.AssertArgumentNotEquals(unidade, null, Erros.UnitDoesNotExist);

            produto.Unidade = unidade;
            produto.Unidade.Validar();
            var produtoIncluido = _produtoRepository.Criar(produto);

            return Map<ProdutoModel>.MapperTo(produtoIncluido);
        }

        public ProdutoModel AtualizarProduto(ProdutoModel model)
        {
            var produto = Map<Produto>.MapperTo(model);
            produto.Validar();
            AssertionConcern<BusinessException>
                .AssertArgumentNotEquals(0, produto.ProdutoCodigo, string.Format(Erros.NotZeroParameter, "ProdutoCodigo"));
            AssertionConcern<BusinessException>
                .AssertArgumentNotEquals(0, produto.Unidade.UnidadeCodigo, string.Format(Erros.NotZeroParameter, "UnidadeCodigo"));

            var unidade = _unidadeRepository.ObterPorCodigo(produto.Unidade.UnidadeCodigo);
            AssertionConcern<BusinessException>.AssertArgumentNotEquals(unidade, null, Erros.UnitDoesNotExist);

            var produtoAtual = _produtoRepository.ObterComUnidadePorCodigo(produto.ProdutoCodigo);
            AssertionConcern<BusinessException>.AssertArgumentNotEquals(produtoAtual, null, Erros.ProductDoesNotExist);

            produtoAtual.PrecoCusto = produto.PrecoCusto;
            produtoAtual.PrecoVenda = produto.PrecoVenda;
            produtoAtual.ProdutoNome = produto.ProdutoNome;
            produtoAtual.Unidade = unidade;
            var produtoAtualizado = _produtoRepository.Atualizar(produtoAtual);
            return Map<ProdutoModel>.MapperTo(produtoAtualizado);
        }

        public void ExcluirProduto(int produtoCodigo)
        {
            var produtoAtual = _produtoRepository.ObterPorCodigo(produtoCodigo);
            AssertionConcern<BusinessException>
                .AssertArgumentNotEquals(produtoAtual, null, Erros.ProductDoesNotExist);

            _produtoRepository.Deletar(produtoAtual);
        }

        public void Dispose()
        {
            _produtoRepository.Dispose();
            _unidadeRepository.Dispose();
        }
    }
}
