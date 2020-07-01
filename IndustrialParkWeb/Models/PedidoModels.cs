using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IndustrialParkWeb.Models
{
    public class Pedido
    {
        public Pedido() {
            this.Items = new List<DetallePedido>();
            this.Items.Add(new DetallePedido { Cantidad = 1, Total = 0 });
            this.Items.Add(new DetallePedido { Cantidad = 1, Total = 0 });
            this.CantidadItems = this.Items.Count;
        }

        public Pedido(bool api)
        {
            this.FechaEntrega = DateTime.Now;
            this.FechaRegistro = DateTime.Now;
            this.Items = new List<DetallePedido>();            
            this.CantidadItems = this.Items.Count;
        }

        [Key]
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [DisplayName("Pedido N°")]        
        public string Numero { get; set; }
        
        [DisplayName("Precio unitario")]
        public decimal SubTotal { get; set; }

        [DisplayName("Impuesto")]
        public decimal Impuesto { get; set; }

        [DisplayName("Total")]
        [Required(ErrorMessage = "*Requerido")]
        public decimal Total { get; set; }

        [DisplayName("Latitud")]
        [Required(ErrorMessage = "*Requerido")]
        public decimal Latitud { get; set; }

        [DisplayName("Longitud")]
        [Required(ErrorMessage = "*Requerido")]
        public decimal Longitud { get; set; }

        [DisplayName("Fecha de Entrega")]
        [Required(ErrorMessage = "*Requerido")]
        [DataType(DataType.Date)]
        public DateTime FechaEntrega { get; set; }

        [DisplayName("Cliente")]
        public virtual Cliente Cliente { get; set; }

        public int? ClienteID { get; set; }

        [DisplayName("Detalle")]
        public virtual List<DetallePedido> Items { get; set; }

        [DisplayName("FechaRegistro")]
        [ScaffoldColumn(false)]
        public DateTime FechaRegistro { get; set; }

        //[NotMapped]
        public bool Anulado { get; set; }

        [NotMapped]
        public int CantidadItems { get; set; }

        [DisplayName("Direccion de entrega")]
        public string DireccionEntrega { get; set; }

        [DisplayName("Referencia")]
        public string DireccionEntregaReferencia { get; set; }

    }

    public class DetallePedido
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [DisplayName("Producto")]
        public virtual Producto Producto { get; set; }

        public int? ProductoID { get; set; }

        [DisplayName("Cantidad")]
        public decimal Cantidad { get; set; }

        [DisplayName("Precio unitario")]
        public decimal PrecioUnitario { get; set; }

        [DisplayName("Total")]
        public decimal Total { get; set; }


        public int PedidoID { get; set; }
        public Pedido Pedido { get; set; }

    }

    public class PedidoLight
    {
        public PedidoLight()
        {
            
        }

        public string orderNumber { get; set; }

        public string clientCode { get; set; }

        public string phoneNumber { get; set; }

        public string email { get; set; }

        public string address { get; set; }

        public string reference { get; set; }

        public decimal totalAmount { get; set; }

        public List<DetallePedidoLight> products { get; set; }



    }

    public class DetallePedidoLight
    {
        public DetallePedidoLight()
        {

        }

        public string code { get; set; }

        public decimal quantity { get; set; }

    }
}