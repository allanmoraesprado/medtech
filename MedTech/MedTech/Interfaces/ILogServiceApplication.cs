using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILogServiceApplication
    {
        void AddLogServer(string documento, string metodo, int linha, string retorno, int status);
    }
}
