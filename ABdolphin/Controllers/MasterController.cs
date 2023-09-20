using ABdolphin.Filters;
using ABdolphin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABdolphin.Controllers
{
    public class MasterController : Controller
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


        #region ExpenseCategoryMaster
        public ActionResult ExpenseCategoryMaster(string Id)
        {
            Master model = new Master();
            if (Id != null)
            {
                model.Pk_ExpenseCategoryId = Crypto.Decrypt(Id);
                DataSet ds = model.ExpenseCategoryList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.ExpenseCategory = ds.Tables[0].Rows[0]["ExpenseCategoryName"].ToString();
                    //model.EMI = ds.Tables[0].Rows[0]["EMI"].ToString();
                    //model.DownPayment = ds.Tables[0].Rows[0]["DownPayment"].ToString();
                    //model.PartPayment = ds.Tables[0].Rows[0]["PartPayment"].ToString();
                }
            }
            return View(model);

        }
        [HttpPost]
        [ActionName("ExpenseCategoryMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult ExpenseCategoryMaster(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveExpenseCategory();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ExpenseCategory"] = "Expense Category save successfully !";
                    }
                    else
                    {
                        TempData["ExpenseCategory"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ExpenseCategory"] = ex.Message;
            }
            return RedirectToAction("ExpenseCategoryMaster", "Master");
        }

        public ActionResult ExpenseCategoryList()
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();
            DataSet ds = model.ExpenseCategoryList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_ExpenseCategoryId = dr["Fk_ExpenseCategoryId"].ToString();
                    obj.ExpenseCategory = dr["ExpenseCategoryName"].ToString();
                    //obj.EMI = dr["EMI"].ToString();
                    //obj.DownPayment = dr["DownPayment"].ToString();
                    //obj.PartPayment = dr["PartPayment"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(dr["Fk_ExpenseCategoryId"].ToString());
                    lst.Add(obj);
                }
                model.lstExpenseCategory = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("ExpenseCategoryMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateExpenseCategoryMaster(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.UpdateExpenseCategory();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ExpenseCategory"] = "Expense Category updated successfully !";
                    }
                    else
                    {
                        TempData["ExpenseCategory"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ExpenseCategory"] = ex.Message;
            }
            return RedirectToAction("ExpenseCategoryMaster", "Master");
        }


        public ActionResult Deleteexpensecategory(Master model, string Id)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                if (Id != null)
                {
                    model.Pk_ExpenseCategoryId = Crypto.Decrypt(Id);
                    DataSet ds = model.Deleteexpensecategory();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            model.Result = "1";
                            TempData["ExpenseCategoryList"] = "Expense Category deleted successfully !";
                        }
                        else
                        {
                            model.Result = "0";
                            TempData["ExpenseCategoryList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else
                    {
                        model.Result = "0";
                        TempData["ExpenseCategoryList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                model.Result = "0";
                TempData["ExpenseCategoryList"] = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}