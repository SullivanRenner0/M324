using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ToDo_App_M324.Logic;

namespace ToDo_App_M324.Api.Controllers;

[ApiController]
[ApiVersion("0.4")]
[Route("v0/todo")]
public class ToDoController(TodoManager manager) : ControllerBase
{
    [HttpGet(Name = "GetAll")]
    public ActionResult<Todo> GetAll()
    {
        var todos = manager.LoadTodos();
        return Ok(todos);
    }

    [HttpGet("{id:long}", Name = "GetById")]
    public ActionResult<Todo> GetById(long id)
    {
        var todo = manager.GetTodo(id);
        if (todo is null)
        {
            return NotFound();
        }
        return Ok(todo);
    }


    [HttpPost("add", Name = "Add")]
    public ActionResult<Todo> Add([FromBody] Todo todo)
    {
        var success = manager.AddTodo(todo);
        if (!success)
        {
            return BadRequest("Failed to add the ToDo item.");
        }

        return CreatedAtRoute("GetById", new { id = todo.Id }, todo);
    }


    [HttpPut("update", Name = "Update")]
    public ActionResult<Todo> Update([FromBody] Todo todo)
    {
        var success = manager.UpdateTodo(todo);
        if (!success)
        {
            return BadRequest();
        }
        return Ok(todo);
    }

    [HttpDelete("{id:long}", Name = "Delete")]
    public ActionResult Delete(long id)
    {
        var success = manager.RemoveTodo(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}
