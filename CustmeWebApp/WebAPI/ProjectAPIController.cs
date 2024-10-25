using Microsoft.AspNetCore.Mvc;
using CustmeWebApp.Data;
using CustmeWebApp.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

[Route("api/projects")]
[ApiController]
public class ProjectsAPIController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProjectsAPIController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/projects?skip=0&limit=10 or GET: api/projects
    [HttpGet]
    public async Task<IActionResult> GetProjects(int? skip = null, int? limit = null)
    {
        IQueryable<Project> query = _context.Projects;

        if (skip.HasValue && limit.HasValue)
        {
            limit = limit > 100 ? 100 : limit.Value;
            query = query.Skip(skip.Value).Take(limit.Value);
        }

        var projects = await query.ToListAsync();

        if (skip.HasValue && limit.HasValue)
        {
            var totalCount = await _context.Projects.CountAsync();
            var nextLink = (skip + limit < totalCount)
                ? Url.Action("GetProjects", null, new { skip = skip + limit, limit }, Request.Scheme)
                : null;

            return Ok(new
            {
                Data = projects,
                Pagination = new
                {
                    Skip = skip,
                    Limit = limit,
                    NextLink = nextLink
                }
            });
        }

        return Ok(projects);
    }


    // GET: api/projects/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }

    // POST: api/projects
    [HttpPost]
    public async Task<IActionResult> CreateProject(int id, Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProject", new { id = project.Id }, project);
    }

    // PUT: api/projects/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, Project project)
    {
        if (id != project.Id)
        {
            return BadRequest();
        }
        _context.Entry(project).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProjectExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return Ok("Project updated");
    }

    // DELETE: api/projects/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return Ok("Project deleted");
    }
    private bool ProjectExists(int id)
    {
        return _context.Projects.Any(e => e.Id == id);
    }
}
