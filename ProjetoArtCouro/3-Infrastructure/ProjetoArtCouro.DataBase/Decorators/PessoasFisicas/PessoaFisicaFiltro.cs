using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Pessoa;

namespace ProjetoArtCouro.DataBase.Decorators.PessoasFisicas
{
    public class PessoaFisicaFiltro : IPessoaFisicaFiltro
    {
        private readonly DataBaseContext _context;

        public PessoaFisicaFiltro(DataBaseContext context)
        {
            _context = context;
        }

        public IQueryable<PessoaFisica> Filtrar(PesquisaPessoaFisica filtro)
        {
            return _context.PessoasFisicas
                    .Include("Pessoa")
                    .Include("Pessoa.Papeis")
                    .Include("Pessoa.MeiosComunicacao")
                    .Include("Pessoa.Enderecos")
                    .AsNoTracking()
                    .AsQueryable();
        }
    }
}
