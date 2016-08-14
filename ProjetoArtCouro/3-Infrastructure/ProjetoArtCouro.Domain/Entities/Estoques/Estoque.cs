using System;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Entities.Produtos;

namespace ProjetoArtCouro.Domain.Entities.Estoques
{
    public class Estoque
    {
        public Guid EstoqueId { get; set; }
        public int EstoqueCodigo { get; set; }
        public DateTime DataUltimaEntrada { get; set; }
        public int Quantidade { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual Compra Compra { get; set; }
    }
}
