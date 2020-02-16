using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.IO;

namespace Sprzedaj24
{
    public partial class Adv : Page
    {
        public static string db = ConfigurationManager.ConnectionStrings["db_Sprzedaj24"].ConnectionString;

        int categoryId;
        int advertisementId;
        string userLogin;
        string action;


        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["go"];
            categoryId = Request.QueryString["id"] != null ? Int32.Parse(Request.QueryString["id"]) : 0;
            advertisementId = Request.QueryString["a"] != null ? Int32.Parse(Request.QueryString["a"]) : 0;
            userLogin = Session["Login"] != null ? Session["Login"].ToString() : "";

            if (!string.IsNullOrEmpty(action))
            {
                switch (action)
                {
                    case "new":
                        if (!string.IsNullOrEmpty(userLogin))
                        {
                            if (string.IsNullOrEmpty(categoryId.ToString()) || categoryId < 1)
                            {
                                Response.Redirect("ErrorPage.aspx");
                            }
                            else if (categoryId > 0)
                            {
                                divEdit.Visible = true;
                                LoadBreadCrumbs();
                                btnSaveAdv.Visible = true;
                            }
                        }
                        else
                        {
                            Page.Response.Redirect("Login.aspx", true);
                        }
                        break;
                    case "show":
                        if (!string.IsNullOrEmpty(userLogin))
                        {
                            if (string.IsNullOrEmpty(advertisementId.ToString()) || advertisementId < 1)
                            {
                                Response.Redirect("ErrorPage.aspx");
                            }
                            else if (advertisementId > 0)
                            {
                                // if (CheckUserPermissions(userLogin, advertisementId) || Session["TypeId"].ToString() == "1")
                                //{
                                divShow.Visible = true;
                                LoadBreadCrumbs();
                                if (!Page.IsPostBack)
                                {
                                    ShowAdvertisement();
                                }
                                btnEditAdv.Visible = true;
                                lblUploadInfo.Visible = true;
                                //}
                            }
                        }
                        else
                        {
                            Page.Response.Redirect("Login.aspx", true);
                        }
                        break;
                    case "edit":
                        if (!string.IsNullOrEmpty(userLogin))
                        {
                            if (string.IsNullOrEmpty(advertisementId.ToString()) || advertisementId < 1)
                            {
                                Response.Redirect("ErrorPage.aspx");
                            }
                            else if (advertisementId > 0)
                            {
                                if (CheckUserPermissions(userLogin, advertisementId) || Session["TypeId"].ToString() == "1")
                                {
                                    LoadBreadCrumbs();
                                    if (!Page.IsPostBack)
                                    {
                                        LoadAdvertisement();
                                    }
                                    divEdit.Visible = true;
                                    btnEditAdv.Visible = true;
                                    lblUploadInfo.Visible = true;
                                }
                            }
                        }
                        else
                        {
                            Page.Response.Redirect("Login.aspx", true);
                        }
                        break;
                    case "del":

                        if (!string.IsNullOrEmpty(userLogin))
                        {
                            if (string.IsNullOrEmpty(categoryId.ToString()) || advertisementId < 1)
                            {
                                Response.Redirect("ErrorPage.aspx");
                            }
                            else if (advertisementId > 0)
                            {
                                if (CheckUserPermissions(userLogin, advertisementId) || Session["TypeId"].ToString() == "1")
                                {
                                    divEdit.Visible = true;
                                    LoadBreadCrumbs();
                                }
                            }
                        }
                        else
                        {
                            Page.Response.Redirect("Login.aspx", true);
                        }
                        break;
                }
            }
            else
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }

