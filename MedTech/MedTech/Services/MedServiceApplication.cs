using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using Infraestructure.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MedServiceApplication : IMedServiceApplication
    {
        private readonly IMedService _medService;
        public MedServiceApplication(IMedService medService)
        {
            _medService = medService;
        }
        public IEnumerable<FichaViewModel> FichasPaciente(int id)
        {
            IEnumerable<FichaViewModel> listModel = null;
            if(id > 0)
            {
                var fichaList = _medService.BuscarFichasPaciente(id);
                listModel = Mappers.MapEnumerable<Fichas, FichaViewModel>(fichaList);
            }
            return listModel;
        }
        public IEnumerable<FichaViewModel> FichasMedico(int id)
        {
            IEnumerable<FichaViewModel> listModel = null;
            if (id > 0)
            {
                var fichaList = _medService.BuscarFichasMedico(id);
                listModel = Mappers.MapEnumerable<Fichas, FichaViewModel>(fichaList);
            }
            return listModel;
        }
        public void Cadastrar(FichaViewModel model)
        {
            var ficha = Mappers.Map<FichaViewModel, Fichas>(model);
            _medService.CadastrarFicha(ficha);
        }
        public void Update(FichaViewModel model)
        {
            var ficha = Mappers.Map<FichaViewModel, Fichas>(model);
            _medService.AlterarFicha(ficha);
        }
        public void Deletar(int id)
        {
            _medService.DeletarFicha(id);
        }
    }
}
