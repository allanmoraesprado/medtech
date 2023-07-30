using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
{
    public class MedViewModel
    {
        [Required(ErrorMessage = "Informe o Nome do Médico!")]
        public string NomeMedico { get; set; }
        [Required(ErrorMessage = "Informe o nome do Paciente!")]
        public string NomePaciente { get; set; }
        public string CpfPaciente { get; set; }
        public string Detalhes { get; set; }
        public string DataCriacao { get; set; }
        public string DataAlteracao { get; set; }
        public int? Id { get; set; }
        public int? PacienteId { get; set; }
        public int? MedicoId { get; set; }
    }
}