        protected bool CheckUserPermissions(string userLogin, int advertisementId)
        {
            bool perm = false;
            SqlConnection conn = new SqlConnection(db);
            DataSet ds = new DataSet();

            try
            {
                string query = @"SELECT u.UserId FROM dbo.Users u
                                 JOIN Advertisements a ON a.UserId = u.UserId
                                 WHERE u.UserId = @UserLogin AND AdvertisementId = @AdvertisementId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userLogin", userLogin);
                cmd.Parameters.AddWithValue("@AdvertisementId", advertisementId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    perm = true;
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
            return perm;
        }

        #region NewAdvertisement
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

        protected void ShowCategoryList()
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
            int AdvId = 0;

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
                            AdvId = Int32.Parse(reader["NewId"].ToString());
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

                if (FileUploadControl.HasFiles && AdvId != 0)
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
                                cmd.Parameters.Add("@AdvertisementId", SqlDbType.Int).Value = AdvId;
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
        #endregion NewAdvertisement

        #region EditAdvertisement
        protected void LoadAdvertisement()
        {
            SqlConnection conn = new SqlConnection(db);
            DataSet ds = new DataSet();

            try
            {
                string query = @"SELECT a.Title, a.Description, a.PhoneNumber, a.City 
                                 FROM dbo.Advertisements a 
                                 WHERE a.AdvertisementId = @AdvertisementId

                                 SELECT PhotoPath FROM AdvertisementsPhotos 
                                 WHERE AdvertisementId = @AdvertisementId 
                                 ORDER BY PhotoNumber";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@AdvertisementId", SqlDbType.Int).Value = advertisementId;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    tbxTitle.Text = reader["Title"].ToString();
                    tbxDescription.Text = reader["Description"].ToString();
                    tbxPhoneNumber.Text = reader["PhoneNumber"].ToString();
                    tbxCity.Text = reader["City"].ToString();

                    reader.NextResult();

                    while (reader.Read())
                    {
                        imgUpload0.ImageUrl = $"/Upload/{reader.GetValue(0).ToString()}";
                        imgUpload1.ImageUrl = $"/Upload/{reader.GetValue(1).ToString()}";
                        imgUpload2.ImageUrl = $"/Upload/{reader.GetValue(2).ToString()}";
                        imgUpload3.ImageUrl = $"/Upload/{reader.GetValue(3).ToString()}";
                    }
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

        protected void btnEditAdv_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_Sprzedaj24"].ConnectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"UPDATE Advertisements SET 
                                     Title = @Title, 
                                     Description = @Description, 
                                     PhoneNumber = @PhoneNumber, 
                                     City = @City,
                                     Modified = GETDATE()
                                     WHERE AdvertisementId = @AdvertisementId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@AdvertisementId", SqlDbType.VarChar).Value = advertisementId;
                        cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = tbxTitle.Text;
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = tbxDescription.Text;
                        cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = tbxPhoneNumber.Text;
                        cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = tbxCity.Text;

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

                if (FileUploadControl.HasFiles)
                {
                    int photoNr = 1;

                    try
                    {
                        conn.Open();

                        string query = @"DELETE FROM AdvertisementsPhotos WHERE AdvertisementId = @AdvertisementId";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.Add("@AdvertisementId", SqlDbType.Int).Value = advertisementId;
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
                                cmd.Parameters.Add("@AdvertisementId", SqlDbType.Int).Value = advertisementId;
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

                Response.Redirect($"Category.aspx?id={categoryId}");
            }
        }


        System.Collections.Generic.IEnumerable<IDataRecord> GetFromReader(IDataReader reader)
        {
            while (reader.Read()) yield return reader;
        }
        #endregion EditAdvertisement



        #region ShowAdvertisement
        protected void ShowAdvertisement()
        {
            SqlConnection conn = new SqlConnection(db);
            DataSet ds = new DataSet();

            try
            {
                string query = @"SELECT a.Title, a.Description, a.PhoneNumber, a.City 
                                 FROM dbo.Advertisements a 
                                 WHERE a.AdvertisementId = @AdvertisementId

                                 SELECT PhotoPath FROM AdvertisementsPhotos 
                                 WHERE AdvertisementId = @AdvertisementId 
                                 ORDER BY PhotoNumber";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@AdvertisementId", SqlDbType.Int).Value = advertisementId;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblTitle.Text = reader["Title"].ToString();
                    lblDescription.Text = reader["Description"].ToString();
                    lblPhoneNumber.Text = reader["PhoneNumber"].ToString();
                    lblCity.Text = reader["City"].ToString();

                    reader.NextResult();

                    while (reader.Read())
                    {
                        imgPrvUpload0.ImageUrl = $"/Upload/{reader.GetValue(0).ToString()}";
                        imgPrvUpload1.ImageUrl = $"/Upload/{reader.GetValue(1).ToString()}";
                        imgPrvUpload2.ImageUrl = $"/Upload/{reader.GetValue(2).ToString()}";
                        imgPrvUpload3.ImageUrl = $"/Upload/{reader.GetValue(3).ToString()}";
                    }
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
        #endregion ShowAdvertisement
    }
}