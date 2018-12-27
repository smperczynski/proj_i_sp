using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace Sprzedaj24
{
    public partial class Login : Page
    {
        String id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["go"];
            if (String.IsNullOrEmpty(id))
            {
                //Logowanie();
            }
            if (id == "registration")
            {
                divRegister.Visible = true;
                btnLogin.Text = "Zarejestruj";
                //Rejestracja();
            }
        }

        //protected void Logowanie()
        //{

        //}

        //protected void Rejestracja()
        //{

        //}
        protected void stary_Clickdzialajacy(Object sender, EventArgs e)
        {
            ////string hashresult = FormsAuthentication.HashPasswordForStoringInConfigFile(tbPassword.Text, "SHA");
            //tbLogin.Text=
            //ComputeSha256Hash(tbPassword.ToString());
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (id == "registration")
            {
                Rejestracja();
            }
            else
            {
                Logowanie();
            }
        }

        protected void StartSesja(string UserName)
        {
            Label lbl = Master.FindControl("lbLogin") as Label;
            lbl.Text += "Zalogowany: " + UserName;
            Session["Login"] = UserName.ToString();
            //Response.Redirect("Default.aspx");
        }

        //protected void PokazKom()
        //{
        //    lbWarning.ForeColor = System.Drawing.Color.Red;
        //    lbWarning.Visible = true;
        //    lbWarning.Text = "Zalogowany";
        //}

        protected void Rejestracja()
        {
            GenerateHash HashAndSalt = new GenerateHash();
            string GetSalt = HashAndSalt.CreateSalt(10);
            string hashString = HashAndSalt.GenarateHash(tbPassword.Text, GetSalt);
            string username = tbLogin.Text;
            string email = tbEmail.Text;
            string connectionString = ConfigurationManager.ConnectionStrings["baza"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Insert into [Uzytkownicy] (Login, SSH, Salt, Email, RangaId) values(" + "@UserName, @PasswordHashed, @Salt, @Email, 2)", conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.Parameters.AddWithValue("@PasswordHashed", hashString);
                    cmd.Parameters.AddWithValue("@Salt", GetSalt);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.ExecuteNonQuery();
                }
            }
            Server.Transfer("login.aspx");
        }

        protected void Logowanie()
        {
            //InsertDatabase LogIn = new InsertDatabase();
            //LogIn.LogInAccount(tbLogin.Text, tbPassword.Text);
            LogInAccount(tbLogin.Text, tbPassword.Text);
            //PokazKom();
            var userS = Session["Login"];
            //Response.Redirect("login.aspx");
        }

        //class InsertDatabase
        //{
        public void LogInAccount(string UserName, string UserPassword)
        {
            string MyConnection = ConfigurationManager.ConnectionStrings["baza"].ToString();
            SqlConnection connection = new SqlConnection(MyConnection);
            connection.Open();
            string compare = @"Select Login, SSH, Salt FROM [Uzytkownicy] WHERE Login=@UserName";
            SqlCommand CompareUser = new SqlCommand(compare, connection);
            CompareUser.Parameters.AddWithValue("@UserName", UserName);
            SqlDataAdapter adapter = new SqlDataAdapter(CompareUser);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            try
            {
                string HashedPassword = dt.Rows[0]["SSH"].ToString();
                string Salt = dt.Rows[0]["Salt"].ToString();
                byte[] passwordAndSalt = System.Text.Encoding.UTF8.GetBytes(UserPassword + Salt);
                byte[] hashPass = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSalt);
                string hashCode = Convert.ToBase64String(hashPass);
                if (hashCode == HashedPassword)
                {
                    StartSesja(UserName);
                    //FormsAuthentication.RedirectFromLoginPage(UserName, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("invalid user name and password");
            }
            connection.Close();
        }
        //}

        class GenerateHash
        {
            public string CreateSalt(int SaltSize)
            {
                var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
                byte[] Salt = new byte[SaltSize];
                rng.GetBytes(Salt);
                return Convert.ToBase64String(Salt);
            }
            public string GenarateHash(string UserPassword, string salt)
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(UserPassword + salt);
                byte[] PasswordHash = new System.Security.Cryptography.SHA256Managed().ComputeHash(bytes);
                return Convert.ToBase64String(PasswordHash);
            }
        }

        //static string ComputeSha256Hash(string rawData)
        //{
        //    // Create a SHA256   
        //    using (SHA256 sha256Hash = SHA256.Create())
        //    {
        //        // ComputeHash - returns byte array  
        //        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

        //        // Convert byte array to a string   
        //        StringBuilder builder = new StringBuilder();
        //        for (int i = 0; i < bytes.Length; i++)
        //        {
        //            builder.Append(bytes[i].ToString("x2"));
        //        }
        //        return builder.ToString();
        //    }
        //}
    }
}