using IndustrialParkWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndustrialParkWeb.Controllers
{
    public class ProductoWebController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ProductoWeb
        public ActionResult Index()
        {
            var entidades = db.Productoes.ToList();
                           //.Where(p => p.Tags == "<sql-server>")
                           //.Select(p => new Producto
                           //{
                           //    Codigo = p.Codigo,
                           //    Descripcion = p.Descripcion,
                           //    Imagen = p.Imagen,
                           //    PrecioUnitario = p.PrecioUnitario
                           //});
                        
            //var listOfIdeas = (from x in db.Productoes select x.ID).ToList();
            //return Ok(posts);
            return View(entidades);
        }

        // GET: ProductoWeb/Details/5
        public ActionResult Details(int id)
        {
            var entidad = db.Productoes.Find(id);
            return View(entidad);            
        }

        // GET: ProductoWeb/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductoWeb/Create
        [HttpPost]
        //public ActionResult Create(FormCollection collection)
        public ActionResult Create(Producto entidad)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(entidad);

                // TODO: Add insert logic here
                if (entidad.UploadedFile!=null)
                {
                    string img = System.IO.Path.GetFileName(entidad.UploadedFile.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/Imagenes"), img);
                    entidad.UploadedFile.SaveAs(path);
                    entidad.Imagen = System.IO.Path.Combine("\\Imagenes",img);
                    db.Productoes.Add(entidad);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: ProductoWeb/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                // TODO: Add update logic here                
                var entidad = db.Productoes.Find(id);
                return View(entidad);
            }
            catch
            {
                return View();
            }            
        }

        // POST: ProductoWeb/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Producto entidad)
        {
            try
            {
                // TODO: Add update logic here
                if (!ModelState.IsValid)                
                    return View(entidad);
                
                if (entidad.UploadedFile != null)
                {
                    string img = System.IO.Path.GetFileName(entidad.UploadedFile.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/Imagenes"), img);
                    entidad.UploadedFile.SaveAs(path);
                    entidad.Imagen = "/Imagenes/"+img;//System.IO.Path.Combine("/Imagenes", img);                    
                    db.Productoes.AddOrUpdate(entidad);
                    db.SaveChanges();
                }                
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: ProductoWeb/Delete/5
        public ActionResult Delete(int id)
        {
            var entidad = db.Productoes.Find(id);
            return View(entidad);            
        }

        // POST: ProductoWeb/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Producto entidad)
        {
            try
            {
                // TODO: Add delete logic here
                var entidad1 = db.Productoes.Find(id);
                db.Productoes.Remove(entidad1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
