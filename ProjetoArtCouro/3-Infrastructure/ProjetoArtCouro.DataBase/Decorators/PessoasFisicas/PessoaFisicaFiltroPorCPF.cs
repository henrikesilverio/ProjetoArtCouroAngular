using System.Linq;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Pessoa;

namespace ProjetoArtCouro.DataBase.Decorators.PessoasFisicas
{
    public class PessoaFisicaFiltroPorCPF : IPessoaFisicaFiltro
    {
        private readonly IPessoaFisicaFiltro _pessoaFisicaFiltro;

        public PessoaFisicaFiltroPorCPF(IPessoaFisicaFiltro pessoaFisicaFiltro)
        {
            _pessoaFisicaFiltro = pessoaFisicaFiltro;
        }

        public IQueryable<PessoaFisica> Filtrar(PesquisaPessoaFisica filtro)
        {
            if (string.IsNullOrEmpty(filtro.CPF))
            {
                return _pessoaFisicaFiltro.Filtrar(filtro);
            }
            return _pessoaFisicaFiltro
                    .Filtrar(filtro)
                    .Where(x => x.CPF == filtro.CPF);
        }
    }
}
