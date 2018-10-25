using ProjetoArtCouro.Domain.Models.Enums;
using System;

namespace ProjetoArtCouro.Domain.Models.Venda
{
    public class PesquisaVenda
    {
        public int CodigoVenda { get; set; }
        public int CodigoCliente { get; set; }
        public int CodigoUsuario { get; set; }
        public DateTime DataCadastro { get; set; }
        public string NomeCliente { get; set; }
        public string CPFCNPJ { get; set; }
        public StatusVendaEnum StatusVenda { get; set; }
    }
}
