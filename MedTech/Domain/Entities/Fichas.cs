using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Fichas : EntityBase
    {
        public string NomeMedico { get; set; }
        public string NomePaciente { get; set; }
        public string CpfPaciente { get; set; }
        public string DataCriacao { get; set; }
        public string DataAlteracao { get; set; }
        public int? PacienteId { get; set; }
        public int? MedicoId { get; set; }
        public string UsuarioPaciente { get; set; }
        public string Detalhes { get; set; }

        public string Descricao { get; set; }

    }
}
