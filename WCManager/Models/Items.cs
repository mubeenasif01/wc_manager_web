using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WCManager.Models
{
    public class Items
    {
        public int I_id { get; set; }
        public int I_tag_no { get; set; }
        public string I_status { get; set; }
        public int I_type_id { get; set; }
        public int I_Sprice { get; set; }
        public int I_Pprice { get; set; }
        public DateTime I_Edate { get; set; }
        public DateTime I_Sdate { get; set; }
        public int I_Karigar_id { get; set; }
        public int I_invoice_id { get; set; }
        public string I_Pic1 { get; set; }
        public string I_Pic2 { get; set; }
        public string I_Pic3 { get; set; }
        public string I_Pic4 { get; set; }
        public string I_Pic5 { get; set; }
        public int I_saleman_id { get; set; }
        public int I_Eprice { get; set; }
    }
}