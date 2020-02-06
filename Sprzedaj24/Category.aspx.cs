using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Sprzedaj24
{
    public partial class Category : Page
    {
        public static string db = ConfigurationManager.ConnectionStrings["db_Sprzedaj24"].ConnectionString;
        String id;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["Id"];

            if (String.IsNullOrEmpty(id) || Int32.Parse(id) < 1)
            {
                Response.Redirect("ErrorPage.aspx");
            }
            else if (Int32.Parse(id) > 0)
            {
                LoadBreadCrumbs();
                ShowAdvertisements();
            }
        }

        protected void LoadBreadCrumbs()
        {
            SqlConnection conn = new SqlConnection(db);
            DataSet ds = new DataSet();

            try
            {
                string query = @"SELECT UPPER(m3.Name)
                                 +' <span class=""glyphicon glyphicon-menu-right""></span> '
                                 + UPPER(m2.Name) + ' <span class=""glyphicon glyphicon-menu-right""></span> '
                                 + UPPER(m1.Name) AS Path,
	                             UPPER(m1.Name) AS Name
                                 FROM
                                 menu m1
                                 JOIN menu m2 ON m1.ParentId = m2.MenuId
                                 JOIN menu m3 ON m2.ParentId = m3.MenuId
                                 WHERE m1.menuid = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", Int32.Parse(id));
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblBreadCrumbs.Text += reader["Path"].ToString();
                    this.Title = reader["Name"].ToString();
                }
                reader.Close();
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

        protected void ShowAdvertisements()
        {
            SqlConnection conn = new SqlConnection(db);
            DataSet ds = new DataSet();

            try
            {
                string query = @"SELECT a.Title, a.Description, a.PhoneNumber, a.City, '/Upload/' + ap.PhotoPath AS PhotoPath
                                 FROM Advertisements a
                                 JOIN AdvertisementsPhotos ap ON a.AdvertisementId = ap.AdvertisementId
                                 WHERE a.CategoryId = @CategoryId AND ap.PhotoNumber = 1
                                 ORDER BY Created DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CategoryId", id);
                SqlDataAdapter ad = new SqlDataAdapter();
                ad.SelectCommand = cmd;
                ad.Fill(ds);

                rptAdv.DataSource = ds;
                rptAdv.DataBind();
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

        protected void btnNoweOgl_Click(object sender, EventArgs e)
        {
            Response.Redirect($"~/NewAdv.aspx?id={Int32.Parse(id)}");
        }
    }
}