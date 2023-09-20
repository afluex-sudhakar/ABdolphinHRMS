using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABdolphin.Controllers
{
    public class AdminController : AdminBaseController
    {
        public ActionResult AdminDashBoard()
        {
            return View();
        }
        
    }
}