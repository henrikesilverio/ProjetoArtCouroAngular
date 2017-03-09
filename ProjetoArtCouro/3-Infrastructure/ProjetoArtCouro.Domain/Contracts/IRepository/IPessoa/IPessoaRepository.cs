using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Common;
using ProjetoArtCouro.Domain.Models.Enums;

namespace ProjetoArtCouro.Domain.Contracts.IRepository.IPessoa
{
    public interface IPessoaRepository : IDisposable 
    {
        Pessoa ObterPorId(Guid id);
        Pessoa ObterPorCodigo(int codigo);
        Pessoa ObterPorCodigoComPessoaCompleta(int codigo);
        Pessoa ObterPorCPFComPessoaCompleta(string cpf);
        Pessoa ObterPorCNPJComPessoaCompleta(string cnpj);
        List<Pessoa> ObterListaComPessoaFisicaEJuridica();
        List<Pessoa> ObterListaComPessoaFisicaEJuridicaPorPapel(TipoPapelPessoaEnum papelCodigo);
        void Criar(Pessoa pessoa);
        void Atualizar(Pessoa pessoa);
        void Deletar(Pessoa pessoa);
        List<PessoaModel> TesteProjecao();
    }
}
