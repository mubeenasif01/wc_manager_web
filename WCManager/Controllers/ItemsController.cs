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
    [Authorize]
    public class ItemsController : Controller
    {
        string cs = ConfigurationManager.ConnectionStrings["DBconString"].ConnectionString;
        string picAddress = "";
        //string temptag = "";
        // GET: Items
        public ActionResult AllItems()
        {



            return View();
        }

        public ActionResult NewItems()
        {
            // get categories
            List<Item_type> obj = new List<Item_type>();
            List<Karigar> kobj = new List<Karigar>();
            List<Saleman> sobj = new List<Saleman>();
           // New_Item tobj = new New_Item();
            New_Item Parent = new New_Item();
            string karigarqery = "Select * from [Karigar]";
            string itemquery = "Select * from [Item_type]";
            string salemanquery = "Select * from [Saleman]";
            string tagquery = "SELECT TOP 1 [I_tag_no] FROM [Items] ORDER BY [I_id] DESC";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(itemquery))
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
                    con.Close();
                }

                using (SqlCommand cmd = new SqlCommand(salemanquery))
                {
                    cmd.Connection = con;
                    // opening connection  
                    con.Open();
                    cs = con.State.ToString();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        Saleman snp = new Saleman();
                        snp.S_id = (int)sdr["S_id"];
                        snp.S_name = (string)sdr["S_name"];
                        sobj.Add(snp);
                    }
                    con.Close();
                }

                using (SqlCommand cmd = new SqlCommand(karigarqery))
                {
                    cmd.Connection = con;
                    // opening connection  
                    con.Open();
                    cs = con.State.ToString();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        Karigar knp = new Karigar();
                        knp.K_id = (int)sdr["K_id"];
                        knp.K_name = (string)sdr["K_name"];
                        kobj.Add(knp);
                    }
                    con.Close();
                }

                using (SqlCommand cmd = new SqlCommand(tagquery))
                {
                    cmd.Connection = con;
                    con.Open();
                    cs = con.State.ToString();
                    SqlDataReader tdr = cmd.ExecuteReader();
                    New_Item tnp = new New_Item();
                       if (tdr.Read())
                        {
                            tnp.I_tag_no = (int)tdr["I_tag_no"] + 1;
                            temptag = tnp.I_tag_no.ToString();
                        }
                        else
                        {
                            tnp.I_tag_no = 5000;
                            temptag = tnp.I_tag_no.ToString();
                        }
                    Parent.I_tag_no = tnp.I_tag_no;
                    con.Close();
                }

            }
            Parent.Iobj = obj;
            Parent.Kobj = kobj;
            Parent.Sobj = sobj;

            return View(Parent);
        }
        public ActionResult SaveItem(Models.Items ISobj)
        {
            picAddress = ISobj.I_Pic1;
            string[] seprator = { "," };
            string[] picAddressList = picAddress.Split(seprator, StringSplitOptions.RemoveEmptyEntries);
            


            using (SqlConnection con = new SqlConnection(cs))
            {
                //    //Authorization
                string query = "Insert into [Items](I_tag_no, I_status, I_type_id, I_Eprice, I_Pprice, I_Karigar_id, I_Pic1,  I_Pic2, I_Pic3, I_Pic4, I_Pic5) VALUES ('" + ISobj.I_tag_no + "','" + ISobj.I_status + "','" + ISobj.I_type_id + "','" + ISobj.I_Eprice + "','" + ISobj.I_Pprice + "','" + ISobj.I_Karigar_id + "','" + picAddressList[0] + "','" + picAddressList[1] + "','" + picAddressList[2] + "','" + picAddressList[3] + "','" + picAddressList[4] + "')";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    // opening connection  
                    con.Open();
                    cs = con.State.ToString();
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction("AllItems");
                }
        }

        [HttpPost]
        public string SaveImage(string base64image, int Number, string tag)
        {

            byte[] imageBytes = Convert.FromBase64String(base64image.Split(',')[1]);
            //string filename = DateTime.Now.ToString("ss")+ temptag.ToString() + Number.ToString() +".jpeg";
            string filename = tag + "_" + Number.ToString() + ".jpeg";
            System.IO.File.WriteAllBytes(Server.MapPath("~/Items_pics/" + filename), imageBytes);
            return filename;
        }
    }

}