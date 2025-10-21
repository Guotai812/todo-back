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
        var data = await _context.TaskItems.ToListAsync();
        return Ok(data);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetById(string id)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task == null)
            return NotFound();

        return Ok(task);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(TaskItemDTO taskDTO)
    {
        var task = new TaskItem
        {
            title = taskDTO.title,
            description = taskDTO.description
        };

        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync();

        return Ok();
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

    
    
    
    
    