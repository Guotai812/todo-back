using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.DTOs;
using TodoApp.Models;

namespace TodoApp.Controllers;

[ApiController]
[Route("/api/tasks")]
public class TaskController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TaskController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll()
    {
        var data = await _context.TaskItems.Include(t => t.category).ToListAsync();
        return Ok(data);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetById(string id)
    {
        var task = await _context.TaskItems
            .Include(t => t.category)
            .FirstOrDefaultAsync(t => t.id == id);
        if (task == null)
            return NotFound();

        return Ok(task);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create( TaskItemDTO taskDTO)
    {
        var category = await _context.Categories.FindAsync(taskDTO.categoryId);
        if (category == null)
        {
            return BadRequest(new { message = "Category not found" });
        }

        var task = new TaskItem
        {
            title = taskDTO.title,
            description = taskDTO.description,
            categoryId = taskDTO.categoryId ,
        };

        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync();

        return Ok(task);
    }
    
    [HttpPatch("{id}")]
    public async Task<ActionResult> Update(string id, TaskItemDTO taskDTO)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        
        if (taskDTO.title != null)
            task.title = taskDTO.title;

        if (taskDTO.description != null)
            task.description = taskDTO.description;
        
        if (taskDTO.categoryId != null)
            task.categoryId = taskDTO.categoryId;

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        _context.TaskItems.Remove(task);
        await _context.SaveChangesAsync();
        return Ok();
    }
    
}

    
    
    
    
    