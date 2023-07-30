using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface ILogRepository : IRepository<Logs>
    {
        void AddLog(string documento, string metodo, int linha, string retorno, int status);
    }
}
