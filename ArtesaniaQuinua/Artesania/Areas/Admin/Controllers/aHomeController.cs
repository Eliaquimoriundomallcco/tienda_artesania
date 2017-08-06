using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Artesania.Areas.Admin.Controllers
{
    public class aHomeController : Controller
    {
        // GET: Admin/aHome
        public ActionResult Index()
        {
            return View();
        }
    }
}