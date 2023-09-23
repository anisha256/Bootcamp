using Bootcamp.Application.Common.Interface.Services;
using Bootcamp.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bootcamp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            return services;
        }
    }
}
