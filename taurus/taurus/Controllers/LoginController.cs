using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace taurus.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            if (Session["uid"] != null && Session["uid"].ToString() != "") {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

    }
}
