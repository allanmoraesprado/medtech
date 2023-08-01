using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserServiceApplication
    {
        IEnumerable<UsuarioViewModel> Listar();
        UsuarioViewModel Buscar(int id);
        UsuarioViewModel Buscar(string user, string senha);
        bool ValidarCpf(string cpf);
        int BuscarId(string user, string senha);
        bool Validar(string user);
        void Cadastrar(UsuarioViewModel model);
        int BuscarRegistro(string cpf);
        void Atualizar(UsuarioViewModel model);
    }
}
