using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ABdolphin.Models
{
    public class Master : Common
    {
        public string Fk_CompanyId { get; set; }
        public string Fk_ExpenseTypeId { get; set; }
        public string ExpenseTypeName { get; set; }
        public string EncryptKey { get; set; }
        public List<Master> lstExpenseType { get; set; }

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
    }
}