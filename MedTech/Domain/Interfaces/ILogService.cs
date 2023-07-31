using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface ILogService
    {
        void AddLog(string documento, string metodo, int linha, string retorno, int status);
    }
}
