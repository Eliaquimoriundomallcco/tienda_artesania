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
    public class PedidoDetalleController : Controller
    {
        private DatabaseEntities2 db = new DatabaseEntities2();

        // GET: Admin/PedidoDetalle
        public ActionResult Index()
        {
            var pedidoDetalle = db.PedidoDetalle.Include(p => p.Pedido).Include(p => p.Producto);
            return View(pedidoDetalle.ToList());
        }

        // GET: Admin/PedidoDetalle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoDetalle pedidoDetalle = db.PedidoDetalle.Find(id);
            if (pedidoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(pedidoDetalle);
        }

        // GET: Admin/PedidoDetalle/Create
        public ActionResult Create()
        {
            ViewBag.PedidoId = new SelectList(db.Pedido, "PedidoId", "Estado");
            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoId", "NombreProducto");
            return View();
        }

        // POST: Admin/PedidoDetalle/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PedidoDetalle1,PedidoId,ProductoId,Cantidad")] PedidoDetalle pedidoDetalle)
        {
            if (ModelState.IsValid)
            {
                db.PedidoDetalle.Add(pedidoDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PedidoId = new SelectList(db.Pedido, "PedidoId", "Estado", pedidoDetalle.PedidoId);
            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoId", "NombreProducto", pedidoDetalle.ProductoId);
            return View(pedidoDetalle);
        }

        // GET: Admin/PedidoDetalle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoDetalle pedidoDetalle = db.PedidoDetalle.Find(id);
            if (pedidoDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.PedidoId = new SelectList(db.Pedido, "PedidoId", "Estado", pedidoDetalle.PedidoId);
            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoId", "NombreProducto", pedidoDetalle.ProductoId);
            return View(pedidoDetalle);
        }

        // POST: Admin/PedidoDetalle/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PedidoDetalle1,PedidoId,ProductoId,Cantidad")] PedidoDetalle pedidoDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedidoDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PedidoId = new SelectList(db.Pedido, "PedidoId", "Estado", pedidoDetalle.PedidoId);
            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoId", "NombreProducto", pedidoDetalle.ProductoId);
            return View(pedidoDetalle);
        }

        // GET: Admin/PedidoDetalle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoDetalle pedidoDetalle = db.PedidoDetalle.Find(id);
            if (pedidoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(pedidoDetalle);
        }

        // POST: Admin/PedidoDetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PedidoDetalle pedidoDetalle = db.PedidoDetalle.Find(id);
            db.PedidoDetalle.Remove(pedidoDetalle);
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
