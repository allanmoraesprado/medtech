using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Infraestructure.StatusSistema
{
    public enum TipoRole
    {
        [Description("INDEFINIDO")]
        Indefinido = 0,
        [Description("Paciente")]
        PACIENTE = 1,
        [Description("Médico")]
        MEDICO = 2
    }
}
