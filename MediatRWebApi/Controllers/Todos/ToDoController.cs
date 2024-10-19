using MediatR;
using MediatRPresentation.Todos.CreateTodo;
using Microsoft.AspNetCore.Mvc;

namespace MediatRWebApi.Controllers.Todos
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private ISender _sender;

        public ToDoController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateTodoRequest request)
        {
            var command = new CreateTodoCommand(request.Title, request.Description);
            var todoId = await _sender.Send(command);

            return Created($"get/{todoId}",todoId);
        }
    }
}
