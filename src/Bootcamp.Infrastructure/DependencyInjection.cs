﻿using Bootcamp.Application.Common.Interfaces;
using Bootcamp.Application.Common.Interfaces.Services;
using Bootcamp.Application.Item.Interface;
using Bootcamp.Application.Item.ItemServices;
using Bootcamp.Infrastructure.Persistence;
using Bootcamp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bootcamp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<BootcampDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
                , builder => builder.MigrationsAssembly(typeof(BootcampDbContext).Assembly.FullName)
                ));
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
