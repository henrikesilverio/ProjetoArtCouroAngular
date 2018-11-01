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

namespace ProjetoArtCouro.Business.Services.VendaService
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
            var contasReceber = Map<List<ContaReceber>>.MapperTo(model);

            AssertionConcern<BusinessException>
                .AssertArgumentFalse(contasReceber.Any(x => x.ContaReceberCodigo.Equals(0)), Erros.ThereReceivableWithZeroCode);

            contasReceber.ForEach(x =>
            {
                var contaReceberAtual = _contaReceberRepository.ObterPorCodigoComVenda(x.ContaReceberCodigo);
                contaReceberAtual.Recebido = x.Recebido;
                contaReceberAtual.StatusContaReceber = x.Recebido
                    ? StatusContaReceberEnum.Recebido
                    : StatusContaReceberEnum.Aberto;
                _contaReceberRepository.Atualizar(contaReceberAtual);
            });
        }

        public void Dispose()
        {
            _contaReceberRepository.Dispose();
        }
    }
}
