using ProjetoArtCouro.Domain.Models.Enums;
using System;

namespace ProjetoArtCouro.Domain.Models.ContaPagar
{
    public class PesquisaContaPagar
    {
        public int CodigoCompra { get; set; }
        public int CodigoFornecedor { get; set; }
        public int CodigoUsuario { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public string NomeFornecedor { get; set; }
        public string CPFCNPJ { get; set; }
        public StatusContaPagarEnum StatusContaPagar { get; set; }
    }
}
