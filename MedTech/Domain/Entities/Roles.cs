using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Roles
    {
        [Key]
        public int? RoleId { get; set; }
        public string Descricao { get; set; }
        public int Autorizacao { get; set; }
    }
}
