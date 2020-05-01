using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Curso2020.Management.Api.Controllers.CargaMasiva
{
	[Route("api/[controller]")]
	[ApiController]
	[EnableCors("_CORS_")]
	public class CargaMasivaDeDatosController : ControllerBase
	{
	}
}