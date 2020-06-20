using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IndustrialParkWeb.Models
{
    public class Producto
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [DisplayName("Código")]
        [Required(ErrorMessage = "*Requerido")]
        public string Codigo { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "*Requerido")]
        public string Descripcion { get; set; }

        public string Imagen { get; set; }

        [DisplayName("Precio unitario")]
        public decimal PrecioUnitario { get; set; }

        public bool Activo { get; set; }

        [NotMapped]
        //[Required(ErrorMessage = "*Requerido")]
        public virtual HttpPostedFileBase UploadedFile { get; set; }

    }
}