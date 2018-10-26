using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Contracts.IRepository.ICompra;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.ContaPagar;
using ProjetoArtCouro.Domain.Models.Enums;

namespace ProjetoArtCouro.DataBase.Repositorios.CompraRepository
{
    public class ContaPagarRepository : IContaPagarRepository
    {
        private readonly DataBaseContext _context;
        public ContaPagarRepository(DataBaseContext context)
        {
            _context = context;
        }

        public ContaPagar ObterPorId(Guid id)
        {
            return _context.ContasPagar.FirstOrDefault(x => x.ContaPagarId.Equals(id));
        }

        public ContaPagar ObterPorCodigo(int codigo)
        {
            return _context.ContasPagar.FirstOrDefault(x => x.ContaPagarCodigo.Equals(codigo));
        }

        public ContaPagar ObterPorCodigoComCompra(int codigo)
        {
            return _context.ContasPagar.Include("Compra").FirstOrDefault(x => x.ContaPagarCodigo.Equals(codigo));
        }

        public List<ContaPagar> ObterLista()
        {
            return _context.ContasPagar.ToList();
        }

        public List<ContaPagar> ObterListaPorCodigoCompra(int codigoCompra)
        {
            return
                _context.ContasPagar.Include("Compra").Where(x => x.Compra.CompraCodigo.Equals(codigoCompra)).ToList();
        }

        public List<ContaPagar> ObterListaPorFiltro(PesquisaContaPagar filtro)
        {
            var query = from contaPagar in _context.ContasPagar
                .Include("Compra")
                .Include("Compra.Usuario")
                .Include("Compra.Fornecedor.PessoaFisica")
                .Include("Compra.Fornecedor.PessoaJuridica")
                        select contaPagar;

            if (filtro.CodigoCompra != 0)
            {
                query = query.Where(x => x.Compra.CompraCodigo == filtro.CodigoCompra);
            }

            if (filtro.CodigoFornecedor != 0)
            {
                query = query.Where(x => x.Compra.Fornecedor.PessoaCodigo == filtro.CodigoFornecedor);
            }

            if (filtro.DataEmissao != new DateTime())
            {
                query = query.Where(x => DbFunctions.TruncateTime(x.Compra.DataCadastro) == filtro.DataEmissao.Date);
            }

            if (filtro.DataVencimento != new DateTime())
            {
                query = query.Where(x => DbFunctions.TruncateTime(x.DataVencimento) == filtro.DataVencimento.Date);
            }

            if (filtro.StatusContaPagar != StatusContaPagarEnum.None)
            {
                query = query.Where(x => x.StatusContaPagar == filtro.StatusContaPagar);
            }

            if (!string.IsNullOrEmpty(filtro.NomeFornecedor))
            {
                query = query.Where(x => x.Compra.Fornecedor.Nome == filtro.NomeFornecedor);
            }

            if (!string.IsNullOrEmpty(filtro.CPFCNPJ))
            {
                query = query.Where(x => x.Compra.Fornecedor.PessoaFisica.CPF == filtro.CPFCNPJ || 
                x.Compra.Fornecedor.PessoaJuridica.CNPJ == filtro.CPFCNPJ);
            }

            if (filtro.CodigoUsuario != 0)
            {
                query = query.Where(x => x.Compra.Usuario.UsuarioCodigo == filtro.CodigoUsuario);
            }

            return query.ToList();
        }

        public void Criar(ContaPagar contaPagar)
        {
            _context.ContasPagar.Add(contaPagar);
            _context.SaveChanges();
        }

        public void Atualizar(ContaPagar contaPagar)
        {
            _context.Entry(contaPagar).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(ContaPagar contaPagar)
        {
            _context.ContasPagar.Remove(contaPagar);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
