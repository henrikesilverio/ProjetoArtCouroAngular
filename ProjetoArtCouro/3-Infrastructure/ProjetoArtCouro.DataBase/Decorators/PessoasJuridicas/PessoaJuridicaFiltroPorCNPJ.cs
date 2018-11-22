using System.Linq;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Pessoa;

namespace ProjetoArtCouro.DataBase.Decorators.PessoasJuridicas
{
    public class PessoaJuridicaFiltroPorCNPJ : IPessoaJuridicaFiltro
    {
        private readonly IPessoaJuridicaFiltro _pessoaJuridicaFiltro;

        public PessoaJuridicaFiltroPorCNPJ(IPessoaJuridicaFiltro pessoaJuridicaFiltro)
        {
            _pessoaJuridicaFiltro = pessoaJuridicaFiltro;
        }

        public IQueryable<PessoaJuridica> Filtrar(PesquisaPessoaJuridica filtro)
        {
            if (string.IsNullOrEmpty(filtro.CNPJ))
            {
                return _pessoaJuridicaFiltro.Filtrar(filtro);
            }
            return _pessoaJuridicaFiltro
                    .Filtrar(filtro)
                    .Where(x => x.CNPJ == filtro.CNPJ);
        }
    }
}
