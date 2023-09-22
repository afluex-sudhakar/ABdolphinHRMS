using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABdolphin.Models
{
    public class Expenses : Common
    {
        public string AccountNumber { get; set; }
        public string AcountHolder { get; set; }
        public string CrAmount { get; set; }
        public string DrAmount { get; set; }
        public string BalanceAmount { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string IFSCCode { get; set; }
        public string IsActive { get; set; }
        public string Pk_BankId { get; set; }
        public string EncryptKey { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<Expenses> AccountList { get; set; }
        public string TransactionID { get; set; }
        public string ExpenseID { get; set; }
        public string ExpenseName { get; set; }
        public string Fk_ExpenseCategoryId { get; set; }
        public string Type { get; set; }
        public string Fk_CompanyId { get; set; }
        public string NFromDate { get; set; }
        public string NToDate { get; set; }
        public string EntryFromDate { get; set; }
        public string EntryToDate { get; set; }
        public string CompanyName { get; set; }
        public string ExpenseHead { get; set; }
        public string ChequeStatus { get; set; }
        public string TransactionTy { get; set; }
        public string Transanction { get; set; }
        public string Remarks { get; set; }
        public string Date { get; set; }
        public string ChequeStatusUpdateDate { get; set; }
        public string ExpenseCategoryName { get; set; }
        public List<Expenses> CrExpenseList { get; set; }
        public string FK_ExpenseHead { get; set; }
        public string Amount { get; set; }
        public string DeletedBy { get; set; }
        public string Pk_ExpenseId { get; set; }
        public string FK_ExpenseNameId { get; set; }
        public string ExpenseTypeId { get; set; }
        public string ExpenseType { get; set; }
        public List<Expenses> ExpenseList { get; set; }
        public string Fk_Expenseid { get; set; }
        public string Pk_ExpenseDetailsId { get; set; }
        public string ChequeNo { get; set; }
        public List<Expenses> ClearedListItem { get; set; }
        public List<SelectListItem> ddlexpensename { get; set; }

        public string Fk_Teamid { get; set; }
        public string TeamName { get; set; }
        public string ExpenseHeadName { get; set; }
        public string PlotInfo { get; set; }
        public string PaymentMode1 { get; set; }
        public List<Expenses> PendingListItem { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentMode2 { get; set; }
        public List<Expenses> BounceListItem { get; set; }
        public string FK_ExpenseDetailsId { get; set; }
        public string ExpenseIDD { get; set; }
        public string ExpenseName1 { get; set; }
        public string ExpenseIDD2 { get; set; }
        public string ExpenseName2 { get; set; }
        public string Status { get; set; }
        public DataTable dtExpenseDetails { get; set; }
        public string ToTransactionID { get; set; }
        public string Check { get; set; }
        public string FK_AccountHeadId { get; set; }
        public string CreditAccountHead { get; set; }



        public DataSet GetAccountlistById()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_BankId",Pk_BankId),
            };
            DataSet ds = Connection.ExecuteQuery("GetAccountlistById", para);
            return ds;
        }

        public DataSet SaveAccountDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@AccountNumber", AccountNumber),
                                      new SqlParameter("@AcountHolder", AcountHolder),
                                      new SqlParameter("@BankName", BankName),
                                      new SqlParameter("@BranchName", BranchName),
                                       new SqlParameter("@IFSCCode", IFSCCode),
                                      new SqlParameter("@AddedBy", AddedBy),
                                      new SqlParameter("@Amount",Amount)
                                  };
            DataSet ds = Connection.ExecuteQuery("SaveAccountDetails", para);
            return ds;
        }

        public DataSet UpdateAccountDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Pk_BankId", Pk_BankId),
                                      new SqlParameter("@AccountNumber", AccountNumber),
                                      new SqlParameter("@AcountHolder", AcountHolder),
                                      new SqlParameter("@BankName", BankName),
                                      new SqlParameter("@BranchName", BranchName),
                                       new SqlParameter("@IFSCCode", IFSCCode),
                                      new SqlParameter("@UpdatedBy", UpdatedBy),
                                       new SqlParameter("@Amount",Amount)
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateAccountDetails", para);
            return ds;
        }

        public DataSet DeleteAccount()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_BankId",Pk_BankId),
                new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteAccount", para);
            return ds;
        }

        public DataSet AccountStatus()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_BankId",Pk_BankId),
                new SqlParameter("@UpdatedBy",UpdatedBy),
                new SqlParameter("@IsActive",IsActive)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateAccountStatus", para);
            return ds;
        }

        public DataSet GetAccountlist()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AcountHolder",AcountHolder),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate),
                new SqlParameter("@Fk_EmployeeId",Fk_EmployeeId)
            };
            DataSet ds = Connection.ExecuteQuery("GetAccountlist", para);
            return ds;
        }

        public DataSet GetExpenseLedger1()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_ExpenseTypeId",ExpenseID),
                new SqlParameter("@Fk_ExpenseId",ExpenseName),
                new SqlParameter("@LedgerType",Type),
                new SqlParameter("@FK_BankId",TransactionID),
                  new SqlParameter("@FromDate",NFromDate),
                new SqlParameter("@ToDate",NToDate),
                new SqlParameter("@Fk_EmployeeId",Fk_EmployeeId)
            };
            DataSet ds = Connection.ExecuteQuery("ExpenseLedger", para);
            return ds;
        }
        
        public DataSet GetTransactionList()
        {

            DataSet ds = Connection.ExecuteQuery("GetTransferAccountList");
            return ds;
        }

        public DataSet GetExpenseCategory()
        {
            DataSet ds = Connection.ExecuteQuery("GetExpenseCategory");
            return ds;
        }

        public DataSet GetExpenseLedger()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_ExpenseTypeId",ExpenseID),
                new SqlParameter("@Fk_ExpenseId",ExpenseName),
                new SqlParameter("@LedgerType",Type),
                new SqlParameter("@FK_BankId",TransactionID),
                  new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate),
                new SqlParameter("@FK_ExpenseHead",FK_ExpenseHead),
                 new SqlParameter("@EntryFromDate",EntryFromDate),
               new SqlParameter("@EntryToDate",EntryToDate),
               new SqlParameter("@Fk_CompanyId",Fk_CompanyId),
               new SqlParameter("@Fk_ExpenseCategoryId",Fk_ExpenseCategoryId)
            };
            DataSet ds = Connection.ExecuteQuery("ExpenseLedger", para);
            return ds;
        }

        public DataSet GetExpenseNameList()
        {
            SqlParameter[] para = {
                new SqlParameter("@ExpenseID", ExpenseID)
            };
            DataSet ds = Connection.ExecuteQuery("GetExpenseNameList", para);
            return ds;
        }

        public DataSet GetExpenselistById()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_ExpenseId",Pk_ExpenseId),
            };
            DataSet ds = Connection.ExecuteQuery("GetExpenselistById", para);
            return ds;
        }

        public DataSet getExpenseTypeListForExpense()
        {

            DataSet ds = Connection.ExecuteQuery("getExpenseTypeListForExpense");
            return ds;
        }

        public DataSet SaveExpenseDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@ExpenseName", ExpenseName),
                                      new SqlParameter("@FK_ExpenseNameId", FK_ExpenseNameId),
                                      new SqlParameter("@AddedBy", AddedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("SaveExpenseDetails", para);
            return ds;
        }

        public DataSet UpdateExpenseDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Pk_ExpenseId", Pk_ExpenseId),
                                      new SqlParameter("@ExpenseName", ExpenseName),
                                       new SqlParameter("@FK_ExpenseNameId", FK_ExpenseNameId),
                                      new SqlParameter("@UpdatedBy", UpdatedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateExpenseDetails", para);
            return ds;
        }

        public DataSet GetExpenselist()
        {
            SqlParameter[] para =
            {
                  new SqlParameter("@FK_ExpenseTypeId",ExpenseID),
                new SqlParameter("@Pk_ExpenseId",ExpenseName),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate),
                new SqlParameter("@Fk_EmployeeId",Fk_EmployeeId)
            };
            DataSet ds = Connection.ExecuteQuery("GetExpenselist", para);
            return ds;
        }

        public DataSet GetExpenseTypeList()
        {
            DataSet ds = Connection.ExecuteQuery("GetExpenseType");
            return ds;
        }

        public DataSet DeleteExpense()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_ExpenseId",Pk_ExpenseId),
                new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteExpense", para);
            return ds;
        }

        public DataSet ExpenseStatus()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_ExpenseId",Pk_ExpenseId),
                new SqlParameter("@UpdatedBy",UpdatedBy),
                new SqlParameter("@IsActive",IsActive)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateExpenseStatus", para);
            return ds;
        }

        public DataSet GetledgerByName()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_ExpenseTypeId",ExpenseTypeId),
                new SqlParameter("@Fk_Expenseid",Fk_Expenseid)
            };
            DataSet ds = Connection.ExecuteQuery("GetExpenseLedgerByName", para);
            return ds;
        }

       
        public DataSet GetCrExpenselist1()
        {
            SqlParameter[] para =
            {
                
                //new SqlParameter("@FK_ExpenseHead",FK_ExpenseHead),
                new SqlParameter("@ExpenseType",ExpenseID),
                new SqlParameter("@FromDate",NFromDate),
                new SqlParameter("@LedgerType",ChequeStatus),
                new SqlParameter("@ToDate",NToDate),
                new SqlParameter("@Fk_CompanyId",Fk_CompanyId),
                new SqlParameter("@Fk_ExpenseNameId",ExpenseName),
               new SqlParameter("@FK_ExpenseHeadId",FK_ExpenseHead),
                new SqlParameter("@EntryFromDate",EntryFromDate),
               new SqlParameter("@EntryToDate",EntryToDate),
               new SqlParameter("@Fk_ExpenseCategoryId",Fk_ExpenseCategoryId),
                 new SqlParameter("@Fk_Transactiontype",TransactionID),
                 new SqlParameter("@Fk_TeamId",Fk_Teamid),
                 new SqlParameter("@Fk_EmployeeId",Fk_EmployeeId)
            };
            DataSet ds = Connection.ExecuteQuery("GetExpenselistByType", para);
            return ds;
        }
        public DataSet GetCrExpenselist()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@ExpenseType",ExpenseID),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@LedgerType",ChequeStatus),
                new SqlParameter("@ToDate",ToDate),
                new SqlParameter("@Fk_CompanyId",Fk_CompanyId),
                new SqlParameter("@Fk_ExpenseNameId",ExpenseName),
                new SqlParameter("@FK_ExpenseHeadId",FK_ExpenseHead),
                 new SqlParameter("@EntryFromDate",EntryFromDate),
               new SqlParameter("@EntryToDate",EntryToDate),
               new SqlParameter("@Fk_ExpenseCategoryId",Fk_ExpenseCategoryId),
                new SqlParameter("@Fk_Transactiontype",TransactionID),
                new SqlParameter("@Fk_TeamId",Fk_Teamid),
                new SqlParameter("@Fk_EmployeeId",Fk_EmployeeId)

            };
            DataSet ds = Connection.ExecuteQuery("GetExpenselistByType", para);
            return ds;
        }
        public DataSet UpdateCheckStatus()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@FK_ExpenseDetailsId",FK_ExpenseDetailsId),
                                new SqlParameter("@ChequeDate",ChequeStatusUpdateDate),
                                new SqlParameter("@ChequeStaus",ChequeStatus),
                                new SqlParameter("@UpdatedBy",AddedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("UpdateExpenseCheckStatus", para);
            return ds;
        }
        public DataSet UpdateCompany()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@FK_ExpenseDetailsId",FK_ExpenseDetailsId),
                                new SqlParameter("@Fk_CompanyId",Fk_CompanyId),
                                new SqlParameter("@UpdatedBy",AddedBy),
                                new SqlParameter("@FK_ExpenseHead",FK_ExpenseHead),
                                 new SqlParameter("@ExpenseTyIDD",ExpenseIDD),
                                  new SqlParameter("@FK_ExpenseID",ExpenseName1),
                                  new SqlParameter("@Fk_ExpenseCategoryId",Fk_ExpenseCategoryId),
                                  new SqlParameter("@Fk_TeamId",Fk_Teamid),
                                  new SqlParameter("@Remarks",Remarks)
                            };
            DataSet ds = Connection.ExecuteQuery("UpdateCompany", para);
            return ds;
        }
        public DataSet updateRemarks()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@Pk_ExpenseDetailsId ",Pk_ExpenseDetailsId),
                                        new SqlParameter("@UpdatedBy ",UpdatedBy),
                                        new SqlParameter("@ExpenseRemarks ", Remarks)

                            };
            DataSet ds = Connection.ExecuteQuery("UpdateExpenseRemarks", para);
            return ds;
        }
        public DataSet DeleteExpenseDetails()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@Pk_ExpenseDetailsId ",Pk_ExpenseDetailsId),
                                        new SqlParameter("@UpdatedBy ",UpdatedBy),
                                        new SqlParameter("@Status",Status)

                            };
            DataSet ds = Connection.ExecuteQuery("DeleteExpenseDetails", para);
            return ds;
        }
        public DataSet GetDrExpenselist1()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@ExpenseType",ExpenseID),
                new SqlParameter("@FromDate",NFromDate),
                  new SqlParameter("@LedgerType",ChequeStatus),
                new SqlParameter("@ToDate",NToDate),
                new SqlParameter("@Fk_EmployeeId",Fk_EmployeeId)
            };
            DataSet ds = Connection.ExecuteQuery("GetExpenselistByType", para);
            return ds;
        }
        public DataSet GetDrExpenselist()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@ExpenseType",ExpenseID),
                new SqlParameter("@FromDate",FromDate),
                  new SqlParameter("@LedgerType",ChequeStatus),
                new SqlParameter("@ToDate",ToDate),
                new SqlParameter("@Fk_CompanyId",Fk_CompanyId),
                new SqlParameter("@Fk_ExpenseNameId",ExpenseName),
                new SqlParameter("@FK_ExpenseHeadId",FK_ExpenseHead),
                new SqlParameter("@EntryFromDate",EntryFromDate),
               new SqlParameter("@EntryToDate",EntryToDate),
                new SqlParameter("@Fk_ExpenseCategoryId",Fk_ExpenseCategoryId),
                 new SqlParameter("@Fk_Transactiontype",TransactionID),
                  new SqlParameter("@Fk_EmployeeId",AddedBy)
            };
            DataSet ds = Connection.ExecuteQuery("GetExpenselistByType", para);
            return ds;
        }
        public DataSet DeleteExpenseDetailsDr()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@Pk_ExpenseDetailsId ",Pk_ExpenseDetailsId),
                                          new SqlParameter("@Status",Status),
                                        new SqlParameter("@UpdatedBy ",UpdatedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("DeleteExpenseDetailsDR", para);
            return ds;
        }

        public DataSet GetAccountHeadDetailsNew()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_EmployeeId",Fk_EmployeeId)
            };
            DataSet ds = Connection.ExecuteQuery("GetAccountHeadList", para);
            return ds;
        }

        public DataSet getexpensecategorylist()
        {
            SqlParameter[] para =
            {
                  new SqlParameter("@Pk_ExpenseCategoryId",Fk_ExpenseCategoryId)
            };
            DataSet ds = Connection.ExecuteQuery("Expensecategorylist", para);
            return ds;
        }

        public DataSet GetExpenseNameLists()
        {
            SqlParameter[] para = { new SqlParameter("@ExpenseID", ExpenseIDD) };
            DataSet ds = Connection.ExecuteQuery("GetExpenseNameList", para);
            return ds;
        }

        public DataSet GetExpenseNameLists2()
        {
            SqlParameter[] para = { new SqlParameter("@ExpenseID", ExpenseIDD2) };
            DataSet ds = Connection.ExecuteQuery("GetExpenseNameList", para);
            return ds;
        }

        public DataSet SaveCrExpenseDetails()
        {
            SqlParameter[] para = { new SqlParameter("@AddedBy",AddedBy) ,
                 new SqlParameter("@ChequeStatus",ChequeStatus) ,
                                  new SqlParameter("@CrdtExpenseDetails",dtExpenseDetails)
            };
            DataSet ds = Connection.ExecuteQuery("SaveExpenseDetailsCr", para);
            return ds;
        }

        public DataSet SaveDrExpenseDetails()
        {
            SqlParameter[] para = { new SqlParameter("@AddedBy",AddedBy) ,
                                    new SqlParameter("@ChequeStatus",ChequeStatus) ,
                                  new SqlParameter("@CrdtExpenseDetails",dtExpenseDetails)

            };
            DataSet ds = Connection.ExecuteQuery("SaveExpenseDetailsDr", para);
            return ds;
        }


    }
}