using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.IO;

namespace Sprzedaj24
{
    public partial class NewAdv : Page
    {
        public static string db = ConfigurationManager.ConnectionStrings["db_Sprzedaj24"].ConnectionString;

        int categoryId;
        string userLogin;

        protected void Page_Load(object sender, EventArgs e)
        {
            categoryId = Request.QueryString["id"] != null ? Int32.Parse(Request.QueryString["id"]) : 0;
            userLogin = Session["Login"] != null ? Session["Login"].ToString() : "";

            if (!string.IsNullOrEmpty(userLogin))
            {
                if (string.IsNullOrEmpty(categoryId.ToString()) || categoryId < 1)
                {
                    Response.Redirect("ErrorPage.aspx");
                }
                else if (categoryId > 0)
                {
                    LoadBreadCrumbs();
                }
            }
            else
            {
                Page.Response.Redirect("Login.aspx", true);
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
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = categoryId;
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

        protected void PokazListeCategory()
        {
            SqlConnection conn = new SqlConnection(db);
            DataSet ds = new DataSet();

            try
            {
                string query = @"SELECT * FROM Advertisements WHERE CategoryegoriaId = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = categoryId;
                SqlDataAdapter ad = new SqlDataAdapter();
                ad.SelectCommand = cmd;
                ad.Fill(ds);
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

        protected void btnSaveAdv_Click(object sender, EventArgs e)
        {
            int newAdvId = 0;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_Sprzedaj24"].ConnectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"INSERT INTO Advertisements (CategoryId, Title, Description, PhoneNumber, City, UserId, Created, Modified) 
                                     VALUES (@CategoryId, @Title, @Description, @PhoneNumber, @City, @UserId, GETDATE(), GETDATE())
                                     SELECT SCOPE_IDENTITY() AS NewId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = categoryId;
                        cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = tbxTitle.Text;
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = tbxDescription.Text;
                        cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = tbxPhoneNumber.Text;
                        cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = tbxCity.Text;
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Session["UserId"];

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            newAdvId = Int32.Parse(reader["NewId"].ToString());
                        }
                    }   
                }
                catch (Exception ex)
                {
                    lblError.Text = "Wystąpił błąd zapisu danych";
                    return;
                }
                finally
                {
                    conn.Close();
                }

                if (FileUploadControl.HasFiles && newAdvId != 0)
                {
                    int photoNr = 1;

                    foreach (HttpPostedFile uploadedFile in FileUploadControl.PostedFiles)
                    {
                        
                        string newFileName = DateTime.Now.ToString("ddMMyyyyhhmmssffffff") + photoNr + ".jpg";

                        uploadedFile.SaveAs(Path.Combine(Server.MapPath("~/Upload"), newFileName));

                        try
                        {
                            conn.Open();

                            string query = @"INSERT INTO AdvertisementsPhotos (AdvertisementId, PhotoNumber, PhotoPath, Modified) 
                                             VALUES (@AdvertisementId, @PhotoNumber, @PhotoPath, GETDATE())";

                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.Add("@AdvertisementId", SqlDbType.Int).Value = newAdvId;
                                cmd.Parameters.Add("@PhotoNumber", SqlDbType.Int).Value = photoNr;
                                cmd.Parameters.Add("@PhotoPath", SqlDbType.VarChar).Value = newFileName;

                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex)
                        {
                            lblError.Text = "Wystąpił błąd zapisu danych";
                            return;
                        }
                        finally
                        {
                            conn.Close();
                        }

                        photoNr++;
                    }
                }
                Page.Response.Redirect($"Category.aspx?id={categoryId}", true);
            }
        }
    }
}