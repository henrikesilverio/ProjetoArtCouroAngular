using System.Linq;
using ProjetoArtCouro.Domain.Entities.Estoques;
using ProjetoArtCouro.Domain.Models.Estoque;

namespace ProjetoArtCouro.DataBase.Decorators.Estoques
{
    public class EstoqueFiltroPorNomeFornecedor : IEstoqueFiltro
    {
        private readonly IEstoqueFiltro _estoqueFiltro;

        public EstoqueFiltroPorNomeFornecedor(IEstoqueFiltro estoqueFiltro)
        {
            _estoqueFiltro = estoqueFiltro;
        }

        public IQueryable<Estoque> Filtrar(PesquisaEstoque filtro)
        {
            if (string.IsNullOrEmpty(filtro.NomeFornecedor))
            {
                return _estoqueFiltro.Filtrar(filtro);
            }

            return _estoqueFiltro
                .Filtrar(filtro)
                .Where(x => x.Compra.Fornecedor.Nome == filtro.NomeFornecedor);
        }
    }
}
