using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class EntityBase
    {
        [Key]
        public int? Id { get; set; }
    }
}
