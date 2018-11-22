using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Decorators.PessoasFisicas;

namespace ProjetoArtCouro.DataBase.Factories
{
    public static class PessoaFisicaFiltroFactory
    {
        public static IPessoaFisicaFiltro Fabricar(
            DataBaseContext context)
        {
            IPessoaFisicaFiltro filtro = new PessoaFisicaFiltro(context);

            filtro = new PessoaFisicaFiltroPorCodigo(filtro);
            filtro = new PessoaFisicaFiltroPorTipoPapelPessoa(filtro);
            filtro = new PessoaFisicaFiltroPorNome(filtro);
            filtro = new PessoaFisicaFiltroPorCPF(filtro);
            filtro = new PessoaFisicaFiltroPorEmail(filtro);

            return filtro;
        }
    }
}
