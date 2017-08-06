﻿using System;
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
    public class ProductoController : Controller
    {
        private DatabaseEntities1 db = new DatabaseEntities1();

        // GET: Admin/Producto
        public ActionResult Index()
        {
            var producto = db.Producto.Include(p => p.SubCategoria);
            return View(producto.ToList());
        }

        // GET: Admin/Producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Admin/Producto/Create
        public ActionResult Create()
        {
            ViewBag.SubCategoriaId = new SelectList(db.SubCategoria, "SubCategoriaId", "NombreSubCategoria");
            return View();
        }

        // POST: Admin/Producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductoId,NombreProducto,SubCategoriaId,Descripcion,Puntos,Existencias,Activo")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubCategoriaId = new SelectList(db.SubCategoria, "SubCategoriaId", "NombreSubCategoria", producto.SubCategoriaId);
            return View(producto);
        }

        // GET: Admin/Producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubCategoriaId = new SelectList(db.SubCategoria, "SubCategoriaId", "NombreSubCategoria", producto.SubCategoriaId);
            return View(producto);
        }

        // POST: Admin/Producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductoId,NombreProducto,SubCategoriaId,Descripcion,Puntos,Existencias,Activo")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubCategoriaId = new SelectList(db.SubCategoria, "SubCategoriaId", "NombreSubCategoria", producto.SubCategoriaId);
            return View(producto);
        }

        // GET: Admin/Producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Admin/Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            db.Producto.Remove(producto);
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
