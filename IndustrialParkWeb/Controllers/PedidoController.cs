using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Schema;
using IndustrialParkWeb.Models;

namespace IndustrialParkWeb.Controllers
{
    public class PedidoController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Pedido
        public IHttpActionResult GetPedidoes()
        {   
            var posts = db.Pedidoes
                           //.Where(p => p.Tags == "<sql-server>")
                           .Select(p => new
                           {
                               code = p.Numero,
                               name = p.Total,                               
                               price = p.ClienteID
                           });
            var rpta = new { productList = posts };
            return Ok(rpta);
        }

        // GET: api/Pedido/5
        [ResponseType(typeof(Pedido))]
        public IHttpActionResult GetPedido(int id)
        {
            Pedido pedido = db.Pedidoes.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        // PUT: api/Pedido/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPedido(int id, Pedido pedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pedido.ID)
            {
                return BadRequest();
            }

            db.Entry(pedido).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Pedido
        //[ResponseType(typeof(Pedido))]
        public IHttpActionResult PostPedido(PedidoLight pedidolight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pedido = ConstruirPedido(pedidolight);
            db.Pedidoes.Add(pedido);
            db.SaveChanges();

            var p = db.Pedidoes
                    .Where(b => b.Numero == pedido.Numero)
                    .FirstOrDefault();
            var rpta = new  {
                               code = p.Numero,
                               name = p.Total,
                               email = p.Cliente.Correo,
                               reference = p.DireccionEntregaReferencia
                            };            
            return Ok(rpta);
            //return CreatedAtRoute("DefaultApi", new { id = pedido.ID }, pedido);
        }

        // DELETE: api/Pedido/5
        [ResponseType(typeof(Pedido))]
        public IHttpActionResult DeletePedido(int id)
        {
            Pedido pedido = db.Pedidoes.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }

            db.Pedidoes.Remove(pedido);
            db.SaveChanges();

            return Ok(pedido);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PedidoExists(int id)
        {
            return db.Pedidoes.Count(e => e.ID == id) > 0;
        }

        private Pedido ConstruirPedido(PedidoLight pedidoLight) {
            Pedido pedido = new Pedido(true);
            int numeroPedido = db.Pedidoes.Count();
            if (numeroPedido == 0) numeroPedido = 1000000; ///1 millón. Si hacen más de 1 millón, habrá problemas
            else numeroPedido = 1000000 + numeroPedido + 1;

            pedido.Numero = string.Format("P{0}A", numeroPedido);
            pedido.DireccionEntrega = pedidoLight.address;
            pedido.DireccionEntregaReferencia = pedidoLight.reference;

            pedido.Cliente = RegistrarCliente(pedidoLight);
            foreach (DetallePedidoLight itemLight in pedidoLight.products)
            {
                DetallePedido item = new DetallePedido();
                item.Cantidad = itemLight.quantity;
                var producto = db.Productoes
                    .Where(b => b.Codigo == itemLight.code)
                    .FirstOrDefault();
                item.Producto = producto;
                item.PrecioUnitario = producto.PrecioUnitario;
                item.Total = Math.Round(item.Cantidad * item.Producto.PrecioUnitario,2);
                pedido.Items.Add(item);
            }

            pedido.CantidadItems = pedido.Items.Count();
            pedido.Total = pedidoLight.totalAmount;
            pedido.SubTotal = Math.Round(pedido.Total / 1.18M);
            pedido.Impuesto = pedido.Total - pedido.SubTotal;
            return pedido;
        }

        private Cliente RegistrarCliente(PedidoLight pedidoLight) {
            Cliente cliente = new Cliente();
            cliente.DocIdentidad = pedidoLight.clientCode;
            cliente.Correo = pedidoLight.email;
            cliente.Celular = pedidoLight.phoneNumber;
            cliente.RazonSocial = "Varios";
            db.Clientes.Add(cliente);
            return cliente;
        }
    }
}