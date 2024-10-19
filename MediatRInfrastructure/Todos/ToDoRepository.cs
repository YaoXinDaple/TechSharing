using MediatRDomain.Todos;
using MediatRInfrastructure.Models;

namespace MediatRInfrastructure.Todos
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly MediatRDbContext _dbContext;

        public ToDoRepository(MediatRDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(ToDo toDo)
        {
            _dbContext.ToDos.Add(toDo);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
