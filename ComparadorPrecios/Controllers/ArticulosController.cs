using ComparadorPrecios.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComparadorPrecios.Controllers
{
    /// <summary>
    /// Controlador de Artículos.
    /// Contiene los endpoints de la API REST de Artículos.
    /// 
    /// Creado por: Santiago Barquero - 2º DAM
    /// Fecha: Junio-2020
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [OpenApiTag("Articulos", Description = "Web API para mantenimiento de Artículos.")]
    public class ArticulosController : ControllerBase
    {
        private readonly BdContext _context;

        public ArticulosController(BdContext context)
        {
            _context = context;
        }

        // GET: api/Articulos
        // GET: api/Articulos/buscar?cadena_a_buscar
        /// <summary>
        /// Devuelve lista de Artículos que contengan la cadena buscada en la descripción y en el código de barras
        /// </summary>
        /// <remarks>Devuelve una lista de todos los Artículos en formato resumido (solo id, descripción y código EAN).</remarks>
        /// <param name="buscar">Cadena buscada</param>
        /// <returns>Devuelve lista de Artículos que contengan la cadena buscada en la descripción y en el código de barras</returns>
        [HttpGet]
        public IEnumerable<ArticuloResumen> GetArticulos([FromQuery] string buscar)
        {
            if (buscar == null)
            {
                buscar = "";
            }
            // https://stackoverflow.com/questions/6370028/return-list-using-select-new-in-linq
            var result = _context.Articulo.Where(x => x.Descripcion.ToUpper().Contains(buscar.ToUpper()) || x.Ean.Contains(buscar))
                                .Select(s => new ArticuloResumen()
                                {
                                    Id = s.Id,
                                    Descripcion = s.Descripcion,
                                    Ean = s.Ean
                                })
                                .OrderBy(o => o.Descripcion);
            return result;
        }

        // GET: api/Articulos/5        
        /// <summary>
        /// Devuelve un Artículo dado un Id
        /// </summary>
        /// <param name="id">Id del artículo</param>
        /// <returns>Devuelve el artículo que corresponde al Id dado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Articulo>> GetArticulo(int id)
        {
            var articulo = await _context.Articulo.FindAsync(id);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.Precios = _context.Precio.Where(w => w.ArticuloId == articulo.Id).OrderBy(o => o.Id).ToList();

            return articulo;
        }

        // GET: api/Articulos/EAN/5        
        /// <summary>
        /// Devuelve un Artículo dado un EAN o código de barras
        /// </summary>
        /// <param name="ean">Código de barrasa</param>
        /// <returns>Devuelve el Artículo con el código de barras o EAN dado</returns>
        [HttpGet("EAN/{ean}")]
        public ActionResult<Articulo> GetArticuloEan(string ean)
        {
            var articulo = _context.Articulo.Where(x => x.Ean == ean).FirstOrDefault();

            if (articulo == null)
            {
                return NotFound();
            }

            return articulo;
        }



        // PUT: api/Articulos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.        
        /// <summary>
        /// Modifica un Artículo dado un Id
        /// </summary>
        /// <param name="id">Id del artículo a modificar</param>
        /// <param name="articulo">Artículo modificado</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticulo(int id, Articulo articulo)
        {
            if (id != articulo.Id)
            {
                return BadRequest();
            }

            _context.Entry(articulo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticuloExists(id))
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



        // POST: api/Articulos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.        
        /// <summary>
        /// Crea un Artículo
        /// </summary>
        /// <param name="articulo">El Artículo</param>
        /// <returns>Devuelve el Artículo creado</returns>
        [HttpPost]
        public async Task<ActionResult<Articulo>> PostArticulo(Articulo articulo)
        {
            _context.Articulo.Add(articulo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticulo", new { id = articulo.Id }, articulo);
        }



        // DELETE: api/Articulos/5        
        /// <summary>
        /// Borra un Artículo dado un Id
        /// </summary>
        /// <param name="id">Id del artículo a borrar</param>
        /// <returns>Devuelve el artículo borrado</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Articulo>> DeleteArticulo(int id)
        {
            var articulo = await _context.Articulo.FindAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }

            _context.Articulo.Remove(articulo);
            await _context.SaveChangesAsync();

            return articulo;
        }

        private bool ArticuloExists(int id)
        {
            return _context.Articulo.Any(e => e.Id == id);
        }
    }
}
