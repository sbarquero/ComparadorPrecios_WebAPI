using System;
using System.Collections.Generic;

namespace ComparadorPrecios.Modelos
{
    /// <summary>
    /// Contiene la clase ArtículoResumen.
    /// Esta clase contiene los datos resumidos de los Artículos y se utiliza para devolver la lista de artículos buscados.
    /// 
    /// Creado por: Santiago Barquero - 2º DAM
    /// Fecha: Junio-2020
    /// </summary>
    public class ArticuloResumen
    {
        public int? Id { get; set; }
        public string Descripcion { get; set; }
        public string Ean { get; set; }
    }
}
