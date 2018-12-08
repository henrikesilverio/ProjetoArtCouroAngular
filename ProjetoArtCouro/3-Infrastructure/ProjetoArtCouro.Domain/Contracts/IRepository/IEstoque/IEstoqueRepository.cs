using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Estoques;
using ProjetoArtCouro.Domain.Models.Estoque;

namespace ProjetoArtCouro.Domain.Contracts.IRepository.IEstoque
{
    public interface IEstoqueRepository : IDisposable
    {
        Estoque ObterPorCodigoProduto(int codigoProduto);
        List<Estoque> ObterListaPorFiltro(PesquisaEstoque filtro);
        void Criar(Estoque estoque);
        void Atualizar(Estoque estoque);
    }
}
