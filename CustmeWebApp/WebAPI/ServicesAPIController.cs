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
        //GET: api/services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            return await _context.Services.ToListAsync();
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
    [Route("api/projects")]
    public async Task<IActionResult> CreateProject([FromBody] Project project)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Перевірка чи існує послуга з таким ServiceId
        var service = await _context.Services.FindAsync(project.ServiceId);
        if (service == null)
        {
            return NotFound("Послуга з таким ServiceId не знайдена");
        }

        // Додаємо новий проект
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return Ok("Project created");
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