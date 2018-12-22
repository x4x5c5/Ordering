using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Ordering.Models;

namespace Ordering.Controllers
{
    public class OrdersController : ApiController
    {
        private OrderingContext db = new OrderingContext();

        // GET: api/Orders
        /*
        public IQueryable<OrderDTO> GetOrders()
        {
            IEnumerable<OrderItem> orderItems1 = db.OrderItems.Where(b=>b.orderId==1);
            int id = 1;
            return db.Orders
                .Select(b => new OrderDTO()
                {
                    orderId = b.orderId,
                    payment = b.payment,
                    state = b.state,
                    orderItems = orderItems1
                });
            
            
        }
        */
/*
        public IHttpActionResult GetOrders()
        {
            IEnumerable<OrderItem> orderItems1 = db.OrderItems;
        
            OrderDTO orderDTO = db.Orders.Include(b => b.orderItems)
                .Select(b => new OrderDTO()
                {   
                    orderId = b.orderId,
                    payment = b.payment,
                    state = b.state,
                    orderItems = b.orderItems
                }).SingleOrDefault(b => b.orderId == 1);
            return Ok(orderDTO);

        }
        */
        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> GetOrder(int id)
        {
            Order order = await db.Orders.FindAsync(id);
           
            if (order == null)
            {
                return NotFound();
            }
            IEnumerable<OrderItem> orderItems = db.OrderItems.Where(b => b.orderId == id);
            OrderDTO orderDTO = new OrderDTO()
            {
                orderId = id,
                orderItems = orderItems,
                payment = order.payment,
                state = order.state

            };
            return Ok(orderDTO);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrder(int id, OrderDTO orderDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderDTO.orderId)
            {
                return BadRequest();
            }
           
            
       //     db.Entry(order).State = EntityState.Modified;

            try
            {
                Order order = await db.Orders.FindAsync(id);
                order.orderItems = orderDTO.orderItems;
                order.payment = orderDTO.payment;
                order.state = orderDTO.state;
                IEnumerable<OrderItem> orderItems = db.OrderItems.Where(b=> b.orderId==id);
               
                foreach (OrderItem orderItem in orderItems)
                {
                    db.OrderItems.Remove(orderItem);//其实可以不用删除的，但这里我就简化了。删除后在添加新的orderitems
        //            db.Entry(orderItem).State = EntityState.Modified;
                }

                IEnumerable<OrderItem> NewOrderItems = orderDTO.orderItems;

                foreach (OrderItem orderItem in NewOrderItems)
                {
                    db.OrderItems.Add(orderItem);//其实可以不用删除的，但这里我就简化了。删除后在添加新的orderitems
                                                    //            db.Entry(orderItem).State = EntityState.Modified;
                }
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/OrdersDTO
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> PostOrder(OrderDTO orderDTO,int UserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            IEnumerable<OrderItem> orderItems = orderDTO.orderItems;
            foreach(OrderItem orderItem in orderItems)
            {
                db.OrderItems.Add(orderItem);
            }
            List<decimal> money = new List<decimal>();
            foreach(OrderItem orderItem in orderItems)
            {
                money.Add(orderItem.price * orderItem.num);
            }
            decimal TotalMoney = money.Sum();
            Order order = new Order()
            {
                creatTime = DateTime.Now,
                orderItems = orderDTO.orderItems,
                payment = TotalMoney,
                state = 0,
                userId =UserId,
                endTime = DateTime.Now
            };
            db.Orders.Add(order);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = order.orderId }, order);
        }

        // DELETE: api/Orders/5
      //  [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> DeleteOrder(int id)
        {
            Order order = await db.Orders.SingleOrDefaultAsync(b =>b.orderId==id);
        
            if (order == null)
            {
                return NotFound();
            }
            IEnumerable<OrderItem> orderItems = db.OrderItems.Where(b => b.orderId == id);
            foreach (OrderItem orderItem in orderItems)
            {
                db.OrderItems.Remove(orderItem);
            }
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
            string result = "succeed";
            return Ok(result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.orderId == id) > 0;
        }
    }
}