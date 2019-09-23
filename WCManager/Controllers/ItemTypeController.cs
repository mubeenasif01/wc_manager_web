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


namespace WCManager.Controllers
{
    [Authorize]
    public class ItemTypeController : Controller
    {
        string cs = ConfigurationManager.ConnectionStrings["DBconString"].ConnectionString;
        // GET: ItemType
        public ActionResult AllTypes()
        {
            List<Item_type> obj = new List<Item_type>();
            using (SqlConnection con = new SqlConnection(cs))
            {

                //    //Authorization
                string query = "Select * from [Item_type]";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    // opening connection  
                    con.Open();
                    cs = con.State.ToString();
                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        Item_type np = new Item_type();
                        np.I_type_id = (int)sdr["I_type_id"];
                        np.I_type_name = (string)sdr["I_type_name"];
                        obj.Add(np);

                    }
                }
            }
            return View(obj);
        }
        public ActionResult NewTypes(Models.Item_type obj)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                String query = "insert Into [Item_type](I_type_name) VALUES('"+ obj.I_type_name +"')";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    cs = con.State.ToString();
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction("AllTypes");
            }
        }
    }
}