using ABdolphin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABdolphin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        #region login
        public ActionResult Login()
        {
            Session.Abandon();
            return View();
        }

        public ActionResult LoginAction(Home obj)
        {
            string FormName = "";
            string Controller = "";
            ProjectStatusResponse datalist = null;
            try
            {
                Home Modal = new Home();
                DataSet ds = obj.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "Login";
                        Controller = "Home";
                    }
                    else if ((ds.Tables[0].Rows[0]["UserType"].ToString() == "Trad Associate"))
                    {
                        if (obj.Password ==  Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()))
                        {
                            Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["Pk_userId"] = ds.Tables[0].Rows[0]["Pk_userId"].ToString();
                            Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                            Session["FullName"] = ds.Tables[0].Rows[0]["FullName"].ToString();
                            Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                            Session["Status"] = ds.Tables[0].Rows[0]["Status"].ToString();
                            Session["CssClass"] = ds.Tables[0].Rows[0]["StatusColor"].ToString();
                            FormName = "AssociateDashBoard";
                            Controller = "AssociateDashboard";
                        }
                        else
                        {
                            TempData["Login"] = "Incorrect Password";
                            FormName = "Login";
                            Controller = "Home";
                        }
                    }
                    else if ((ds.Tables[0].Rows[0]["UserType"].ToString() == "Customer"))
                    {
                        if (obj.Password == Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()))
                        {
                            Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["Pk_userId"] = ds.Tables[0].Rows[0]["Pk_userId"].ToString();
                            Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                            Session["FullName"] = ds.Tables[0].Rows[0]["FullName"].ToString();
                            Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                            Session["Status"] = ds.Tables[0].Rows[0]["Status"].ToString();
                            Session["CssClass"] = ds.Tables[0].Rows[0]["StatusColor"].ToString();
                            FormName = "CustomerDashBoard";
                            Controller = "CustomerDashboard";
                        }
                        else
                        {
                            TempData["Login"] = "Incorrect Password";
                            FormName = "Login";
                            Controller = "Home";
                        }
                    }
                    else if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Admin")
                    {
                        Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["Pk_AdminId"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                        Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                        Session["UserTypeName"] = ds.Tables[0].Rows[0]["UserTypeName"].ToString();
                        Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();


                        FormName = "AdminDashBoard";
                        Controller = "Admin";
                    }
                }
                else
                {
                    TempData["Login"] = "Incorrect Login ID Or Password";
                    FormName = "Login";
                    Controller = "Home";

                }
            }
            catch (Exception ex)
            {
                TempData["Login"] = ex.Message;
                FormName = "Login";
                Controller = "Home";
            }

            return RedirectToAction(FormName, Controller);
        }

        #endregion
    }
}