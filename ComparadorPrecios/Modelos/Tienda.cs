using System;
using System.Collections.Generic;

namespace ComparadorPrecios.Modelos
{
    /// <summary>
    /// Contiene la clase Tienda.
    /// Esta clase contiene los datos de las Tiendas y se utiliza para crear los objetos enviados y devueltos por la web API.
    /// 
    /// Creado por: Santiago Barquero - 2º DAM
    /// Fecha: Junio-2020
    /// </summary>
    public class Tienda
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
