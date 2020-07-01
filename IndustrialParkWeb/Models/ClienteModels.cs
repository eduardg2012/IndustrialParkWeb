using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndustrialParkWeb.Models
{
    public class Cliente
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [DisplayName("N° Documento")]
        [Required(ErrorMessage = "*Requerido")]
        public string DocIdentidad { get; set; }

        [DisplayName("Nombre Completo")]
        [Required(ErrorMessage = "*Requerido")]
        public string RazonSocial { get; set; }

        [DisplayName("Correo"), EmailAddress]
        [Required(ErrorMessage = "*Requerido")]

        public string Correo { get; set; }

        [DisplayName("N° Celular")]
        [Required(ErrorMessage = "*Requerido")]
        public string Celular { get; set; }
    }
}