using System;
using System.Collections.Generic;
using AutoMapper;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Entities.Produtos;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.Cliente;
using ProjetoArtCouro.Domain.Models.Common;
using ProjetoArtCouro.Domain.Models.Compra;
using ProjetoArtCouro.Domain.Models.CondicaoPagamento;
using ProjetoArtCouro.Domain.Models.ContaPagar;
using ProjetoArtCouro.Domain.Models.ContaReceber;
using ProjetoArtCouro.Domain.Models.FormaPagamento;
using ProjetoArtCouro.Domain.Models.Fornecedor;
using ProjetoArtCouro.Domain.Models.Funcionario;
using ProjetoArtCouro.Domain.Models.Produto;
using ProjetoArtCouro.Domain.Models.Usuario;
using ProjetoArtCouro.Domain.Models.Venda;
using ProjetoArtCouro.Api.AutoMapper.Converters;

namespace ProjetoArtCouro.Api.AutoMapper
{
    public class ModelToDomainMappingProfile : Profile
    {
        //Configuração para auto mapeamento de classes
        public ModelToDomainMappingProfile()
        {
            MapperUser();

            MapperPerson();

            MapperProduct();

            MapperSale();

            MapperBuy();

            MapperAccount();
        }

        private void MapperAccount()
        {
            CreateMap<ContaReceberModel, ContaReceber>()
                .ForMember(d => d.ContaReceberCodigo, m => m.MapFrom(s => s.CodigoContaReceber))
                .ForMember(d => d.DataVencimento, m => m.MapFrom(s => s.DataVencimento))
                .ForMember(d => d.Recebido, m => m.MapFrom(s => s.Recebido))
                .ForMember(d => d.StatusContaReceber,
                    m => m.MapFrom(s => Enum.Parse(typeof(StatusContaReceberEnum), s.Status)))
                .ForMember(d => d.ValorDocumento, m => m.MapFrom(s => s.ValorDocumento));

            CreateMap<ContaPagarModel, ContaPagar>()
                .ForMember(d => d.ContaPagarCodigo, m => m.MapFrom(s => s.CodigoContaPagar))
                .ForMember(d => d.DataVencimento, m => m.MapFrom(s => s.DataVencimento))
                .ForMember(d => d.Pago, m => m.MapFrom(s => s.Pago))
                .ForMember(d => d.StatusContaPagar,
                    m => m.MapFrom(s => Enum.Parse(typeof(StatusContaPagarEnum), s.Status)))
                .ForMember(d => d.ValorDocumento, m => m.MapFrom(s => s.ValorDocumento));
        }

        private void MapperBuy()
        {
            CreateMap<CompraModel, Compra>()
               .ForMember(d => d.Fornecedor, m => m.MapFrom(s => new Pessoa { PessoaCodigo = s.FornecedorId ?? 0 }))
               .ForMember(d => d.CondicaoPagamento,
                   m => m.MapFrom(s => new CondicaoPagamento { CondicaoPagamentoCodigo = s.CondicaoPagamentoId ?? 0 }))
               .ForMember(d => d.FormaPagamento,
                   m => m.MapFrom(s => new FormaPagamento { FormaPagamentoCodigo = s.FormaPagamentoId ?? 0 }))
               .ForMember(d => d.DataCadastro, m => m.MapFrom(s => s.DataCadastro.ToDateTime()))
               .ForMember(d => d.ItensCompra, m => m.MapFrom(s => s.ItemCompraModel))
               .ForMember(d => d.StatusCompra, m => m.MapFrom(s => Enum.Parse(typeof(StatusCompraEnum), s.StatusCompra)))
               .ForMember(d => d.CompraCodigo, m => m.MapFrom(s => s.CodigoCompra ?? 0))
               .ForMember(d => d.ValorTotalBruto, m => m.MapFrom(s => s.ValorTotalBruto.ToDecimal()))
               .ForMember(d => d.ValorTotalFrete, m => m.MapFrom(s => s.ValorTotalFrete.ToDecimal()))
               .ForMember(d => d.ValorTotalLiquido, m => m.MapFrom(s => s.ValorTotalLiquido.ToDecimal()));

            CreateMap<ItemCompraModel, ItemCompra>()
                .ForMember(d => d.ProdutoCodigo, m => m.MapFrom(s => s.Codigo))
                .ForMember(d => d.ProdutoNome, m => m.MapFrom(s => s.Descricao))
                .ForMember(d => d.PrecoVenda, m => m.MapFrom(s => s.PrecoVenda.ToDecimal()))
                .ForMember(d => d.ValorBruto, m => m.MapFrom(s => s.ValorBruto.ToDecimal()))
                .ForMember(d => d.ValorLiquido, m => m.MapFrom(s => s.ValorLiquido.ToDecimal()));
        }

