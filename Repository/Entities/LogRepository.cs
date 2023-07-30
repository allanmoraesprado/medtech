using System.Text;
using Microsoft.EntityFrameworkCore;
using Domain.Repositories;
using System.Linq;
using Domain.Entities;

namespace Repository.Entities
{
    public class LogRepository : Repository<Logs>, ILogRepository
    {
        public LogRepository(DbContext dbContext) : base(dbContext) { }

        public void AddLog(string documento, string metodo, int linha, string retorno, int status)
        {
            try
            {
                var log = new Logs
                {
                    Documento = documento,
                    Metodo = metodo,
                    Linha = linha,
                    Retorno = retorno,
                    StatusId = status
                };
                _dbContext.Add(log);
                _dbContext.SaveChanges();
            }
            catch
            {

            }
        }
    }
}
