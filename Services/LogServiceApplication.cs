using Application.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LogServiceApplication : ILogServiceApplication
    {
        private readonly ILogService _logService;
        public LogServiceApplication(ILogService logService)
        {
            _logService = logService;
        }
        public void AddLogServer(string documento, string metodo, int linha, string retorno, int status)
        {
            _logService.AddLog(documento, metodo, linha, retorno, status);
        }
    }
}
