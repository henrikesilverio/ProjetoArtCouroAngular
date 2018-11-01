using System.Collections.Generic;
using ProjetoArtCouro.Domain.Contracts.IRepository.IEstoque;
using ProjetoArtCouro.Domain.Contracts.IService.IEstoque;
using ProjetoArtCouro.Domain.Models.Estoque;
using ProjetoArtCouro.Mapping;

namespace ProjetoArtCouro.Business.Services.EstoqueService
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IEstoqueRepository _estoqueRepository;

        public EstoqueService(IEstoqueRepository estoqueRepository)
        {
            _estoqueRepository = estoqueRepository;
        }

        public List<EstoqueModel> PesquisarEstoque(PesquisaEstoqueModel model)
        {
            var filtro = Map<PesquisaEstoque>.MapperTo(model);
            var estoques = _estoqueRepository.ObterListaPorFiltro(filtro);
            return Map<List<EstoqueModel>>.MapperTo(estoques);
        }

        public void Dispose()
        {
            _estoqueRepository.Dispose();
        }
    }
}
