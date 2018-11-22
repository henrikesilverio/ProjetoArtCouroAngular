using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Pessoa;
using System.Linq;

namespace ProjetoArtCouro.DataBase.Decorators.PessoasJuridicas
{
    public interface IPessoaJuridicaFiltro
    {
        IQueryable<PessoaJuridica> Filtrar(PesquisaPessoaJuridica filtro);
    }
}
