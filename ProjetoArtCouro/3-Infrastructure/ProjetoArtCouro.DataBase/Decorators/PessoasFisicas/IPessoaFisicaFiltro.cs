using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Pessoa;
using System.Linq;

namespace ProjetoArtCouro.DataBase.Decorators.PessoasFisicas
{
    public interface IPessoaFisicaFiltro
    {
        IQueryable<PessoaFisica> Filtrar(PesquisaPessoaFisica filtro);
    }
}
