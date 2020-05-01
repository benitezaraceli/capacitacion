using Curso.Model.Context;
using Curso2020.Common.DTO;
using Curso2020.Model.Model;
using Curso2020.Service.Empresa.ABM.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso2020.Service.Empresa.ABM
{
    public class AbmServiceAsync : IAbmServiceAsync
    {
        private readonly ILogger<AbmServiceAsync> _logger;
        private readonly CursoContext _empresaContext;

        public AbmServiceAsync(ILogger<AbmServiceAsync> logger, CursoContext empresaContext)
        {
            _logger = logger;
            _empresaContext = empresaContext;
            _logger.LogInformation("Constructor AbmServiceAsync");
        }

        public async Task<Model.Model.Empresa> AltaEmpresa(EmpresaDTO empresaDTO)
        {
            var dbEmpresa = await _empresaContext.Empresas.Where(e => e.cuit == empresaDTO.Cuit).FirstOrDefaultAsync();
            if (dbEmpresa == null)
            {
                Model.Model.Empresa empresaAlta = new Model.Model.Empresa();
                empresaAlta.cuit = empresaDTO.Cuit;
                empresaAlta.nombre = empresaDTO.Nombre;
                empresaAlta.razonSocial = empresaDTO.RazonSocial;
                empresaAlta.direccion = empresaDTO.Direccion;
              

                _empresaContext.Empresas.Add(empresaAlta);

                List<Puesto>dbPuestos =  await _empresaContext.Puestos.ToListAsync();

                foreach( Puesto puesto in dbPuestos)
                {
                    PuestoEmpresa puestoEmpresa = new PuestoEmpresa();
                    puestoEmpresa.puestoId=puesto.id  ;
                    puestoEmpresa.pagoPorHora= puesto.salarioPorDefecto ;
                    puestoEmpresa.empresaCuit = empresaAlta.cuit;

                    //_empresaContext.PuestosEmpresas.Add(puestoEmpresa);

                    await _empresaContext.SaveChangesAsync();
                }
          
                await _empresaContext.SaveChangesAsync();
                return empresaAlta;
            }
            else
                return null;

        }
    }
}
