using Microsoft.AspNetCore.Mvc;
using CustmeWebApp.Data;
using CustmeWebApp.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

    [Route("api/services")]
    [ApiController]
    public class ServicesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServicesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

    // GET: api/services?skip=0&limit=10 or GET: api/services
    [HttpGet]
    public async Task<IActionResult> GetServices(int? skip = null, int? limit = null)
    {
        IQueryable<Service> query = _context.Services;

        if (skip.HasValue && limit.HasValue)
        {
            limit = limit > 100 ? 100 : limit.Value;
            query = query.Skip(skip.Value).Take(limit.Value);
        }

        var services = await query.ToListAsync();

        if (skip.HasValue && limit.HasValue)
        {
            var totalCount = await _context.Services.CountAsync();
            var nextLink = (skip + limit < totalCount)
                ? Url.Action("GetServices", null, new { skip = skip + limit, limit }, Request.Scheme)
                : null;

            return Ok(new
            {
                Data = services,
                Pagination = new
                {
                    Skip = skip,
                    Limit = limit,
                    NextLink = nextLink
                }
            });
        }

        return Ok(services);
    }

    [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }
    [HttpPost]
        public async Task<ActionResult<Service>> PostService(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetService", new { id = service.Id }, service);
        }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutService(int id, Service service)
    {
        if (id != service.Id)
        {
            return BadRequest();
        }

        _context.Entry(service).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ServiceExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok("Service updated");
    }

    [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return Ok("Service deleted");
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }