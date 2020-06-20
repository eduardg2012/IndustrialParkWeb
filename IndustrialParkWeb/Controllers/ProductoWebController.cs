using IndustrialParkWeb.Models;
using System;
using System.Collections.Generic;
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
            return View();
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
                // TODO: Add insert logic here
                if (entidad.UploadedFile!=null)
                {
                    string img = System.IO.Path.GetFileName(entidad.UploadedFile.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/Imagenes"), img);
                    entidad.UploadedFile.SaveAs(path);
                    entidad.Imagen = path;
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
            return View();
        }

        // POST: ProductoWeb/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoWeb/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductoWeb/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
