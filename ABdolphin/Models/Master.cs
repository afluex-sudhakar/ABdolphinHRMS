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

        public DataSet GetCompanyList()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_CompanyID", Fk_CompanyId),
                                      new SqlParameter("@Fk_EmployeeId", Fk_EmployeeId)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetCompanyList", para);
            return ds;
        }
    }
}