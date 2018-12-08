using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.ContaPagar;

namespace ProjetoArtCouro.Domain.Contracts.IRepository.ICompra
{
    public interface IContaPagarRepository : IDisposable
    {
        ContaPagar ObterPorCodigoComCompra(int codigo);
        List<ContaPagar> ObterListaPorCompraCodigo(int compraCodigo);
        List<ContaPagar> ObterListaPorFiltro(PesquisaContaPagar filtro);
        void Criar(ContaPagar contaPagar);
        void Atualizar(ContaPagar contaPagar);
        void Deletar(ContaPagar contaPagar);
    }
}
