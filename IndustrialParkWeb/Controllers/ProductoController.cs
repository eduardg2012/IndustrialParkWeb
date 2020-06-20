﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using IndustrialParkWeb.Models;

namespace IndustrialParkWeb.Controllers
{
    public class ProductoController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Producto
        //public IQueryable<Producto> GetProductoes()
        //{
        //    return db.Productoes;             
        //}

        // GET: api/Producto
        //[ResponseType(typeof(List<Producto>))]
        public IHttpActionResult GetProductoes()
        {
            //var siteName = System.Web.Hosting.HostingEnvironment.SiteName;
            //string ruta = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
            String rutaImagenes = HelperUtil.ObtenerValorConfig("RutaImagenes").ToString();
            var posts = db.Productoes
                           //.Where(p => p.Tags == "<sql-server>")
                           .Select(p => new
                           {
                               code = p.Codigo,
                               name = p.Descripcion,
                               imageUrl = rutaImagenes + p.Imagen,
                               price = p.PrecioUnitario
                           });
            var rpta = new { productList = posts };
            return Ok(rpta);
        }

        // GET: api/Producto/5
        [ResponseType(typeof(Producto))]
        public IHttpActionResult GetProducto(int id)
        {
            Producto producto = db.Productoes.Find(id);
            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // PUT: api/Producto/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProducto(int id, Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != producto.ID)
            {
                return BadRequest();
            }

            db.Entry(producto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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

        // POST: api/Producto
        [ResponseType(typeof(Producto))]
        public IHttpActionResult PostProducto(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Productoes.Add(producto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = producto.ID }, producto);
        }

        // DELETE: api/Producto/5
        [ResponseType(typeof(Producto))]
        public IHttpActionResult DeleteProducto(int id)
        {
            Producto producto = db.Productoes.Find(id);
            if (producto == null)
            {
                return NotFound();
            }

            db.Productoes.Remove(producto);
            db.SaveChanges();

            return Ok(producto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductoExists(int id)
        {
            return db.Productoes.Count(e => e.ID == id) > 0;
        }
    }
}