using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.Venda;
using System;
using System.Data.Entity;
using System.Linq;

namespace ProjetoArtCouro.DataBase.Decorators.Vendas
{
    public class VendaFiltroPorDataCadastro : IVendaFiltro
    {
        private readonly IVendaFiltro _vendaFiltro;

        public VendaFiltroPorDataCadastro(IVendaFiltro vendaFiltro)
        {
            _vendaFiltro = vendaFiltro;
        }

        public IQueryable<Venda> Filtrar(PesquisaVenda filtro)
        {
            if (filtro.DataCadastro == new DateTime())
            {
                return _vendaFiltro.Filtrar(filtro);
            }

            return _vendaFiltro
                .Filtrar(filtro)
                .Where(x => DbFunctions.TruncateTime(x.DataCadastro) == filtro.DataCadastro.Date);
        }
    }
}
