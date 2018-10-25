using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Contracts.IRepository.ICompra;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.Compra;

namespace ProjetoArtCouro.DataBase.Repositorios.CompraRepository
{
    public class CompraRepository : ICompraRepository
    {
        private readonly DataBaseContext _context;
        public CompraRepository(DataBaseContext context)
        {
            _context = context;
        }

        public Compra ObterPorId(Guid id)
        {
            return _context.Compras.FirstOrDefault(x => x.CompraId.Equals(id));
        }

        public Compra ObterPorCodigo(int codigo)
        {
            return _context.Compras.FirstOrDefault(x => x.CompraCodigo.Equals(codigo));
        }

        public Compra ObterPorCodigoComItensCompra(int codigo)
        {
            return _context.Compras
                .Include("ItensCompra")
                .FirstOrDefault(x => x.CompraCodigo == codigo);
        }

        public List<Compra> ObterLista()
        {
            return _context.Compras.ToList();
        }

        public List<Compra> ObterListaPorFiltro(PesquisaCompra filtro)
        {
            var query = from compra in _context.Compras
                .Include("Usuario")
                .Include("ItensCompra")
                .Include("Fornecedor")
                .Include("Fornecedor.PessoaFisica")
                .Include("Fornecedor.PessoaJuridica")
                        select compra;

            if (filtro.CodigoCompra != 0)
            {
                query = query.Where(x => x.CompraCodigo == filtro.CodigoCompra);
            }

            if (filtro.CodigoFornecedor == 0)
            {
                query = query.Where(x => x.Fornecedor.PessoaCodigo == filtro.CodigoFornecedor);
            }

            if (filtro.DataCadastro != new DateTime())
            {
                query = query.Where(x => DbFunctions.TruncateTime(x.DataCadastro) == filtro.DataCadastro.Date);
            }

            if (filtro.StatusCompra != 0)
            {
                query = query.Where(x => x.StatusCompra == filtro.StatusCompra);
            }

            if (!string.IsNullOrEmpty(filtro.NomeFornecedor))
            {
                query = query.Where(x => x.Fornecedor.Nome == filtro.NomeFornecedor);
            }

            if (!string.IsNullOrEmpty(filtro.CPFCNPJ))
            {
                query = query.Where(x => x.Fornecedor.PessoaFisica.CPF == filtro.CPFCNPJ || 
                x.Fornecedor.PessoaJuridica.CNPJ == filtro.CPFCNPJ);
            }

            if (filtro.CodigoUsuario != 0)
            {
                query = query.Where(x => x.Usuario.UsuarioCodigo == filtro.CodigoUsuario);
            }

            return query.ToList();
        }

        public void Criar(Compra compra)
        {
            _context.Compras.Add(compra);
            _context.SaveChanges();
        }

        public void Atualizar(Compra compra)
        {
            _context.Entry(compra).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(Compra compra)
        {
            _context.Compras.Remove(compra);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
