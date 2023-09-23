using ABdolphin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ABdolphin.Controllers
{
    public class PlotController : AdminBaseController
    {

        public ActionResult CustomerListForAutoSearch()
        {
            Reports obj = new Reports();
            List<Reports> lst = new List<Reports>();
            obj.LoginId = Session["LoginId"].ToString();
            DataSet ds = obj.GetCustomerListAutoSeach();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Reports objList = new Reports();
                    objList.UserName = dr["Fullname"].ToString();
                    objList.LoginIDD = dr["LoginId"].ToString();
                    objList.Fk_CustomerId = dr["Pk_UserId"].ToString();
                    lst.Add(objList);
                }
            }
            var jsonResult = Json(lst, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}