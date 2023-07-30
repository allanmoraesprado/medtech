using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Usuarios : EntityBase
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Foto { get; set; }
        public int? RoleId { get; set; }      
    }
}
