using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Decorators.Compras;

namespace ProjetoArtCouro.DataBase.Factories
{
    public static class CompraFiltroFactory
    {
        public static ICompraFiltro Fabricar(
            DataBaseContext context)
        {
            ICompraFiltro filtro = new CompraFiltro(context);

            filtro = new CompraFiltroPorCodigo(filtro);
            filtro = new CompraFiltroPorCodigoFornecedor(filtro);
            filtro = new CompraFiltroPorDataCadastro(filtro);
            filtro = new CompraFiltroPorStatusCompra(filtro);
            filtro = new CompraFiltroPorNomeFornecedor(filtro);
            filtro = new CompraFiltroPorDocumentoFornecedor(filtro);
            filtro = new CompraFiltroPorCodigoUsuario(filtro);

            return filtro;
        }
    }
}
