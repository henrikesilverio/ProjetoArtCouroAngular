using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
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

        public Estoque ObterPorId(Guid id)
        {
            return _context.Estoques.FirstOrDefault(x => x.EstoqueId.Equals(id));
        }

        public Estoque ObterPorCodigo(int codigo)
        {
            return _context.Estoques.FirstOrDefault(x => x.EstoqueCodigo.Equals(codigo));
        }

        public List<Estoque> ObterLista()
        {
            return _context.Estoques.ToList();
        }

        public Estoque ObterPorCodigoProduto(int codigoProduto)
        {
            return
                _context.Estoques.Include("Produto").FirstOrDefault(x => x.Produto.ProdutoCodigo.Equals(codigoProduto));
        }

        public List<Estoque> ObterListaPorFiltro(PesquisaEstoque filtro)
        {
            var query = from estoque in _context.Estoques
                .Include("Produto")
                .Include("Compra")
                .Include("Compra.Fornecedor")
                        select estoque;

            if (filtro.CodigoProduto != 0)
            {
                query = query.Where(x => x.Produto.ProdutoCodigo == filtro.CodigoProduto);
            }

            if (filtro.QuantidaEstoque != 0)
            {
                query = query.Where(x => x.Quantidade == filtro.QuantidaEstoque);
            }

            if (filtro.CodigoFornecedor != 0)
            {
                query = query.Where(x => x.Compra.Fornecedor.PessoaCodigo == filtro.CodigoFornecedor);
            }

            if (!string.IsNullOrEmpty(filtro.DescricaoProduto))
            {
                query = query.Where(x => x.Produto.ProdutoNome.Contains(filtro.DescricaoProduto));
            }

            if (!string.IsNullOrEmpty(filtro.NomeFornecedor))
            {
                query = query.Where(x => x.Compra.Fornecedor.Nome.Contains(filtro.NomeFornecedor));
            }

            return query.ToList();
        }

        public void Criar(Estoque estoque)
        {
            _context.Estoques.Add(estoque);
            _context.SaveChanges();
        }

        public void Atualizar(Estoque estoque)
        {
            _context.Entry(estoque).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(Estoque estoque)
        {
            _context.Estoques.Remove(estoque);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
