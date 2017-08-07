using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Artesania.Controllers
{

    public class HomeController : Controller
    {
        private Models.DatabaseEntities2 bd = new Models.DatabaseEntities2();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Chekout()
        {
            return View();
        }


        public ActionResult Producto(string id = "")
        {

            //var prod = (from ep in bd.Producto
            //            join e in bd.SubCategoria on ep.SubCategoriaId equals e.SubCategoriaId
            //            join t in bd.Categoria on e.CategoriaId equals t.CategoriaId
            //            where ep.NombreProducto == id
            //            select new
            //            {
            //               nombre = ep.NombreProducto,
            //                descripcion = ep.Descripcion,
            //                precio = ep.Puntos,
            //                descricpion = ep.NombreProducto,
            //                categoria = t.NombreCategoria,
            //                subcat = e.NombreSubCategoria

            //            });
            //ViewBag.clave = id;
            //var ppp = prod.ToList().Take(20);
            //return View(prod);
            

            var producto = bd.Producto
                .Where(x => x.NombreProducto.Contains(id) )
                .Take(20)
                .ToList();
            ViewBag.clave = id;
            return View(producto);

        }


        public ActionResult About()
        {
            return View();
        }


        public ActionResult Email()
        {
            return View();
        }


        public ActionResult Login(string usuario, string clave)
        {
            var u = bd.Cliente.FirstOrDefault(x => x.Usuario == usuario && x.Clave == clave);
            if (u != null)
            {
                Helper.SessionHelper.AddUserToSession(u.ClienteId.ToString());
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Helper.SessionHelper.DestroyUserSession();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult RegistrarCliente(Models.Cliente c)
        {
            bd.Cliente.Add(c);
            bd.SaveChanges();
            Helper.SessionHelper.AddUserToSession(c.ClienteId.ToString());
            return RedirectToAction("Index", "Home");
        }

        public static string ObtenerNombreUsuario()
        {
            using (var b = new Models.DatabaseEntities2())
            {
                return b.Cliente.Find(Helper.SessionHelper.GetUser()).Nombres;
            }
        }


        public ActionResult Faq()
        {
            return View();
        }
        public ActionResult Jeans()
        {
            return View();
        }
        public ActionResult Salwars()
        {
            return View();
        }
        public ActionResult Sandals()
        {
            return View();
        }

        public ActionResult Saress()
        {
            return View();
        }


    }

}