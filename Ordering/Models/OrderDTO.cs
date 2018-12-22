using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ordering.Models
{
    public class OrderDTO
    {
        public int orderId { set; get; }
        public int state { set; get; }
        public decimal payment { set; get; }
        public IEnumerable<OrderItem> orderItems { set; get; }
    }
}