using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABdolphin.Models
{
    public class Master : Common
    {
        public string Fk_CompanyId { get; set; }
        public string Fk_ExpenseTypeId { get; set; }
        public string ExpenseTypeName { get; set; }
        public string EncryptKey { get; set; }
        public List<Master> lstExpenseType { get; set; }
        public List<Master> lstExpenseCategory { get; set; }
        public string ExpenseCategory { get; set; }
        public string Pk_ExpenseCategoryId { get; set; }
        public string UserType { get; set; }

        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public string BlockID { get; set; }
        public List<SelectListItem> lstBlock { get; set; }





        public DataSet GetCompanyList()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_CompanyID", Fk_CompanyId),
                                      new SqlParameter("@Fk_EmployeeId", Fk_EmployeeId)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetCompanyList", para);
            return ds;
        }

        public DataSet GetExpenseHeadList()
        {
            DataSet ds = Connection.ExecuteQuery("GetExpenseHeadDetails");
            return ds;
        }

        #region ExpenseTypeMaster
        public DataSet GetExpenseTypeList()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@Fk_ExpenseTypeId",Fk_ExpenseTypeId)

                                  };
            DataSet ds = Connection.ExecuteQuery("getExpenseTypeListForExpense", para);
            return ds;
        }
        public DataSet SaveExpenseType()
        {
            SqlParameter[] para = {
                                       new SqlParameter("@AddedBy", AddedBy),
                                       new SqlParameter("@ExpenseTypeName",ExpenseTypeName),
                                  };
            DataSet ds = Connection.ExecuteQuery("SaveExpenseType", para);
            return ds;
        }
        public DataSet UpdateExpenseType()
        {
            SqlParameter[] para = {
                                       new SqlParameter("@UpdatedBy", AddedBy),
                                       new SqlParameter("@Fk_ExpenseTypeId",Fk_ExpenseTypeId),
                                       new SqlParameter("@ExpenseTypeName",ExpenseTypeName)
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateExpenseType", para);
            return ds;
        }
        public DataSet DeleteExpenseType()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_ExpenseTypeId", Fk_ExpenseTypeId),
                                      new SqlParameter("@DeletedBy ", AddedBy )

                                  };
            DataSet ds = Connection.ExecuteQuery("DeleteExpenseType", para);
            return ds;
        }
        #endregion
        public DataSet Teamlist()
        {
            DataSet ds = Connection.ExecuteQuery("Teamlist");
            return ds;
        }



        #region ExpenseCategoryMaster
        public DataSet SaveExpenseCategory()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@ExpenseCategory",ExpenseCategory),
                                  new SqlParameter("@AddedBy",AddedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("SaveExpenseCategory", para);
            return ds;
        }
        public DataSet UpdateExpenseCategory()
        {
            SqlParameter[] para =
                            {
                  new SqlParameter("@Pk_ExpenseCategoryId",Pk_ExpenseCategoryId),
                                new SqlParameter("@ExpenseCategory",ExpenseCategory),
                                  new SqlParameter("@AddedBy",AddedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("UpdateExpenseCategory", para);
            return ds;
        }
        public DataSet ExpenseCategoryList()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@Pk_ExpenseCategoryId",Pk_ExpenseCategoryId)
                            };
            DataSet ds = Connection.ExecuteQuery("Expensecategorylist", para);
            return ds;
        }
        public DataSet Deleteexpensecategory()
        {
            SqlParameter[] para =
                            {
                  new SqlParameter("@Fk_ExpenseCategoryId",Pk_ExpenseCategoryId),
                                  new SqlParameter("@DeletedBy",AddedBy)
                            };
            DataSet ds = Connection.ExecuteQuery("Deleteexpensecategory", para);
            return ds;
        }
        #endregion


        public DataSet GetMenuPermissionList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_AdminId", Fk_UserId),
                                    new SqlParameter("@UserType", UserType),
                                    new SqlParameter("@URL",Url)
            };
            DataSet ds = Connection.ExecuteQuery("GetMenuListForUser", para);
            return ds;
        }

        public DataSet GetBlockList()
        {
            SqlParameter[] para ={ new SqlParameter("@SiteID",SiteID),
                                     new SqlParameter("@SectorID",SectorID),
                                     new SqlParameter("@BlockID",BlockID),
                                 };
            DataSet ds = Connection.ExecuteQuery("GetBlockList", para);
            return ds;
        }
    }
}