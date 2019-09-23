using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WCManager.Models;

namespace WCManager.Models
{
    public class New_Item
    {
        public List<Item_type> Iobj = new List<Item_type>();
        public List<Karigar> Kobj = new List<Karigar>();
        public List<Saleman> Sobj = new List<Saleman>();
        public int I_tag_no { get; set; }
    }
}