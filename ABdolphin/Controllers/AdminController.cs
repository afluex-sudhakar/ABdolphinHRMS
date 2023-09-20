using ABdolphin.Models;
using System;
using System.Collections.Generic;
using System.Data;
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


        public ActionResult GetStateCity(string Pincode)
        {
            Common obj = new Common();
            obj.Pincode = Pincode;
            DataSet ds = obj.GetStateCity();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                obj.State = ds.Tables[0].Rows[0]["State"].ToString();
                obj.City = ds.Tables[0].Rows[0]["City"].ToString();
                obj.Result = "1";
            }
            else
            {
                obj.Result = "Invalid PinCode";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}