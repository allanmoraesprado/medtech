using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        IEnumerable<Usuarios> ListarUsuarios();
        void CadastrarUsuario(Usuarios usuario);
        void AtualizarUsuario(Usuarios usuario);
        Usuarios BuscarUsuario(int id);
        Usuarios BuscarUsuario(string user, string senha);
        int BuscarRegistroUsuario(string cpf);
        bool ValidarCpf(string cpf);
        int BuscarUsuarioId(string user, string senha);
        bool ValidarLogin(string user);       
        void DeletarUsuario(int id);
    }
}
