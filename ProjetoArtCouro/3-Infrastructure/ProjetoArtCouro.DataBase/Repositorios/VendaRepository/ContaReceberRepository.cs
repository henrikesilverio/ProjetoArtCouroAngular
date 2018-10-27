using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Contracts.IRepository.IVenda;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.ContaReceber;
using ProjetoArtCouro.Domain.Models.Enums;

namespace ProjetoArtCouro.DataBase.Repositorios.VendaRepository
{
    public class ContaReceberRepository : IContaReceberRepository
    {
        private readonly DataBaseContext _context;
        public ContaReceberRepository(DataBaseContext context)
        {
            _context = context;
        }

        public ContaReceber ObterPorId(Guid id)
        {
            return _context.ContasReceber.FirstOrDefault(x => x.ContaReceberId.Equals(id));
        }

        public ContaReceber ObterPorCodigo(int codigo)
        {
            return _context.ContasReceber.FirstOrDefault(x => x.ContaReceberCodigo.Equals(codigo));
        }

        public ContaReceber ObterPorCodigoComVenda(int codigo)
        {
            return _context.ContasReceber.Include("Venda").FirstOrDefault(x => x.ContaReceberCodigo.Equals(codigo));
        }

        public List<ContaReceber> ObterLista()
        {
            return _context.ContasReceber.ToList();
        }

        public List<ContaReceber> ObterListaPorCodigoVenda(int codigoVenda)
        {
            return _context.ContasReceber.Include("Venda").Where(x => x.Venda.VendaCodigo.Equals(codigoVenda)).ToList();
        }

        public List<ContaReceber> ObterListaPorFiltro(PesquisaContaReceber filtro)
        {
            var query = from contaReceber in _context.ContasReceber
                .Include("Venda")
                .Include("Venda.Usuario")
                .Include("Venda.Cliente.PessoaFisica")
                .Include("Venda.Cliente.PessoaJuridica")
                        select contaReceber;

            if (filtro.CodigoVenda != 0)
            {
                query = query.Where(x => x.Venda.VendaCodigo == filtro.CodigoVenda);
            }

            if (filtro.CodigoCliente != 0)
            {
                query = query.Where(x => x.Venda.Cliente.PessoaCodigo == filtro.CodigoCliente);
            }

            if (filtro.DataEmissao != new DateTime())
            {
                query = query.Where(x => DbFunctions.TruncateTime(x.Venda.DataCadastro) == filtro.DataEmissao.Date);
            }

            if (filtro.DataVencimento != new DateTime())
            {
                query = query.Where(x => DbFunctions.TruncateTime(x.DataVencimento) == filtro.DataVencimento.Date);
            }

            if (filtro.StatusContaReceber != StatusContaReceberEnum.None)
            {
                query = query.Where(x => x.StatusContaReceber == filtro.StatusContaReceber);
            }

            if (!string.IsNullOrEmpty(filtro.NomeCliente))
            {
                query = query.Where(x => x.Venda.Cliente.Nome == filtro.NomeCliente);
            }

            if (!string.IsNullOrEmpty(filtro.CPFCNPJ))
            {
                query = query.Where(x => x.Venda.Cliente.PessoaFisica.CPF == filtro.CPFCNPJ || 
                x.Venda.Cliente.PessoaJuridica.CNPJ == filtro.CPFCNPJ);
            }

            if (filtro.CodigoUsuario != 0)
            {
                query = query.Where(x => x.Venda.Usuario.UsuarioCodigo == filtro.CodigoUsuario);
            }

            return query.ToList();
        }

        public void Criar(ContaReceber contaReceber)
        {
            _context.ContasReceber.Add(contaReceber);
            _context.SaveChanges();
        }

        public void Atualizar(ContaReceber contaReceber)
        {
            _context.Entry(contaReceber).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(ContaReceber contaReceber)
        {
            _context.ContasReceber.Remove(contaReceber);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
