using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Decorators.ContasReceber;

namespace ProjetoArtCouro.DataBase.Factories
{
    public static class ContaReceberFiltroFactory
    {
        public static IContaReceberFiltro Fabricar(
            DataBaseContext context)
        {
            IContaReceberFiltro filtro = new ContaReceberFiltro(context);

            filtro = new ContaReceberFiltroPorClienteCodigo(filtro);
            filtro = new ContaReceberFiltroPorDataEmissao(filtro);
            filtro = new ContaReceberFiltroPorDataVencimento(filtro);
            filtro = new ContaReceberFiltroPorDocumentoCliente(filtro);
            filtro = new ContaReceberFiltroPorNomeCliente(filtro);
            filtro = new ContaReceberFiltroPorStatusContaReceber(filtro);
            filtro = new ContaReceberFiltroPorUsuarioCodigo(filtro);
            filtro = new ContaReceberFiltroPorVendaCodigo(filtro);

            return filtro;
        }
    }
}
