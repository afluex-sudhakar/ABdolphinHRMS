using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ABdolphin.Models
{
    public class Plot
    {

        public DataSet GetExpenseTypeList()
        {
            DataSet ds = Connection.ExecuteQuery("GetExpenseType");
            return ds;

        }
    }
}