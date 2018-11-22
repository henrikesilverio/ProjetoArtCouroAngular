using System.Linq;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Pessoa;

namespace ProjetoArtCouro.DataBase.Decorators.PessoasJuridicas
{
    public class PessoaJuridicaFiltroPorCodigo : IPessoaJuridicaFiltro
    {
        private readonly IPessoaJuridicaFiltro _pessoaJuridicaFiltro;

        public PessoaJuridicaFiltroPorCodigo(IPessoaJuridicaFiltro pessoaJuridicaFiltro)
        {
            _pessoaJuridicaFiltro = pessoaJuridicaFiltro;
        }

        public IQueryable<PessoaJuridica> Filtrar(PesquisaPessoaJuridica filtro)
        {
            if (filtro.Codigo == 0)
            {
                return _pessoaJuridicaFiltro.Filtrar(filtro);
            }
            return _pessoaJuridicaFiltro
                    .Filtrar(filtro)
                    .Where(x => x.Pessoa.PessoaCodigo == filtro.Codigo);
        }
    }
}
