using Curso2020.Common.DTO;
using Curso2020.Service.Empresa.ABM.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Curso2020.Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly ILogger<EmpresaController> _logger;
        private readonly IAbmServiceAsync _abmServiceAsync;

        public EmpresaController(ILogger<EmpresaController> logger, IAbmServiceAsync abmServiceAsync)
        {
            _logger = logger;
            _logger.LogInformation("Constructor EmpresaController");
            _abmServiceAsync = abmServiceAsync;
        }


        //POST: api/Empresa/Alta
        [HttpPost("Alta")]

        public async Task <ActionResult> Alta([FromBody] EmpresaDTO empresaDTO)
        {
            if (empresaDTO.Cuit == null || empresaDTO.Cuit.Length != 11)
            {
                return BadRequest(new ResultJson() { Message = "Los valores ingresados no corresponden a un CUIT valido" });
               
            }
            else
            {
                if (empresaDTO.Nombre == null || empresaDTO.Nombre == "" || empresaDTO.RazonSocial == null || empresaDTO.RazonSocial == ""
                    || empresaDTO.Direccion == null || empresaDTO.Direccion == "")
                    return BadRequest(new ResultJson() { Message = "complete TODOS los campos" });
            }
           

            var businessCreateDB = await _abmServiceAsync.AltaEmpresa(empresaDTO);

            if (businessCreateDB != null)
            {
                return Created("", new ResultJson() { Message = "Empresa dada de ALTA correctamente" });
            }
            else
            {
                return BadRequest(new ResultJson() { Message = "Empresa EXISTENTE" });
            }

        }
    }
}
