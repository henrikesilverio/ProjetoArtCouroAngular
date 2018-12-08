using System;
using ProjetoArtCouro.Domain.Entities.Compras;

namespace ProjetoArtCouro.Domain.Contracts.IRepository.ICompra
{
    public interface IItemCompraRepository : IDisposable
    {
        ItemCompra Criar(ItemCompra itemCompra);
        void Deletar(ItemCompra itemCompra);
    }
}
