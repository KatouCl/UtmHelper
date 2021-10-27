using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Utm.Application.Repositories.Rest;
using Utm.Application.Repositories.WorkingWithFiles;

namespace Utm.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            services.AddScoped<IRestRepository, RestService>();
            services.AddScoped<IWorkingWithFilesRepository, WorkingWithFilesService>();

            return services;
        }
    }
}