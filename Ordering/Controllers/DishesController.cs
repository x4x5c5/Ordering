﻿using System;
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
    public class DishesController : ApiController
    {
        private OrderingContext db = new OrderingContext();

        // GET: api/Dishes
        public IQueryable<Dish> GetDishes()
        {
            return db.Dishes;
        }

        // GET: api/Dishes/5
        [ResponseType(typeof(Dish))]
        public async Task<IHttpActionResult> GetDish(int id)
        {
            Dish dish = await db.Dishes.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }

            return Ok(dish);
        }

        // PUT: api/Dishes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDish(int id, Dish dish)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dish.dishId)
            {
                return BadRequest();
            }

            db.Entry(dish).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishExists(id))
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

        // POST: api/Dishes
        [ResponseType(typeof(Dish))]
        public async Task<IHttpActionResult> PostDish(Dish dish)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dishes.Add(dish);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dish.dishId }, dish);
        }

        // DELETE: api/Dishes/5
        [ResponseType(typeof(Dish))]
        public async Task<IHttpActionResult> DeleteDish(int id)
        {
            Dish dish = await db.Dishes.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }

            db.Dishes.Remove(dish);
            await db.SaveChangesAsync();

            return Ok(dish);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DishExists(int id)
        {
            return db.Dishes.Count(e => e.dishId == id) > 0;
        }
    }
}