using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface IMedRepository : IRepository<Fichas>
    {
        IEnumerable<Fichas> BuscarFichasPaciente(int id);
        IEnumerable<Fichas> BuscarFichasMedico(int id);
    }
}
