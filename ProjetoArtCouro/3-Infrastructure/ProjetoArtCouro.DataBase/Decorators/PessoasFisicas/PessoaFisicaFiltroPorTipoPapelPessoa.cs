using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Models.Pessoa;
using System.Linq;

namespace ProjetoArtCouro.DataBase.Decorators.PessoasFisicas
{
    public class PessoaFisicaFiltroPorTipoPapelPessoa : IPessoaFisicaFiltro
    {
        private readonly IPessoaFisicaFiltro _pessoaFisicaFiltro;

        public PessoaFisicaFiltroPorTipoPapelPessoa(IPessoaFisicaFiltro pessoaFisicaFiltro)
        {
            _pessoaFisicaFiltro = pessoaFisicaFiltro;
        }

        public IQueryable<PessoaFisica> Filtrar(PesquisaPessoaFisica filtro)
        {
            if (filtro.TipoPapelPessoa == TipoPapelPessoaEnum.Nenhum)
            {
                return _pessoaFisicaFiltro.Filtrar(filtro);
            }
            return _pessoaFisicaFiltro
                    .Filtrar(filtro)
                    .Where(x => x.Pessoa.Papeis.Any(a => a.PapelCodigo == (int)filtro.TipoPapelPessoa));
        }
    }
}
