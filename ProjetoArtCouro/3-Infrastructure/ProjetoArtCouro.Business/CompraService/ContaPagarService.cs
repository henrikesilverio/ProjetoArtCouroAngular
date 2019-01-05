using System.Collections.Generic;
using ProjetoArtCouro.Domain.Contracts.IRepository.ICompra;
using ProjetoArtCouro.Domain.Contracts.IService.ICompra;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Models.ContaPagar;
using ProjetoArtCouro.Resource.Validation;
using ProjetoArtCouro.Domain.Exceptions;
using System.Linq;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Mapping;
using System;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Business.CompraService
{
    public class ContaPagarService : IContaPagarService
    {
        private readonly IContaPagarRepository _contaPagarRepository;

        public ContaPagarService(IContaPagarRepository contaPagarRepository)
        {
            _contaPagarRepository = contaPagarRepository;
        }

        public List<ContaPagarModel> PesquisarContaPagar(int codigoUsuario, PesquisaContaPagarModel model)
        {
            var filtro = Map<PesquisaContaPagar>.MapperTo(model);
            filtro.CodigoUsuario = codigoUsuario;
            var contasPagar = _contaPagarRepository.ObterListaPorFiltro(filtro);
            return Map<List<ContaPagarModel>>.MapperTo(contasPagar);
        }

        public void PagarContas(List<ContaPagarModel> model)
        {
            AssertionConcern<BusinessException>
                .AssertArgumentTrue(model.Any(), Erros.ListOfAccountsPayableEmpty);

            var contasPagar = Map<List<ContaPagar>>.MapperTo(model);
            contasPagar.ForEach(conta => ValidarContas(conta));

            contasPagar.ForEach(x =>
            {
                var contaPagarAtual = _contaPagarRepository.ObterPorCodigoComCompra(x.ContaPagarCodigo);
                AssertionConcern<BusinessException>
                    .AssertArgumentNotNull(contaPagarAtual, Erros.AccountPayableNotFound);

                contaPagarAtual.Pago = x.Pago;
                contaPagarAtual.StatusContaPagar = x.Pago
                    ? StatusContaPagarEnum.Pago
                    : StatusContaPagarEnum.Aberto;
                _contaPagarRepository.Atualizar(contaPagarAtual);
            });
        }

        private void ValidarContas(ContaPagar conta)
        {
            new ValidationContract<ContaPagar>(conta)
                .IsNotZero(x => x.ContaPagarCodigo)
                .IsNotEquals(x => x.DataVencimento, new DateTime())
                .IsNotZero(x => x.ValorDocumento);

            AssertionConcern<DomainException>
                .AssertArgumentTrue(conta.IsValid(), conta.GetMergeNotifications());
        }

        public void Dispose()
        {
            _contaPagarRepository.Dispose();
        }
    }
}
