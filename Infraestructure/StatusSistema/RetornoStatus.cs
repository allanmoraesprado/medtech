using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Infraestructure.StatusSistema
{
    public enum RetornoStatus
    {
        [Description("INDEFINIDO")]
        Indefinido = 0,
        [Description("ERRO")]
        Erro = 1,
        [Description("SUCESSO")]
        Sucesso = 2,
        [Description("AVISO")]
        Aviso = 3,
    }
}
