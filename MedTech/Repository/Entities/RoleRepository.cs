using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Domain.Repositories;
using System.Linq;

namespace Repository.Entities
{
    public class RoleRepository : IRoleRepository
    {
        protected DataContext _dataContext;

        public RoleRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public int ObterAutorizacao(int id)
        {
            var role = _dataContext.Roles.Where(p => p.RoleId == id).FirstOrDefault();
            return role.Autorizacao;
        }
    }
}
