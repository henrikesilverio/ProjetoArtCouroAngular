using System.Linq;
using ProjetoArtCouro.Domain.Entities.Estoques;
using ProjetoArtCouro.Domain.Models.Estoque;

namespace ProjetoArtCouro.DataBase.Decorators.Estoques
{
    class EstoqueFiltroPorDescricaoProduto : IEstoqueFiltro
    {
        private readonly IEstoqueFiltro _estoqueFiltro;

        public EstoqueFiltroPorDescricaoProduto(IEstoqueFiltro estoqueFiltro)
        {
            _estoqueFiltro = estoqueFiltro;
        }

        public IQueryable<Estoque> Filtrar(PesquisaEstoque filtro)
        {
            if (string.IsNullOrEmpty(filtro.DescricaoProduto))
            {
                return _estoqueFiltro.Filtrar(filtro);
            }

            return _estoqueFiltro
                .Filtrar(filtro)
                .Where(x => x.Produto.ProdutoNome == filtro.DescricaoProduto);
        }
    }
}
