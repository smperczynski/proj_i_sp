using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Security.Cryptography;
using System.Text;

namespace Sprzedaj24
{
    public partial class Login : Page
    {
        string id = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["go"];

            if (id == "registration")
            {
                divRegister.Visible = true;
                btnLogin.Text = "Zarejestruj";
                hlReg.Visible = false;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (id == "registration")
            {
                NewUserRegistration();
            }
            else
            {
                LoginUser();
            }
        }

        protected void NewUserRegistration()
        {
            GenerateHash HashAndSalt = new GenerateHash();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_Sprzedaj24"].ConnectionString))
            {
                conn.Open();

                string query = "INSERT INTO Users (Login, SSH, Salt, Email, TypeId) VALUES (@UserName, @PasswordHashed, @Salt, @Email, 2)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = tbxLogin.Text;
                    cmd.Parameters.Add("@PasswordHashed", SqlDbType.VarChar).Value = HashAndSalt.GenarateHash(tbxPassword.Text, HashAndSalt.CreateSalt(10));
                    cmd.Parameters.Add("@Salt", SqlDbType.VarChar).Value = HashAndSalt.CreateSalt(10);
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = tbxEmail.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            Page.Response.Redirect("Default.aspx", true);
        }

        public void LoginUser()
        {
            bool isLoggedIn = false;
            string userName = tbxLogin.Text;
            string userPassword = tbxPassword.Text;

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_Sprzedaj24"].ToString());
            conn.Open();

            string query = @"SELECT UserId, TypeId, Login, SSH, Salt FROM Users WHERE Login = @UserName";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserName", SqlDbType.VarChar).Value = userName;
            cmd.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                lblError.Text = "Nieprawidłowa nazwa użytkownika lub hasło";
                lblError.Visible = true;
                return;
            }

            try
            {
                string hashedPassword = dt.Rows[0]["SSH"].ToString();
                string salt = dt.Rows[0]["Salt"].ToString();
                int userId = Int32.Parse(dt.Rows[0]["UserId"].ToString());
                int typeId = Int32.Parse(dt.Rows[0]["TypeId"].ToString());
                byte[] passwordAndSalt = Encoding.UTF8.GetBytes(userPassword + salt);
                byte[] hashPass = new SHA256Managed().ComputeHash(passwordAndSalt);
                string hashCode = Convert.ToBase64String(hashPass);
                if (hashCode == hashedPassword)
                {
                    Session["Login"] = userName.ToString();
                    Session["UserId"] = userId;
                    Session["TypeId"] = typeId;
                    isLoggedIn = true;
                }
                else
                {
                    lblError.Text = "Nieprawidłowa nazwa użytkownika lub hasło";
                    lblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Wystąpił błąd";
                lblError.Visible = true;
            }
            finally
            {
                conn.Close();
            }

            if (isLoggedIn)
            {
                Page.Response.Redirect("Default.aspx", true);
            }
        }

        protected class GenerateHash
        {
            public string CreateSalt(int SaltSize)
            {
                var rng = new RNGCryptoServiceProvider();
                byte[] Salt = new byte[SaltSize];
                rng.GetBytes(Salt);
                return Convert.ToBase64String(Salt);
            }
            public string GenarateHash(string UserPassword, string salt)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(UserPassword + salt);
                byte[] passwordHash = new SHA256Managed().ComputeHash(bytes);
                return Convert.ToBase64String(passwordHash);
            }
        }
    }
}