        private void MapperSale()
        {
            CreateMap<VendaModel, Venda>()
                .ForMember(d => d.Cliente, m => m.MapFrom(s => new Pessoa { PessoaCodigo = s.ClienteId ?? 0 }))
                .ForMember(d => d.CondicaoPagamento,
                    m => m.MapFrom(s => new CondicaoPagamento { CondicaoPagamentoCodigo = s.CondicaoPagamentoId ?? 0 }))
                .ForMember(d => d.FormaPagamento,
                    m => m.MapFrom(s => new FormaPagamento { FormaPagamentoCodigo = s.FormaPagamentoId ?? 0 }))
                .ForMember(d => d.DataCadastro, m => m.MapFrom(s => s.DataCadastro.ToDateTime()))
                .ForMember(d => d.ItensVenda, m => m.MapFrom(s => s.ItemVendaModel))
                .ForMember(d => d.StatusVenda, m => m.MapFrom(s => Enum.Parse(typeof(StatusVendaEnum), s.Status)))
                .ForMember(d => d.VendaCodigo, m => m.MapFrom(s => s.CodigoVenda ?? 0))
                .ForMember(d => d.ValorTotalBruto, m => m.MapFrom(s => s.ValorTotalBruto.ToDecimal()))
                .ForMember(d => d.ValorTotalDesconto, m => m.MapFrom(s => s.ValorTotalDesconto.ToDecimal()))
                .ForMember(d => d.ValorTotalLiquido, m => m.MapFrom(s => s.ValorTotalLiquido.ToDecimal()));

            CreateMap<ItemVendaModel, ItemVenda>()
                .ForMember(d => d.ProdutoCodigo, m => m.MapFrom(s => s.Codigo))
                .ForMember(d => d.ProdutoNome, m => m.MapFrom(s => s.Descricao))
                .ForMember(d => d.PrecoVenda, m => m.MapFrom(s => s.PrecoVenda.ToDecimal()))
                .ForMember(d => d.ValorBruto, m => m.MapFrom(s => s.ValorBruto.ToDecimal()))
                .ForMember(d => d.ValorDesconto, m => m.MapFrom(s => s.ValorDesconto.ToDecimal()))
                .ForMember(d => d.ValorLiquido, m => m.MapFrom(s => s.ValorLiquido.ToDecimal()));
        }

        private void MapperProduct()
        {
            CreateMap<ProdutoModel, Produto>()
                .ForMember(d => d.ProdutoId, m => m.Ignore())
                .ForMember(d => d.ProdutoNome, m => m.MapFrom(s => s.Descricao))
                .ForMember(d => d.PrecoCusto, m => m.MapFrom(s => decimal.Parse(s.PrecoCusto)))
                .ForMember(d => d.PrecoVenda, m => m.MapFrom(s => decimal.Parse(s.PrecoVenda)))
                .ForMember(d => d.Unidade, m => m.MapFrom(s => new Unidade { UnidadeCodigo = s.UnidadeId }));

            CreateMap<CondicaoPagamentoModel, CondicaoPagamento>();

            CreateMap<FormaPagamentoModel, FormaPagamento>();
        }

