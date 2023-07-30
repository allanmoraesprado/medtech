using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o Usuário!")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Informe a Senha!")]
        public string Senha { get; set; }
    }
}
