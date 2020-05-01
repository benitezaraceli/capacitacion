using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using Curso2020.Service.ABMEmpleado.Interfaces;

namespace Curso2020.Management.Api.Controllers
{
    [Route("api/EmpleadoController")]
    [ApiController]
    [EnableCors("_CORS_")]
    public class EmpleadoController : ControllerBase
    {        
        private readonly ILogger<EmpleadoController> _logger;
        private readonly IEmployeeABMAsync _employeeABM;
        public EmpleadoController( ILogger<EmpleadoController> logger, IEmployeeABMAsync employeeABM )
        {
            this._logger = logger;
            _employeeABM = employeeABM;
            _logger.LogInformation("Constructor EmpleadoController");
        }
    }
}