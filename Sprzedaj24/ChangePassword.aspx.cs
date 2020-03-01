using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Security.Cryptography;
using System.Text;

namespace Sprzedaj24
{
    public partial class EditProfile : Page
    {
        public static string db = ConfigurationManager.ConnectionStrings["db_Sprzedaj24"].ConnectionString;
        string userLogin = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            userLogin = Session["Login"] != null ? Session["Login"].ToString() : "";

            if (string.IsNullOrEmpty(userLogin))
            {
                Page.Response.Redirect("Default.aspx", true);
            }
        }

        protected void btnPassword_Click(object sender, EventArgs e)
        {
            if (CheckPasswords())
            {
                SetNewPassword();
            };
        }

        protected bool CheckPasswords()
        {
            bool response = true;

            if (tbxNew1.Text != tbxNew2.Text)
            {
                lblError.Text = "Pola z nowym hasłem nie są takie same";
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Visible = true;
                response = false;
                return false;
            }

            string userPassword = tbxLast.Text;

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_Sprzedaj24"].ToString());
            conn.Open();

            string query = @"SELECT SSH, Salt FROm Users WHERE Login = @UserLogin";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserLogin", SqlDbType.VarChar).Value = userLogin;
            cmd.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            try
            {
                string hashedPassword = dt.Rows[0]["SSH"].ToString();
                string salt = dt.Rows[0]["Salt"].ToString();
                byte[] passwordAndSalt = Encoding.UTF8.GetBytes(userPassword + salt);
                byte[] hashPass = new SHA256Managed().ComputeHash(passwordAndSalt);
                string hashCode = Convert.ToBase64String(hashPass);

                if (hashCode != hashedPassword)
                {
                    lblError.Text = "Nieprawidłowe hasło";
                    lblError.ForeColor = System.Drawing.Color.Red;
                    lblError.Visible = true;
                    response = false;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Wystąpił błąd";
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Visible = true;
                response = false;
            }
            finally
            {
                conn.Close();
            }
            return response;
        }


        protected void SetNewPassword()
        {
            try
            {

                GenerateHash HashAndSalt = new GenerateHash();
                string GetSalt = HashAndSalt.CreateSalt(10);
                string hashString = HashAndSalt.GenarateHash(tbxNew1.Text, GetSalt);

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_Sprzedaj24"].ConnectionString))
                {
                    conn.Open();

                    string query = "UPDATE Users SET SSH = @passwordHashed, Salt = @salt WHERE Login = @userLogin";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userLogin", userLogin);
                        cmd.Parameters.AddWithValue("@passwordHashed", hashString);
                        cmd.Parameters.AddWithValue("@salt", GetSalt);
                        cmd.ExecuteNonQuery();
                    }
                }
                lblError.Text = "Hasło zostało zmienione";
                lblError.ForeColor = System.Drawing.Color.Green;
                lblError.Visible = true;
            }
            catch
            {
                lblError.Text = "Wystąpił błąd";
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Visible = true;
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