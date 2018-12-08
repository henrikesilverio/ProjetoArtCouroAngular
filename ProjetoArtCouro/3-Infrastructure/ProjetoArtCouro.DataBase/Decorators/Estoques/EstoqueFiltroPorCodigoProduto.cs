using System.Linq;
using ProjetoArtCouro.Domain.Entities.Estoques;
using ProjetoArtCouro.Domain.Models.Estoque;

namespace ProjetoArtCouro.DataBase.Decorators.Estoques
{
    public class EstoqueFiltroPorCodigoProduto : IEstoqueFiltro
    {
        private readonly IEstoqueFiltro _estoqueFiltro;

        public EstoqueFiltroPorCodigoProduto(IEstoqueFiltro estoqueFiltro)
        {
            _estoqueFiltro = estoqueFiltro;
        }

        public IQueryable<Estoque> Filtrar(PesquisaEstoque filtro)
        {
            if (filtro.CodigoProduto == 0)
            {
                return _estoqueFiltro.Filtrar(filtro);
            }

            return _estoqueFiltro
                .Filtrar(filtro)
                .Where(x => x.Produto.ProdutoCodigo == filtro.CodigoProduto);
        }
    }
}
