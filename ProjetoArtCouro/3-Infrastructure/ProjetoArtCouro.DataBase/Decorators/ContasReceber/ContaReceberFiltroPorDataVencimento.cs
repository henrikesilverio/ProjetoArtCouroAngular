using System;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.ContaReceber;

namespace ProjetoArtCouro.DataBase.Decorators.ContasReceber
{
    public class ContaReceberFiltroPorDataVencimento : IContaReceberFiltro
    {
        private readonly IContaReceberFiltro _contaReceberFiltro;

        public ContaReceberFiltroPorDataVencimento(IContaReceberFiltro contaReceberFiltro)
        {
            _contaReceberFiltro = contaReceberFiltro;
        }

        public IQueryable<ContaReceber> Filtrar(PesquisaContaReceber filtro)
        {
            if (filtro.DataVencimento == new DateTime())
            {
                return _contaReceberFiltro.Filtrar(filtro);
            }

            return _contaReceberFiltro
                .Filtrar(filtro)
                .Where(x => DbFunctions.TruncateTime(x.DataVencimento) == filtro.DataVencimento.Date);
        }
    }
}
