using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage="Informe o Usuário!")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Informe a Senha!")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Confirme a senha!")]
        public string ConfirmacaoSenha { get; set; }
        [Required(ErrorMessage = "Informe o Email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe o Nome!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Informe o CPF!")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Informe o Telefone!")]
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public int? RoleId { get; set; }       
    }
}
