using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sprzedaj24
{
    public partial class Category : Page
    {
        public static string db = ConfigurationManager.ConnectionStrings["db_Sprzedaj24"].ConnectionString;
        string id = "";
        string search = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["id"];
            search = Request.QueryString["search"];

            if (!string.IsNullOrEmpty(id))
            {

                switch (id)
                {
                    case "own":
                        if (Session["UserId"] != null)
                        {

                            ddCategoriesSwitch.Visible = true;
                            ShowAdvertisements(Int32.Parse(Session["UserId"].ToString()), ddCategoriesSwitch.SelectedValue);
                        }
                        else
                        {
                            Response.Redirect("ErrorPage.aspx");
                        }
                        break;

                    default:
                        if (!string.IsNullOrEmpty(id) && Int32.Parse(id) > 0)
                        {
                            LoadBreadCrumbs();
                            ShowAdvertisements(0,id);
                            btnNoweOgl.Visible = true;
                        }
                        else
                        {
                            Response.Redirect("ErrorPage.aspx");
                        }
                        break;
                }
            }
            else if (!string.IsNullOrEmpty(search) && string.IsNullOrEmpty(id))
            {
                ShowAdvertisements(0,"",true);
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

        protected void ShowAdvertisements(int userId = 0, string menuId = "", bool isSearch = false)
        {
            SqlConnection conn = new SqlConnection(db);
            DataSet ds = new DataSet();
            string query = "";
            string querySearch = "";

            if (!string.IsNullOrEmpty(search))
            {
                querySearch = $" AND a.Title LIKE '%'+ @Search + '%' ";
            }

            try
            {
                if (userId != 0)
                {
                    query = @"DECLARE @Menu TABLE (CategoryId int)
                              INSERT INTO @Menu 
                              SELECT m2.MenuId AS CategoryId
                              FROM Menu m
                              JOIN Menu m2 ON m.MenuId = m2.ParentId
                              WHERE m.ParentId = @menuid

                              SELECT a.AdvertisementId, 1 AS Edit, a.CategoryId, a.Title, a.Description, a.PhoneNumber, a.City, '/Upload/' + ap.PhotoPath AS PhotoPath
                              FROM Advertisements a
                              JOIN @Menu m ON a.CategoryId = m.CategoryId
                              LEFT JOIN AdvertisementsPhotos ap ON a.AdvertisementId = ap.AdvertisementId
                              WHERE a.UserId = @UserId AND ap.PhotoNumber = 1
                              ORDER BY Created DESC";
                }
                else if (!string.IsNullOrEmpty(menuId))
                {
                    query = $@"DECLARE @Edit int = 0;
                               IF (@Admin = 1)
                               BEGIN
                               SET @Edit = 1;
                               END
                               
                               SELECT a.AdvertisementId, @Edit AS Edit, a.CategoryId, a.Title, a.Description, a.PhoneNumber, a.City, '/Upload/' + ap.PhotoPath AS PhotoPath
                               FROM Advertisements a
                               LEFT JOIN AdvertisementsPhotos ap ON a.AdvertisementId = ap.AdvertisementId
                               WHERE a.CategoryId = @CategoryId 
                               AND ap.PhotoNumber = 1
                               {querySearch}
                               ORDER BY Created DESC";
                }
                else if (isSearch && menuId == "")
                {
                    query = $@"DECLARE @Edit int = 0;
                              IF (@Admin = 1)
                              BEGIN
                              SET @Edit = 1;
                              END

                              SELECT a.AdvertisementId, @Edit AS Edit, a.CategoryId, a.Title, a.Description, a.PhoneNumber, a.City, '/Upload/' + ap.PhotoPath AS PhotoPath
                              FROM Advertisements a
                              LEFT JOIN AdvertisementsPhotos ap ON a.AdvertisementId = ap.AdvertisementId
                              WHERE ap.PhotoNumber = 1
                              AND a.Title LIKE '%'+ @Search + '%'
                              ORDER BY Created DESC";
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CategoryId", id ?? "");
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@MenuId", menuId ?? "");
                cmd.Parameters.AddWithValue("@Admin", Session["TypeId"] ?? 0);
                cmd.Parameters.AddWithValue("@Search", search ?? "");

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

        protected void ddCategoriesSwitch_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SqlConnection conn = new SqlConnection(db);
                DataSet ds = new DataSet();

                try
                {
                    string query = @"SELECT MenuId, Name FROM Menu WHERE ParentId IS NULL ORDER BY Name";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter ad = new SqlDataAdapter();
                    ad.SelectCommand = cmd;
                    ad.Fill(ds);

                    ddCategoriesSwitch.DataSource = ds;
                    ddCategoriesSwitch.DataBind();
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
        }

        protected void btnNoweOgl_Click(object sender, EventArgs e)
        {
            Response.Redirect($"~/Adv.aspx?go=new&id={Int32.Parse(id)}");
        }
    }
}