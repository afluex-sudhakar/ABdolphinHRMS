using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABdolphin.Models
{
    public class Plot : Common
    {
        public List<SelectListItem> ddlSector { get; set; }
        public string SiteID { get; set; }


        public DataSet GetExpenseTypeList()
        {
            DataSet ds = Connection.ExecuteQuery("GetExpenseType");
            return ds;

        }

        public DataSet GetBranchList()
        {
            DataSet ds = Connection.ExecuteQuery("GetBranchList");
            return ds;
        }
        public DataSet GetSiteList()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_EmployeeId",Fk_EmployeeId)
            };
            DataSet ds = Connection.ExecuteQuery("SiteList", para);
            return ds;
        }

        public DataSet GetSectorList()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID) };
            DataSet ds = Connection.ExecuteQuery("GetSectorList", para);
            return ds;
        }
    }
}