using System.Linq;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Models.Pessoa;

namespace ProjetoArtCouro.DataBase.Decorators.PessoasJuridicas
{
    public class PessoaJuridicaFiltroPorEmail : IPessoaJuridicaFiltro
    {
        private readonly IPessoaJuridicaFiltro _pessoaJuridicaFiltro;

        public PessoaJuridicaFiltroPorEmail(IPessoaJuridicaFiltro pessoaJuridicaFiltro)
        {
            _pessoaJuridicaFiltro = pessoaJuridicaFiltro;
        }

        public IQueryable<PessoaJuridica> Filtrar(PesquisaPessoaJuridica filtro)
        {
            if (string.IsNullOrEmpty(filtro.Email))
            {
                return _pessoaJuridicaFiltro.Filtrar(filtro);
            }
            return _pessoaJuridicaFiltro
                    .Filtrar(filtro)
                    .Where(x => x.Pessoa.MeiosComunicacao
                    .Any(a => a.TipoComunicacao == TipoComunicacaoEnum.Email && a.MeioComunicacaoNome == filtro.Email));
        }
    }
}
