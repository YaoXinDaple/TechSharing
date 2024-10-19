using MediatRDomain.Abstraction;
using MediatRInfrastructure.Models;

namespace MediatRDomain.Todos
{
    public interface IToDoRepository: IRepository
    {
        Task<int> CreateAsync(ToDo toDo);
    }
}
