using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Artesania.Models;
using System.IO;

namespace Artesania.Areas.Admin.Controllers
{
    public class UsuarioController : Controller
    {
        private DatabaseEntities2 db = new DatabaseEntities2();

        // GET: Admin/Usuario
        public ActionResult Index()
        {
            return View(db.Usuario.ToList());
        }

        // GET: Admin/Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Admin/Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsuarioID,Usuario1,Clave,Correo,Activo")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Admin/Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Admin/Usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsuarioID,Usuario1,Clave,Correo,Activo")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Admin/Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Admin/Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
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




        public JsonResult Adjuntar(int UsuarioID, HttpPostedFileBase documento)
        {
            var respuesta = new Models.ResponseModel
            {
                respuesta = true,
                error = ""
            };

            if (documento != null)
            {
                string adjunto = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(documento.FileName);
                documento.SaveAs(Server.MapPath("~/ImgProductos/" + adjunto));

                db.UsuarioImagen.Add(new UsuarioImagen { UsuarioId = UsuarioID, Imagen = adjunto, Titulo = "Ejemplo", Descripcion = "Ejemplo" });
                db.SaveChanges();

            }
            else
            {
                respuesta.respuesta = false;
                respuesta.error = "Debe adjuntar un documento";
            }

            return Json(respuesta);
        }

        public PartialViewResult Adjuntos(int UsuarioId)
        {
            return PartialView(db.UsuarioImagen.Where(x => x.UsuarioId == UsuarioId).ToList());
        }

        public JsonResult EliminarImagen(int UsuarioImagenId)
        {
            var rpt = new Models.ResponseModel()
            {
                respuesta = true,
                error = ""
            };
            var img = db.UsuarioImagen.Find(UsuarioImagenId);

            if (System.IO.File.Exists(Server.MapPath("~/ImgProductos/" + img.Imagen)))
                System.IO.File.Delete(Server.MapPath("~/ImgProductos/" + img.Imagen));

            db.UsuarioImagen.Remove(img);
            db.SaveChanges();

            return Json(rpt);
        }



    }
}
