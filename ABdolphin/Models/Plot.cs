using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ABdolphin.Models
{
    public class Plot
    {

        public string PaymentMode { get; set; }

        public DataSet GetExpenseTypeList()
        {
            DataSet ds = Connection.ExecuteQuery("GetExpenseType");
            return ds;

        }

        public DataSet GetPaymentModeList()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@PK_paymentID",PaymentMode)
                            };
            DataSet ds = Connection.ExecuteQuery("GetPaymentModeList", para);
            return ds;
        }
    }
}