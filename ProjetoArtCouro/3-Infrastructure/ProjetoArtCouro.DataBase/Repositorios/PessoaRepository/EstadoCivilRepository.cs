﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPessoa;
using ProjetoArtCouro.Domain.Entities.Pessoas;

namespace ProjetoArtCouro.DataBase.Repositorios.PessoaRepository
{
    public class EstadoCivilRepository : IEstadoCivilRepository
    {
        private readonly DataBaseContext _context;

        public EstadoCivilRepository(DataBaseContext context)
        {
            _context = context;
        }

        public EstadoCivil ObterPorId(Guid id)
        {
            return _context.EstadosCivis.FirstOrDefault(x => x.EstadoCivilId == id);
        }

        public EstadoCivil ObterPorCodigo(int codigo)
        {
            return _context.EstadosCivis.FirstOrDefault(x => x.EstadoCivilCodigo == codigo);
        }

        public List<EstadoCivil> ObterLista()
        {
            return _context.EstadosCivis.ToList();
        }

        public void Criar(EstadoCivil estadoCivil)
        {
            _context.EstadosCivis.Add(estadoCivil);
            _context.SaveChanges();
        }

        public void Atualizar(EstadoCivil estadoCivil)
        {
            _context.Entry(estadoCivil).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(EstadoCivil estadoCivil)
        {
            _context.EstadosCivis.Remove(estadoCivil);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
