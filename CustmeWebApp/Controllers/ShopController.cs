using CustmeWebApp.Data;
using CustmeWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustmeWebApp.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Projects
                .Include(p => p.Service);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Service)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            var similarProjects = await GetSimilarProjects(project.Id);

            ViewBag.SimilarProjects = similarProjects;

            return View(project);
        }

        private async Task<List<Project>> GetSimilarProjects(int projectId)
        {
            var currentProject = await _context.Projects
                .Include(p => p.Service)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (currentProject == null)
            {
                return new List<Project>();
            }

            return await _context.Projects
                .Where(p => p.ServiceId == currentProject.ServiceId && p.Id != currentProject.Id)
                .Take(4)
                .ToListAsync();
        }
    }
}
