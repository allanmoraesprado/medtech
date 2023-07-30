using Domain.Interfaces;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class RoleService : IRoleService
    {
        readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public int ObterAutorizacao(int id)
        {
            return _roleRepository.ObterAutorizacao(id);
        }
    }
}
