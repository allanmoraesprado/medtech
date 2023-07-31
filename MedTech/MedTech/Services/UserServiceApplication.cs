using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserServiceApplication : IUserServiceApplication
    {
        private readonly IUserService _userService;

        public UserServiceApplication(IUserService userService)
        {
            _userService = userService;
        }
        public UsuarioViewModel Buscar(int id)
        {
            throw new NotImplementedException();
        }
        public UsuarioViewModel Buscar(string user, string senha)
        {
            var usuario = _userService.BuscarUsuario(user, senha);
            var model = Mappers.Map<Usuarios, UsuarioViewModel>(usuario);
            return model;
        }
        public int BuscarId(string user, string senha)
        {
            return _userService.BuscarUsuarioId(user, senha);
        }
        public bool Validar(string user)
        {
            return _userService.ValidarLogin(user);
        }            
        public IEnumerable<UsuarioViewModel> Listar()
        {
            var userList = _userService.ListarUsuarios();
            var userModelList = Mappers.MapEnumerable<Usuarios, UsuarioViewModel>(userList);
            return userModelList;
        }

        public void Cadastrar(UsuarioViewModel model)
        {
            var usuario = Mappers.Map<UsuarioViewModel, Usuarios>(model);
            _userService.CadastrarUsuario(usuario);
        }

        public bool ValidarCpf(string cpf)
        {
            return _userService.ValidarCpf(cpf);
        }

        public int BuscarRegistro(string cpf)
        {
            return _userService.BuscarRegistroUsuario(cpf);
        }
    }
}
