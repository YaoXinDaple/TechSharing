using FluentValidation;
using MediatR;
using MediatRDomain.Todos;
using MediatRInfrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace MediatRPresentation.Todos.CreateTodo
{
    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, int>
    {
        private readonly IToDoRepository _toDoRepository;

        public CreateTodoCommandHandler(
            IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public async Task<int> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = new ToDo(request.Title, request.Description);
            var todoId = await _toDoRepository.CreateAsync(todo);
            return todoId;
        }
    }
}
