using MediatRDomain.Plans;
using MediatRDomain.Todos;
using MediatRInfrastructure.Plans;
using MediatRInfrastructure.Todos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MediatRInfrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<MediatRDbContext>(options => {
            options.UseSqlServer("Data Source=.;Initial Catalog=ToDoTestDb;Persist Security Info=True;User ID=sa;Password=123456;encrypt=false");
        });
        services.AddScoped<IToDoRepository, ToDoRepository>();
        services.AddScoped<IPlanRepository, PlanRepository>();

        return services;
    }
}
