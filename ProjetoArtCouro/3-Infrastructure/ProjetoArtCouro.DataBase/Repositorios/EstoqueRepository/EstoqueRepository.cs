using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Factories;
using ProjetoArtCouro.Domain.Contracts.IRepository.IEstoque;
using ProjetoArtCouro.Domain.Entities.Estoques;
using ProjetoArtCouro.Domain.Models.Estoque;

namespace ProjetoArtCouro.DataBase.Repositorios.EstoqueRepository
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly DataBaseContext _context;

        public EstoqueRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void Criar(Estoque estoque)
        {
            _context.Estoques.Add(estoque);
            _context.SaveChanges();
        }

        public Estoque ObterPorCodigoProduto(int codigoProduto)
        {
            return _context.Estoques
                .Include("Produto")
                .FirstOrDefault(x => x.Produto.ProdutoCodigo == codigoProduto);
        }

        public List<Estoque> ObterListaPorFiltro(PesquisaEstoque filtro)
        {
            var estoqueFiltro = EstoqueFiltroFactory.Fabricar(_context);
            return estoqueFiltro.Filtrar(filtro).ToList();
        }

        public void Atualizar(Estoque estoque)
        {
            _context.Entry(estoque).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
