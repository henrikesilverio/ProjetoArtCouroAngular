using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Decorators.ContasPagar;

namespace ProjetoArtCouro.DataBase.Factories
{
    public static class ContaPagarFiltroFactory
    {
        public static IContaPagarFiltro Fabricar(
            DataBaseContext context)
        {
            IContaPagarFiltro filtro = new ContaPagarFiltro(context);

            filtro = new ContaPagarFiltroPorCodigoFornecedor(filtro);
            filtro = new ContaPagarFiltroPorDataEmissao(filtro);
            filtro = new ContaPagarFiltroPorDataVencimento(filtro);
            filtro = new ContaPagarFiltroPorDocumentoFornecedor(filtro);
            filtro = new ContaPagarFiltroPorNomeFornecedor(filtro);
            filtro = new ContaPagarFiltroPorStatusContaPagar(filtro);
            filtro = new ContaPagarFiltroPorUsuarioCodigo(filtro);
            filtro = new ContaPagarFiltroPorCompraCodigo(filtro);

            return filtro;
        }
    }
}
