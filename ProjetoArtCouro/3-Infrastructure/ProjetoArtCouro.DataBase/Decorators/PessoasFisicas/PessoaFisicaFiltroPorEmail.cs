using System.Linq;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Models.Pessoa;

namespace ProjetoArtCouro.DataBase.Decorators.PessoasFisicas
{
    public class PessoaFisicaFiltroPorEmail : IPessoaFisicaFiltro
    {
        private readonly IPessoaFisicaFiltro _pessoaFisicaFiltro;

        public PessoaFisicaFiltroPorEmail(IPessoaFisicaFiltro pessoaFisicaFiltro)
        {
            _pessoaFisicaFiltro = pessoaFisicaFiltro;
        }

        public IQueryable<PessoaFisica> Filtrar(PesquisaPessoaFisica filtro)
        {
            if (string.IsNullOrEmpty(filtro.Email))
            {
                return _pessoaFisicaFiltro.Filtrar(filtro);
            }
            return _pessoaFisicaFiltro
                    .Filtrar(filtro)
                    .Where(x => x.Pessoa.MeiosComunicacao
                    .Any(a => a.TipoComunicacao == TipoComunicacaoEnum.Email && a.MeioComunicacaoNome == filtro.Email));
        }
    }
}
