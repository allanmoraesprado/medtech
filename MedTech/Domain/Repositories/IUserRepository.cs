using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepository<Usuarios>
    {
        Usuarios BuscarUsuario(string user, string senha);
        int BuscarUsuarioId(string user, string senha);
        int BuscarRegistroUsuario(string cpf);
        bool ValidarLogin(string user);
        bool ValidarCpf(string cpf);
    }
}
