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
        private readonly IValidator<CreateTodoCommand> _validator;

        public CreateTodoCommandHandler(
            IToDoRepository toDoRepository, 
            IValidator<CreateTodoCommand> validator)
        {
            _toDoRepository = toDoRepository;
            _validator = validator;
        }

        public async Task<int> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);
            var todo = new ToDo(request.Title, request.Description);
            var todoId = await _toDoRepository.CreateAsync(todo);
            return todoId;
        }
    }
}
