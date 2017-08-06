using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Artesania.Controllers
{

    public class HomeController : Controller
    {
        private Models.DatabaseEntities bd = new Models.DatabaseEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Chekout()
        {
            return View();
        }

        public ActionResult Producto()

        {
            return View();
        }


        public ActionResult About()
        {
            return View();
        }

<<<<<<< HEAD
=======

>>>>>>> 135128699f065d90fee877d739f7e91dc66c1d81

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
            using (var b = new Models.DatabaseEntities())
            {
                return b.Cliente.Find(Helper.SessionHelper.GetUser()).Nombres;
            }
        }

<<<<<<< HEAD
=======

>>>>>>> 135128699f065d90fee877d739f7e91dc66c1d81
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

<<<<<<< HEAD
    }
=======

    }

>>>>>>> 135128699f065d90fee877d739f7e91dc66c1d81
}