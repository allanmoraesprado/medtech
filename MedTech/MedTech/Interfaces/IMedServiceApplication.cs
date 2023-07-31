using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMedServiceApplication
    {
        void Cadastrar(FichaViewModel model);
        void Update(FichaViewModel model);
        void Deletar(int id);
        IEnumerable<FichaViewModel> FichasPaciente(int id);
        IEnumerable<FichaViewModel> FichasMedico(int id);
    }
}
