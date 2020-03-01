using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sprzedaj24
{
    public partial class Admin : Page
    {
        public static string db = ConfigurationManager.ConnectionStrings["db_Sprzedaj24"].ConnectionString;
        string id = "";
        string search = "";

        int userId;
        string userLogin;
        string action;

        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["go"];
            userId = Request.QueryString["id"] != null ? Int32.Parse(Request.QueryString["id"]) : 0;
            userLogin = Session["Login"] != null ? Session["Login"].ToString() : "";

            if (!string.IsNullOrEmpty(action) || !string.IsNullOrEmpty(userLogin) || Session["TypeId"].ToString() == "1")
            {
                switch (action)
                {
                    case "list":
                        ShowUsers();
                        break;

                    case "setAdm":
                        if (string.IsNullOrEmpty(userId.ToString()) || userId < 1)
                        {
                            Response.Redirect("ErrorPage.aspx");
                        }
                        else if (userId > 0)
                        {
                            SetAdminPermission(userId);
                        }
                        break;

                    case "del":

                        if (string.IsNullOrEmpty(userId.ToString()) || userId < 1)
                        {
                            Response.Redirect("ErrorPage.aspx");
                        }
                        else if (userId > 0)
                        {
                            DeleteUser(userId);
                        }
                        break;
                }
            }
            else
            {
                Response.Redirect("ErrorPage.aspx");
            }

        }

        protected void ShowUsers(int userId = 0, string menuId = "", bool isSearch = false)
        {
            SqlConnection conn = new SqlConnection(db);
            DataSet ds = new DataSet();

            try
            {
                string query = "SELECT UserId, Login, Email, TypeId FROM Users WHERE Login != @userLogin";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userLogin", userLogin);

                SqlDataAdapter ad = new SqlDataAdapter();
                ad.SelectCommand = cmd;
                ad.Fill(ds);

                gvUsers.DataSource = ds;
                gvUsers.DataBind();
            }
            catch (Exception ex)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }

        protected void DeleteUser(int userId)
        {
            SqlConnection conn = new SqlConnection(db);
            DataSet ds = new DataSet();

            try
            {
                string query = @"DELETE FROM Users WHERE UserId = @userId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
                Response.Redirect("Admin.aspx?go=list", true);
            }
        }

        protected void SetAdminPermission(int userId)
        {
            SqlConnection conn = new SqlConnection(db);
            DataSet ds = new DataSet();

            try
            {
                string query = @"DECLARE @typeId int;
                                 SET @typeId = (SELECT TypeId FROM Users WHERE UserId = @userId);
                                 
                                 IF (@typeId = 2)
                                 UPDATE Users SET TypeId = 1 WHERE UserId = @userId
                                 ELSE
                                 UPDATE Users SET TypeId = 2 WHERE UserId = @userId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
                Response.Redirect("Admin.aspx?go=list", true);
            }
        }

        protected void btnNoweOgl_Click(object sender, EventArgs e)
        {
            Response.Redirect($"~/Adv.aspx?go=new&id={Int32.Parse(id)}");
        }
    }
}