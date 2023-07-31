using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Entities
{
    public class MedRepository : Repository<Fichas>, IMedRepository
    {
        public MedRepository(DbContext dbContext) : base(dbContext) { }
        public IEnumerable<Fichas> BuscarFichasMedico(int id)
        {
            var fichas = _dbSet.Where(p => p.MedicoId == id);
            return fichas;
        }
        public IEnumerable<Fichas> BuscarFichasPaciente(int id)
        {
            var fichas = _dbSet.Where(p => p.PacienteId == id);
            return fichas;
        }
    }
}
