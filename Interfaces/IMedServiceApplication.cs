using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMedServiceApplication
    {
        void Cadastrar(MedViewModel model);
        void Update(MedViewModel model);
        void Deletar(int id);
        IEnumerable<MedViewModel> FichasPaciente(int id);
        IEnumerable<MedViewModel> FichasMedico(int id);
    }
}
