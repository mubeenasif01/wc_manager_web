using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WCManager.Models
{
    public class User
    {
        public int U_id { get; set; }
        public string U_name { get; set; }
        public string U_username { get; set; }
        public string U_password { get; set; }
        public string U_role { get; set; }
    }
}