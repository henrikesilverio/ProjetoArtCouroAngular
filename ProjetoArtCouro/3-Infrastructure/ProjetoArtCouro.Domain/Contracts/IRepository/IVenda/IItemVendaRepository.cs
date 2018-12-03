using System;
using ProjetoArtCouro.Domain.Entities.Vendas;

namespace ProjetoArtCouro.Domain.Contracts.IRepository.IVenda
{
    public interface IItemVendaRepository : IDisposable
    {
        ItemVenda Criar(ItemVenda itemVenda);
        void Deletar(ItemVenda itemVenda);
    }
}
