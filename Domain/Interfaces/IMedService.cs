using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IMedService
    {
        void CadastrarFicha(Fichas ficha);
        void AlterarFicha(Fichas ficha);
        void DeletarFicha(int id);
        IEnumerable<Fichas> BuscarFichasPaciente(int id);
        IEnumerable<Fichas> BuscarFichasMedico(int id);
    }
}
