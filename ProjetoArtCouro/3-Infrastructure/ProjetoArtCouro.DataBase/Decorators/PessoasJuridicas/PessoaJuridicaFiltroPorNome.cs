using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Pessoa;
using System.Linq;

namespace ProjetoArtCouro.DataBase.Decorators.PessoasJuridicas
{
    public class PessoaJuridicaFiltroPorNome : IPessoaJuridicaFiltro
    {
        private readonly IPessoaJuridicaFiltro _pessoaJuridicaFiltro;

        public PessoaJuridicaFiltroPorNome(IPessoaJuridicaFiltro pessoaJuridicaFiltro)
        {
            _pessoaJuridicaFiltro = pessoaJuridicaFiltro;
        }

        public IQueryable<PessoaJuridica> Filtrar(PesquisaPessoaJuridica filtro)
        {
            if (string.IsNullOrEmpty(filtro.Nome))
            {
                return _pessoaJuridicaFiltro.Filtrar(filtro);
            }
            return _pessoaJuridicaFiltro
                    .Filtrar(filtro)
                    .Where(x => x.Pessoa.Nome == filtro.Nome);
        }
    }
}
