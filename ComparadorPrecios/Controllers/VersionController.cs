using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace ComparadorPrecios.Controllers
{
    /// <summary>
    /// Controlador de Versión.
    /// Contiene el endpoint que devuelve la versión de la web API.
    /// 
    /// Creado por: Santiago Barquero - 2º DAM
    /// Fecha: Junio-2020
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [OpenApiTag("Version", Description = "Informa de la versión de la Web API")]
    public class VersionController : ControllerBase
    {
        // GET: api/Version
        /// <summary>
        /// Devuelve la versión de la Web API ComparadorPrecios.
        /// </summary>
        /// <returns>Devuelve la versión de la Web API ComparadorPrecios.</returns>
        [HttpGet]
        public string GetVersion()
        {
            return "1.0";
        }
    }
}