using Domain.Interfaces;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class LogService : ILogService
    {
        readonly ILogRepository _logRepository;
        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public void AddLog(string documento, string metodo, int linha, string retorno, int status)
        {
            _logRepository.AddLog(documento, metodo, linha, retorno, status);
        }
    }
}
