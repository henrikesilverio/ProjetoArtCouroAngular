using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Contracts.IRepository.IVenda;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Models.Venda;

namespace ProjetoArtCouro.DataBase.Repositorios.VendaRepository
{
    public class VendaRepository : IVendaRepository
    {
        private readonly DataBaseContext _context;
        public VendaRepository(DataBaseContext context)
        {
            _context = context;
        }

        public Venda ObterPorId(Guid id)
        {
            return _context.Vendas.FirstOrDefault(x => x.VendaId.Equals(id));
        }

        public Venda ObterPorCodigo(int codigo)
        {
            return _context.Vendas.FirstOrDefault(x => x.VendaCodigo.Equals(codigo));
        }

        public Venda ObterPorCodigoComItensVenda(int codigo)
        {
            return _context.Vendas.Include("ItensVenda").FirstOrDefault(x => x.VendaCodigo.Equals(codigo));
        }

        public List<Venda> ObterLista()
        {
            return _context.Vendas.ToList();
        }

        public List<Venda> ObterListaPorFiltro(PesquisaVenda filtro)
        {
            var query = from venda in _context.Vendas
                .Include("Usuario")
                .Include("ItensVenda")
                .Include("Cliente")
                .Include("Cliente.PessoaFisica")
                .Include("Cliente.PessoaJuridica")
                select venda;

            if (filtro.CodigoVenda != 0)
            {
                query = query.Where(x => x.VendaCodigo == filtro.CodigoVenda);
            }

            if (filtro.CodigoCliente != 0)
            {
                query = query.Where(x => x.Cliente.PessoaCodigo == filtro.CodigoCliente);
            }

            if (filtro.DataCadastro != new DateTime())
            {
                query = query.Where(x => DbFunctions.TruncateTime(x.DataCadastro) == filtro.DataCadastro.Date);
            }

            if (filtro.StatusVenda != StatusVendaEnum.None)
            {
                query = query.Where(x => x.StatusVenda == filtro.StatusVenda);
            }

            if (!string.IsNullOrEmpty(filtro.NomeCliente))
            {
                query = query.Where(x => x.Cliente.Nome == filtro.NomeCliente);
            }

            if (!string.IsNullOrEmpty(filtro.CPFCNPJ))
            {
                query = query.Where(x => x.Cliente.PessoaFisica.CPF == filtro.CPFCNPJ ||
                x.Cliente.PessoaJuridica.CNPJ == filtro.CPFCNPJ);
            }

            if (filtro.CodigoUsuario != 0)
            {
                query = query.Where(x => x.Usuario.UsuarioCodigo == filtro.CodigoUsuario);
            }

            return query.ToList();
        }

        public void Criar(Venda venda)
        {
            _context.Vendas.Add(venda);
            _context.SaveChanges();
        }

        public void Atualizar(Venda venda)
        {
            _context.Entry(venda).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(Venda venda)
        {
            _context.Vendas.Remove(venda);
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
