using Domain.Entities;
using Domain.Interfaces;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AtualizarUsuario(Usuarios usuario)
        {
            throw new NotImplementedException();
        }
        public void CadastrarUsuario(Usuarios usuario)
        {
            _userRepository.Create(usuario);
        }
        public Usuarios BuscarUsuario(int id)
        {
            throw new NotImplementedException();
        }
        public Usuarios BuscarUsuario(string user, string senha)
        {
            return _userRepository.BuscarUsuario(user, senha);
        }
        public int BuscarUsuarioId(string user, string senha)
        {
            return _userRepository.BuscarUsuarioId(user, senha);
        }
        public void DeletarUsuario(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuarios> ListarUsuarios()
        {
            return _userRepository.Read();
        }      
        public bool ValidarLogin(string user)
        {
            return _userRepository.ValidarLogin(user);
        }

        public bool ValidarCpf(string cpf)
        {
            return _userRepository.ValidarCpf(cpf);
        }

        public int BuscarRegistroUsuario(string cpf)
        {
            return _userRepository.BuscarRegistroUsuario(cpf);
        }
    }
}
