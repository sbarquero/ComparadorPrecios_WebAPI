using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComparadorPrecios.Modelos;
using NSwag.Annotations;

namespace ComparadorPrecios.Controllers
{
    /// <summary>
    /// Controlador de Precios.
    /// Contiene los endpoints de la API REST de Precios.
    /// 
    /// Creado por: Santiago Barquero - 2º DAM
    /// Fecha: Junio-2020
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [OpenApiTag("Precios", Description = "Web API para mantenimiento de los Precios.")]
    public class PreciosController : ControllerBase
    {
        private readonly BdContext _context;

        public PreciosController(BdContext context)
        {
            _context = context;
        }

        // GET: api/Precios        
        /// <summary>
        /// Devuelve lista con todos los precios
        /// </summary>
        /// <returns>Devuelve lista con todos los precios</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Precio>>> GetPrecio()
        {
            return await _context.Precio.ToListAsync();
        }

        // GET: api/Precios/5        
        /// <summary>
        /// Devuelve un precio dado un Id
        /// </summary>
        /// <param name="id">Id del precio</param>
        /// <returns>Devuelve un precio dado un Id</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Precio>> GetPrecio(int id)
        {
            var precio = await _context.Precio.FindAsync(id);

            if (precio == null)
            {
                return NotFound();
            }

            return precio;
        }

        // PUT: api/Precios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Modifica el Precio dado un Id
        /// </summary>
        /// <param name="id">Id del precio a modificar</param>
        /// <param name="precio"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrecio(int id, Precio precio)
        {
            if (id != precio.Id)
            {
                return BadRequest();
            }

            _context.Entry(precio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrecioExists(id))
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

        // POST: api/Precios
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.        
        /// <summary>
        /// Crea un Precio
        /// </summary>
        /// <param name="precio">Precio a crear</param>
        /// <returns>Devuelve el Precio creado</returns>
        [HttpPost]
        public async Task<ActionResult<Precio>> PostPrecio(Precio precio)
        {
            _context.Precio.Add(precio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrecio", new { id = precio.Id }, precio);
        }

        // DELETE: api/Precios/5        
        /// <summary>
        /// Borra un precio dado un Id
        /// </summary>
        /// <param name="id">Id del Precio a borrar</param>
        /// <returns>Devuelve el Precio borrado</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Precio>> DeletePrecio(int id)
        {
            var precio = await _context.Precio.FindAsync(id);
            if (precio == null)
            {
                return NotFound();
            }

            _context.Precio.Remove(precio);
            await _context.SaveChangesAsync();

            return precio;
        }

        private bool PrecioExists(int id)
        {
            return _context.Precio.Any(e => e.Id == id);
        }
    }
}
