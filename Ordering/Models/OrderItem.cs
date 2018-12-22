using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Ordering.Models
{
    public class OrderItem
    {
        [Key]
        public int orderItemId { get; set; }
        public int itemId { get; set; }  //item = dishes
        public int orderId { get; set; }
        public int num { get; set; }
        public string title { get; set; }
        public decimal price { get; set; }
        public decimal totalFree { get; set; }
    }
}