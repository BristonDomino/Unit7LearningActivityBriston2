using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using OrderProcessingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderProcessingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private static List<Order> orders = new List<Order>
        {
            new Order
            {
                Id = 1,
                OrderDate = DateTime.Now.AddDays(-2),
                CustomerName = "Alice Johnson",
                Products = new List<Product>
                {
                    new Product { Id = 1, Name = "Laptop", Price = 999.99m },
                    new Product { Id = 2, Name = "Mouse", Price = 25.99m }
                }
            },
            new Order
            {
                Id = 2,
                OrderDate = DateTime.Now.AddDays(-1),
                CustomerName = "Bob Smith",
                Products = new List<Product>
                {
                    new Product { Id = 3, Name = "Keyboard", Price = 49.99m }
                }
            }
        };

        // GET: api/orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            return Ok(orders);
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        // POST: api/orders
        [HttpPost]
        public ActionResult<Order> CreateOrder(Order order)
        {
            order.Id = orders.Max(o => o.Id) + 1;
            order.OrderDate = DateTime.Now;
            orders.Add(order);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        // PUT: api/orders/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateOrder(int id, Order updatedOrder)
        {
            var order = orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound();

            order.CustomerName = updatedOrder.CustomerName;
            order.Products = updatedOrder.Products;
            return NoContent();
        }

        // DELETE: api/orders/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            var order = orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound();

            orders.Remove(order);
            return NoContent();
        }
    }
}

