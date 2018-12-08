using System;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.ContaPagar;

namespace ProjetoArtCouro.DataBase.Decorators.ContasPagar
{
    public class ContaPagarFiltroPorDataEmissao : IContaPagarFiltro
    {
        private readonly IContaPagarFiltro _contaPagarFiltro;

        public ContaPagarFiltroPorDataEmissao(IContaPagarFiltro contaPagarFiltro)
        {
            _contaPagarFiltro = contaPagarFiltro;
        }

        public IQueryable<ContaPagar> Filtrar(PesquisaContaPagar filtro)
        {
            if (filtro.DataEmissao == new DateTime())
            {
                return _contaPagarFiltro.Filtrar(filtro);
            }

            return _contaPagarFiltro
                .Filtrar(filtro)
                .Where(x => DbFunctions.TruncateTime(x.Compra.DataCadastro) == filtro.DataEmissao.Date);
        }
    }
}
