using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Decorators.Vendas;

namespace ProjetoArtCouro.DataBase.Factories
{
    public static class VendaFiltroFactory
    {
        public static IVendaFiltro Fabricar(
            DataBaseContext context)
        {
            IVendaFiltro filtro = new VendaFiltro(context);

            filtro = new VendaFiltroPorCodigo(filtro);
            filtro = new VendaFiltroPorCodigoCliente(filtro);
            filtro = new VendaFiltroPorCodigoUsuario(filtro);
            filtro = new VendaFiltroPorDataCadastro(filtro);
            filtro = new VendaFiltroPorDocumentoCliente(filtro);
            filtro = new VendaFiltroPorNomeCliente(filtro);
            filtro = new VendaFiltroPorStatus(filtro);

            return filtro;
        }
    }
}
