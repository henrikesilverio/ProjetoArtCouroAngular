﻿using System.ComponentModel.DataAnnotations;
using ProjetoArtCouro.Resources.Resources;

namespace ProjetoArtCouro.Domain.Models.ContaReceber
{
    public class PesquisaContaReceberModel
    {
        [Display(Name = "SaleCode", ResourceType = typeof(Mensagens))]
        public int? CodigoVenda { get; set; }

        [Display(Name = "ClientCode", ResourceType = typeof(Mensagens))]
        public int? CodigoCliente { get; set; }

        [Display(Name = "IssuanceDate", ResourceType = typeof(Mensagens))]
        public string DataEmissao { get; set; }

        [Display(Name = "DueDate", ResourceType = typeof(Mensagens))]
        public string DataVencimento { get; set; }

        [Display(Name = "ClientName", ResourceType = typeof(Mensagens))]
        public string NomeCliente { get; set; }

        [Display(Name = "CPFCNPJ", ResourceType = typeof(Mensagens))]
        public string CPFCNPJ { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Mensagens))]
        public int? StatusId { get; set; }
    }
}
