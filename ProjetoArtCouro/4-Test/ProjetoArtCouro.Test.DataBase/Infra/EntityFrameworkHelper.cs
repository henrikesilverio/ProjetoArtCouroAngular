using ProjetoArtCouro.DataBase.EntityConfig.CompraConfiguration;
using ProjetoArtCouro.DataBase.EntityConfig.EstoqueConfiguration;
using ProjetoArtCouro.DataBase.EntityConfig.PagamentoConfiguration;
using ProjetoArtCouro.DataBase.EntityConfig.PessoaConfiguration;
using ProjetoArtCouro.DataBase.EntityConfig.ProdutoConfiguration;
using ProjetoArtCouro.DataBase.EntityConfig.UsuarioConfiguration;
using ProjetoArtCouro.DataBase.EntityConfig.VendaConfiguration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ProjetoArtCouro.Test.DataBase.Infra
{
    public static class EntityFrameworkHelper
    {
        public static DbModelBuilder GetDbModelBuilder()
        {
            var modelBuilder = new DbModelBuilder();
            //Removendo pluralização dos nomes das tabelas
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Setando as configurações para criação dos objetos
            modelBuilder.Configurations.Add(new CondicaoPagamentoConfiguration());
            modelBuilder.Configurations.Add(new ContaPagarConfiguration());
            modelBuilder.Configurations.Add(new ContaReceberConfiguration());
            modelBuilder.Configurations.Add(new CompraConfiguration());
            modelBuilder.Configurations.Add(new EnderecoConfiguration());
            modelBuilder.Configurations.Add(new EstadoConfiguration());
            modelBuilder.Configurations.Add(new EstadoCivilConfiguration());
            modelBuilder.Configurations.Add(new EstoqueConfiguration());
            modelBuilder.Configurations.Add(new FormaPagamentoConfiguration());
            modelBuilder.Configurations.Add(new GrupoPermissaoConfiguration());
            modelBuilder.Configurations.Add(new ItemCompraConfiguration());
            modelBuilder.Configurations.Add(new ItemVendaConfiguration());
            modelBuilder.Configurations.Add(new MeioComunicacaoConfiguration());
            modelBuilder.Configurations.Add(new PapelConfiguration());
            modelBuilder.Configurations.Add(new PermissaoConfiguration());
            modelBuilder.Configurations.Add(new PessoaConfiguration());
            modelBuilder.Configurations.Add(new PessoaFisicaConfiguration());
            modelBuilder.Configurations.Add(new PessoaJuridicaConfiguration());
            modelBuilder.Configurations.Add(new ProdutoConfiguration());
            modelBuilder.Configurations.Add(new UnidadeConfiguration());
            modelBuilder.Configurations.Add(new UsuarioConfiguration());
            modelBuilder.Configurations.Add(new VendaConfiguration());

            return modelBuilder;
        }
    }
}
