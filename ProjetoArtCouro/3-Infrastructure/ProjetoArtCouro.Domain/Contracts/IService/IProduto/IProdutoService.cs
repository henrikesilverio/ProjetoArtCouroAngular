using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Produtos;
using ProjetoArtCouro.Domain.Models.Common;
using ProjetoArtCouro.Domain.Models.Produto;

namespace ProjetoArtCouro.Domain.Contracts.IService.IProduto
{
    public interface IProdutoService : IDisposable
    {
        List<ProdutoModel> ObterListaProduto();
        List<LookupModel> ObterListaUnidade();
        Produto ObterProdutoPorCodigo(int codigo);
        ProdutoModel CriarProduto(ProdutoModel model);
        ProdutoModel AtualizarProduto(ProdutoModel model);
        void ExcluirProduto(int produtoCodigo);
    }
}
