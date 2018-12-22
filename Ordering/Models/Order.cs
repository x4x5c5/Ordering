using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ordering.Models
{
    public class Order
    {
        [Key]
        public int orderId { get; set; }
        public decimal payment { get; set; }
        public int state { get; set; }  //表示购买状态、未支付，已支付，已完成
        public DateTime creatTime { get; set; } //订单创建时间
        public DateTime endTime { get; set; }  //结束时间
        public int userId { get; set; }  //用户id

        public IEnumerable<OrderItem> orderItems { get; set; }
    }
}