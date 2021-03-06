﻿using System;
using System.Linq;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.ContaReceber;
using ProjetoArtCouro.Domain.Models.Enums;

namespace ProjetoArtCouro.DataBase.Decorators.ContasReceber
{
    public class ContaReceberFiltroPorStatusContaReceber : IContaReceberFiltro
    {
        private readonly IContaReceberFiltro _contaReceberFiltro;

        public ContaReceberFiltroPorStatusContaReceber(IContaReceberFiltro contaReceberFiltro)
        {
            _contaReceberFiltro = contaReceberFiltro;
        }

        public IQueryable<ContaReceber> Filtrar(PesquisaContaReceber filtro)
        {
            if (filtro.StatusContaReceber == StatusContaReceberEnum.None)
            {
                return _contaReceberFiltro.Filtrar(filtro);
            }

            return _contaReceberFiltro
                .Filtrar(filtro)
                .Where(x => x.StatusContaReceber == filtro.StatusContaReceber);
        }
    }
}
