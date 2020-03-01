using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Security.Cryptography;
using System.Text;

namespace Sprzedaj24
{
    public partial class Logout : Page
    {
        string id = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Login"] = null;
            Session["UserId"] = null;
            Session["TypeId"] = null;
            Page.Response.Redirect("Default.aspx", true);
        }
    }
}