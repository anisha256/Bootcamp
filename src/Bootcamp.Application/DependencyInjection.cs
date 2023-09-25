using Bootcamp.Application.Category.Dto.Service;
using Bootcamp.Application.Category.Interface;
using Bootcamp.Application.Item.Interface;
using Bootcamp.Application.Item.ItemServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ICategoryservice, CategoryService>();
            return services;
        }
    }
}