        private void MapperPerson()
        {
            CreateMap<PessoaModel, PessoaFisica>()
                .ForMember(d => d.EstadoCivil, m => m.MapFrom(s => s));

            CreateMap<ClienteModel, PessoaFisica>()
                .ForMember(d => d.EstadoCivil, m => m.MapFrom(s => s));

            CreateMap<FuncionarioModel, PessoaFisica>()
                .ForMember(d => d.EstadoCivil, m => m.MapFrom(s => s));

            CreateMap<FornecedorModel, PessoaFisica>()
                .ForMember(d => d.EstadoCivil, m => m.MapFrom(s => s));

            CreateMap<PessoaModel, PessoaJuridica>();
            CreateMap<ClienteModel, PessoaJuridica>();
            CreateMap<FuncionarioModel, PessoaJuridica>();
            CreateMap<FornecedorModel, PessoaJuridica>();

            CreateMap<PessoaModel, Pessoa>()
                .ForMember(d => d.PessoaCodigo, m => m.MapFrom(s => s.Codigo))
                .ForMember(d => d.Nome, m => m.MapFrom(s => s.EPessoaFisica ? s.Nome : s.RazaoSocial))
                .ForMember(d => d.Papeis, m => m.MapFrom(s => new List<Papel>
                {
                    new Papel { PapelCodigo = s.PapelPessoa }
                }))
                .ForMember(d => d.MeiosComunicacao, m => m.MapFrom(s => s.MeioComunicacao))
                .ForMember(d => d.Enderecos, m => m.MapFrom(s => new List<Endereco>
                {
                    new Endereco
                    {
                        EnderecoCodigo = s.Endereco.EnderecoId ?? 0,
                        Logradouro = s.Endereco.Logradouro,
                        Numero = s.Endereco.Numero,
                        Bairro = s.Endereco.Bairro,
                        Complemento = s.Endereco.Complemento,
                        Cidade = s.Endereco.Cidade,
                        CEP = s.Endereco.Cep,
                        Estado = new Estado {EstadoCodigo = s.Endereco.UFId ?? 0},
                        Principal = true
                    }
                }))
                .Include<ClienteModel, Pessoa>()
                .Include<FuncionarioModel, Pessoa>()
                .Include<FornecedorModel, Pessoa>();

            CreateMap<ClienteModel, Pessoa>();
            CreateMap<FuncionarioModel, Pessoa>();
            CreateMap<FornecedorModel, Pessoa>();

            CreateMap<PessoaModel, EstadoCivil>()
                .ForMember(d => d.EstadoCivilId, m => m.Ignore())
                .ForMember(d => d.EstadoCivilNome, m => m.Ignore())
                .ForMember(d => d.EstadoCivilCodigo, m => m.MapFrom(s => s.EstadoCivilId));

            CreateMap<ClienteModel, EstadoCivil>()
                .ForMember(d => d.EstadoCivilId, m => m.Ignore())
                .ForMember(d => d.EstadoCivilNome, m => m.Ignore())
                .ForMember(d => d.EstadoCivilCodigo, m => m.MapFrom(s => s.EstadoCivilId));

            CreateMap<FuncionarioModel, EstadoCivil>()
                .ForMember(d => d.EstadoCivilId, m => m.Ignore())
                .ForMember(d => d.EstadoCivilNome, m => m.Ignore())
                .ForMember(d => d.EstadoCivilCodigo, m => m.MapFrom(s => s.EstadoCivilId));

            CreateMap<FornecedorModel, EstadoCivil>()
                .ForMember(d => d.EstadoCivilId, m => m.Ignore())
                .ForMember(d => d.EstadoCivilNome, m => m.Ignore())
                .ForMember(d => d.EstadoCivilCodigo, m => m.MapFrom(s => s.EstadoCivilId));

            CreateMap<MeioComunicacaoModel, ICollection<MeioComunicacao>>()
                .ConvertUsing<MeioComunicacaoConverter>();
        }

        private void MapperUser()
        {
            CreateMap<UsuarioModel, Usuario>()
                .ForMember(d => d.GrupoPermissao,
                    m => m.MapFrom(s => new GrupoPermissao { GrupoPermissaoCodigo = s.GrupoId.Value }));

            CreateMap<PermissaoModel, Permissao>()
                .ForMember(d => d.PermissaoCodigo, m => m.MapFrom(s => s.Codigo))
                .ForMember(d => d.PermissaoNome, m => m.MapFrom(s => s.Nome));

            CreateMap<GrupoModel, GrupoPermissao>()
                .ForMember(d => d.GrupoPermissaoCodigo, m => m.MapFrom(s => s.GrupoCodigo))
                .ForMember(d => d.GrupoPermissaoNome, m => m.MapFrom(s => s.GrupoNome))
                .ForMember(d => d.Permissoes, m => m.MapFrom(s => s.Permissoes));
        }
    }
}