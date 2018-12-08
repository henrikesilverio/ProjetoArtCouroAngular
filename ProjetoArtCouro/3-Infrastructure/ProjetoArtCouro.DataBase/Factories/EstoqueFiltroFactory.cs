using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Decorators.Estoques;

namespace ProjetoArtCouro.DataBase.Factories
{
    public static class EstoqueFiltroFactory
    {
        public static IEstoqueFiltro Fabricar(
            DataBaseContext context)
        {
            IEstoqueFiltro filtro = new EstoqueFiltro(context);

            filtro = new EstoqueFiltroPorCodigoProduto(filtro);
            filtro = new EstoqueFiltroPorCodigoFornecedor(filtro);
            filtro = new EstoqueFiltroPorDescricaoProduto(filtro);
            filtro = new EstoqueFiltroPorNomeFornecedor(filtro);
            filtro = new EstoqueFiltroPorQuantidade(filtro);

            return filtro;
        }
    }
}
