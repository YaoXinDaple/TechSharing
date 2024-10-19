using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediatRPresentation.Todos.CreateTodo
{
    public sealed record CreateTodoCommand(string Title,string Description): IRequest<int>;
}
