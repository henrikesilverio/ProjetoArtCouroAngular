using ProjetoArtCouro.Domain.Models.Enums;
using System;

namespace ProjetoArtCouro.Domain.Models.Compra
{
    public class PesquisaCompra
    {
        public int CodigoCompra { get; set; }
        public int CodigoFornecedor { get; set; }
        public int CodigoUsuario { get; set; }
        public DateTime DataCadastro { get; set; }
        public string NomeFornecedor { get; set; }
        public string CPFCNPJ { get; set; }
        public StatusCompraEnum StatusCompra { get; set; }
    }
}
