using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Domain.Repositories;
using System.Linq;
using Infraestructure.Tools;

namespace Repository.Entities
{
    public class UserRepository : Repository<Usuarios>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext) { }

        public Usuarios BuscarUsuario(string user, string senha)
        {
            var usuario = _dbSet.Where(p => p.Usuario == user && p.Senha == senha).FirstOrDefault();
            return usuario;
        }
        public int BuscarUsuarioId(string user, string senha)
        {
            var usuario = _dbSet.Where(p => p.Usuario == user && p.Senha == senha).FirstOrDefault();
            return (int)usuario.Id;
        }
        public bool ValidarLogin(string user)
        {
            var usuario = _dbSet.Where(p => p.Usuario == user).FirstOrDefault();
            return usuario == null ? false : true;
        }
        public bool ValidarCpf(string cpf)
        {
            cpf = cpf.Replace("-", ".");
            var usuario = _dbSet.Where(p => p.Cpf == cpf).FirstOrDefault();
            return usuario == null ? false : true;
        }

        public int BuscarRegistroUsuario(string cpf)
        {
            cpf = cpf.Replace("-", ".");
            var usuario = _dbSet.Where(p => p.Cpf == cpf).FirstOrDefault();
            return (int)usuario.Id;
        }
    }
}
