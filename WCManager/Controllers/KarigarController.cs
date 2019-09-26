using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using WCManager.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Drawing;
using System.IO;


namespace WCManager.Controllers
{
    public class KarigarController : Controller
    {
        string cs = ConfigurationManager.ConnectionStrings["DBconString"].ConnectionString;
        // GET: Karigar
        public ActionResult AllKarigar()
        {
            List<Karigar> obj = new List<Karigar>();
            using (SqlConnection con = new SqlConnection(cs))
            {

                //    //Authorization
                string query = "Select * from [Karigar]";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    // opening connection  
                    con.Open();
                    cs = con.State.ToString();
                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        Karigar np = new Karigar();
                        np.K_id = (int)sdr["K_id"];
                        np.K_name = (string)sdr["K_name"];
                        np.K_phone = (string)sdr["K_phone"];
                        np.K_address = (string)sdr["K_address"];
                        obj.Add(np);

                    }
                    con.Close();
                }
            }
            return View(obj);
        }
        public ActionResult Newkarigar(Models.Karigar obj)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                String query = "insert Into [Karigar](K_name, K_phone, K_address) VALUES('" + obj.K_name + "','" + obj.K_phone + "','" + obj.K_address + "')";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    cs = con.State.ToString();
                    cmd.ExecuteNonQuery();
                }

                con.Close();
                return RedirectToAction("AllKarigar");
            }
        }
    }
}