using ABdolphin.Filters;
using ABdolphin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABdolphin.Controllers
{
    public class FarmersController : AdminBaseController
    {
        // GET: Farmers
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddFarmers(string id)
        {
            Farmer model = new Farmer();
            try
            {
                if (id != null)
                {
                    model.PK_Farmerid = Crypto.Decrypt(id);
                    DataSet ds = model.GetlistById();
                    //if (ds.Tables.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
                    //{
                    //    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    //    model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    //    model.Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
                    //    model.DOB = ds.Tables[0].Rows[0]["DOB"].ToString();
                    //    model.SQFT = ds.Tables[0].Rows[0]["SQFT"].ToString();
                    //    model.Acre = ds.Tables[0].Rows[0]["Acre"].ToString();
                    //    model.Hectare = ds.Tables[0].Rows[0]["Hectare"].ToString();
                    //    model.Pincode = ds.Tables[0].Rows[0]["Pincode"].ToString();
                    //    model.Photo = ds.Tables[0].Rows[0]["Photo"].ToString();
                    //    model.IDProof = ds.Tables[0].Rows[0]["IDProof"].ToString();
                    //    model.City = ds.Tables[0].Rows[0]["City"].ToString();
                    //    model.State = ds.Tables[0].Rows[0]["State"].ToString();
                    //    model.DelearName = ds.Tables[0].Rows[0]["DelearName"].ToString();
                    //    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    //    model.PK_Farmerid = ds.Tables[0].Rows[0]["PK_Farmerid"].ToString();
                    //    model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                    //    model.AssociateID = ds.Tables[0].Rows[0]["AssociatId"].ToString();
                    //    model.AssociateLoginID = ds.Tables[0].Rows[0]["AssociatId"].ToString();
                    //    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    //    model.GataKhasaraN = ds.Tables[0].Rows[0]["GataKhasaraN"].ToString();
                    //    model.Village = ds.Tables[0].Rows[0]["Village"].ToString();
                    //    model.Status = ds.Tables[0].Rows[0]["Status"].ToString();
                    //    model.RegistryDate = ds.Tables[0].Rows[0]["RegistryDate"].ToString();
                    //    model.Fk_CompanyId = ds.Tables[0].Rows[0]["Fk_CompanyId"].ToString();
                    //    model.IDProof = ds.Tables[0].Rows[0]["IDProof"].ToString();
                    //}

                }
                List<SelectListItem> ddltitle = FTitle();
            ViewBag.FTitle = ddltitle;

            List<SelectListItem> ddlStatus = FarmerStatus();
            ViewBag.ddlStatus = ddlStatus;

            //#region ddlcompany
            //int ccount = 0;
            //Master master = new Master();
            //List<SelectListItem> ddlcompany = new List<SelectListItem>();
            //master.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
            //DataSet dscompany = master.GetCompanyList();
            //if (dscompany != null && dscompany.Tables.Count > 0 && dscompany.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in dscompany.Tables[0].Rows)
            //    {
            //        if (ccount == 0)
            //        {
            //            ddlcompany.Add(new SelectListItem { Text = "Select Company", Value = "0" });
            //        }
            //        ddlcompany.Add(new SelectListItem { Text = r["CompanyName"].ToString(), Value = r["PK_CompanyID"].ToString() });
            //        ccount = ccount + 1;
            //    }
            //}
            //ViewBag.ddlcompany = ddlcompany;
            //#endregion
        }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(model);
        }


        [HttpPost]
        [ActionName("AddFarmers")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveFarmers(IEnumerable<HttpPostedFileBase> IDProof, IEnumerable<HttpPostedFileBase> Photo, Farmer obj)
        {
            try
            {
                foreach (var file in IDProof)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        obj.IDProof = "~/FarmerPhotoandIdproof/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(obj.IDProof)));
                    }
                }
                foreach (var photo in Photo)
                {
                    if (photo != null && photo.ContentLength > 0)
                    {
                        obj.Photo = "~/FarmerPhotoandIdproof/" + Guid.NewGuid() + Path.GetExtension(photo.FileName);
                        photo.SaveAs(Path.Combine(Server.MapPath(obj.Photo)));
                    }
                }
                obj.DOB = string.IsNullOrEmpty(obj.DOB) ? null : Common.ConvertToSystemDate(obj.DOB, "dd/MM/yyyy");
                obj.RegistryDate = string.IsNullOrEmpty(obj.RegistryDate) ? null : Common.ConvertToSystemDate(obj.RegistryDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.SaveFarmerDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SucMsgFarmer"] = "Farmer Details Saved successfully";

                        obj = new Farmer();
                        ModelState.Clear();
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrMsgFarmer"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                List<SelectListItem> ddltitle = FTitle();
                ViewBag.FTitle = ddltitle;
            }
            catch (Exception ex)
            {
                TempData["MsgFarmer"] = ex.Message;
            }
            return RedirectToAction("AddFarmers", "Farmers");
        }

        [HttpPost]
        [ActionName("AddFarmers")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateFarmers(IEnumerable<HttpPostedFileBase> IDProof, IEnumerable<HttpPostedFileBase> Photo, Farmer obj)
        {

            try
            {
                foreach (var file in IDProof)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        obj.IDProof = "/FarmerPhotoandIdproof/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(obj.IDProof)));
                    }

                }
                foreach (var photo in Photo)
                {
                    if (photo != null && photo.ContentLength > 0)
                    {
                        obj.Photo = "/FarmerPhotoandIdproof/" + Guid.NewGuid() + Path.GetExtension(photo.FileName);
                        photo.SaveAs(Path.Combine(Server.MapPath(obj.Photo)));
                    }
                }
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.DOB = string.IsNullOrEmpty(obj.DOB) ? null : Common.ConvertToSystemDate(obj.DOB, "dd/MM/yyyy");
                obj.RegistryDate = string.IsNullOrEmpty(obj.RegistryDate) ? null : Common.ConvertToSystemDate(obj.RegistryDate, "dd/MM/yyyy");

                DataSet ds = obj.UpdateFarmerDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SucMsgFarmer"] = "Farmer Details Updated successfully";
                        obj.PK_Farmerid = null;
                        obj = new Farmer();
                        ModelState.Clear();
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrMsgFarmer"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }

            catch (Exception ex)
            {
                TempData["ErrMsgFarmer"] = ex.Message;
            }
            List<SelectListItem> ddltitle = FTitle();
            ViewBag.FTitle = ddltitle;
           
            return RedirectToAction("AddFarmers", "Farmers");
        }


        public ActionResult DeleteFarmers(string Id)
        {
            Farmer model = new Farmer();
            try
            {
                if (Id != null)
                {
                    model.PK_Farmerid = Crypto.Decrypt(Id);
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = model.DeleteFarmers();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["DeleteFarmers"] = "Farmer Deleted Successfully";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["DeleteFarmers"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else
                    {
                        TempData["DeleteFarmers"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["DeleteFarmers"] = ex.Message;
            }
            return RedirectToAction("Farmerlist", "Farmers");
        }


        public ActionResult Farmerlist()
        {
            Farmer model = new Farmer();
            List<Farmer> lst = new List<Farmer>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            model.PK_Farmerid = model.PK_Farmerid == "0" ? null : model.PK_Farmerid;
            model.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
            DataSet ds = model.Getlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var i = 0;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Farmer obj = new Farmer();
                    obj.Name = r["Name"].ToString();
                    obj.Hectare = r["Hectare"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.City = r["City"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.FStatus = r["Status"].ToString();
                    obj.IsActive = r["IsActive"].ToString();
                    obj.JoiningDate = r["AddedDate"].ToString();
                    obj.GataKhasaraN = r["GataKhasaraN"].ToString();
                    obj.Hectare = r["Hectare"].ToString();
                    obj.Village = r["Village"].ToString();
                    obj.RegistryDate = r["RegistryDate"].ToString();
                    obj.Status = r["FarmerStatus"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["Pk_FarmerID"].ToString());
                    //obj.CompanyName = r["CompanyName"].ToString();
                    obj.IDProof = r["IDProof"].ToString();
                    obj.SQFT = r["SQFT"].ToString();
                    lst.Add(obj);
                }
                model.FarmerList = lst;
                ViewBag.Hectare = double.Parse(ds.Tables[0].Compute("sum(Hectare)", "").ToString()).ToString("n2");
            }

            List<SelectListItem> ddlfarmername = new List<SelectListItem>();
            model.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds1 = model.GetFarmerList();

            if (ds1 != null && ds1.Tables.Count > 0)
            {
                int count1 = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlfarmername.Add(new SelectListItem { Text = "Select Farmer", Value = "0" });
                    }
                    ddlfarmername.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_FarmerId"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.FarmerloginId = ddlfarmername;

            //#region ddlcompany
            //int ccount = 0;
            //Master master = new Master();
            //master.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
            //List<SelectListItem> ddlcompany = new List<SelectListItem>();
            //DataSet dscompany = master.GetCompanyList();
            //if (dscompany != null && dscompany.Tables.Count > 0 && dscompany.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in dscompany.Tables[0].Rows)
            //    {
            //        if (ccount == 0)
            //        {
            //            ddlcompany.Add(new SelectListItem { Text = "Select Company", Value = "0" });
            //        }
            //        ddlcompany.Add(new SelectListItem { Text = r["CompanyName"].ToString(), Value = r["PK_CompanyID"].ToString() });
            //        ccount = ccount + 1;
            //    }
            //}
            //ViewBag.ddlcompany = ddlcompany;
            //#endregion

            return View(model);
        }
        [HttpPost]
        [ActionName("Farmerlist")]
        [OnAction(ButtonName = "Search")]
        public ActionResult FarmerList(Farmer model)
        {

            List<Farmer> lst = new List<Farmer>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.PK_Farmerid = model.PK_Farmerid == "0" ? null : model.PK_Farmerid;
            model.Fk_CompanyId = model.Fk_CompanyId == "0" ? null : model.Fk_CompanyId;
            model.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
            DataSet ds = model.Getlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Farmer obj = new Farmer();
                    obj.Name = r["Name"].ToString();
                    obj.Hectare = r["Hectare"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.City = r["City"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.FStatus = r["Status"].ToString();
                    obj.IsActive = r["IsActive"].ToString();
                    obj.JoiningDate = r["AddedDate"].ToString();
                    obj.GataKhasaraN = r["GataKhasaraN"].ToString();
                    obj.Hectare = r["Hectare"].ToString();
                    obj.Village = r["Village"].ToString();
                    obj.Status = r["FarmerStatus"].ToString();
                    obj.RegistryDate = r["RegistryDate"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["Pk_FarmerID"].ToString());
                    //obj.CompanyName = r["CompanyName"].ToString();
                    obj.IDProof = r["IDProof"].ToString();
                    obj.SQFT = r["SQFT"].ToString();
                    lst.Add(obj);
                }
                model.FarmerList = lst;
                ViewBag.Hectare = double.Parse(ds.Tables[0].Compute("sum(Hectare)", "").ToString()).ToString("n2");
            }

            List<SelectListItem> ddlfarmername = new List<SelectListItem>();
            model.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds1 = model.GetFarmerList();

            if (ds1 != null && ds1.Tables.Count > 0)
            {
                int count1 = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlfarmername.Add(new SelectListItem { Text = "Select Farmer", Value = "0" });
                    }
                    ddlfarmername.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_FarmerId"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.FarmerloginId = ddlfarmername;

            //#region ddlcompany
            //int ccount = 0;
            //Master master = new Master();
            //master.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
            //List<SelectListItem> ddlcompany = new List<SelectListItem>();
            //DataSet dscompany = master.GetCompanyList();
            //if (dscompany != null && dscompany.Tables.Count > 0 && dscompany.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in dscompany.Tables[0].Rows)
            //    {
            //        if (ccount == 0)
            //        {
            //            ddlcompany.Add(new SelectListItem { Text = "Select Company", Value = "0" });
            //        }
            //        ddlcompany.Add(new SelectListItem { Text = r["CompanyName"].ToString(), Value = r["PK_CompanyID"].ToString() });
            //        ccount = ccount + 1;
            //    }
            //}
            //ViewBag.ddlcompany = ddlcompany;
            //#endregion

            return View(model);
        }

        public static List<SelectListItem> FTitle()
        {
            List<SelectListItem> FTitle = new List<SelectListItem>();

            FTitle.Add(new SelectListItem { Text = "Select", Value = "0" });
            FTitle.Add(new SelectListItem { Text = "Mr.", Value = "Mr." });
            FTitle.Add(new SelectListItem { Text = "Ms.", Value = "Ms." });
            FTitle.Add(new SelectListItem { Text = "Mrs.", Value = "Mrs." });
            return FTitle;
        }

        public static List<SelectListItem> FarmerStatus()
        {
            List<SelectListItem> FarmerStatus = new List<SelectListItem>();
            FarmerStatus.Add(new SelectListItem { Text = "-Select-", Value = "" });
            FarmerStatus.Add(new SelectListItem { Text = "Notary", Value = "Notary" });
            FarmerStatus.Add(new SelectListItem { Text = "Registry", Value = "Registry" });
            FarmerStatus.Add(new SelectListItem { Text = "Agreement", Value = "Agreement" });
            return FarmerStatus;
        }


        public ActionResult FarmerPlotRegistrylist(Farmer model)
        {
            #region GetFarmerList
            AssociateBooking obj1 = new AssociateBooking();
            List<SelectListItem> ddlfarmername = new List<SelectListItem>();
            DataSet ds1 = obj1.GetFarmerList();

            if (ds1 != null && ds1.Tables.Count > 0)
            {
                int count1 = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlfarmername.Add(new SelectListItem { Text = "Select Farmer", Value = "0" });
                    }
                    ddlfarmername.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_FarmerId"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.FarmerloginId = ddlfarmername;
            #endregion
            #region GetcustomerList
            List<SelectListItem> ddlcustomername = new List<SelectListItem>();
            DataSet dscust = obj1.GetcustomerList();

            if (dscust != null && dscust.Tables.Count > 0)
            {
                int count2 = 0;
                foreach (DataRow r in dscust.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlcustomername.Add(new SelectListItem { Text = "Select Customer", Value = "0" });
                    }
                    ddlcustomername.Add(new SelectListItem { Text = r["Fullname"].ToString(), Value = r["Pk_UserId"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.CustomerLoginId = ddlcustomername;
            #endregion
            List<Farmer> lst = new List<Farmer>();
            model.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
            DataSet ds = model.GetPlotRegistrylist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var i = 0;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    //if(i<25)
                    //{
                    Farmer obj = new Farmer();

                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.PlotNumber = r["PlotDetails"].ToString();
                    obj.GataKhasaraN = r["GataKhasraNo"].ToString();
                    obj.FarmerName = r["FarmerName"].ToString();
                    obj.FStatus = r["Status"].ToString();
                    obj.TotalBalance = r["TotalArea"].ToString();
                    obj.PlotSize = r["PlotSize"].ToString();
                    obj.IsActive = r["IsActive"].ToString();
                    obj.RemainingArea = r["RemainingArea"].ToString();
                    obj.RegistryDate = r["RegistryDate"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_PlotRegistryId"].ToString());
                    lst.Add(obj);
                    //}
                    //i = i + 1;
                }
                model.FarmerPlotList = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("FarmerPlotRegistrylist")]
        [OnAction(ButtonName = "Search")]
        public ActionResult PlotRegistryList(Farmer model)
        {
            #region GetFarmerList
            AssociateBooking obj1 = new AssociateBooking();
            List<SelectListItem> ddlfarmername = new List<SelectListItem>();
            DataSet ds1 = obj1.GetFarmerList();

            if (ds1 != null && ds1.Tables.Count > 0)
            {
                int count1 = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlfarmername.Add(new SelectListItem { Text = "Select Farmer", Value = "0" });
                    }
                    ddlfarmername.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_FarmerId"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.FarmerloginId = ddlfarmername;
            #endregion
            #region GetcustomerList
            List<SelectListItem> ddlcustomername = new List<SelectListItem>();
            DataSet dscust = obj1.GetcustomerList();

            if (dscust != null && dscust.Tables.Count > 0)
            {
                int count2 = 0;
                foreach (DataRow r in dscust.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlcustomername.Add(new SelectListItem { Text = "Select Customer", Value = "0" });
                    }
                    ddlcustomername.Add(new SelectListItem { Text = r["Fullname"].ToString(), Value = r["Pk_UserId"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.CustomerLoginId = ddlcustomername;
            #endregion
            List<Farmer> lst = new List<Farmer>();
            model.CustomerId = model.CustomerId == "0" ? null : model.CustomerId;
            model.PK_Farmerid = model.PK_Farmerid == "0" ? null : model.PK_Farmerid;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
            DataSet ds = model.GetPlotRegistrylist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Farmer obj = new Farmer();

                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.PlotNumber = r["PlotDetails"].ToString();
                    obj.GataKhasaraN = r["GataKhasraNo"].ToString();
                    obj.FarmerName = r["FarmerName"].ToString();
                    obj.FStatus = r["Status"].ToString();
                    obj.TotalBalance = r["TotalArea"].ToString();
                    obj.PlotSize = r["PlotSize"].ToString();
                    obj.IsActive = r["IsActive"].ToString();
                    obj.RemainingArea = r["RemainingArea"].ToString();
                    obj.RegistryDate = r["RegistryDate"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_PlotRegistryId"].ToString());
                    lst.Add(obj);
                }
                model.FarmerPlotList = lst;
            }

            return View(model);
        }

        public ActionResult Farmerpaymentlist(Farmer model)
        {

            List<SelectListItem> CheckStatus = Common.CheckStatus();
            ViewBag.CheckStatus = CheckStatus;
            try
            {
                List<Farmer> lst = new List<Farmer>();
                model.CheqStatus = model.CheqStatus == "0" ? "Clearing" : model.CheqStatus;
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                model.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
                DataSet ds = model.GetFarmerpaymentList();
                if (ds.Tables.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    var i = 0;

                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        if (i < 25)
                        {
                            Farmer obj = new Farmer();
                            obj.Name = r["Name"].ToString();
                            obj.Reciept = r["ReceiptNo"].ToString();
                            obj.ID = r["PId"].ToString();
                            obj.PaidAmount = r["PaidAmount"].ToString();
                            obj.TotalBalance = r["GeneratedAmount"].ToString();
                            obj.CashDate = r["PaidDate"].ToString();
                            obj.PayType = r["PayType"].ToString();
                            obj.ChequeNo = r["ChequeNo"].ToString();
                            obj.BankName = r["BankName"].ToString();
                            obj.Remarks = r["Remark"].ToString();
                            obj.CheqStatus = r["PaidStatus"].ToString();
                            obj.EncryptKey = Crypto.Encrypt(r["PId"].ToString());
                            lst.Add(obj);
                        }
                        ViewBag.Total = ds.Tables[0].Compute("sum(PaidAmount)", "").ToString();
                        i = i++;
                    }
                    model.PaymetListFarm = lst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(model);
        }

        public ActionResult GetUserList()
        {
            Reports obj = new Reports();
            List<Reports> lst = new List<Reports>();
            obj.LoginId = Session["LoginId"].ToString();
            DataSet ds = obj.GettingUserProfile();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Reports objList = new Reports();
                    objList.UserName = dr["Fullname"].ToString();
                    objList.LoginIDD = dr["LoginId"].ToString();
                    lst.Add(objList);
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
    }
}

