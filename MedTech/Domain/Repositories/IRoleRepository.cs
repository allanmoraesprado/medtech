using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRoleRepository
    {
        int ObterAutorizacao(int id);
    }
}
