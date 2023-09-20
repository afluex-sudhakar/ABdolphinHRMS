using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ABdolphin.Models
{
    public class AssociateBooking : Common
    {
        public string FarmerId { get; set; }
        public string CustomerLoginID { get; set; }

        public DataSet GetFarmerList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_FarmerId", FarmerId) };
            DataSet ds = Connection.ExecuteQuery("GetFarmerListforAutoSearch", para);
            return ds;
        }

        public DataSet GetcustomerList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", CustomerLoginID) };
            DataSet ds = Connection.ExecuteQuery("GetCustomerlist", para);
            return ds;
        }
    }
}