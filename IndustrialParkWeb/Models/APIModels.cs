using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IndustrialParkWeb.Models
{
    public class Producto
    {
        [Key]
        public int ID { get; set; }
        public string Codigo { get; set; }        
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public decimal PrecioUnitario { get; set; }

    }
}