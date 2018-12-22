using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ordering.Models
{
    public class User
    {
        [Key]
        public int UserId { set; get; }
        public string UserAccount { set; get; }
        public string UserName { set; get; }
        public string UserPassword { get; set; }
        public DateTime CreateData { get; set; }
    }
}