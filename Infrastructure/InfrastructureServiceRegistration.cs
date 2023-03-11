using Application.Abstracts.Storage;
using Infrastructure.Services;
using Infrastructure.Services.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
        }

        public static void Storage<T>(this IServiceCollection services) where T : Storage,IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
