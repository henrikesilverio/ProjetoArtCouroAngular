using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Pessoa;

namespace ProjetoArtCouro.Domain.Contracts.IRepository.IPessoa
{
    public interface IPessoaJuridicaRepository : IDisposable
    {
        PessoaJuridica ObterPorId(Guid id);
        PessoaJuridica ObterPorCNPJ(string cnpj);
        List<PessoaJuridica> ObterLista();
        List<PessoaJuridica> ObterListaPorFiltro(PesquisaPessoaJuridica filtro);
        void Criar(PessoaJuridica pessoaJuridica);
        void Atualizar(PessoaJuridica pessoaJuridica);
        void Deletar(PessoaJuridica pessoaJuridica);
    }
}
