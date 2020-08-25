using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComparadorPrecios.Modelos;
using NSwag.Annotations;

namespace ComparadorPrecios.Controllers
{
    /// <summary>
    /// Controlador de Tiendas.
    /// Contiene los endpoints de la API REST de Tiendas.
    /// 
    /// Creado por: Santiago Barquero - 2º DAM
    /// Fecha: Junio-2020
    /// </summary>
    [OpenApiTag("Tiendas", Description = "Web API para mantenimiento de Tiendas.")]
    [Route("api/[controller]")]
    [ApiController]
    public class TiendasController : ControllerBase
    {
        private readonly BdContext _context;

        public TiendasController(BdContext context)
        {
            _context = context;
        }

        // GET: api/Tiendas
        /// <summary>
        /// Devuelve lista de Tiendas en formato resumido (Id y Nombre) que contiene una cadena buscada.
        /// </summary>
        /// <param name="buscar">Cadena buscada en los nombre de tienda</param>
        /// <returns>Devuelve listado de tiendas que contiene una cadena buscada.</returns>
        [HttpGet]
        public IEnumerable<TiendaResumen> GetTienda([FromQuery] string buscar)
        {
            buscar = buscar ?? "";
            return _context.Tienda.Where(x => x.Nombre.ToUpper().Contains(buscar.ToUpper()))
                .Select(s => new TiendaResumen()
                {
                    Id = s.Id,
                    Nombre = s.Nombre
                }).OrderBy(o => o.Nombre);
        }

        // GET: api/Tiendas/5        
        /// <summary>
        /// Devuelve datos de la Tienda con Id
        /// </summary>
        /// <remarks>Devuelve una Tienda dado un Id</remarks>
        /// <param name="id">Id de la tienda</param>
        /// <returns>Devuelve tienda con Id</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Tienda>> GetTienda(int id)
        {
            var tienda = await _context.Tienda.FindAsync(id);

            if (tienda == null)
            {
                return NotFound();
            }

            return tienda;
        }

        // PUT: api/Tiendas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Modifica Tienda dado un Id
        /// </summary>
        /// <param name="id">Id de la tienda</param>
        /// <param name="tienda">Objeto tienda modificada</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTienda(int id, Tienda tienda)
        {
            if (id != tienda.Id)
            {
                return BadRequest();
            }

            _context.Entry(tienda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TiendaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tiendas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Crea una Tienda y devuelve la Tienda creada
        /// </summary>
        /// <param name="tienda">Objeto tienda que se va a crear</param>
        /// <returns>Devuelve la tienda creada</returns>
        [HttpPost]
        public async Task<ActionResult<Tienda>> PostTienda(Tienda tienda)
        {
            _context.Tienda.Add(tienda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTienda", new { id = tienda.Id }, tienda);
        }

        // DELETE: api/Tiendas/5
        /// <summary>
        /// Borra una tienda dado un Id
        /// </summary>
        /// <param name="id">Id de la tienda</param>
        /// <returns>Devuelve la tienda borrada</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tienda>> DeleteTienda(int id)
        {
            var tienda = await _context.Tienda.FindAsync(id);
            if (tienda == null)
            {
                return NotFound();
            }

            _context.Tienda.Remove(tienda);
            await _context.SaveChangesAsync();

            return tienda;
        }

        private bool TiendaExists(int id)
        {
            return _context.Tienda.Any(e => e.Id == id);
        }
    }
}
