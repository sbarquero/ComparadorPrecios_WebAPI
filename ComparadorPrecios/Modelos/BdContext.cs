using Microsoft.EntityFrameworkCore;
using ComparadorPrecios.Modelos;

namespace ComparadorPrecios.Modelos
{
    /// <summary>
    /// Contiene el contexto para acceso a la base de datos y las tablas.
    /// 
    /// Creado por: Santiago Barquero - 2º DAM
    /// Fecha: Junio-2020
    /// </summary>

    public class BdContext : DbContext
    {
        public BdContext(DbContextOptions<BdContext> options) : base(options)
        {
        }

        public DbSet<ComparadorPrecios.Modelos.Articulo> Articulo { get; set; }

        public DbSet<ComparadorPrecios.Modelos.Tienda> Tienda { get; set; }

        public DbSet<ComparadorPrecios.Modelos.Precio> Precio { get; set; }

    }
}