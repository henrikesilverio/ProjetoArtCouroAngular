using System.Linq;
using ProjetoArtCouro.Domain.Entities.Estoques;
using ProjetoArtCouro.Domain.Models.Estoque;

namespace ProjetoArtCouro.DataBase.Decorators.Estoques
{
    public class EstoqueFiltroPorCodigoFornecedor : IEstoqueFiltro
    {
        private readonly IEstoqueFiltro _estoqueFiltro;

        public EstoqueFiltroPorCodigoFornecedor(IEstoqueFiltro estoqueFiltro)
        {
            _estoqueFiltro = estoqueFiltro;
        }

        public IQueryable<Estoque> Filtrar(PesquisaEstoque filtro)
        {
            if (filtro.CodigoFornecedor == 0)
            {
                return _estoqueFiltro.Filtrar(filtro);
            }

            return _estoqueFiltro
                .Filtrar(filtro)
                .Where(x => x.Compra.Fornecedor.PessoaCodigo == filtro.CodigoFornecedor);
        }
    }
}
