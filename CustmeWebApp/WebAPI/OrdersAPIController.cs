using Microsoft.AspNetCore.Mvc;
using CustmeWebApp.Data;
using CustmeWebApp.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace CustmeWebApp.WebAPI
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/orders?skip=0&limit=10 or GET: api/orders
        [HttpGet]
        public async Task<IActionResult> GetOrders(int? skip = null, int? limit = null)
        {
            IQueryable<Order> query = _context.Orders;

            if (skip.HasValue && limit.HasValue)
            {
                limit = limit > 100 ? 100 : limit.Value;
                query = query.Skip(skip.Value).Take(limit.Value);
            }

            var orders = await query.ToListAsync();

            if (skip.HasValue && limit.HasValue)
            {
                var totalCount = await _context.Orders.CountAsync();
                var nextLink = (skip + limit < totalCount)
                    ? Url.Action("GetOrders", null, new { skip = skip + limit, limit }, Request.Scheme)
                    : null;

                return Ok(new
                {
                    Data = orders,
                    Pagination = new
                    {
                        Skip = skip,
                        Limit = limit,
                        NextLink = nextLink
                    }
                });
            }

            return Ok(orders);
        }


        // GET: api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.Include(o => o.OrderItems)
                                             .ThenInclude(oi => oi.Project)
                                             .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound("Order not found");
            }

            return Ok(order);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // PUT: api/orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            if (id != order.Id)
            {
                return BadRequest("Order ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound("Order not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Order updated");
        }

        // DELETE: api/orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.Include(o => o.OrderItems)
                                             .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return NotFound("Order not found");
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok("Order deleted");
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(o => o.Id == id);
        }
    }
}
