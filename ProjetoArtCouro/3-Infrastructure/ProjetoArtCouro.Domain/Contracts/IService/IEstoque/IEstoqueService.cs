using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Models.Estoque;

namespace ProjetoArtCouro.Domain.Contracts.IService.IEstoque
{
    public interface IEstoqueService : IDisposable
    {
        List<EstoqueModel> PesquisarEstoque(PesquisaEstoqueModel model);
    }
}
