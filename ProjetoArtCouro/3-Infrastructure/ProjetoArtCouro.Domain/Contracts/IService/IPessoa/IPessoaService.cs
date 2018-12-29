using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Common;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Models.Pessoa;

namespace ProjetoArtCouro.Domain.Contracts.IService.IPessoa
{
    public interface IPessoaService : IDisposable
    {
        List<LookupModel> ObterEstados();
        List<LookupModel> ObterEstadosCivis();
        List<PessoaModel> ObterListaPessoa();
        List<Pessoa> ObterListaPessoaFisicaEJuridicaPorPapel(TipoPapelPessoaEnum papelCodigo);
        List<PessoaFisica> ObterListaPessoaFisicaPorPapel(TipoPapelPessoaEnum papelCodigo);
        List<PessoaJuridica> ObterListaPessoaJuridicaPorPapel(TipoPapelPessoaEnum papelCodigo);
        List<PessoaModel> PesquisarPessoa(PesquisaPessoaModel filtro);
        PessoaModel ObterPessoaPorCodigo(int codigo);
        void CriarPessoa(PessoaModel model);
        void AtualizarPessoa(PessoaModel pessoa);
        void ExcluirPessoa(int pessoaCodigo);
    }
}
