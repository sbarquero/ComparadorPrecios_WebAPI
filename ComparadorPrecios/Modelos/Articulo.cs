using System;
using System.Collections.Generic;

namespace ComparadorPrecios.Modelos
{
    /// <summary>
    /// Contiene la clase Artículo.
    /// Esta clase contiene los datos de los Artículos y se utiliza para crear los objetos enviados y devueltos por la web API.
    /// 
    /// Creado por: Santiago Barquero - 2º DAM
    /// Fecha: Junio-2020
    /// </summary>
    public class Articulo
    {
        public int? Id { get; set; }
        public string Descripcion { get; set; }
        public string Ean { get; set; }
        public DateTime FechaAlta { get; set; }
        public List<Precio> Precios { get; set; } = new List<Precio>();
        public string Imagen { get; set; }

    }
}
