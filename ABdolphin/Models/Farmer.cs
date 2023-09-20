using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ABdolphin.Models
{
    public class Farmer : Common
    {
        public List<Farmer> FarmerList { get; set; }
        public List<Farmer> FarmerPlotList { get; set; }
        public List<Farmer> PaymetListFarm { get; set; }
        public string Name { get; set; }
        public string PK_Farmerid { get; set; }
        public string Mobile { get; set; }
        public string Amount { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        public string SQFT { get; set; }
        public string Acre { get; set; }
        public string Hectare { get; set; }
        public string Photo { get; set; }
        public string IDProof { get; set; }
        public string DelearName { get; set; }
        public string Title { get; set; }
        public string AssociateID { get; set; }
        public string AssociateLoginID { get; set; }
        public string Address { get; set; }
        public string GataKhasaraN { get; set; }
        public string Village { get; set; }
        public string Status { get; set; }
        public string RegistryDate { get; set; }
        public string Fk_CompanyId { get; set; }
        public string FStatus { get; set; }
        public string IsActive { get; set; }
        public string JoiningDate { get; set; }
        public string EncryptKey { get; set; }
        public string CompanyName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CustomerId { get; set; }
        public string RegistryId { get; set; }

        public string CustomerName { get; set; }
        public string PlotNumber { get; set; }
        public string FarmerName { get; set; }
        public string TotalBalance { get; set; }
        public string PlotSize { get; set; }
        public string RemainingArea { get; set; }
        public string CheqStatus { get; set; }
        public string Reciept { get; set; }
        public string ID { get; set; }

        public string PaidAmount { get; set; }
        public string PayType { get; set; }
        public string CashDate { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public string Remarks { get; set; }



        public DataSet GetlistById()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_FId",PK_Farmerid)
            };
            DataSet ds = Connection.ExecuteQuery("GetFarmerListById", para);
            return ds;
        }

        public DataSet SaveFarmerDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Title",Title),
                                      new SqlParameter("@Name", Name),
                                      new SqlParameter("@Amount", Amount),
                                      new SqlParameter("@Mobile", Mobile),
                                      new SqlParameter("@Email", Email),
                                      new SqlParameter("@DOB", DOB),
                                      new SqlParameter("@Hectare", Hectare),
                                      new SqlParameter("@SQFT", SQFT),
                                      new SqlParameter("@Acre", Acre),
                                      new SqlParameter("@Pincode", Pincode),
                                      new SqlParameter("@State", State),
                                      new SqlParameter("@City", City),
                                      new SqlParameter("@Photo", Photo),
                                      new SqlParameter("@IDProof", IDProof),
                                      new SqlParameter("@AddedBy", AddedBy),
                                      new SqlParameter("@Address",Address),
                                      new SqlParameter("@AssociatId",AssociateID),
                                      new SqlParameter("@AssociatName", DelearName),
                                      new SqlParameter("@GataGhasaraN",GataKhasaraN),
                                      new SqlParameter("@Village",Village),
                                      new SqlParameter("@Status",Status),
                                      new SqlParameter("@RegistryDate",RegistryDate),
                                      new SqlParameter("@Fk_CompanyId",Fk_CompanyId)

                                  };
            DataSet ds = Connection.ExecuteQuery("SaveFarmerDetails", para);
            return ds;
        }

        public DataSet UpdateFarmerDetails()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Name", Name),
                                      new SqlParameter("@Amount", Amount),
                                      new SqlParameter("@Mobile", Mobile),
                                      new SqlParameter("@Email", Email),
                                      new SqlParameter("@DOB", DOB),
                                      new SqlParameter("@Hectare", Hectare),
                                      new SqlParameter("@SQFT", SQFT),
                                      new SqlParameter("@Acre", Acre),
                                      new SqlParameter("@Pincode", Pincode),
                                      new SqlParameter("@State", State),
                                      new SqlParameter("@City", City),
                                      new SqlParameter("@Photo", Photo),
                                      new SqlParameter("@IDProof", IDProof),
                                      new SqlParameter("@DelearName", DelearName),
                                      new SqlParameter("@UpdatedBy", AddedBy),
                                      new SqlParameter("@Pk_FarmerId",PK_Farmerid),
                                      new SqlParameter("@Address",Address),
                                      new SqlParameter("@AssociatId",AssociateID),
                                      new SqlParameter("@Title",Title),
                                      new SqlParameter("@GataGhasaraN",GataKhasaraN),
                                      new SqlParameter("@Village",Village),
                                      new SqlParameter("@Status",Status),
                                      new SqlParameter("@RegistryDate",RegistryDate),
                                      new SqlParameter("@Fk_CompanyId",Fk_CompanyId)
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateFarmerListById", para);
            return ds;
        }

        public DataSet DeleteFarmers()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_FarmerId",PK_Farmerid),
                 new SqlParameter("@AddedBy", AddedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteFormer", para);
            return ds;
        }

        public DataSet Getlist()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_CompanyId",Fk_CompanyId),
                new SqlParameter("@PK_FarmerId",PK_Farmerid),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate),
                new SqlParameter("@Fk_EmployeeId",Fk_EmployeeId)
            };
            DataSet ds = Connection.ExecuteQuery("GetFarmerList", para);
            return ds;
        }

        public DataSet GetFarmerList()
        {
            SqlParameter[] para = {

                new SqlParameter("@PK_FarmerId", PK_Farmerid),
                new SqlParameter("@Fk_EmployeeId",AddedBy)
            };
            DataSet ds = Connection.ExecuteQuery("GetFarmerListforAutoSearch", para);
            return ds;
        }

        public DataSet GetPlotRegistrylist()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FK_Farmerid",PK_Farmerid),
                new SqlParameter("@Fk_CustomerId",CustomerId),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate),
                 new SqlParameter("@RegistryId",RegistryId),
                 new SqlParameter("@Fk_EmployeeId",Fk_EmployeeId)
            };
            DataSet ds = Connection.ExecuteQuery("GetPlotRegistryList", para);
            return ds;
        }

        public DataSet GetFarmerpaymentList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Name",Name),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate),
                new SqlParameter("@CheckStatus",CheqStatus),
                new SqlParameter("@Fk_EmployeeId",Fk_EmployeeId)
            };
            DataSet ds = Connection.ExecuteQuery("GetFarmerpaymentList", para);
            return ds;
        }

    }
}