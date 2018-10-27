using ProjetoArtCouro.Domain.Models.Enums;
using System;

namespace ProjetoArtCouro.Domain.Models.ContaReceber
{
    public class PesquisaContaReceber
    {
        public int CodigoVenda { get; set; }
        public int CodigoCliente { get; set; }
        public int CodigoUsuario { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public string NomeCliente { get; set; }
        public string CPFCNPJ { get; set; }
        public StatusContaReceberEnum StatusContaReceber { get; set; }
    }
}
