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
    public partial class _Default : Page
    {
        public static string baza = ConfigurationManager.ConnectionStrings["baza"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            PokazKategorie();
        }

        protected void PokazKategorie()
        {
            SqlConnection conn = new SqlConnection(baza);
            DataSet ds = new DataSet();

            //lab1.Text = "<h4><font color=#ffff1a>test</h4></font>";
            for (int i = 1; i < 13; i++)
            {
                try
                {
                    //string query = @"create table #Temp
                    //             (
                    //                 Nag varchar(500), 
                    //                 MenuId int, 
                    //                 ParentId int, 
                    //                 Kat varchar(500)
                    //             )

                    //             Insert Into #Temp
                    //             SELECT
                    //             '<h4>'+n.Nazwa+'</h4>' AS Nag,
                    //             m.MenuId, 
                    //             m.ParentId,
                    //             CASE WHEN m.ParentId IN (SELECT menuid FROM menu WHERE ParentId IS NULL) AND m.ParentId IS NOT NULL THEN '<b>'+ m.Nazwa + '</b><br/>'
                    //             ELSE m.Nazwa +'<br/>'
                    //             END as Kat
                    //             FROM menu m
                    //             JOIN (SELECT * FROM menu WHERE ParentId IS NULL) n ON n.MenuId=m.ParentId
                    //             WHERE m.ParentId IS NOT NULL AND m.ParentId=@Id
                    //             -------------------------------------------
                    //             SELECT --m2.parentid pp, 
                    //             t.Kat + REPLACE(STUFF((
                    //             SELECT ';' + m1.Nazwa
                    //             --t.Kat + ' ' + m.Nazwa
                    //             --t.Kat, 
                    //             FROM menu m1 
                    //             JOIN #Temp t ON t.MenuId=m1.ParentId
                    //             WHERE m1.parentid = m2.parentid
                    //             --WHERE t.ParentId=1
                    //             FOR XML PATH ('')
                    //                         ), 1, 1, ''),';','<br/>') + '<br/>' AS Kat
                    //             FROM menu m2 
                    //             JOIN #Temp t ON t.MenuId=m2.ParentId 
                    //             group by m2.parentid, t.Kat


                    //             --SELECT
                    //             --t.*,m.*
                    //             --FROM menu m 
                    //             --JOIN #Temp t ON t.MenuId=m.ParentId
                    //             -------------------------------------------

                    //             DROP TABLE #Temp";

                    string query = @"create table #Temp
                                     (
                                         Nag varchar(500), 
                                         MenuId int, 
                                         ParentId int, 
                                         Kat varchar(500)
                                     )
                                     
                                     Insert Into #Temp
                                     SELECT
                                     '<h4>'+n.Nazwa+'</h4>' AS Nag,
                                     m.MenuId, 
                                     m.ParentId,
                                     CASE WHEN m.ParentId IN (SELECT menuid FROM menu WHERE ParentId IS NULL) AND m.ParentId IS NOT NULL THEN '<b>'+ m.Nazwa + '</b><br>'
                                     ELSE m.Nazwa +'<br>'
                                     END as Kat
                                     FROM menu m
                                     JOIN (SELECT * FROM menu WHERE ParentId IS NULL) n ON n.MenuId=m.ParentId
                                     WHERE m.ParentId IS NOT NULL AND m.ParentId=@Id
                                     
                                     SELECT 
                                     t.Kat + REPLACE(REPLACE(REPLACE(STUFF((
                                     SELECT ';' + '!' + m1.Link + '$' + m1.Nazwa + ''
                                     FROM menu m1 
                                     JOIN #Temp t ON t.MenuId=m1.ParentId
                                     WHERE m1.parentid = m2.parentid
                                     FOR XML PATH ('')
                                                 ), 1, 1, ''),';','</a><br/>'),'!','<a runat=""server"" href='),'$','>') + '</a><br/>' AS Kat
                                     FROM menu m2
                                     JOIN #Temp t ON t.MenuId=m2.ParentId 
                                     group by m2.parentid, t.Kat
                                     
                                     DROP TABLE #Temp";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", i);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    string lbl = "label" + i;

                    Control myControl1 = (Label)FindControl(lbl.ToString());

                    Label lblResult = ((Label)ResultsPanel.FindControl(lbl));

                    while (reader.Read())
                    {
                        lblResult.Text += reader["Kat"].ToString();
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
}