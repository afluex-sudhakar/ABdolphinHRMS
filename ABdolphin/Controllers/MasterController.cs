﻿using ABdolphin.Filters;
using ABdolphin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABdolphin.Controllers
{
    public class MasterController : AdminBaseController
    {
        // GET: Master
        public ActionResult Index()
        {
            return View();
        }
        #region ExpenseTypeMaster
        public ActionResult ExpenseTypeMaster(string cid)
        {

            Master obj = new Master();
            if (cid != null)
            {
                obj.Fk_ExpenseTypeId = Crypto.Decrypt(cid);
                DataSet dscmpny = obj.GetExpenseTypeList();
                if (dscmpny != null && dscmpny.Tables.Count > 0)
                {
                    obj.Fk_ExpenseTypeId = dscmpny.Tables[0].Rows[0]["Pk_ExpenseTypeId"].ToString();
                    obj.ExpenseTypeName = dscmpny.Tables[0].Rows[0]["ExpenseTypeName"].ToString();
                }
            }
            return View(obj);
        }

        [HttpPost]
        [ActionName("ExpenseTypeMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveExpenseTypeMaster(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveExpenseType();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ExpenseType"] = "Expense Type saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ExpenseType"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["ExpenseType"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ExpenseType"] = ex.Message;
            }
            return RedirectToAction("ExpenseTypeMaster", "Master");
        }

        [HttpPost]
        [ActionName("ExpenseTypeMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateExpenseType(Master obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateExpenseType();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ExpenseType"] = "Expense Type updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ExpenseType"] = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                else
                {
                    TempData["ExpenseType"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["ExpenseType"] = ex.Message;
            }
            return RedirectToAction("ExpenseTypeMaster", "Master");
        }

        public ActionResult ExpenseTypeMasterList(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetExpenseTypeList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Fk_ExpenseTypeId = r["Pk_ExpenseTypeId"].ToString();
                    obj.ExpenseTypeName = r["ExpenseTypeName"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["Pk_ExpenseTypeId"].ToString());
                    lst.Add(obj);
                }
                model.lstExpenseType = lst;
            }
            return View(model);
        }

        public ActionResult DeleteExpenseType(string Eid)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.Fk_ExpenseTypeId = Crypto.Decrypt(Eid);
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteExpenseType();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ExpenseType"] = "Expense type deleted successfully";
                        FormName = "ExpenseTypeMasterList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["ExpenseType"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ExpenseTypeMasterList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ExpenseType"] = ex.Message;
                FormName = "ExpenseTypeMasterList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion

        public ActionResult GetMenuDetails(string URL)
        {
            try
            {
                Master model = new Master();
                model.Fk_UserId = Session["Pk_AdminId"].ToString();
                model.UserType = Session["UserTypeName"].ToString();
                model.Url = URL;
                DataSet ds = model.GetMenuPermissionList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        var MenuList = JsonConvert.SerializeObject(ds.Tables[0]);
                        return Json(MenuList, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("0", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}