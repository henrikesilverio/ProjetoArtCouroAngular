using System;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.Compra;

namespace ProjetoArtCouro.DataBase.Decorators.Compras
{
    public class CompraFiltroPorDataCadastro : ICompraFiltro
    {
        private readonly ICompraFiltro _compraFiltro;

        public CompraFiltroPorDataCadastro(ICompraFiltro compraFiltro)
        {
            _compraFiltro = compraFiltro;
        }

        public IQueryable<Compra> Filtrar(PesquisaCompra filtro)
        {
            if (filtro.DataCadastro == new DateTime())
            {
                return _compraFiltro.Filtrar(filtro);
            }

            return _compraFiltro
                .Filtrar(filtro)
                .Where(x => DbFunctions.TruncateTime(x.DataCadastro) == filtro.DataCadastro.Date);
        }
    }
}
