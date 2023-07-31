using Domain.Entities;
using Domain.Interfaces;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class MedService : IMedService
    {
        readonly IMedRepository _medRepository;
        public MedService(IMedRepository medRepository)
        {
            _medRepository = medRepository;
        }
        public IEnumerable<Fichas> BuscarFichasMedico(int id)
        {
            return _medRepository.BuscarFichasMedico(id);
        }
        public IEnumerable<Fichas> BuscarFichasPaciente(int id)
        {
            return _medRepository.BuscarFichasPaciente(id);
        }
        public void CadastrarFicha(Fichas ficha)
        {
            ficha.DataCriacao = DateTime.Now.ToString();
            ficha.DataAlteracao = ficha.DataCriacao;
            _medRepository.Create(ficha);
        }
        public void AlterarFicha(Fichas ficha)
        {
            _medRepository.Update(ficha);
        }

        public void DeletarFicha(int id)
        {
            _medRepository.Delete(id);
        }
    }
}
