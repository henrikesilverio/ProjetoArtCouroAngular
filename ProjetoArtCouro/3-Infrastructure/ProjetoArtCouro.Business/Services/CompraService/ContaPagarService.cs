using System.Collections.Generic;
using ProjetoArtCouro.Domain.Contracts.IRepository.ICompra;
using ProjetoArtCouro.Domain.Contracts.IService.ICompra;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Models.ContaPagar;
using AutoMapper;
using ProjetoArtCouro.Resource.Validation;
using ProjetoArtCouro.Domain.Exceptions;
using System.Linq;
using ProjetoArtCouro.Resources.Resources;

namespace ProjetoArtCouro.Business.Services.CompraService
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
            var filtro = Mapper.Map<PesquisaContaPagar>(model);
            filtro.CodigoUsuario = codigoUsuario;
            var contasPagar = _contaPagarRepository.ObterListaPorFiltro(filtro);
            return Mapper.Map<List<ContaPagarModel>>(contasPagar);
        }

        public void PagarContas(List<ContaPagarModel> model)
        {
            var contasPagar = Mapper.Map<List<ContaPagar>>(model);
            AssertionConcern<BusinessException>
                .AssertArgumentFalse(contasPagar.Any(x => x.ContaPagarCodigo.Equals(0)), Erros.ThereAccountPayableWithCodeZero);

            contasPagar.ForEach(x =>
            {
                var contaPagarAtual = _contaPagarRepository.ObterPorCodigoComCompra(x.ContaPagarCodigo);
                contaPagarAtual.Pago = x.Pago;
                contaPagarAtual.StatusContaPagar = x.Pago
                    ? StatusContaPagarEnum.Pago
                    : StatusContaPagarEnum.Aberto;
                _contaPagarRepository.Atualizar(contaPagarAtual);
            });
        }

        public void Dispose()
        {
            _contaPagarRepository.Dispose();
        }
    }
}
