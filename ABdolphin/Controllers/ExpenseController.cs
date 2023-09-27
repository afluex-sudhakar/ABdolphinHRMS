using ABdolphin.Filters;
using ABdolphin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ABdolphin.Controllers
{
    public class ExpenseController : AdminBaseController
    {
        
        // GET: Expense
        #region Account
        public ActionResult AddAccount(string id)
        {
            Expenses model = new Expenses();
            try
            {
                if (id != null)
                {
                    model.Pk_BankId = Crypto.Decrypt(id);
                    List<Expenses> lst = new List<Expenses>();
                    DataSet ds = model.GetAccountlistById();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        model.Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
                        model.AccountNumber = ds.Tables[0].Rows[0]["AcNumber"].ToString();
                        model.AcountHolder = ds.Tables[0].Rows[0]["AcHolderName"].ToString();
                        model.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                        model.BranchName = ds.Tables[0].Rows[0]["BranchName"].ToString();
                        model.IFSCCode = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                        model.IsActive = ds.Tables[0].Rows[0]["IsActive"].ToString();
                        model.EncryptKey = ds.Tables[0].Rows[0]["Pk_BankId"].ToString();
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("AddAccount")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveAccount(Expenses model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveAccountDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["MsgAccount"] = "Account Details Saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["MsgAccount"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                TempData["MsgAccount"] = ex.Message;
            }
            return RedirectToAction("AddAccount", "Expense");
        }

        [HttpPost]
        [ActionName("AddAccount")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateAccount(Expenses model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.UpdateAccountDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["MsgAccount"] = "Account Details Updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["MsgAccount"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                TempData["MsgAccount"] = ex.Message;
            }
            return RedirectToAction("AddAccount", "Expense");
        }

        public ActionResult DeleteAccount(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Expenses obj = new Expenses();
                obj.Pk_BankId = Crypto.Decrypt(id);
                obj.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteAccount();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["AccountList"] = "Account deleted successfully";
                        FormName = "AccountList";
                        Controller = "Expense";
                    }
                    else
                    {
                        TempData["AccountList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AccountList";
                        Controller = "Expense";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["AccountList"] = ex.Message;
                FormName = "AccountList";
                Controller = "Expense";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult AccountStatus(string id, string IsActive)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Expenses obj = new Expenses();
                if (IsActive == "False")
                {
                    obj.IsActive = "1";
                }
                else
                {
                    obj.IsActive = "0";
                }
                obj.Pk_BankId = Crypto.Decrypt(id);
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.AccountStatus();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["AccountList"] = "Status Updated successfully";
                        FormName = "AccountList";
                        Controller = "Expense";
                    }
                    else
                    {
                        TempData["AccountList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AccountList";
                        Controller = "Expense";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["AccountList"] = ex.Message;
                FormName = "AccountList";
                Controller = "Expense";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult Accountlist(Expenses model)
        {
            List<Expenses> lst = new List<Expenses>();
            model.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
            DataSet ds = model.GetAccountlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var i = 0;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    // if (i < 25)
                    {
                        Expenses obj = new Expenses();
                        obj.AccountNumber = r["AcNumber"].ToString();
                        obj.AcountHolder = r["AcHolderName"].ToString();
                        obj.CrAmount = double.Parse(r["CrAmount"].ToString()).ToString("n2");
                        obj.DrAmount = double.Parse(r["DrAmount"].ToString()).ToString("n2");
                        obj.BalanceAmount = double.Parse(r["BalanceAmount"].ToString()).ToString("n2");
                        obj.BankName = r["BankName"].ToString();
                        obj.BranchName = r["BranchName"].ToString();
                        obj.IFSCCode = r["IFSCCode"].ToString();
                        obj.IsActive = r["IsActive"].ToString();
                        obj.Pk_BankId = Crypto.Encrypt(r["Pk_BankId"].ToString());
                        obj.EncryptKey = Crypto.Encrypt(r["Pk_BankId"].ToString());
                        lst.Add(obj);
                    }
                    //   i = i + 1;
                }
                model.AccountList = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("Accountlist")]
        [OnAction(ButtonName = "Search")]
        public ActionResult AccountList(Expenses model)
        {
            List<Expenses> lst = new List<Expenses>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
            DataSet ds = model.GetAccountlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.AccountNumber = r["AcNumber"].ToString();
                    obj.AcountHolder = r["AcHolderName"].ToString();
                    obj.CrAmount = double.Parse(r["CrAmount"].ToString()).ToString("n2");
                    obj.DrAmount = double.Parse(r["DrAmount"].ToString()).ToString("n2");
                    obj.BalanceAmount = double.Parse(r["BalanceAmount"].ToString()).ToString("n2");
                    obj.BankName = r["BankName"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.IFSCCode = r["IFSCCode"].ToString();
                    obj.IsActive = r["IsActive"].ToString();
                    obj.Pk_BankId = Crypto.Encrypt(r["Pk_BankId"].ToString());
                    obj.EncryptKey = Crypto.Encrypt(r["Pk_BankId"].ToString());
                    lst.Add(obj);
                }
                model.AccountList = lst;
            }

            return View(model);
        }

        public ActionResult ExpenseLedger(Expenses model, string bankid, string BankName)
        {

            if (bankid != "" && bankid != null)
            {
                model.TransactionID = Crypto.Decrypt(bankid);
                ViewBag.BankName = BankName;
            }
            List<Expenses> lst = new List<Expenses>();
            model.ExpenseID = model.ExpenseID == "0" ? null : model.ExpenseID;
            model.ExpenseName = model.ExpenseName == "0" ? null : model.ExpenseName;
            model.Fk_ExpenseCategoryId = model.Fk_ExpenseCategoryId == "0" ? null : model.Fk_ExpenseCategoryId;
            model.Type = model.Type == "0" ? null : model.Type;
            model.Fk_CompanyId = model.Fk_CompanyId == "0" ? null : model.Fk_CompanyId;
            DateTime now = DateTime.Now;
            DateTime currentDate = DateTime.Now;
            currentDate = currentDate.AddDays(-7);
            model.FromDate = currentDate.ToString("dd/MM/yyyy");
            model.ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            model.NFromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.NToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.EntryFromDate = string.IsNullOrEmpty(model.EntryFromDate) ? null : Common.ConvertToSystemDate(model.EntryFromDate, "dd/MM/yyyy");
            model.EntryToDate = string.IsNullOrEmpty(model.EntryToDate) ? null : Common.ConvertToSystemDate(model.EntryToDate, "dd/MM/yyyy");
            model.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
            DataSet ds = model.GetExpenseLedger1();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var i = 0;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    //if(i<25)
                    //{
                    Expenses expense = new Expenses();
                    expense.Pk_BankId = Crypto.Encrypt(r["FK_BankID"].ToString()).ToString();
                    expense.CompanyName = r["CompanyName"].ToString();
                    expense.ExpenseHead = r["ExpenseHead"].ToString();
                    expense.ExpenseName = r["ExpenseName"].ToString();
                    expense.ExpenseID = r["ExpenseTypeName"].ToString();
                    expense.ChequeStatus = r["TransactionStatus"].ToString();
                    expense.TransactionTy = r["TransactionType"].ToString();
                    expense.Transanction = r["TransactionStatus"].ToString();
                    //expense.CrAmount = double.Parse(r["CrAmount"].ToString()).ToString("n2");
                    expense.CrAmount = r["CrAmount"].ToString();
                    //expense.DrAmount = double.Parse(r["DrAmount"].ToString()).ToString("n2");
                    expense.DrAmount = r["DrAmount"].ToString();
                    expense.Type = r["LedgerType"].ToString();
                    expense.BalanceAmount = double.Parse(r["Balance"].ToString()).ToString("n2");
                    expense.Remarks = r["Remarks"].ToString();
                    expense.Date = r["CheckDate"].ToString();
                    expense.ChequeStatusUpdateDate = r["chequedate"].ToString();
                    expense.ExpenseCategoryName = r["ExpenseCategory"].ToString();
                    lst.Add(expense);
                    //}
                    //i = i + 1;
                }
                model.CrExpenseList = lst;
                //ViewBag.CrAmount = double.Parse(ds.Tables[0].Compute("sum(CrAmount)", "").ToString()).ToString("n2");
                ViewBag.CrAmount = ds.Tables[0].Compute("sum(CrAmount)", "").ToString();
                //ViewBag.DrAmount = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
                ViewBag.DrAmount = ds.Tables[0].Compute("sum(DrAmount)", "").ToString();
                ViewBag.BalanceAmount = double.Parse(ds.Tables[0].Compute("sum(Balance)", "").ToString()).ToString("n2");

            }
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
            #region ddlExpenseHead
            int ccount12 = 0;
            Master master = new Master();
            List<SelectListItem> ddlExpenseHead = new List<SelectListItem>();
            DataSet ds1 = master.GetExpenseHeadList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (ccount12 == 0)
                    {
                        ddlExpenseHead.Add(new SelectListItem { Text = "Select Expense Head", Value = "0" });
                    }
                    ddlExpenseHead.Add(new SelectListItem { Text = r["ExpenseHeadName"].ToString(), Value = r["PK_ExpenseHead"].ToString() });
                    ccount12 = ccount12 + 1;
                }
            }
            ViewBag.ddlExpenseHead = ddlExpenseHead;
            #endregion
            #region ExpenseType
            int count = 0;
            Plot obj = new Plot();
            List<SelectListItem> ddlExpenseType = new List<SelectListItem>();
            DataSet dsExpenseType = obj.GetExpenseTypeList();
            if (dsExpenseType != null && dsExpenseType.Tables.Count > 0 && dsExpenseType.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsExpenseType.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlExpenseType.Add(new SelectListItem { Text = "Select Expense Type", Value = "0" });
                    }
                    ddlExpenseType.Add(new SelectListItem { Text = r["ExpenseTypeName"].ToString(), Value = r["Pk_ExpenseTypeId"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ExpenseType = ddlExpenseType;
            #endregion
            #region ddlexpensename
            List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            ddlexpensename.Add(new SelectListItem { Text = "Select Expense Name", Value = "0" });
            ViewBag.ddlexpensename = ddlexpensename;
            #endregion
            #region ddlTransactionType
            int count1 = 0;
            List<SelectListItem> ddlTransactionType = new List<SelectListItem>();
            DataSet ddlTransaction = model.GetTransactionList();
            if (ddlTransaction != null && ddlTransaction.Tables.Count > 0 && ddlTransaction.Tables[0].Rows.Count > 0)
            {
                //ddlTransactionType.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
                foreach (DataRow r in ddlTransaction.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlTransactionType.Add(new SelectListItem { Text = "Select TransactionType", Value = "0" });
                    }
                    ddlTransactionType.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlTransactionType = ddlTransactionType;
            List<SelectListItem> EntryType = Common.EntryType();
            ViewBag.EntryType = EntryType;
            #endregion
            #region ddlExpenseCategory
            int Category = 0;
            List<SelectListItem> ddlexpensecategory = new List<SelectListItem>();
            DataSet dscat = model.GetExpenseCategory();
            if (dscat != null && dscat.Tables.Count > 0 && dscat.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in dscat.Tables[0].Rows)
                {
                    if (Category == 0)
                    {
                        ddlexpensecategory.Add(new SelectListItem { Text = "Expense Category", Value = "0" });
                    }
                    ddlexpensecategory.Add(new SelectListItem { Text = r["ExpenseCategory"].ToString(), Value = r["Pk_ExpenseCategoryId"].ToString() });
                    Category = Category + 1;
                }
            }
            ViewBag.ExpenseCategory = ddlexpensecategory;
            #endregion
            return View(model);
        }

        [HttpPost]
        [ActionName("ExpenseLedger")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult SearchExpenseLedger(Expenses model)
        {
            List<Expenses> lst = new List<Expenses>();
            model.ExpenseID = model.ExpenseID == "0" ? null : model.ExpenseID;
            model.ExpenseName = model.ExpenseName == "0" ? null : model.ExpenseName;
            model.Type = model.Type == "0" ? null : model.Type;
            model.TransactionID = model.TransactionID == "0" ? null : model.TransactionID;
            model.FK_ExpenseHead = model.FK_ExpenseHead == "0" ? null : model.FK_ExpenseHead;
            model.Fk_CompanyId = model.Fk_CompanyId == "0" ? null : model.Fk_CompanyId;
            model.Fk_ExpenseCategoryId = model.Fk_ExpenseCategoryId == "0" ? null : model.Fk_ExpenseCategoryId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.EntryFromDate = string.IsNullOrEmpty(model.EntryFromDate) ? null : Common.ConvertToSystemDate(model.EntryFromDate, "dd/MM/yyyy");
            model.EntryToDate = string.IsNullOrEmpty(model.EntryToDate) ? null : Common.ConvertToSystemDate(model.EntryToDate, "dd/MM/yyyy");
            model.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
            DataSet ds = model.GetExpenseLedger();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.CompanyName = r["CompanyName"].ToString();
                    obj.ExpenseHead = r["ExpenseHead"].ToString();
                    obj.ExpenseName = r["ExpenseName"].ToString();
                    obj.ExpenseID = r["ExpenseTypeName"].ToString();
                    obj.ChequeStatus = r["TransactionStatus"].ToString();
                    obj.TransactionTy = r["TransactionType"].ToString();
                    obj.Transanction = r["TransactionStatus"].ToString();
                    //obj.CrAmount = r["CrAmount"].ToString();
                    //obj.DrAmount = r["DrAmount"].ToString();
                    obj.CrAmount = double.Parse(r["CrAmount"].ToString()).ToString("n2");
                    obj.DrAmount = double.Parse(r["DrAmount"].ToString()).ToString("n2");
                    obj.Type = r["LedgerType"].ToString();
                    //obj.BalanceAmount = r["Balance"].ToString();
                    obj.BalanceAmount = double.Parse(r["Balance"].ToString()).ToString("n2");
                    obj.BalanceAmount = (r["Balance"].ToString());//.ToString("n2");
                                                                  // obj.BalanceAmount = double.Parse(r["Balance"].ToString()).ToString("n2");
                    obj.Remarks = r["Remarks"].ToString();
                    obj.Date = r["CheckDate"].ToString();
                    obj.ChequeStatusUpdateDate = r["chequedate"].ToString();
                    obj.ExpenseCategoryName = r["ExpenseCategory"].ToString();
                    lst.Add(obj);
                }
                model.CrExpenseList = lst;
                ViewBag.CrAmount = double.Parse(ds.Tables[0].Compute("sum(CrAmount)", "").ToString()).ToString("n2");
                ViewBag.DrAmount = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
                ViewBag.BalanceAmount = double.Parse(ds.Tables[0].Compute("sum(Balance)", "").ToString()).ToString("n2");
            }
            #region ExpenseType
            int count = 0;
            Plot obj1 = new Plot();
            List<SelectListItem> ddlExpenseType = new List<SelectListItem>();
            DataSet dsExpenseType = obj1.GetExpenseTypeList();
            if (dsExpenseType != null && dsExpenseType.Tables.Count > 0 && dsExpenseType.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsExpenseType.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlExpenseType.Add(new SelectListItem { Text = "Select Expense Type", Value = "0" });
                    }
                    ddlExpenseType.Add(new SelectListItem { Text = r["ExpenseTypeName"].ToString(), Value = r["Pk_ExpenseTypeId"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ExpenseType = ddlExpenseType;
            #endregion
            //#region ddlexpensename
            //List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            //ddlexpensename.Add(new SelectListItem { Text = "Select Expense Name", Value = "0" });
            //ViewBag.ddlexpensename = ddlexpensename;
            //#endregion
            #region ddlTransactionType
            int count1 = 0;
            List<SelectListItem> ddlTransactionType = new List<SelectListItem>();
            DataSet ddlTransaction = model.GetTransactionList();
            if (ddlTransaction != null && ddlTransaction.Tables.Count > 0 && ddlTransaction.Tables[0].Rows.Count > 0)
            {
                //ddlTransactionType.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
                foreach (DataRow r in ddlTransaction.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlTransactionType.Add(new SelectListItem { Text = "Select TransactionType", Value = "0" });
                    }
                    ddlTransactionType.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlTransactionType = ddlTransactionType;
            List<SelectListItem> EntryType = Common.EntryType();
            ViewBag.EntryType = EntryType;
            #endregion
            //#region ddlcompany
            //int ccount = 0;
            //Master master = new Master();
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

            #region ddlExpenseHead
            int ccount12 = 0;
            Master master = new Master();
            List<SelectListItem> ddlExpenseHead = new List<SelectListItem>();
            DataSet ds1 = master.GetExpenseHeadList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (ccount12 == 0)
                    {
                        ddlExpenseHead.Add(new SelectListItem { Text = "Select Expense Head", Value = "0" });
                    }
                    ddlExpenseHead.Add(new SelectListItem { Text = r["ExpenseHeadName"].ToString(), Value = r["PK_ExpenseHead"].ToString() });
                    ccount12 = ccount12 + 1;
                }
            }
            ViewBag.ddlExpenseHead = ddlExpenseHead;
            #endregion
            #region ddlExpenseCategory
            int Category = 0;
            List<SelectListItem> ddlexpensecategory = new List<SelectListItem>();
            DataSet dscat = model.GetExpenseCategory();
            if (dscat != null && dscat.Tables.Count > 0 && dscat.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in dscat.Tables[0].Rows)
                {
                    if (Category == 0)
                    {
                        ddlexpensecategory.Add(new SelectListItem { Text = "Expense Category", Value = "0" });
                    }
                    ddlexpensecategory.Add(new SelectListItem { Text = r["ExpenseCategory"].ToString(), Value = r["Pk_ExpenseCategoryId"].ToString() });
                    Category = Category + 1;
                }
            }
            ViewBag.ExpenseCategory = ddlexpensecategory;
            #endregion
            // model.ExpenseID = Expense;
            #region GetExpenseName
            List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            model.Result = "yes";
            DataSet ds11 = model.GetExpenseNameList();

            if (ds11 != null && ds11.Tables.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    ddlexpensename.Add(new SelectListItem { Text = r["ExpenseName"].ToString(), Value = r["FK_ExpenseId"].ToString() });

                }
            }
            ViewBag.ddlexpensename = ddlexpensename;
            #endregion



            return View(model);
        }

        #endregion
        
        #region Expense
        public ActionResult AddExpense(string id)
        {
            Expenses model = new Expenses();
            try
            {

                if (id != null)
                {
                    model.Pk_ExpenseId = Crypto.Decrypt(id);
                    List<Expenses> lst = new List<Expenses>();
                    DataSet ds = model.GetExpenselistById();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        model.FK_ExpenseNameId = ds.Tables[0].Rows[0]["FK_ExpenseTypeId"].ToString();
                        model.ExpenseName = ds.Tables[0].Rows[0]["ExpenseName"].ToString();
                        model.IsActive = ds.Tables[0].Rows[0]["IsActive"].ToString();
                        model.EncryptKey = ds.Tables[0].Rows[0]["Pk_ExpenseId"].ToString();
                    }
                }

                else
                {

                }
                #region ddlexpenseType
                int count1 = 0;
                List<SelectListItem> ddlExpenseType = new List<SelectListItem>();
                DataSet ds2 = model.getExpenseTypeListForExpense();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds2.Tables[0].Rows)
                    {
                        if (count1 == 0)
                        {
                            ddlExpenseType.Add(new SelectListItem { Text = "Select Expense Type", Value = "0" });
                        }
                        ddlExpenseType.Add(new SelectListItem { Text = r["ExpenseTypeName"].ToString(), Value = r["Pk_ExpenseTypeId"].ToString() });
                        count1 = count1 + 1;
                    }
                }
                ViewBag.ddlExpenseType = ddlExpenseType;
                #endregion
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("AddExpense")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveExpense(Expenses model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveExpenseDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["MsgExpense"] = "Expeses  Saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["MsgExpense"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                #region ddlexpenseType
                int count1 = 0;
                List<SelectListItem> ddlExpenseType = new List<SelectListItem>();
                DataSet ds2 = model.getExpenseTypeListForExpense();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds2.Tables[0].Rows)
                    {
                        if (count1 == 0)
                        {
                            ddlExpenseType.Add(new SelectListItem { Text = "Select Expense Type", Value = "0" });
                        }
                        ddlExpenseType.Add(new SelectListItem { Text = r["ExpenseTypeName"].ToString(), Value = r["Pk_ExpenseTypeId"].ToString() });
                        count1 = count1 + 1;
                    }
                }
                ViewBag.ddlExpenseType = ddlExpenseType;
                #endregion
            }
            catch (Exception ex)
            {

                TempData["MsgExpense"] = ex.Message;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("AddExpense")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateExpense(Expenses model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.UpdateExpenseDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["MsgExpense"] = "Expenses Updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["MsgExpense"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                #region ddlexpenseType
                int count1 = 0;
                List<SelectListItem> ddlExpenseType = new List<SelectListItem>();
                DataSet ds2 = model.getExpenseTypeListForExpense();
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds2.Tables[0].Rows)
                    {
                        if (count1 == 0)
                        {
                            ddlExpenseType.Add(new SelectListItem { Text = "Select Expense Type", Value = "0" });
                        }
                        ddlExpenseType.Add(new SelectListItem { Text = r["ExpenseTypeName"].ToString(), Value = r["Pk_ExpenseTypeId"].ToString() });
                        count1 = count1 + 1;
                    }
                }
                ViewBag.ddlExpenseType = ddlExpenseType;
                #endregion
            }
            catch (Exception ex)
            {

                TempData["MsgExpense"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult Expenselist(Expenses model)
        {
            List<Expenses> lst = new List<Expenses>();
            model.ExpenseID = model.ExpenseID == "0" ? null : model.ExpenseID;
            model.ExpenseName = model.ExpenseName == "0" ? null : model.ExpenseName;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
            DataSet ds = model.GetExpenselist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var i = 0;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    //  if (i < 25)
                    {
                        Expenses obj = new Expenses();
                        obj.ExpenseTypeId = Crypto.Encrypt(r["FK_ExpenseTypeId"].ToString());
                        obj.ExpenseType = r["ExpenseTypeName"].ToString();
                        obj.ExpenseName = r["Pk_ExpenseId"].ToString();
                        obj.AddedOn = r["AddedDate"].ToString();
                        obj.ExpenseName = r["ExpenseName"].ToString();
                        obj.FK_ExpenseNameId = r["ExpenseName"].ToString();
                        obj.IsActive = r["IsActive"].ToString();
                        obj.EncryptKey = Crypto.Encrypt(r["Pk_ExpenseId"].ToString());
                        lst.Add(obj);
                    }
                    //   i = i + 1;
                }
                model.ExpenseList = lst;
            }

            #region ddlExpensetype
            int ccount = 0;
            int ecount = 0;
            List<SelectListItem> ddlexpensetype = new List<SelectListItem>();
            DataSet dsexpensetype = model.GetExpenseTypeList();
            if (dsexpensetype != null && dsexpensetype.Tables.Count > 0 && dsexpensetype.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in dsexpensetype.Tables[0].Rows)
                {
                    if (ecount == 0)
                    {
                        ddlexpensetype.Add(new SelectListItem { Text = "Expense Type", Value = "0" });
                    }
                    ddlexpensetype.Add(new SelectListItem { Text = r["ExpenseTypeName"].ToString(), Value = r["Pk_ExpenseTypeId"].ToString() });
                    ecount = ecount + 1;
                }
            }
            ViewBag.ExpenseType = ddlexpensetype;
            #endregion

            List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            ddlexpensename.Add(new SelectListItem { Text = "Select Expense Name", Value = "0" });
            ViewBag.ddlexpensename = ddlexpensename;

            return View(model);
        }
        [HttpPost]
        [ActionName("Expenselist")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ExpenseList(Expenses model)
        {
            List<Expenses> lst = new List<Expenses>();
            model.ExpenseID = model.ExpenseID == "0" ? null : model.ExpenseID;
            model.ExpenseName = model.ExpenseName == "0" ? null : model.ExpenseName;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
            DataSet ds = model.GetExpenselist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.ExpenseTypeId = Crypto.Encrypt(r["FK_ExpenseTypeId"].ToString());
                    obj.ExpenseType = r["ExpenseTypeName"].ToString();
                    obj.ExpenseName = r["Pk_ExpenseId"].ToString();
                    obj.AddedOn = r["AddedDate"].ToString();
                    obj.ExpenseName = r["ExpenseName"].ToString();
                    obj.FK_ExpenseNameId = r["ExpenseName"].ToString();
                    obj.IsActive = r["IsActive"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["Pk_ExpenseId"].ToString());
                    lst.Add(obj);
                }
                model.ExpenseList = lst;
            }

            #region ddlExpensetype
            int ccount = 0;
            int ecount = 0;
            List<SelectListItem> ddlexpensetype = new List<SelectListItem>();
            DataSet dsexpensetype = model.GetExpenseTypeList();
            if (dsexpensetype != null && dsexpensetype.Tables.Count > 0 && dsexpensetype.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in dsexpensetype.Tables[0].Rows)
                {
                    if (ecount == 0)
                    {
                        ddlexpensetype.Add(new SelectListItem { Text = "Expense Type", Value = "0" });
                    }
                    ddlexpensetype.Add(new SelectListItem { Text = r["ExpenseTypeName"].ToString(), Value = r["Pk_ExpenseTypeId"].ToString() });
                    ecount = ecount + 1;
                }
            }
            ViewBag.ExpenseType = ddlexpensetype;
            #endregion

            List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            ddlexpensename.Add(new SelectListItem { Text = "Select Expense Name", Value = "0" });
            ViewBag.ddlexpensename = ddlexpensename;
            return View(model);
        }
        public ActionResult DeleteExpense(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Expenses obj = new Expenses();
                obj.Pk_ExpenseId = Crypto.Decrypt(id);
                obj.DeletedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteExpense();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Expenselist"] = "Expense deleted successfully";
                        FormName = "Expenselist";
                        Controller = "Expense";
                    }
                    else
                    {
                        TempData["Expenselist"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "Expenselist";
                        Controller = "Expense";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Expenselist"] = ex.Message;
                FormName = "Expenselist";
                Controller = "Expense";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult ExpenseStatus(string id, string IsActive)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Expenses obj = new Expenses();
                if (IsActive == "False")
                {
                    obj.IsActive = "1";
                }
                else
                {
                    obj.IsActive = "0";
                }
                obj.Pk_ExpenseId = Crypto.Decrypt(id);
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.ExpenseStatus();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Expenselist"] = "Status Updated successfully";
                        FormName = "Expenselist";
                        Controller = "Expense";
                    }
                    else
                    {
                        TempData["Expenselist"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "Expenselist";
                        Controller = "Expense";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Expenselist"] = ex.Message;
                FormName = "Expenselist";
                Controller = "Expense";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion

        public ActionResult ExpenseLedgerByName(string FId, string N)
        {
            Expenses model = new Expenses();
            model.ExpenseID = model.ExpenseID == "0" ? null : model.ExpenseID;
            model.Fk_Expenseid = Crypto.Decrypt(N);
            model.ExpenseTypeId = Crypto.Decrypt(FId);
            List<Expenses> Clearedlst = new List<Expenses>();
            DataSet ds = model.GetledgerByName();
            ViewBag.TotalDrAmount = 0;
            ViewBag.TotalCrAmount = 0;
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.Pk_ExpenseDetailsId = r["Pk_ExpenseDetailsId"].ToString();
                    obj.ExpenseName = r["ExpenseName"].ToString();
                    obj.ExpenseID = r["ExpenseType"].ToString();
                    obj.Remarks = r["Remarks"].ToString();
                    obj.ChequeStatus = r["TransactionStatus"].ToString();
                    obj.BankName = r["TransactionType"].ToString();
                    obj.TransactionTy = r["EntryType"].ToString();
                    obj.Transanction = r["TransactionStatus"].ToString();
                    obj.ChequeNo = r["ChequeNo"].ToString();
                    obj.ChequeStatusUpdateDate = r["ChequeStatusUpdateDate"].ToString();
                    //obj.CrAmount = double.Parse(r["CrAmount"].ToString()).ToString("n2");
                    obj.CrAmount = r["CrAmount"].ToString();
                    //obj.DrAmount = double.Parse(r["DrAmount"].ToString()).ToString("n2");
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Date = r["CheckDate"].ToString();
                    Clearedlst.Add(obj);
                    ViewBag.TotalDrAmount = Convert.ToDecimal(ViewBag.TotalDrAmount) + Convert.ToDecimal(r["DrAmount"].ToString());
                    ViewBag.TotalCrAmount = Convert.ToDecimal(ViewBag.TotalCrAmount) + Convert.ToDecimal(r["CrAmount"].ToString());

                }
                model.ClearedListItem = Clearedlst;
                //ViewBag.TotalDrAmount = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
                //ViewBag.TotalCrAmount = double.Parse(ds.Tables[0].Compute("sum(CrAmount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }

        public ActionResult ViewCrExpense(Expenses model)
        {

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
            List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            ddlexpensename.Add(new SelectListItem { Text = "Select Expense Name", Value = "0" });
            ViewBag.ddlexpensename = ddlexpensename;
            //ViewBag.ddlcompany = ddlcompany;
            //#endregion
            #region ddlExpenseHead
            int ccount12 = 0;
            Master master = new Master();
            List<SelectListItem> ddlExpenseHead = new List<SelectListItem>();
            DataSet ds1 = master.GetExpenseHeadList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (ccount12 == 0)
                    {
                        ddlExpenseHead.Add(new SelectListItem { Text = "Select Expense Head", Value = "0" });
                    }
                    ddlExpenseHead.Add(new SelectListItem { Text = r["ExpenseHeadName"].ToString(), Value = r["PK_ExpenseHead"].ToString() });
                    ccount12 = ccount12 + 1;
                }
            }
            ViewBag.ddlExpenseHead = ddlExpenseHead;
            #endregion
            #region transactiontype
            int count = 0;
            List<SelectListItem> ddlTransactionType = new List<SelectListItem>();
            DataSet ddlTransaction = model.GetTransactionList();
            if (ddlTransaction != null && ddlTransaction.Tables.Count > 0 && ddlTransaction.Tables[0].Rows.Count > 0)
            {
                //ddlTransactionType.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
                foreach (DataRow r in ddlTransaction.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlTransactionType.Add(new SelectListItem { Text = "Select TransactionType", Value = "0" });
                    }
                    ddlTransactionType.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });
                    count = count + 1;

                }
            }
            ViewBag.ddlTransactionType = ddlTransactionType;
            #endregion
            List<SelectListItem> CheckStatus = Common.CheckStatus();
            ViewBag.CheckStatus = CheckStatus;
            #region ddlExpensetype
            int ecount = 0;
            List<SelectListItem> ddlexpensetype = new List<SelectListItem>();
            DataSet dsexpensetype = model.GetExpenseTypeList();
            if (dsexpensetype != null && dsexpensetype.Tables.Count > 0 && dsexpensetype.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in dsexpensetype.Tables[0].Rows)
                {
                    if (ecount == 0)
                    {
                        ddlexpensetype.Add(new SelectListItem { Text = "Expense Type", Value = "0" });
                    }
                    ddlexpensetype.Add(new SelectListItem { Text = r["ExpenseTypeName"].ToString(), Value = r["Pk_ExpenseTypeId"].ToString() });
                    ecount = ecount + 1;
                }
            }
            ViewBag.ExpenseType = ddlexpensetype;
            #endregion
            #region ddlExpenseCategory
            int Category = 0;
            List<SelectListItem> ddlexpensecategory = new List<SelectListItem>();
            DataSet dscat = model.GetExpenseCategory();
            if (dscat != null && dscat.Tables.Count > 0 && dscat.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in dscat.Tables[0].Rows)
                {
                    if (Category == 0)
                    {
                        ddlexpensecategory.Add(new SelectListItem { Text = "Expense Category", Value = "0" });
                    }
                    ddlexpensecategory.Add(new SelectListItem { Text = r["ExpenseCategory"].ToString(), Value = r["Pk_ExpenseCategoryId"].ToString() });
                    Category = Category + 1;
                }
            }
            ViewBag.ExpenseCategory = ddlexpensecategory;
            #endregion
            #region bindTeam
            Master team = new Master();
            int teamcount = 0;
            List<SelectListItem> ddlteam = new List<SelectListItem>();
            DataSet teamds = team.Teamlist();
            if (teamds != null && teamds.Tables.Count > 0 && teamds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in teamds.Tables[0].Rows)
                {
                    if (teamcount == 0)
                    {
                        ddlteam.Add(new SelectListItem { Text = "Select Team", Value = "0" });
                    }
                    ddlteam.Add(new SelectListItem { Text = r["TeamName"].ToString(), Value = r["PK_TeamId"].ToString() });
                    teamcount = teamcount = 1;
                }
            }
            ViewBag.ddlTeam = ddlteam;
            #endregion


            //List<Expenses> Crlst = new List<Expenses>();
            List<Expenses> Clearedlst = new List<Expenses>();
            List<Expenses> Pendinglst = new List<Expenses>();
            List<Expenses> Bouncelst = new List<Expenses>();
            model.ChequeStatus = "Cr";
            model.ExpenseID = model.ExpenseID == "0" ? null : model.ExpenseID;
            model.ExpenseName = model.ExpenseName == "0" ? null : model.ExpenseName;
            model.Fk_CompanyId = model.Fk_CompanyId == "0" ? null : model.Fk_CompanyId;
            model.FK_ExpenseHead = model.FK_ExpenseHead == "0" ? null : model.FK_ExpenseHead;
            model.TransactionID = model.TransactionID == "0" ? null : model.TransactionID;
            model.Fk_ExpenseCategoryId = model.Fk_ExpenseCategoryId == "0" ? null : model.Fk_ExpenseCategoryId;
            model.Fk_Teamid = model.Fk_Teamid == "0" ? null : model.Fk_Teamid;
            DateTime now = DateTime.Now;
            DateTime currentDate = DateTime.Now;
            currentDate = currentDate.AddDays(-7);
            model.FromDate = currentDate.ToString("dd/MM/yyyy");
            model.ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            model.NFromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.NToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.EntryFromDate = string.IsNullOrEmpty(model.EntryFromDate) ? null : Common.ConvertToSystemDate(model.EntryFromDate, "dd/MM/yyyy");
            model.EntryToDate = string.IsNullOrEmpty(model.EntryToDate) ? null : Common.ConvertToSystemDate(model.EntryToDate, "dd/MM/yyyy");
            model.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
            //model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            //model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetCrExpenselist1();
            ViewBag.TotalCleredPaid = 0;
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
            {
                var i = 0;

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (i < 25)
                    {
                        Expenses obj = new Expenses();
                        obj.CompanyName = r["CompanyName"].ToString();
                        obj.TeamName = r["TeamName"].ToString();
                        obj.ExpenseHeadName = r["ExpenseHeadName"].ToString();
                        obj.PlotInfo = r["PlotInfo"].ToString();
                        obj.Pk_ExpenseDetailsId = r["Pk_ExpenseDetailsId"].ToString();
                        obj.ExpenseName = r["ExpenseName"].ToString();
                        obj.ExpenseID = r["ExpenseType"].ToString();
                        obj.Remarks = r["Remarks"].ToString();
                        obj.ChequeStatus = r["TransactionStatus"].ToString();
                        obj.TransactionTy = r["TransactionType"].ToString();
                        obj.Transanction = r["TransactionStatus"].ToString();
                        obj.CrAmount = (r["CrAmount"].ToString());
                        obj.ChequeNo = r["ChequeNo"].ToString();
                        obj.ChequeStatusUpdateDate = r["ChequeStatusUpdateDate"].ToString();
                        obj.DrAmount = double.Parse(r["DrAmount"].ToString()).ToString("n2");
                        obj.Date = r["CheckDate"].ToString();
                        obj.ExpenseCategoryName = r["ExpenseCategoryName"].ToString();
                        obj.PaymentMode = r["PaymentMode1"].ToString();
                        Clearedlst.Add(obj);
                        ViewBag.TotalCleredPaid = Convert.ToDecimal(ViewBag.TotalCleredPaid) + Convert.ToDecimal(r["CrAmount"].ToString());
                    }
                    i = i + 1;
                }
                model.ClearedListItem = Clearedlst;

            }
            ViewBag.TotalPendingPaid = 0;
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[1].Rows.Count > 0)
            {
                var i = 0;
                foreach (DataRow pr in ds.Tables[1].Rows)
                {
                    if (i < 25)
                    {
                        Expenses obj = new Expenses();
                        obj.CompanyName = pr["CompanyName"].ToString();
                        obj.TeamName = pr["TeamName"].ToString();
                        obj.ExpenseHeadName = pr["ExpenseHeadName"].ToString();
                        obj.PlotInfo = pr["PlotInfo"].ToString();
                        obj.Pk_ExpenseDetailsId = pr["Pk_ExpenseDetailsId"].ToString();
                        obj.ExpenseName = pr["ExpenseName"].ToString();
                        obj.ExpenseID = pr["ExpenseType"].ToString();
                        obj.Remarks = pr["Remarks"].ToString();
                        obj.ChequeStatus = pr["TransactionStatus"].ToString();
                        obj.TransactionTy = pr["TransactionType"].ToString();
                        obj.Transanction = pr["TransactionStatus"].ToString();
                        obj.ChequeNo = pr["ChequeNo"].ToString();
                        obj.ChequeStatusUpdateDate = pr["ChequeStatusUpdateDate"].ToString();
                        obj.CrAmount = double.Parse(pr["CrAmount"].ToString()).ToString("n2");  //CrAmount 
                        obj.DrAmount = double.Parse(pr["DrAmount"].ToString()).ToString("n2");
                        obj.ExpenseCategoryName = pr["ExpenseCategoryName"].ToString();
                        obj.Date = pr["CheckDate"].ToString();
                        obj.PaymentMode1 = pr["PaymentMode"].ToString();
                        Pendinglst.Add(obj);
                        ViewBag.TotalPendingPaid = Convert.ToDecimal(ViewBag.TotalPendingPaid) + Convert.ToDecimal(pr["CrAmount"].ToString());
                    }
                    i = i + 1;
                }
                model.PendingListItem = Pendinglst;

            }
            ViewBag.TotalBouncePaid = 0;
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[2].Rows.Count > 0)
            {
                var i = 0;
                foreach (DataRow r in ds.Tables[2].Rows)
                {
                    if (i < 25)
                    {
                        Expenses obj = new Expenses();
                        obj.CompanyName = r["CompanyName"].ToString();
                        obj.ExpenseHeadName = r["ExpenseHeadName"].ToString();
                        obj.PlotInfo = r["PlotInfo"].ToString();
                        obj.Pk_ExpenseDetailsId = r["Pk_ExpenseDetailsId"].ToString();
                        obj.ExpenseName = r["ExpenseName"].ToString();
                        obj.ExpenseID = r["ExpenseType"].ToString();
                        obj.Remarks = r["Remarks"].ToString();
                        obj.ChequeStatus = r["TransactionStatus"].ToString();
                        obj.TransactionTy = r["TransactionType"].ToString();
                        obj.Transanction = r["TransactionStatus"].ToString();
                        obj.ChequeNo = r["ChequeNo"].ToString();
                        obj.ChequeStatusUpdateDate = r["ChequeStatusUpdateDate"].ToString();
                        obj.CrAmount = double.Parse(r["CrAmount"].ToString()).ToString("n2");
                        obj.DrAmount = double.Parse(r["DrAmount"].ToString()).ToString("n2");
                        obj.ExpenseCategoryName = r["ExpenseCategoryName"].ToString();
                        obj.PaymentMode2 = r["PaymentMode"].ToString();
                        Bouncelst.Add(obj);
                        ViewBag.TotalBouncePaid = Convert.ToDecimal(ViewBag.TotalBouncePaid) + Convert.ToDecimal(r["CrAmount"].ToString());
                    }
                    i = i + 1;
                }
                model.BounceListItem = Bouncelst;
            }


            return View(model);

            //List<SelectListItem> CheckStatus = Common.CheckStatus();
            //ViewBag.CheckStatus = CheckStatus;
            //List<SelectListItem> ExpenseType = Common.ExpenseType();
            //ViewBag.ExpenseType = ExpenseType;
            //return View(model);
        }
        [HttpPost]
        [ActionName("ViewCrExpense")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult ViewCrExpens(Expenses model)
        {
            List<SelectListItem> CheckStatus = Common.CheckStatus();
            ViewBag.CheckStatus = CheckStatus;
            //List<SelectListItem> ExpenseType = Common.ExpenseType();
            //ViewBag.ExpenseType = ExpenseType;
            #region ddlExpensetype
            int ccount = 0;
            int ecount = 0;
            List<SelectListItem> ddlexpensetype = new List<SelectListItem>();
            DataSet dsexpensetype = model.GetExpenseTypeList();
            if (dsexpensetype != null && dsexpensetype.Tables.Count > 0 && dsexpensetype.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in dsexpensetype.Tables[0].Rows)
                {
                    if (ecount == 0)
                    {
                        ddlexpensetype.Add(new SelectListItem { Text = "Expense Type", Value = "0" });
                    }
                    ddlexpensetype.Add(new SelectListItem { Text = r["ExpenseTypeName"].ToString(), Value = r["Pk_ExpenseTypeId"].ToString() });
                    ecount = ecount + 1;
                }
            }
            ViewBag.ExpenseType = ddlexpensetype;
            #endregion
            #region ddlExpenseHead
            int ccount12 = 0;
            Master master = new Master();
            List<SelectListItem> ddlExpenseHead = new List<SelectListItem>();
            DataSet ds1 = master.GetExpenseHeadList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (ccount12 == 0)
                    {
                        ddlExpenseHead.Add(new SelectListItem { Text = "Select Expense Head", Value = "0" });
                    }
                    ddlExpenseHead.Add(new SelectListItem { Text = r["ExpenseHeadName"].ToString(), Value = r["PK_ExpenseHead"].ToString() });
                    ccount12 = ccount12 + 1;
                }
            }
            ViewBag.ddlExpenseHead = ddlExpenseHead;
            #endregion
            #region ddlExpenseCategory
            int Category = 0;
            List<SelectListItem> ddlexpensecategory = new List<SelectListItem>();
            DataSet dscat = model.GetExpenseCategory();
            if (dscat != null && dscat.Tables.Count > 0 && dscat.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in dscat.Tables[0].Rows)
                {
                    if (Category == 0)
                    {
                        ddlexpensecategory.Add(new SelectListItem { Text = "Expense Category", Value = "0" });
                    }
                    ddlexpensecategory.Add(new SelectListItem { Text = r["ExpenseCategory"].ToString(), Value = r["Pk_ExpenseCategoryId"].ToString() });
                    Category = Category + 1;
                }
            }
            ViewBag.ExpenseCategory = ddlexpensecategory;
            #endregion
            #region transactiontype
            int count = 0;
            List<SelectListItem> ddlTransactionType = new List<SelectListItem>();
            DataSet ddlTransaction = model.GetTransactionList();
            if (ddlTransaction != null && ddlTransaction.Tables.Count > 0 && ddlTransaction.Tables[0].Rows.Count > 0)
            {
                //ddlTransactionType.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
                foreach (DataRow r in ddlTransaction.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlTransactionType.Add(new SelectListItem { Text = "Select TransactionType", Value = "0" });
                    }
                    ddlTransactionType.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });
                    count = count + 1;

                }
            }
            ViewBag.ddlTransactionType = ddlTransactionType;
            #endregion
            #region bindTeam
            Master team = new Master();
            int teamcount = 0;
            List<SelectListItem> ddlteam = new List<SelectListItem>();
            DataSet teamds = team.Teamlist();
            if (teamds != null && teamds.Tables.Count > 0 && teamds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in teamds.Tables[0].Rows)
                {
                    if (teamcount == 0)
                    {
                        ddlteam.Add(new SelectListItem { Text = "Select Team", Value = "0" });
                    }
                    ddlteam.Add(new SelectListItem { Text = r["TeamName"].ToString(), Value = r["PK_TeamId"].ToString() });
                    teamcount = teamcount = 1;
                }
            }
            ViewBag.ddlTeam = ddlteam;
            #endregion
            //List<Expenses> Crlst = new List<Expenses>();
            List<Expenses> Clearedlst = new List<Expenses>();
            List<Expenses> Pendinglst = new List<Expenses>();
            List<Expenses> Bouncelst = new List<Expenses>();
            model.ChequeStatus = "Cr";
            model.ExpenseID = model.ExpenseID == "0" ? null : model.ExpenseID;
            model.ExpenseName = model.ExpenseName == "0" ? null : model.ExpenseName;
            model.Fk_CompanyId = model.Fk_CompanyId == "0" ? null : model.Fk_CompanyId;
            model.FK_ExpenseHead = model.FK_ExpenseHead == "0" ? null : model.FK_ExpenseHead;
            model.TransactionID = model.TransactionID == "0" ? null : model.TransactionID;
            model.Fk_ExpenseCategoryId = model.Fk_ExpenseCategoryId == "0" ? null : model.Fk_ExpenseCategoryId;
            model.Fk_Teamid = model.Fk_Teamid == "0" ? null : model.Fk_Teamid;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.EntryFromDate = string.IsNullOrEmpty(model.EntryFromDate) ? null : Common.ConvertToSystemDate(model.EntryFromDate, "dd/MM/yyyy");
            model.EntryToDate = string.IsNullOrEmpty(model.EntryToDate) ? null : Common.ConvertToSystemDate(model.EntryToDate, "dd/MM/yyyy");
            model.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
            DataSet ds = model.GetCrExpenselist();
            ViewBag.TotalCleredPaid = 0;
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.TeamName = r["TeamName"].ToString();
                    obj.CompanyName = r["CompanyName"].ToString();
                    obj.ExpenseHeadName = r["ExpenseHeadName"].ToString();
                    obj.PlotInfo = r["PlotInfo"].ToString();
                    obj.Pk_ExpenseDetailsId = r["Pk_ExpenseDetailsId"].ToString();
                    obj.ExpenseName = r["ExpenseName"].ToString();
                    obj.ExpenseID = r["ExpenseType"].ToString();
                    obj.Remarks = r["Remarks"].ToString();
                    obj.ChequeStatus = r["TransactionStatus"].ToString();
                    obj.TransactionTy = r["TransactionType"].ToString();
                    obj.Transanction = r["TransactionStatus"].ToString();
                    obj.ChequeNo = r["ChequeNo"].ToString();
                    obj.ChequeStatusUpdateDate = r["ChequeStatusUpdateDate"].ToString();
                    obj.CrAmount = double.Parse(r["CrAmount"].ToString()).ToString();
                    obj.DrAmount = double.Parse(r["DrAmount"].ToString()).ToString("n2");
                    obj.Date = r["CheckDate"].ToString();
                    obj.ExpenseCategoryName = r["ExpenseCategoryName"].ToString();
                    obj.PaymentMode = r["PaymentMode1"].ToString();
                    Clearedlst.Add(obj);
                    ViewBag.TotalCleredPaid = Convert.ToDecimal(ViewBag.TotalCleredPaid) + Convert.ToDecimal(r["CrAmount"].ToString());
                }
                model.ClearedListItem = Clearedlst;

            }
            ViewBag.TotalPendingPaid = 0;
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow pr in ds.Tables[1].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.TeamName = pr["TeamName"].ToString();
                    obj.CompanyName = pr["CompanyName"].ToString();
                    obj.ExpenseHeadName = pr["ExpenseHeadName"].ToString();
                    obj.PlotInfo = pr["PlotInfo"].ToString();
                    obj.Pk_ExpenseDetailsId = pr["Pk_ExpenseDetailsId"].ToString();
                    obj.ExpenseName = pr["ExpenseName"].ToString();
                    obj.ExpenseID = pr["ExpenseType"].ToString();
                    obj.Remarks = pr["Remarks"].ToString();
                    obj.ChequeStatus = pr["TransactionStatus"].ToString();
                    obj.TransactionTy = pr["TransactionType"].ToString();
                    obj.Transanction = pr["TransactionStatus"].ToString();
                    obj.ChequeNo = pr["ChequeNo"].ToString();
                    obj.ChequeStatusUpdateDate = pr["ChequeStatusUpdateDate"].ToString();
                    obj.CrAmount = double.Parse(pr["CrAmount"].ToString()).ToString("n2");
                    obj.DrAmount = double.Parse(pr["DrAmount"].ToString()).ToString("n2");
                    obj.Date = pr["CheckDate"].ToString();
                    obj.ExpenseCategoryName = pr["ExpenseCategoryName"].ToString();
                    obj.PaymentMode1 = pr["PaymentMode"].ToString();
                    Pendinglst.Add(obj);
                    ViewBag.TotalPendingPaid = Convert.ToDecimal(ViewBag.TotalPendingPaid) + Convert.ToDecimal(pr["CrAmount"].ToString());
                }
                model.PendingListItem = Pendinglst;


            }
            ViewBag.TotalBouncePaid = 0;
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[2].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.CompanyName = r["CompanyName"].ToString();
                    obj.ExpenseHeadName = r["ExpenseHeadName"].ToString();
                    obj.PlotInfo = r["PlotInfo"].ToString();
                    obj.Pk_ExpenseDetailsId = r["Pk_ExpenseDetailsId"].ToString();
                    obj.ExpenseName = r["ExpenseName"].ToString();
                    obj.ExpenseID = r["ExpenseType"].ToString();
                    obj.Remarks = r["Remarks"].ToString();
                    obj.ChequeStatus = r["TransactionStatus"].ToString();
                    obj.TransactionTy = r["TransactionType"].ToString();
                    obj.Transanction = r["TransactionStatus"].ToString();
                    obj.ChequeNo = r["ChequeNo"].ToString();
                    obj.ChequeStatusUpdateDate = r["ChequeStatusUpdateDate"].ToString();
                    obj.CrAmount = double.Parse(r["CrAmount"].ToString()).ToString("n2");
                    obj.DrAmount = double.Parse(r["DrAmount"].ToString()).ToString("n2");
                    obj.Date = r["CheckDate"].ToString();
                    obj.ExpenseCategoryName = r["ExpenseCategoryName"].ToString();
                    obj.PaymentMode2 = r["PaymentMode"].ToString();
                    Bouncelst.Add(obj);
                    ViewBag.TotalBouncePaid = Convert.ToDecimal(ViewBag.TotalBouncePaid) + Convert.ToDecimal(r["CrAmount"].ToString());
                }
                model.BounceListItem = Bouncelst;
                //ViewBag.TotalBouncePaid = double.Parse(ds.Tables[2].Compute("sum(CrAmount)", "").ToString()).ToString("n2");
            }
            //#region ddlcompany
            //Master obj1 = new Master();
            //List<SelectListItem> ddlcompany = new List<SelectListItem>();
            //obj1.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
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
            ////List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            ////ddlexpensename.Add(new SelectListItem { Text = "Select Expense Name", Value = "0" });
            ////ViewBag.ddlexpensename = ddlexpensename;
            //#endregion
            #region GetExpenseName
            List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            model.Result = "yes";
            int ExpenseCount = 0;
            DataSet ds11 = model.GetExpenseNameList();
            if (ds11 != null && ds11.Tables.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    if (ExpenseCount == 0)
                    {
                        ddlexpensename.Add(new SelectListItem { Text = "Select Expense Name", Value = "0" });
                    }
                    ddlexpensename.Add(new SelectListItem { Text = r["ExpenseName"].ToString(), Value = r["FK_ExpenseId"].ToString() });
                    ExpenseCount = ExpenseCount + 1;
                }
            }
            ViewBag.ddlexpensename = ddlexpensename;
            #endregion
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in ds.Tables[0].Rows)
            //    {
            //        Expenses obj = new Expenses();
            //        obj.Pk_ExpenseDetailsId = r["Pk_ExpenseDetailsId"].ToString();
            //        obj.ExpenseName = r["ExpenseName"].ToString();
            //        obj.ExpenseID = r["ExpenseType"].ToString();
            //        obj.Remarks = r["Remarks"].ToString();
            //        obj.ChequeStatus = r["TransactionStatus"].ToString();
            //        obj.TransactionTy = r["TransactionType"].ToString();
            //        obj.Transanction = r["TransactionStatus"].ToString();
            //        obj.CrAmount = r["CrAmount"].ToString();
            //        obj.DrAmount = r["DrAmount"].ToString();
            //        obj.Date = r["CheckDate"].ToString();   
            //        Crlst.Add(obj);
            //    }
            //    model.CrExpenseList = Crlst;
            //}

            return View(model);
        }
        public ActionResult UpdateExpenseCheckStaus(string CheqStatus, string ExpenseDetailsId, string CheckDate)
        {
            string FormName = "";
            string Controller = "";
            Expenses obj = new Expenses();
            try
            {
                obj.FK_ExpenseDetailsId = ExpenseDetailsId;
                obj.ChequeStatus = CheqStatus;
                obj.ChequeStatusUpdateDate = string.IsNullOrEmpty(CheckDate) ? null : Common.ConvertToSystemDate(CheckDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateCheckStatus();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Result = "Yes";
                        FormName = "ViewCrExpense";
                        Controller = "Expense";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ViewCrExpense";
                        Controller = "Expense";
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
                FormName = "ViewCrExpense";
                Controller = "Expense";
            }
            //return RedirectToAction(FormName); 
            var uid = (ExpenseDetailsId);
            return RedirectToAction(FormName, new { fid = uid });
            // return RedirectToAction((FormName+"?id="+UserId).Trim(),Controller);
        }
        public ActionResult UpdateCompany(string CompanyId, string ExpenseDetailsId, string ExpenseheadId, string expenseTypeId, string expenseName, string expensecategoryId, string Fk_Teamid, string Remarks)
        {

            Expenses obj = new Expenses();
            try
            {
                obj.FK_ExpenseDetailsId = ExpenseDetailsId;
                if (CompanyId != "0")
                {
                    obj.Fk_CompanyId = CompanyId;
                }
                else
                {
                    obj.Fk_CompanyId = CompanyId == "0" ? null : CompanyId;
                }
                if (ExpenseheadId != "0")
                {
                    obj.FK_ExpenseHead = ExpenseheadId;
                }
                else
                {
                    obj.FK_ExpenseHead = ExpenseheadId == "0" ? null : ExpenseheadId;

                }
                if (expenseTypeId != "0")
                {
                    obj.ExpenseIDD = expenseTypeId;
                }
                else
                {
                    obj.ExpenseIDD = expenseTypeId == "0" ? null : expenseTypeId;

                }
                if (expenseName != "0")
                {
                    obj.ExpenseName1 = expenseName;
                }
                else
                {
                    obj.ExpenseName1 = expenseName == "0" ? null : expenseName;

                }
                if (expensecategoryId != "0")
                {
                    obj.Fk_ExpenseCategoryId = expensecategoryId;
                }
                else
                {
                    obj.Fk_ExpenseCategoryId = expensecategoryId == "0" ? null : expensecategoryId;
                }
                if (Fk_Teamid != "0")
                {
                    obj.Fk_Teamid = Fk_Teamid;
                }
                else
                {
                    obj.Fk_Teamid = Fk_Teamid == "0" ? null : Fk_Teamid;
                }
                if (Remarks != "")
                {
                    obj.Remarks = Remarks;
                }

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateCompany();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Result = "Yes";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
            //return RedirectToAction(FormName); 
            //var uid = (ExpenseDetailsId);
            //return RedirectToAction(FormName, new { fid = uid });
            // return RedirectToAction((FormName+"?id="+UserId).Trim(),Controller);
        }
        public ActionResult UpdateRemarks(string ExpenseDetailsId, string Remark)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Expenses model = new Expenses();

                model.Pk_ExpenseDetailsId = ExpenseDetailsId;
                model.Remarks = Remark;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.updateRemarks();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["CrExpenseList"] = "Remarks Updated!";
                    }
                    else
                    {
                        TempData["CrExpenseList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["CrExpenseList"] = ex.Message;
            }
            FormName = "ViewCrExpense";
            Controller = "Expense";
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult DeleteExpenseDetails(string ExpenseDetailsId, string status)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Expenses model = new Expenses();
                model.Status = status;
                model.Pk_ExpenseDetailsId = ExpenseDetailsId;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteExpenseDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {

                        TempData["CrExpenseList"] = "Expense Details Deleted!";
                    }
                    else
                    {
                        TempData["CrExpenseList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["CrExpenseList"] = ex.Message;
            }
            FormName = "ViewCrExpense";
            Controller = "Expense";

            return RedirectToAction(FormName, Controller);
        }

        #region ViewDrExpense
        public ActionResult ViewDrExpense(Expenses model)
        {
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
            #region transactiontype
            int count = 0;
            List<SelectListItem> ddlTransactionType = new List<SelectListItem>();
            DataSet ddlTransaction = model.GetTransactionList();
            if (ddlTransaction != null && ddlTransaction.Tables.Count > 0 && ddlTransaction.Tables[0].Rows.Count > 0)
            {
                //ddlTransactionType.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
                foreach (DataRow r in ddlTransaction.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlTransactionType.Add(new SelectListItem { Text = "Select TransactionType", Value = "0" });
                    }
                    ddlTransactionType.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });
                    count = count + 1;

                }
            }
            ViewBag.ddlTransactionType = ddlTransactionType;
            #endregion
            #region ddlExpenseHead
            int ccount12 = 0;
            Master master = new Master();
            List<SelectListItem> ddlExpenseHead = new List<SelectListItem>();
            DataSet ds1 = master.GetExpenseHeadList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (ccount12 == 0)
                    {
                        ddlExpenseHead.Add(new SelectListItem { Text = "Select Expense Head", Value = "0" });
                    }
                    ddlExpenseHead.Add(new SelectListItem { Text = r["ExpenseHeadName"].ToString(), Value = r["PK_ExpenseHead"].ToString() });
                    ccount12 = ccount12 + 1;
                }
            }
            ViewBag.ddlExpenseHead = ddlExpenseHead;
            #endregion
            List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            ddlexpensename.Add(new SelectListItem { Text = "Select Expense Name", Value = "0" });
            ViewBag.ddlexpensename = ddlexpensename;
            List<SelectListItem> CheckStatus = Common.CheckStatus();
            ViewBag.CheckStatus = CheckStatus;
            #region ddlExpensetype
            int ecount = 0;
            List<SelectListItem> ddlexpensetype = new List<SelectListItem>();
            DataSet dsexpensetype = model.GetExpenseTypeList();
            if (dsexpensetype != null && dsexpensetype.Tables.Count > 0 && dsexpensetype.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in dsexpensetype.Tables[0].Rows)
                {
                    if (ecount == 0)
                    {
                        ddlexpensetype.Add(new SelectListItem { Text = "Expense Type", Value = "0" });
                    }
                    ddlexpensetype.Add(new SelectListItem { Text = r["ExpenseTypeName"].ToString(), Value = r["Pk_ExpenseTypeId"].ToString() });
                    ecount = ecount + 1;
                }
            }
            ViewBag.ExpenseType = ddlexpensetype;
            #endregion
            #region ddlExpenseCategory
            int Category = 0;
            List<SelectListItem> ddlexpensecategory = new List<SelectListItem>();
            DataSet dscat = model.GetExpenseCategory();
            if (dscat != null && dscat.Tables.Count > 0 && dscat.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in dscat.Tables[0].Rows)
                {
                    if (Category == 0)
                    {
                        ddlexpensecategory.Add(new SelectListItem { Text = "Expense Category", Value = "0" });
                    }
                    ddlexpensecategory.Add(new SelectListItem { Text = r["ExpenseCategory"].ToString(), Value = r["Pk_ExpenseCategoryId"].ToString() });
                    Category = Category + 1;
                }
            }
            ViewBag.ExpenseCategory = ddlexpensecategory;
            #endregion
            List<Expenses> Clearedlst = new List<Expenses>();
            List<Expenses> Pendinglst = new List<Expenses>();
            List<Expenses> Bouncelst = new List<Expenses>();

            List<Expenses> CancelledPendinglst = new List<Expenses>();
            model.ChequeStatus = "Dr";
            model.ExpenseID = model.ExpenseID == "0" ? null : model.ExpenseID;
            model.FK_ExpenseHead = model.FK_ExpenseHead == "0" ? null : model.FK_ExpenseHead;
            model.Fk_ExpenseCategoryId = model.Fk_ExpenseCategoryId == "0" ? null : model.Fk_ExpenseCategoryId;
            model.TransactionID = model.TransactionID == "0" ? null : model.TransactionID;
            DateTime now = DateTime.Now;
            DateTime currentDate = DateTime.Now;
            currentDate = currentDate.AddDays(-7);
            model.FromDate = currentDate.ToString("dd/MM/yyyy");
            model.ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            model.NFromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.NToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.EntryFromDate = string.IsNullOrEmpty(model.EntryFromDate) ? null : Common.ConvertToSystemDate(model.EntryFromDate, "dd/MM/yyyy");
            model.EntryToDate = string.IsNullOrEmpty(model.EntryToDate) ? null : Common.ConvertToSystemDate(model.EntryToDate, "dd/MM/yyyy");

            //model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            //model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
            DataSet ds = model.GetDrExpenselist1();
            ViewBag.TotalCleredPaid = 0;
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
            {
                var i = 0;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (i < 25)
                    {
                        Expenses obj = new Expenses();
                        obj.CompanyName = r["CompanyName"].ToString();
                        obj.ExpenseHeadName = r["ExpenseHeadName"].ToString();
                        obj.Pk_ExpenseDetailsId = r["Pk_ExpenseDetailsId"].ToString();
                        obj.ExpenseName = r["ExpenseName"].ToString();
                        obj.ExpenseID = r["ExpenseType"].ToString();
                        obj.Remarks = r["Remarks"].ToString();
                        obj.ChequeStatus = r["TransactionStatus"].ToString();
                        obj.TransactionTy = r["TransactionType"].ToString();
                        obj.Transanction = r["TransactionStatus"].ToString();
                        obj.ChequeNo = r["ChequeNo"].ToString();
                        obj.ChequeStatusUpdateDate = r["ChequeStatusUpdateDate"].ToString();
                        //obj.CrAmount = double.Parse(r["CrAmount"].ToString()).ToString("n2");
                        obj.CrAmount = r["CrAmount"].ToString();
                        //  obj.DrAmount = double.Parse(r["DrAmount"].ToString()).ToString("n2");
                        obj.DrAmount = (r["DrAmount"].ToString());//.ToString("n2");
                        obj.Date = r["CheckDate"].ToString();
                        obj.ExpenseCategoryName = r["ExpenseCategoryName"].ToString();

                        obj.PaymentMode = r["PaymentMode1"].ToString();
                        Clearedlst.Add(obj);
                        ViewBag.TotalCleredPaid = Convert.ToDecimal(ViewBag.TotalCleredPaid) + Convert.ToDecimal(r["DrAmount"].ToString());
                    }
                    i = i + 1;
                }
                model.ClearedListItem = Clearedlst;
                //ViewBag.TotalCleredPaid = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
            }
            ViewBag.TotalPendingPaid = 0;
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[1].Rows.Count > 0)
            {
                var i = 0;
                foreach (DataRow pr in ds.Tables[1].Rows)
                {
                    if (i < 25)
                    {
                        Expenses obj = new Expenses();
                        obj.CompanyName = pr["CompanyName"].ToString();
                        obj.ExpenseHeadName = pr["ExpenseHeadName"].ToString();
                        obj.Pk_ExpenseDetailsId = pr["Pk_ExpenseDetailsId"].ToString();
                        obj.ExpenseName = pr["ExpenseName"].ToString();
                        obj.ExpenseID = pr["ExpenseType"].ToString();
                        obj.Remarks = pr["Remarks"].ToString();
                        obj.ChequeStatus = pr["TransactionStatus"].ToString();
                        obj.TransactionTy = pr["TransactionType"].ToString();
                        obj.Transanction = pr["TransactionStatus"].ToString();
                        obj.ChequeNo = pr["ChequeNo"].ToString();
                        obj.ChequeStatusUpdateDate = pr["ChequeStatusUpdateDate"].ToString();
                        //obj.CrAmount = double.Parse(pr["CrAmount"].ToString()).ToString("n2");
                        obj.CrAmount = pr["CrAmount"].ToString();
                        //obj.DrAmount = double.Parse(pr["DrAmount"].ToString()).ToString("n2");
                        obj.DrAmount = pr["DrAmount"].ToString();
                        obj.Date = pr["CheckDate"].ToString();
                        obj.ExpenseCategoryName = pr["ExpenseCategoryName"].ToString();
                        obj.PaymentMode1 = pr["PaymentMode"].ToString();
                        Pendinglst.Add(obj);
                        ViewBag.TotalPendingPaid = Convert.ToDecimal(ViewBag.TotalPendingPaid) + Convert.ToDecimal(pr["DrAmount"].ToString());
                    }
                    i = i + 1;
                }
                model.PendingListItem = Pendinglst;
                //ViewBag.TotalPendingPaid = double.Parse(ds.Tables[1].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
            }
            ViewBag.TotalBouncePaid = 0;
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[2].Rows.Count > 0)
            {
                var i = 0;
                foreach (DataRow r in ds.Tables[2].Rows)
                {
                    if (i < 25)
                    {
                        Expenses obj = new Expenses();
                        obj.CompanyName = r["CompanyName"].ToString();
                        obj.ExpenseHeadName = r["ExpenseHeadName"].ToString();
                        obj.Pk_ExpenseDetailsId = r["Pk_ExpenseDetailsId"].ToString();
                        obj.ExpenseName = r["ExpenseName"].ToString();
                        obj.ExpenseID = r["ExpenseType"].ToString();
                        obj.Remarks = r["Remarks"].ToString();
                        obj.ChequeStatus = r["TransactionStatus"].ToString();
                        obj.TransactionTy = r["TransactionType"].ToString();
                        obj.Transanction = r["TransactionStatus"].ToString();
                        obj.ChequeNo = r["ChequeNo"].ToString();
                        obj.ChequeStatusUpdateDate = r["ChequeStatusUpdateDate"].ToString();
                        obj.CrAmount = double.Parse(r["CrAmount"].ToString()).ToString("n2");
                        obj.DrAmount = double.Parse(r["DrAmount"].ToString()).ToString("n2");
                        obj.ExpenseCategoryName = r["ExpenseCategoryName"].ToString();
                        obj.Date = r["CheckDate"].ToString();
                        obj.PaymentMode2 = r["PaymentMode"].ToString();
                        Bouncelst.Add(obj);
                        ViewBag.TotalBouncePaid = Convert.ToDecimal(ViewBag.TotalBouncePaid) + Convert.ToDecimal(r["DrAmount"].ToString());
                    }
                    i = i + 1;
                }
                model.BounceListItem = Bouncelst;
                ViewBag.TotalBouncePaid = double.Parse(ds.Tables[2].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
            }

            return View(model);
        }
        [HttpPost]
        [ActionName("ViewDrExpense")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult ViewDrExpens(Expenses model)
        {
            List<SelectListItem> CheckStatus = Common.CheckStatus();
            ViewBag.CheckStatus = CheckStatus;
            #region ddlExpensetype
            int ecount = 0;
            List<SelectListItem> ddlexpensetype = new List<SelectListItem>();
            DataSet dsexpensetype = model.GetExpenseTypeList();
            if (dsexpensetype != null && dsexpensetype.Tables.Count > 0 && dsexpensetype.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in dsexpensetype.Tables[0].Rows)
                {
                    if (ecount == 0)
                    {
                        ddlexpensetype.Add(new SelectListItem { Text = "Expense Type", Value = "0" });
                    }
                    ddlexpensetype.Add(new SelectListItem { Text = r["ExpenseTypeName"].ToString(), Value = r["Pk_ExpenseTypeId"].ToString() });
                    ecount = ecount + 1;
                }
            }
            ViewBag.ExpenseType = ddlexpensetype;
            #endregion
            #region ddlExpenseCategory
            int Category = 0;
            List<SelectListItem> ddlexpensecategory = new List<SelectListItem>();
            DataSet dscat = model.GetExpenseCategory();
            if (dscat != null && dscat.Tables.Count > 0 && dscat.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in dscat.Tables[0].Rows)
                {
                    if (Category == 0)
                    {
                        ddlexpensecategory.Add(new SelectListItem { Text = "Expense Category", Value = "0" });
                    }
                    ddlexpensecategory.Add(new SelectListItem { Text = r["ExpenseCategory"].ToString(), Value = r["Pk_ExpenseCategoryId"].ToString() });
                    Category = Category + 1;
                }
            }
            ViewBag.ExpenseCategory = ddlexpensecategory;
            #endregion
            #region transactiontype
            int count = 0;
            List<SelectListItem> ddlTransactionType = new List<SelectListItem>();
            DataSet ddlTransaction = model.GetTransactionList();
            if (ddlTransaction != null && ddlTransaction.Tables.Count > 0 && ddlTransaction.Tables[0].Rows.Count > 0)
            {
                //ddlTransactionType.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
                foreach (DataRow r in ddlTransaction.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlTransactionType.Add(new SelectListItem { Text = "Select TransactionType", Value = "0" });
                    }
                    ddlTransactionType.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });
                    count = count + 1;

                }
            }
            ViewBag.ddlTransactionType = ddlTransactionType;
            #endregion
            List<Expenses> Clearedlst = new List<Expenses>();
            List<Expenses> Pendinglst = new List<Expenses>();
            List<Expenses> Bouncelst = new List<Expenses>();
            model.ChequeStatus = "Dr";
            model.ExpenseID = model.ExpenseID == "0" ? null : model.ExpenseID;
            model.Fk_CompanyId = model.Fk_CompanyId == "0" ? null : model.Fk_CompanyId;
            model.ExpenseName = model.ExpenseName == "0" ? null : model.ExpenseName;
            model.FK_ExpenseHead = model.FK_ExpenseHead == "0" ? null : model.FK_ExpenseHead;
            model.Fk_ExpenseCategoryId = model.Fk_ExpenseCategoryId == "0" ? null : model.Fk_ExpenseCategoryId;
            model.TransactionID = model.TransactionID == "0" ? null : model.TransactionID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.EntryFromDate = string.IsNullOrEmpty(model.EntryFromDate) ? null : Common.ConvertToSystemDate(model.EntryFromDate, "dd/MM/yyyy");
            model.EntryToDate = string.IsNullOrEmpty(model.EntryToDate) ? null : Common.ConvertToSystemDate(model.EntryToDate, "dd/MM/yyyy");
            model.AddedBy = Session["Pk_AdminId"].ToString();
            model.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
            DataSet ds = model.GetDrExpenselist();
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.CompanyName = r["CompanyName"].ToString();
                    obj.ExpenseHeadName = r["ExpenseHeadName"].ToString();
                    obj.Pk_ExpenseDetailsId = r["Pk_ExpenseDetailsId"].ToString();
                    obj.ExpenseName = r["ExpenseName"].ToString();
                    obj.ExpenseID = r["ExpenseType"].ToString();
                    obj.Remarks = r["Remarks"].ToString();
                    obj.ChequeStatus = r["TransactionStatus"].ToString();
                    obj.TransactionTy = r["TransactionType"].ToString();
                    obj.Transanction = r["TransactionStatus"].ToString();
                    obj.ChequeNo = r["ChequeNo"].ToString();
                    obj.ChequeStatusUpdateDate = r["ChequeStatusUpdateDate"].ToString();
                    obj.CrAmount = double.Parse(r["CrAmount"].ToString()).ToString("n2");
                    obj.DrAmount = double.Parse(r["DrAmount"].ToString()).ToString("n2");
                    obj.ExpenseCategoryName = r["ExpenseCategoryName"].ToString();
                    obj.Date = r["CheckDate"].ToString();
                    obj.PaymentMode = r["PaymentMode1"].ToString();
                    Clearedlst.Add(obj);
                }
                model.ClearedListItem = Clearedlst;
                ViewBag.TotalCleredPaid = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
            }
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow pr in ds.Tables[1].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.CompanyName = pr["CompanyName"].ToString();
                    obj.ExpenseHeadName = pr["ExpenseHeadName"].ToString();
                    obj.Pk_ExpenseDetailsId = pr["Pk_ExpenseDetailsId"].ToString();
                    obj.ExpenseName = pr["ExpenseName"].ToString();
                    obj.ExpenseID = pr["ExpenseType"].ToString();
                    obj.Remarks = pr["Remarks"].ToString();
                    obj.ChequeStatus = pr["TransactionStatus"].ToString();
                    obj.TransactionTy = pr["TransactionType"].ToString();
                    obj.Transanction = pr["TransactionStatus"].ToString();
                    obj.ChequeNo = pr["ChequeNo"].ToString();
                    obj.ChequeStatusUpdateDate = pr["ChequeStatusUpdateDate"].ToString();
                    obj.CrAmount = double.Parse(pr["CrAmount"].ToString()).ToString("n2");
                    obj.DrAmount = double.Parse(pr["DrAmount"].ToString()).ToString("n2");
                    obj.ExpenseCategoryName = pr["ExpenseCategoryName"].ToString();
                    obj.Date = pr["CheckDate"].ToString();
                    obj.PaymentMode1 = pr["PaymentMode"].ToString();
                    Pendinglst.Add(obj);
                }
                model.PendingListItem = Pendinglst;
                ViewBag.TotalPendingPaid = double.Parse(ds.Tables[1].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
            }
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[2].Rows)
                {
                    Expenses obj = new Expenses();
                    obj.CompanyName = r["CompanyName"].ToString();
                    obj.ExpenseHeadName = r["ExpenseHeadName"].ToString();
                    obj.Pk_ExpenseDetailsId = r["Pk_ExpenseDetailsId"].ToString();
                    obj.ExpenseName = r["ExpenseName"].ToString();
                    obj.ExpenseID = r["ExpenseType"].ToString();
                    obj.Remarks = r["Remarks"].ToString();
                    obj.ChequeStatus = r["TransactionStatus"].ToString();
                    obj.TransactionTy = r["TransactionType"].ToString();
                    obj.Transanction = r["TransactionStatus"].ToString();
                    obj.ChequeNo = r["ChequeNo"].ToString();
                    obj.ChequeStatusUpdateDate = r["ChequeStatusUpdateDate"].ToString();
                    obj.CrAmount = double.Parse(r["CrAmount"].ToString()).ToString("n2");
                    obj.DrAmount = double.Parse(r["DrAmount"].ToString()).ToString("n2");
                    obj.ExpenseCategoryName = r["ExpenseCategoryName"].ToString();
                    obj.Date = r["CheckDate"].ToString();
                    obj.PaymentMode2 = r["PaymentMode"].ToString();
                    Bouncelst.Add(obj);
                }
                model.BounceListItem = Bouncelst;
                ViewBag.TotalBouncePaid = double.Parse(ds.Tables[2].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
            }
            //#region ddlcompany
            //int ccount = 0;
            //Master master = new Master();
            //model.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
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
            #region GetExpenseName
            List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            model.Result = "yes";
            DataSet ds11 = model.GetExpenseNameList();
            int ExpenseCount = 0;
            if (ds11 != null && ds11.Tables.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    if (ExpenseCount == 0)
                    {
                        ddlexpensename.Add(new SelectListItem { Text = "Select Expense Name", Value = "0" });
                    }
                    ddlexpensename.Add(new SelectListItem { Text = r["ExpenseName"].ToString(), Value = r["FK_ExpenseId"].ToString() });
                    ExpenseCount = ExpenseCount + 1;
                }
            }
            ViewBag.ddlexpensename = ddlexpensename;
            #endregion
            //List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            //ddlexpensename.Add(new SelectListItem { Text = "Select Expense Name", Value = "0" });
            //ViewBag.ddlexpensename = ddlexpensename;

            #region ddlExpenseHead
            int ccount12 = 0;
            Master master = new Master();
            List<SelectListItem> ddlExpenseHead = new List<SelectListItem>();
            DataSet ds1 = master.GetExpenseHeadList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (ccount12 == 0)
                    {
                        ddlExpenseHead.Add(new SelectListItem { Text = "Select Expense Head", Value = "0" });
                    }
                    ddlExpenseHead.Add(new SelectListItem { Text = r["ExpenseHeadName"].ToString(), Value = r["PK_ExpenseHead"].ToString() });
                    ccount12 = ccount12 + 1;
                }
            }
            ViewBag.ddlExpenseHead = ddlExpenseHead;
            #endregion
            return View(model);
        }
        public ActionResult DeleteExpenseDetailsDr(string ExpenseDetailsId, string status)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Expenses model = new Expenses();
                model.Status = status;
                model.Pk_ExpenseDetailsId = ExpenseDetailsId;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteExpenseDetailsDr();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {

                        TempData["CrExpenseList"] = "Expense Details Deleted!";
                    }
                    else
                    {
                        TempData["CrExpenseList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["CrExpenseList"] = ex.Message;
            }
            FormName = "ViewCrExpense";
            Controller = "Expense";

            return RedirectToAction(FormName, Controller);
        }

        #endregion


        public ActionResult CrExpense(Expenses model)
        {
            //#region ddlcompany
            //int ccount = 0;
            //Master master = new Master();
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

            #region ddlExpenseHead
            int ccount12 = 0;
            Master master = new Master();
            List<SelectListItem> ddlExpenseHead = new List<SelectListItem>();
            DataSet ds1 = master.GetExpenseHeadList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (ccount12 == 0)
                    {
                        ddlExpenseHead.Add(new SelectListItem { Text = "Select Expense Head", Value = "0" });
                    }
                    ddlExpenseHead.Add(new SelectListItem { Text = r["ExpenseHeadName"].ToString(), Value = r["PK_ExpenseHead"].ToString() });
                    ccount12 = ccount12 + 1;
                }
            }
            ViewBag.ddlExpenseHead = ddlExpenseHead;
            #endregion


            #region ddlPaymentMode
            int count3 = 0;
            Plot obj = new Plot();
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            #region ddlExpensetype
            int ecount = 0;
            List<SelectListItem> ddlexpensetype = new List<SelectListItem>();
            DataSet dsexpensetype = model.GetExpenseTypeList();
            if (dsexpensetype != null && dsexpensetype.Tables.Count > 0 && dsexpensetype.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in dsexpensetype.Tables[0].Rows)
                {
                    if (ecount == 0)
                    {
                        ddlexpensetype.Add(new SelectListItem { Text = "Expense Type", Value = "0" });
                    }
                    ddlexpensetype.Add(new SelectListItem { Text = r["ExpenseTypeName"].ToString(), Value = r["Pk_ExpenseTypeId"].ToString() });
                    ecount = ecount + 1;
                }
            }
            ViewBag.ExpenseType = ddlexpensetype;
            #endregion
            #region AccountHead
            int countAccHead = 0;
            List<SelectListItem> ddlAccountHead = new List<SelectListItem>();
            model.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
            DataSet ds = model.GetAccountHeadDetailsNew();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (countAccHead == 0)
                    {
                        ddlAccountHead.Add(new SelectListItem { Text = "Select Account Head", Value = "0" });
                    }
                    ddlAccountHead.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_AccountHeadId"].ToString() });
                    countAccHead = countAccHead + 1;
                }
                //ddlAccountHead.Add(new SelectListItem { Text = "N/A", Value = "0" });
            }
            ViewBag.ddlAccountHead = ddlAccountHead;
            #endregion
            #region expensename
            List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            ddlexpensename.Add(new SelectListItem { Text = "Select Expense Name", Value = "0" });
            ViewBag.ddlexpensename = ddlexpensename;
            #endregion
            #region expensetype
            int count = 0;
            List<SelectListItem> ddlTransactionType = new List<SelectListItem>();
            DataSet ddlTransaction = model.GetTransactionList();
            if (ddlTransaction != null && ddlTransaction.Tables.Count > 0 && ddlTransaction.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in ddlTransaction.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlTransactionType.Add(new SelectListItem { Text = "Select TransactionType", Value = "0" });
                        //ddlTransactionType.Add(new SelectListItem { Text = "Cash", Value = "1" });
                    }

                    ddlTransactionType.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });
                    count = count + 1;

                }
            }
            ViewBag.ddlTransactionType = ddlTransactionType;
            #endregion
            #region ddlExpensecategory
            int countcat = 0;
            //Master master = new Master();
            List<SelectListItem> ddlExpenseCategoy = new List<SelectListItem>();
            DataSet dscategory = model.getexpensecategorylist();
            if (dscategory != null && dscategory.Tables.Count > 0 && dscategory.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dscategory.Tables[0].Rows)
                {
                    if (countcat == 0)
                    {
                        ddlExpenseCategoy.Add(new SelectListItem { Text = "Select Expense Category", Value = "0" });
                    }
                    ddlExpenseCategoy.Add(new SelectListItem { Text = r["ExpenseCategoryName"].ToString(), Value = r["Fk_ExpenseCategoryId"].ToString() });
                    countcat = countcat + 1;
                }
            }
            ViewBag.ddlExpenseCategory = ddlExpenseCategoy;
            #endregion
            return View();
        }


        public ActionResult GetExpenseDetails(string ExpenseID)
        {
            try
            {
                Expenses model = new Expenses();
                model.ExpenseID = ExpenseID;
                #region GetExpenseName
                List<SelectListItem> ddlexpensename = new List<SelectListItem>();
                model.Result = "yes";
                DataSet ds = model.GetExpenseNameList();
                int Category = 0;
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        //if (Category == 0)
                        //{
                        //    ddlexpensename.Add(new SelectListItem { Text = "Expense Name", Value = "0" });
                        //}
                        ddlexpensename.Add(new SelectListItem { Text = r["ExpenseName"].ToString(), Value = r["FK_ExpenseId"].ToString() });
                        Category = Category + 1;
                    }
                }
                model.ddlexpensename = ddlexpensename;
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetExpenseDetailss(string ExpenseIDD)
        {
            try
            {
                Expenses model = new Expenses();
                model.ExpenseIDD = ExpenseIDD;
                //model.Pk_ExpenseDetailsId = expenseTypeId;
                #region GetExpenseName
                List<SelectListItem> ddlexpensename = new List<SelectListItem>();
                model.Result = "yes";
                DataSet ds = model.GetExpenseNameLists();
                int Category = 0;
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        //if (Category == 0)
                        //{
                        //    ddlexpensename.Add(new SelectListItem { Text = "Expense Name", Value = "0" });
                        //}
                        ddlexpensename.Add(new SelectListItem { Text = r["ExpenseName"].ToString(), Value = r["FK_ExpenseId"].ToString() });
                        Category = Category + 1;
                    }
                }
                model.ddlexpensename = ddlexpensename;
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetExpenseDetailss1(string ExpenseIDD2)
        {
            try
            {
                Expenses model = new Expenses();
                model.ExpenseIDD2 = ExpenseIDD2;
                #region GetExpenseName
                List<SelectListItem> ddlexpensename = new List<SelectListItem>();
                model.Result = "yes";
                int Category = 0;
                DataSet ds = model.GetExpenseNameLists2();

                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        //if(Category==0)
                        //{
                        //    ddlexpensename.Add(new SelectListItem { Text = "Expense Name", Value = "0" });
                        //}
                        ddlexpensename.Add(new SelectListItem { Text = r["ExpenseName"].ToString(), Value = r["FK_ExpenseId"].ToString() });
                        Category = Category + 1;
                    }
                }
                model.ddlexpensename = ddlexpensename;
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public JsonResult save(Expenses order, string dataValue)
        {
            bool status = false;
            //string Fk_CompanyId = "";
            string FK_ExpenseHead = "";
            string ExpenseType = "";
            string Fk_ExpenseCategoryId = "";
            string ExpenseName = "";
            string PaymentMode = "";
            string TransactionID = "";
            string Check = "";
            string Amount = "";
            string FK_AccountHeadId = "";
            string Date = "";
            string Remarks = "";
            int rowsno = 0;
            var isValidModel = TryUpdateModel(order);
            var jss = new JavaScriptSerializer();
            var jdv = jss.Deserialize<dynamic>(dataValue);
            //var serializeData = JsonConvert.DeserializeObject<List<Customer>>(empdata);
            //System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
            DataTable CrExpenseDetails = new DataTable();
            order.ChequeStatus = "Cr";
            //CrExpenseDetails.Columns.Add("Fk_CompanyId");
            CrExpenseDetails.Columns.Add("FK_ExpenseHead");
            CrExpenseDetails.Columns.Add("ExpenseType");
            CrExpenseDetails.Columns.Add("Fk_ExpenseCategoryId");
            CrExpenseDetails.Columns.Add("ExpenseName");
            CrExpenseDetails.Columns.Add("TransactionID");
            CrExpenseDetails.Columns.Add("PaymentMode");
            CrExpenseDetails.Columns.Add("Check");
            CrExpenseDetails.Columns.Add("Amount");
            CrExpenseDetails.Columns.Add("Date");
            CrExpenseDetails.Columns.Add("Remarks");
            CrExpenseDetails.Columns.Add("FK_AccountHeadId");
            CrExpenseDetails.Columns.Add("rowsno");
            DataTable dt = new DataTable();
            dt = JsonConvert.DeserializeObject<DataTable>(jdv["CRExpenses"]);
            int numberOfRecords = dt.Rows.Count;
            //foreach (DataRow row in dt.Rows)
            foreach (DataRow row in dt.Rows)
            {
                //Fk_CompanyId = row["Fk_CompanyId"].ToString();
                FK_ExpenseHead = row["FK_ExpenseHead"].ToString();
                ExpenseType = row["ExpenseType"].ToString();
                Fk_ExpenseCategoryId = row["Fk_ExpenseCategoryId"].ToString();
                ExpenseName = row["ExpenseName"].ToString();
                TransactionID = row["TransactionID"].ToString();
                PaymentMode = row["PaymentMode"].ToString();
                Check = row["Check"].ToString();
                Amount = row["Amount"].ToString();
                Date = string.IsNullOrEmpty(row["Date"].ToString()) ? null : Common.ConvertToSystemDate(row["Date"].ToString(), "dd/MM/yyyy");
                Remarks = row["Remarks"].ToString();
                FK_AccountHeadId = row["FK_AccountHeadId"].ToString();
                rowsno = rowsno + 1;
                CrExpenseDetails.Rows.Add(FK_ExpenseHead, ExpenseType, Fk_ExpenseCategoryId, ExpenseName, TransactionID, PaymentMode, Check, Amount, Date, Remarks, FK_AccountHeadId, rowsno);
            }
            order.dtExpenseDetails = CrExpenseDetails;
            order.AddedBy = Session["Pk_AdminId"].ToString();
            // order.Date = string.IsNullOrEmpty(order.Date.ToString()) ? null : Common.ConvertToSystemDate(row["Date"].ToString(), "dd/MM/yyyy");
            //order.Date = string.IsNullOrEmpty(order.Date) ? null : Common.ConvertToSystemDate(order.Date, "dd/MM/yyyy");
            //jdv["Amount"]= string.IsNullOrEmpty(jdv["Amount"]) ? null : Common.ConvertToSystemDate(jdv["Amount"], "dd/MM/yyyy");
            DataSet ds = new DataSet();
            ds = order.SaveCrExpenseDetails();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    TempData["DrExpenses"] = " Expenses Save  successfully";
                    status = true;
                }
                else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["DrExpenses"] = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            else
            {
                TempData["DrExpenses"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
            }
            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult DrExpense(Expenses model)
        {
            #region ddlPaymentMode
            int count3 = 0;
            Plot obj = new Plot();
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            //#region ddlcompany
            //int ccount = 0;
            //Master master = new Master();
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

            #region ddlExpenseHead
            int ccount12 = 0;
            Master master = new Master();
            List<SelectListItem> ddlExpenseHead = new List<SelectListItem>();
            DataSet ds1 = master.GetExpenseHeadList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (ccount12 == 0)
                    {
                        ddlExpenseHead.Add(new SelectListItem { Text = "Select Expense Head", Value = "0" });
                    }
                    ddlExpenseHead.Add(new SelectListItem { Text = r["ExpenseHeadName"].ToString(), Value = r["PK_ExpenseHead"].ToString() });
                    ccount12 = ccount12 + 1;
                }
            }
            ViewBag.ddlExpenseHead = ddlExpenseHead;
            #endregion
            #region AccountHead
            int countAccHead = 0;
            List<SelectListItem> ddlAccountHead = new List<SelectListItem>();
            model.Fk_EmployeeId = Session["Pk_AdminId"].ToString();
            DataSet ds = model.GetAccountHeadDetailsNew();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (countAccHead == 0)
                    {
                        ddlAccountHead.Add(new SelectListItem { Text = "Select Account Head", Value = "0" });
                    }
                    ddlAccountHead.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_AccountHeadId"].ToString() });
                    countAccHead = countAccHead + 1;
                }
                //ddlAccountHead.Add(new SelectListItem { Text = "N/A", Value = "0" });
            }
            ViewBag.ddlAccountHead = ddlAccountHead;
            #endregion
            #region ddlExpensetype
            int ecount = 0;
            List<SelectListItem> ddlexpensetype = new List<SelectListItem>();
            DataSet dsexpensetype = model.GetExpenseTypeList();
            if (dsexpensetype != null && dsexpensetype.Tables.Count > 0 && dsexpensetype.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in dsexpensetype.Tables[0].Rows)
                {
                    if (ecount == 0)
                    {
                        ddlexpensetype.Add(new SelectListItem { Text = "Expense Type", Value = "0" });
                    }
                    ddlexpensetype.Add(new SelectListItem { Text = r["ExpenseTypeName"].ToString(), Value = r["Pk_ExpenseTypeId"].ToString() });
                    ecount = ecount + 1;
                }
            }
            ViewBag.ExpenseType = ddlexpensetype;
            #endregion
            #region transactiontype
            List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            ddlexpensename.Add(new SelectListItem { Text = "Select Expense Name", Value = "0" });
            ViewBag.ddlexpensename = ddlexpensename;
            int count = 0;
            List<SelectListItem> ddlTransactionType = new List<SelectListItem>();
            DataSet ddlTransaction = model.GetTransactionList();
            if (ddlTransaction != null && ddlTransaction.Tables.Count > 0 && ddlTransaction.Tables[0].Rows.Count > 0)
            {
                //ddlTransactionType.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
                foreach (DataRow r in ddlTransaction.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlTransactionType.Add(new SelectListItem { Text = "Select TransactionType", Value = "0" });
                    }
                    ddlTransactionType.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });
                    count = count + 1;

                }
            }
            ViewBag.ddlTransactionType = ddlTransactionType;
            #endregion
            #region ddlExpenseCategory
            int Category = 0;
            List<SelectListItem> ddlexpensecategory = new List<SelectListItem>();
            DataSet dscat = model.GetExpenseCategory();
            if (dscat != null && dscat.Tables.Count > 0 && dscat.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in dscat.Tables[0].Rows)
                {
                    if (Category == 0)
                    {
                        ddlexpensecategory.Add(new SelectListItem { Text = "Expense Category", Value = "0" });
                    }
                    ddlexpensecategory.Add(new SelectListItem { Text = r["ExpenseCategory"].ToString(), Value = r["Pk_ExpenseCategoryId"].ToString() });
                    Category = Category + 1;
                }
            }
            ViewBag.ExpenseCategory = ddlexpensecategory;
            #endregion
            return View();
        }

        [HttpPost]
        public JsonResult SaveDrExpense(Expenses order, string dataValue)
        {

            try
            {
                string Fk_CompanyId = "";
                string FK_ExpenseHead = "";
                string ExpenseType = "";
                string Fk_ExpenseCategoryId = "";
                string ExpenseName = "";
                string PaymentMode = "";
                //string FromTransactionID = "";
                string TransactionID = "";
                string ToTransactionID = "";
                string Check = "";
                string Amount = "";
                string FK_AccountHeadId = "";
                string CreditAccountHead = "";
                string Date = "";
                string Remarks = "";
                int rowsno = 0;
                var isValidModel = TryUpdateModel(order);
                var jss = new JavaScriptSerializer();
                var jdv = jss.Deserialize<dynamic>(dataValue);

                DataTable DrExpenseDetails = new DataTable();

                DrExpenseDetails.Columns.Add("Fk_CompanyId");
                DrExpenseDetails.Columns.Add("FK_ExpenseHead");
                DrExpenseDetails.Columns.Add("ExpenseType");
                DrExpenseDetails.Columns.Add("Fk_ExpenseCategoryId");
                DrExpenseDetails.Columns.Add("ExpenseName");
                //DrExpenseDetails.Columns.Add("FromTransactionID");
                DrExpenseDetails.Columns.Add("TransactionID");
                DrExpenseDetails.Columns.Add("ToTransactionID");
                DrExpenseDetails.Columns.Add("PaymentMode");
                DrExpenseDetails.Columns.Add("Check");
                DrExpenseDetails.Columns.Add("Amount");
                DrExpenseDetails.Columns.Add("Date");
                DrExpenseDetails.Columns.Add("Remarks");
                DrExpenseDetails.Columns.Add("FK_AccountHeadId");
                DrExpenseDetails.Columns.Add("CreditAccountHead");
                DrExpenseDetails.Columns.Add("rowsno");
                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(jdv["DrExpenses"]);

                int numberOfRecords = dt.Rows.Count;
                //foreach (DataRow row in dt.Rows)

                foreach (DataRow row in dt.Rows)
                {
                    Fk_CompanyId = row["Fk_CompanyId"].ToString();
                    FK_ExpenseHead = row["FK_ExpenseHead"].ToString();
                    ExpenseType = row["ExpenseType"].ToString();
                    Fk_ExpenseCategoryId = row["Fk_ExpenseCategoryId"].ToString();
                    ExpenseName = row["ExpenseName"].ToString();
                    //FromTransactionID = row["FromTransactionID"].ToString();
                    TransactionID = row["TransactionID"].ToString();

                    if (row["ToTransactionID"].ToString() == null || row["ToTransactionID"].ToString() == "")
                    {
                        ToTransactionID = "0";
                    }
                    else
                    {
                        ToTransactionID = row["ToTransactionID"].ToString();
                    }

                    //ToTransactionID = row["ToTransactionID"].ToString();
                    PaymentMode = row["PaymentMode"].ToString();
                    Check = row["Check"].ToString();
                    Amount = row["Amount"].ToString();
                    Date = string.IsNullOrEmpty(row["Date"].ToString()) ? null : Common.ConvertToSystemDate(row["Date"].ToString(), "dd/MM/yyyy");
                    Remarks = row["Remarks"].ToString();
                    FK_AccountHeadId = row["FK_AccountHeadId"].ToString();
                    CreditAccountHead = row["CreditAccountHead"].ToString();
                    rowsno = rowsno + 1;
                    DrExpenseDetails.Rows.Add(Fk_CompanyId, FK_ExpenseHead, ExpenseType, Fk_ExpenseCategoryId, ExpenseName, TransactionID, ToTransactionID, PaymentMode, Check, Amount, Date, Remarks, FK_AccountHeadId, CreditAccountHead, rowsno);
                }
                order.dtExpenseDetails = DrExpenseDetails;
                order.ToTransactionID = ToTransactionID;
                order.AddedBy = Session["Pk_AdminId"].ToString();
                order.ChequeStatus = "Dr";
                DataSet ds = new DataSet();
                ds = order.SaveDrExpenseDetails();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {

                        order.Result = "Yes";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        order.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["DrExpenses"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return new JsonResult { Data = new { status = order.Result } };
        }


    }
}