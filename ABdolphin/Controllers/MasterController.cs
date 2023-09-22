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

        public ActionResult GetSiteDetailsForPlotBooking(string SiteID)
        {
            try
            {
                Plot model = new Plot();
                model.SiteID = SiteID;

                #region GetSiteRate
                //DataSet dsSiteRate = model.GetSiteList();
                //if (dsSiteRate != null)
                //{
                //    model.Rate = dsSiteRate.Tables[0].Rows[0]["Rate"].ToString();
                //    model.Result = "yes";
                //}
                #endregion
                #region GetSectors
                List<SelectListItem> ddlSector = new List<SelectListItem>();
                model.Result = "yes";
                DataSet dsSector = model.GetSectorList();

                if (dsSector != null && dsSector.Tables.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[1].Rows)
                    {
                        ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                    }
                }
                model.ddlSector = ddlSector;
                #endregion
                //#region SitePLCCharge
                //List<Master> lstPlcCharge = new List<Master>();
                //DataSet dsPlcCharge = model.GetPLCChargeList();

                //if (dsPlcCharge != null && dsPlcCharge.Tables.Count > 0)
                //{
                //    foreach (DataRow r in dsPlcCharge.Tables[0].Rows)
                //    {
                //        Master obj = new Master();
                //        obj.SiteName = r["SiteName"].ToString();
                //        obj.PLCName = r["PLCName"].ToString();
                //        obj.PLCCharge = r["PLCCharge"].ToString();
                //        obj.PLCID = r["PK_PLCID"].ToString();

                //        lstPlcCharge.Add(obj);
                //    }
                //    model.lstPLC = lstPlcCharge;
                //}
                //#endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult GetBlockList(string SiteID, string SectorID)
        {
            List<SelectListItem> lstBlock = new List<SelectListItem>();
            Master model = new Master();
            model.SiteID = SiteID;
            model.SectorID = SectorID;
            DataSet dsblock = model.GetBlockList();

            #region ddlBlock
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                }

            }

            model.lstBlock = lstBlock;
            #endregion

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}