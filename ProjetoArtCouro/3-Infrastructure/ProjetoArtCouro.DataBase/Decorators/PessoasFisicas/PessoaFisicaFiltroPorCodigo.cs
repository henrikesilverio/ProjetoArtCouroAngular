using System.Linq;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Pessoa;

namespace ProjetoArtCouro.DataBase.Decorators.PessoasFisicas
{
    public class PessoaFisicaFiltroPorCodigo : IPessoaFisicaFiltro
    {
        private readonly IPessoaFisicaFiltro _pessoaFisicaFiltro;

        public PessoaFisicaFiltroPorCodigo(IPessoaFisicaFiltro pessoaFisicaFiltro)
        {
            _pessoaFisicaFiltro = pessoaFisicaFiltro;
        }

        public IQueryable<PessoaFisica> Filtrar(PesquisaPessoaFisica filtro)
        {
            if (filtro.Codigo == 0)
            {
                return _pessoaFisicaFiltro.Filtrar(filtro);
            }
            return _pessoaFisicaFiltro
                    .Filtrar(filtro)
                    .Where(x => x.Pessoa.PessoaCodigo == filtro.Codigo);
        }
    }
}
