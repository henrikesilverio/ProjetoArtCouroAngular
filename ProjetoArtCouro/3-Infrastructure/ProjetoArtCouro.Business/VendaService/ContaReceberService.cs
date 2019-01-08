using System.Collections.Generic;
using System.Linq;
using ProjetoArtCouro.Domain.Contracts.IRepository.IVenda;
using ProjetoArtCouro.Domain.Contracts.IService.IVenda;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resource.Validation;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.ContaReceber;
using ProjetoArtCouro.Mapping;
using ProjetoArtCouro.Resources.Validation;
using System;

namespace ProjetoArtCouro.Business.VendaService
{
    public class ContaReceberService : IContaReceberService
    {
        private readonly IContaReceberRepository _contaReceberRepository;

        public ContaReceberService(IContaReceberRepository contaReceberRepository)
        {
            _contaReceberRepository = contaReceberRepository;
        }

        public List<ContaReceberModel> PesquisarContaReceber(int codigoUsuario, PesquisaContaReceberModel model)
        {
            var filtro = Map<PesquisaContaReceber>.MapperTo(model);
            filtro.CodigoUsuario = codigoUsuario;
            var contasReceber = _contaReceberRepository.ObterListaPorFiltro(filtro);
            return Map<List<ContaReceberModel>>.MapperTo(contasReceber);
        }

        public void ReceberContas(List<ContaReceberModel> model)
        {
            AssertionConcern<BusinessException>
                .AssertArgumentTrue(model.Any(), Erros.ListOfAccountsReceivableEmpty);

            var contasReceber = Map<List<ContaReceber>>.MapperTo(model);
            contasReceber.ForEach(conta => ValidarContas(conta));

            contasReceber.ForEach(x =>
            {
                var contaReceberAtual = _contaReceberRepository
                     .ObterPorCodigoComVenda(x.ContaReceberCodigo);
                AssertionConcern<BusinessException>
                    .AssertArgumentNotNull(contaReceberAtual, Erros.AccountIncomingNotFound);

                contaReceberAtual.Recebido = x.Recebido;
                contaReceberAtual.StatusContaReceber = x.Recebido
                    ? StatusContaReceberEnum.Recebido
                    : StatusContaReceberEnum.Aberto;
                _contaReceberRepository.Atualizar(contaReceberAtual);
            });
        }

        private void ValidarContas(ContaReceber conta)
        {
            new ValidationContract<ContaReceber>(conta)
                .IsNotZero(x => x.ContaReceberCodigo)
                .IsNotEquals(x => x.DataVencimento, new DateTime())
                .IsNotZero(x => x.ValorDocumento);

            AssertionConcern<DomainException>
                .AssertArgumentTrue(conta.IsValid(), conta.GetMergeNotifications());
        }

        public void Dispose()
        {
            _contaReceberRepository.Dispose();
        }
    }
}
