using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Models.Pessoa;
using System.Linq;

namespace ProjetoArtCouro.DataBase.Decorators.PessoasJuridicas
{
    public class PessoaJuridicaFiltroPorTipoPapelPessoa : IPessoaJuridicaFiltro
    {
        private readonly IPessoaJuridicaFiltro _pessoaJuridicaFiltro;

        public PessoaJuridicaFiltroPorTipoPapelPessoa(IPessoaJuridicaFiltro pessoaJuridicaFiltro)
        {
            _pessoaJuridicaFiltro = pessoaJuridicaFiltro;
        }

        public IQueryable<PessoaJuridica> Filtrar(PesquisaPessoaJuridica filtro)
        {
            if (filtro.TipoPapelPessoa == TipoPapelPessoaEnum.Nenhum)
            {
                return _pessoaJuridicaFiltro.Filtrar(filtro);
            }
            return _pessoaJuridicaFiltro
                    .Filtrar(filtro)
                    .Where(x => x.Pessoa.Papeis.Any(a => a.PapelCodigo == (int)filtro.TipoPapelPessoa));
        }
    }
}
