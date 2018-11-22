using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Pessoa;
using System.Linq;

namespace ProjetoArtCouro.DataBase.Decorators.PessoasFisicas
{
    public class PessoaFisicaFiltroPorNome : IPessoaFisicaFiltro
    {
        private readonly IPessoaFisicaFiltro _pessoaFisicaFiltro;

        public PessoaFisicaFiltroPorNome(IPessoaFisicaFiltro pessoaFisicaFiltro)
        {
            _pessoaFisicaFiltro = pessoaFisicaFiltro;
        }

        public IQueryable<PessoaFisica> Filtrar(PesquisaPessoaFisica filtro)
        {
            if (string.IsNullOrEmpty(filtro.Nome))
            {
                return _pessoaFisicaFiltro.Filtrar(filtro);
            }
            return _pessoaFisicaFiltro
                    .Filtrar(filtro)
                    .Where(x => x.Pessoa.Nome == filtro.Nome);
        }
    }
}
