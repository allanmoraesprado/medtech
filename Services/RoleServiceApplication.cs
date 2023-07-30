using Application.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RoleServiceApplication : IRoleServiceApplication
    {
        private readonly IRoleService _roleService;
        public RoleServiceApplication(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public int ObterAutorizacao(int id)
        {
            return _roleService.ObterAutorizacao(id);
        }
    }
}
