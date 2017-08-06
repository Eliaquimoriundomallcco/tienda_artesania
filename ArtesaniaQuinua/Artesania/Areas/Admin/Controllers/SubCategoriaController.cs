using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Artesania.Models;

namespace Artesania.Areas.Admin.Controllers
{
    public class SubCategoriaController : Controller
    {
        private DatabaseEntities1 db = new DatabaseEntities1();

        // GET: Admin/SubCategoria
        public ActionResult Index()
        {
            var subCategoria = db.SubCategoria.Include(s => s.Categoria);
            return View(subCategoria.ToList());
        }

        // GET: Admin/SubCategoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoria subCategoria = db.SubCategoria.Find(id);
            if (subCategoria == null)
            {
                return HttpNotFound();
            }
            return View(subCategoria);
        }

        // GET: Admin/SubCategoria/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categoria, "CategoriaId", "NombreCategoria");
            return View();
        }

        // POST: Admin/SubCategoria/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubCategoriaId,NombreSubCategoria,CategoriaId,Descripcion,Activo")] SubCategoria subCategoria)
        {
            if (ModelState.IsValid)
            {
                db.SubCategoria.Add(subCategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categoria, "CategoriaId", "NombreCategoria", subCategoria.CategoriaId);
            return View(subCategoria);
        }

        // GET: Admin/SubCategoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoria subCategoria = db.SubCategoria.Find(id);
            if (subCategoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(db.Categoria, "CategoriaId", "NombreCategoria", subCategoria.CategoriaId);
            return View(subCategoria);
        }

        // POST: Admin/SubCategoria/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubCategoriaId,NombreSubCategoria,CategoriaId,Descripcion,Activo")] SubCategoria subCategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subCategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(db.Categoria, "CategoriaId", "NombreCategoria", subCategoria.CategoriaId);
            return View(subCategoria);
        }

        // GET: Admin/SubCategoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoria subCategoria = db.SubCategoria.Find(id);
            if (subCategoria == null)
            {
                return HttpNotFound();
            }
            return View(subCategoria);
        }

        // POST: Admin/SubCategoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubCategoria subCategoria = db.SubCategoria.Find(id);
            db.SubCategoria.Remove(subCategoria);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
