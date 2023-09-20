using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ABdolphin.Models
{
    public class Reports
    {
        public string LoginId { get; set; }
        public string UserName { get; set; }
        public string LoginIDD { get; set; }


        public DataSet GettingUserProfile()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId)};
            DataSet ds = Connection.ExecuteQuery("GetUserProfile", para);
            return ds;
        }
    }
}