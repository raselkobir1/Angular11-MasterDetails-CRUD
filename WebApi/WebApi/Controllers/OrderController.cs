using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class OrderController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Order
        public System.Object GetOrders()
        {
            var result = (from o in db.Orders
                          join c in db.Customers on o.CustomerID equals c.CustomerID
                          select new
                          {
                              o.OrderID,
                              o.OrderNo,
                              c.Name,
                              o.PMethod,
                              o.GTotal
                          }).ToList();
            return result;
        }

        // GET: api/Order/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(long id)
        {
            var order = (from o in db.Orders
                         where o.OrderID == id
                         select new
                         {
                             o.OrderID,
                             o.OrderNo,
                             o.CustomerID,
                             o.PMethod,
                             o.GTotal
                         }).FirstOrDefault();

            var orderDetails = (from oi in db.OrderItems
                                join i in db.Items on oi.ItemID equals i.ItemID
                                where oi.OrderID == id
                                select new
                                {
                                    oi.OrderID,
                                    oi.OrderItemID,
                                    oi.ItemID,
                                    ItemName = i.Name,
                                    i.Price,
                                    oi.Quantity,
                                    Total = oi.Quantity * i.Price
                                }).ToList();
           
            return Ok(new { order, orderDetails });
        }

        // PUT: api/Order/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(long id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.OrderID)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
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

        // POST: api/Order
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(Order order)
        {
            try
            {
                //----save data Order table:Master
                db.Orders.Add(order);
                db.SaveChanges();
                //----save data OrderItems table:Detail
                //foreach (var item in order.OrderItems)
                //{
                //    db.OrderItems.Add(item); 
                //}
                //db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // DELETE: api/Order/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(long id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(long id)
        {
            return db.Orders.Count(e => e.OrderID == id) > 0;
        }
    }
}