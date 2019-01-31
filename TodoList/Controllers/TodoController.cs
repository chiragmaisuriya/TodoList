using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;
using Services;
using TodoList.ViewModels;

namespace TodoList.Controllers
{
    [Produces("application/json")]
    [Route("api/Todo")]
    public class TodoController : Controller
    {
        private readonly ITodoService todoService;
        private readonly ITodoViewModel todoModel;

        public TodoController( ITodoService service, ITodoViewModel model)
        {
            todoService = service;
            todoModel = model;
        }

        // GET: api/Todo
        [HttpGet("{id}")]
        public async Task<ITodoViewModel> GetTodo([FromRoute] int id = 1)
        {
            todoModel.Pages = await todoService.GetPageNumber();
            todoModel.TodoList = await todoService.GetData(id);
            return todoModel;
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo([FromRoute] int id, [FromBody] Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todo.TodoId)
            {
                return BadRequest();
            }

            
            try
            {
                await todoService.Update(todo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<IActionResult> PostTodo([FromBody] Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await todoService.Create(todo);

            return CreatedAtAction("GetTodo", new { id = todo.TodoId }, todo);
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = await todoService.GetData(id);
            if (todo == null)
            {
                return NotFound();
            }

            await todoService.Delete(id);

            return Ok(todo);
        }

        private bool TodoExists(int id)
        {
            return todoService.GetById(id) != null ;
        }
    }
}