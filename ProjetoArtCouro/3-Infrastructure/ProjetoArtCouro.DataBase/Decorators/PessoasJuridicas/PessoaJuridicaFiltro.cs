using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Pessoa;

namespace ProjetoArtCouro.DataBase.Decorators.PessoasJuridicas
{
    public class PessoaJuridicaFiltro : IPessoaJuridicaFiltro
    {
        private readonly DataBaseContext _context;

        public PessoaJuridicaFiltro(DataBaseContext context)
        {
            _context = context;
        }

        public IQueryable<PessoaJuridica> Filtrar(PesquisaPessoaJuridica filtro)
        {
            return _context.PessoasJuridicas
                    .Include("Pessoa")
                    .Include("Pessoa.Papeis")
                    .Include("Pessoa.MeiosComunicacao")
                    .Include("Pessoa.Enderecos")
                    .AsNoTracking()
                    .AsQueryable();
        }
    }
}
