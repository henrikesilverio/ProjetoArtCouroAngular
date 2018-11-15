using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ProjetoArtCouro.DataBase.Conventions;
using ProjetoArtCouro.DataBase.EntityConfig.CompraConfiguration;
using ProjetoArtCouro.DataBase.EntityConfig.EstoqueConfiguration;
using ProjetoArtCouro.DataBase.EntityConfig.PagamentoConfiguration;
using ProjetoArtCouro.DataBase.EntityConfig.PessoaConfiguration;
using ProjetoArtCouro.DataBase.EntityConfig.ProdutoConfiguration;
using ProjetoArtCouro.DataBase.EntityConfig.UsuarioConfiguration;
using ProjetoArtCouro.DataBase.EntityConfig.VendaConfiguration;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Entities.Estoques;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Entities.Produtos;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Entities.Vendas;

namespace ProjetoArtCouro.DataBase.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("ProjetoArtCouroConnectionString")
        {
            Database.Initialize(true);
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DataBaseContext(DbConnection dbConnection, DbModelBuilder modelBuilder)
            : base(dbConnection, modelBuilder.Build(dbConnection).Compile(), true)
        {
            Database.Initialize(true);
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<CondicaoPagamento> CondicoesPagamento { get; set; }
        public virtual DbSet<ContaPagar> ContasPagar { get; set; }
        public virtual DbSet<ContaReceber> ContasReceber { get; set; }
        public virtual DbSet<Compra> Compras { get; set; }
        public virtual DbSet<Endereco> Enderecos { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<Estoque> Estoques { get; set; }
        public virtual DbSet<EstadoCivil> EstadosCivis { get; set; }
        public virtual DbSet<FormaPagamento> FormasPagamento { get; set; }
        public virtual DbSet<GrupoPermissao> GruposPermissao { get; set; }
        public virtual DbSet<ItemCompra> ItensCompra { get; set; }
        public virtual DbSet<ItemVenda> ItensVenda { get; set; }
        public virtual DbSet<MeioComunicacao> MeiosComunicacao { get; set; }
        public virtual DbSet<Papel> Papeis { get; set; }
        public virtual DbSet<Permissao> Permissoes { get; set; }
        public virtual DbSet<Pessoa> Pessoas { get; set; }
        public virtual DbSet<PessoaFisica> PessoasFisicas { get; set; }
        public virtual DbSet<PessoaJuridica> PessoasJuridicas { get; set; }
        public virtual DbSet<Produto> Produtos { get; set; }
        public virtual DbSet<Unidade> Unidades { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Venda> Vendas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new SqlServerConvention());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

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
        }
    }
}