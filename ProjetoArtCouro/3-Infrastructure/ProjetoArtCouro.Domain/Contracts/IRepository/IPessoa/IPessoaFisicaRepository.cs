using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Pessoa;

namespace ProjetoArtCouro.Domain.Contracts.IRepository.IPessoa
{
    public interface IPessoaFisicaRepository: IDisposable
    {
        PessoaFisica ObterPorId(Guid id);
        PessoaFisica ObterPorCPF(string cpf);
        List<PessoaFisica> ObterLista();
        List<PessoaFisica> ObterListaPorFiltro(PesquisaPessoaFisica filtro); 
        void Criar(PessoaFisica pessoaFisica);
        void Atualizar(PessoaFisica pessoaFisica);
        void Deletar(PessoaFisica pessoaFisica);
    }
}
