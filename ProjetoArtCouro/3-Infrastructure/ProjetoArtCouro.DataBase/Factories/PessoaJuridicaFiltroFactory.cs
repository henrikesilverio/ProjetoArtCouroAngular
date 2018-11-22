using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Decorators.PessoasJuridicas;

namespace ProjetoArtCouro.DataBase.Factories
{
    public static class PessoaJuridicaFiltroFactory
    {
        public static IPessoaJuridicaFiltro Fabricar(
            DataBaseContext context)
        {
            IPessoaJuridicaFiltro filtro = new PessoaJuridicaFiltro(context);

            filtro = new PessoaJuridicaFiltroPorCodigo(filtro);
            filtro = new PessoaJuridicaFiltroPorTipoPapelPessoa(filtro);
            filtro = new PessoaJuridicaFiltroPorNome(filtro);
            filtro = new PessoaJuridicaFiltroPorCNPJ(filtro);
            filtro = new PessoaJuridicaFiltroPorEmail(filtro);

            return filtro;
        }
    }
}
