using System;
using System.Web.UI;

namespace Sprzedaj24
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userLogin = Session["Login"] != null ? Session["Login"].ToString() : "";
            SprawdzLogin(userLogin);
        }

        protected void SprawdzLogin(string userLogin)
        {
            if (!string.IsNullOrEmpty(userLogin))
            {
                if (Session["TypeId"].ToString() == "1")
                {
                    hlAdmin.Visible = true;
                }

                ddLogin.InnerHtml = $"Zalogowany jako: <b>{userLogin}</b> <span class='caret'/>";
                hlLogin.Visible = false;
                divAccount.Visible = true;
            }
            else
            {
                ddLogin.InnerHtml = "";
                hlLogin.Visible = true;
                divAccount.Visible = false;
            }
        }
    }
}