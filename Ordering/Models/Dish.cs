using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Ordering.Models
{
    public class Dish
    {
        [Key]
        public int dishId { get; set; }
        public string dishName { get; set; }
        public decimal dishPrice { get; set; }
        public string dishIntroduction { get; set; }
        public string dishPhoto { get; set; }

    }
}