using System;

namespace ComparadorPrecios.Modelos
{
    /// <summary>
    /// Contiene la clase Precio.
    /// Esta clase contiene los datos de los precios y se utiliza para crear los objetos enviados y devueltos por la web API.
    /// 
    /// Creado por: Santiago Barquero - 2º DAM
    /// Fecha: Junio-2020
    /// </summary>
    public class Precio
    {
        public int? Id { get; set; }
        public decimal Importe { get; set; }
        public DateTime Fecha { get; set; }

        public int ArticuloId { get; set; }
        public int TiendaId { get; set; }

    }
}
