using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sprzedaj24
{
    public partial class _Default : Page
    {
        public static string db = ConfigurationManager.ConnectionStrings["db_Sprzedaj24"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowCategories();
        }

        protected void ShowCategories()
        {
            SqlConnection conn = new SqlConnection(db);
            DataSet ds = new DataSet();

            for (int i = 1; i < 13; i++)
            {
                try
                {
                    string query = @"create table #Temp
                                     (
                                         Nag varchar(500), 
                                         MenuId int, 
                                         ParentId int, 
                                         Category varchar(500)
                                     )
                                     
                                     INSERT INTO #Temp
                                     SELECT
                                     '<h4>'+n.Name+'</h4>' AS Nag,
                                     m.MenuId, 
                                     m.ParentId,
                                     CASE WHEN m.ParentId IN (SELECT menuid FROM menu WHERE ParentId IS NULL) AND m.ParentId IS NOT NULL THEN '<b>'+ m.Name + '</b><br>'
                                     ELSE m.Name +'<br>'
                                     END as Category
                                     FROM menu m
                                     JOIN (SELECT * FROM menu WHERE ParentId IS NULL) n ON n.MenuId=m.ParentId
                                     WHERE m.ParentId IS NOT NULL AND m.ParentId=@Id
                                     
                                     SELECT 
                                     t.Category + REPLACE(REPLACE(REPLACE(STUFF((
                                     SELECT ';' + '!' + m1.Link + '$' + m1.Name + ''
                                     FROM menu m1 
                                     JOIN #Temp t ON t.MenuId=m1.ParentId
                                     WHERE m1.parentid = m2.parentid
                                     FOR XML PATH ('')
                                                 ), 1, 1, ''),';','</a><br/>'),'!','<a runat=""server"" href='),'$','>') + '</a><br/>' AS Category
                                     FROM menu m2
                                     JOIN #Temp t ON t.MenuId=m2.ParentId 
                                     GROUP BY m2.parentid, t.Category
                                     
                                     DROP TABLE #Temp";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", i);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    string lbl = "lbl" + i;

                    Control myControl1 = (Label)FindControl(lbl.ToString());

                    Label lblResult = ((Label)ResultsPanel.FindControl(lbl));

                    while (reader.Read())
                    {
                        lblResult.Text += reader["Category"].ToString();
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

        protected void ddCategoriesSwitch_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SqlConnection conn = new SqlConnection(db);
                DataSet ds = new DataSet();

                try
                {
                    string query = @"SELECT '- Wszystkie kategorie -' Path, -1 MenuId
                                     UNION
                                     SELECT UPPER(m3.Name) +' / '
                                     + UPPER(m2.Name) + ' / '
                                     + UPPER(m1.Name) AS Path,
                                     UPPER(m1.menuid) AS MenuId
                                     FROM
                                     menu m1
                                     JOIN menu m2 ON m1.ParentId = m2.MenuId
                                     JOIN menu m3 ON m2.ParentId = m3.MenuId
                                     ORDER BY Path";

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddCategoriesSwitch.SelectedValue == "-1")
            {
                Response.Redirect($"Category.aspx?search={txtSearch.Text}");
            }
            Response.Redirect($"Category.aspx?id={ddCategoriesSwitch.SelectedValue}&search={txtSearch.Text}");
        }
    }
}