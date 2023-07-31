using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Logs : EntityBase
    {
        public string Documento { get; set; }
        public string Metodo { get; set; }
        public int Linha { get; set; }
        public string Retorno { get; set; }
        public int StatusId { get; set; }
    }
}
