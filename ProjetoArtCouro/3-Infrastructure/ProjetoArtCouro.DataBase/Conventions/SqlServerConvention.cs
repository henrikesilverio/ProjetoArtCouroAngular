using System;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ProjetoArtCouro.DataBase.Conventions
{
    public class SqlServerConvention : Convention
    {
        public SqlServerConvention()
        {
            Properties<string>().Configure(p => p.HasColumnType("varchar"));
            Properties<long>().Configure(p => p.HasColumnType("bigint"));
            Properties<int>().Configure(p => p.HasColumnType("int"));
            Properties<DateTime>().Configure(p => p.HasColumnType("datetime2"));
            Properties<decimal>().Configure(p => p.HasColumnType("decimal"));
            Properties<bool>().Configure(p => p.HasColumnType("bit"));
            Properties<byte[]>().Configure(p => p.HasColumnType("image"));
            Properties().Where(p => p.PropertyType.IsEnum).Configure(p => p.HasColumnType("int"));
        }
    }
}
