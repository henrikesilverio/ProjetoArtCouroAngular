using System.Collections.Generic;
using AutoMapper;
using ProjetoArtCouro.Domain.Contracts.IRepository.IEstoque;
using ProjetoArtCouro.Domain.Contracts.IService.IEstoque;
using ProjetoArtCouro.Domain.Models.Estoque;

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
            var filtro = Mapper.Map<PesquisaEstoque>(model);
            var estoques = _estoqueRepository.ObterListaPorFiltro(filtro);
            return Mapper.Map<List<EstoqueModel>>(estoques);
        }

        public void Dispose()
        {
            _estoqueRepository.Dispose();
        }
    }
}
