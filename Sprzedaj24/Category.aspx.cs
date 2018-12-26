using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sprzedaj24
{
    public partial class Category : Page
    {
        public static string baza = ConfigurationManager.ConnectionStrings["baza"].ConnectionString;
        String id;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["id"];

            if (String.IsNullOrEmpty(id) || Int32.Parse(id) < 1)
            {
                Response.Redirect("ErrorPage.aspx");
            }
            else if (Int32.Parse(id) > 0)
            {
                PokazSciezke();
                // PokazListeKat();
            }
        }

        protected void PokazSciezke()
        {
            SqlConnection conn = new SqlConnection(baza);
            DataSet ds = new DataSet();

            //lab1.Text = "<h4><font color=#ffff1a>test</h4></font>";
            try
            {
                string query = @"SELECT UPPER(m3.Nazwa)
                                 +' <span class=""glyphicon glyphicon-menu-right""></span> '
                                     + UPPER(m2.Nazwa) + ' <span class=""glyphicon glyphicon-menu-right""></span> '
                                     + UPPER(m1.Nazwa) AS Sciezka FROM
                                      menu m1
                                      JOIN menu m2 ON m1.ParentId = m2.MenuId
                                 JOIN menu m3 ON m2.ParentId = m3.MenuId
                                 WHERE m1.menuid = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", Int32.Parse(id));
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                // string lbl = "label" + i;

                //Control myControl1 = (Label)FindControl(lbl.ToString());

                //Label lblResult = ((Label)ResultsPanel.FindControl(lbl));

                if (reader.Read())
                {
                    lblSciezka.Text += reader["Sciezka"].ToString();
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

        protected void PokazListeKat()
        {
            //lblNaglowek.Text = "Róźne -> Kupię -> Obrazy";
            SqlConnection conn = new SqlConnection(baza);
            DataSet ds = new DataSet();

            //lab1.Text = "<h4><font color=#ffff1a>test</h4></font>";
            try
            {
                string query = @" ";

                SqlCommand cmd = new SqlCommand(query, conn);
                //cmd.Parameters.AddWithValue("@Id", i);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                // string lbl = "label" + i;

                //Control myControl1 = (Label)FindControl(lbl.ToString());

                //Label lblResult = ((Label)ResultsPanel.FindControl(lbl));

                while (reader.Read())
                {
                    //lblNaglowek.Text += reader["Kat"].ToString();
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
    }

}