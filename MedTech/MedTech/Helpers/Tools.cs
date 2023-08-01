using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Helpers
{
    public class Tools
    {
        public static bool ValidUserModel(UsuarioViewModel usuario)
        {
            if (usuario.Nome != null || usuario.Email != null
                || usuario.Endereco != null || usuario.Telefone != null)
                return true;

            return false;
        }
    }
}
