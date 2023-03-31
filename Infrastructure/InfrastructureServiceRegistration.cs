using Application.Abstracts.Hubs;
using Application.Abstracts.Storage;
using Application.Abstracts.Token;
using Infrastructure.Services;
using Infrastructure.Services.SignalR.HubServices;
using Infrastructure.Services.Storage;
using Infrastructure.Services.Token;
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
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddTransient<IPostHubService, PostHubService>();
            services.AddSignalR();
        }

        public static void Storage<T>(this IServiceCollection services) where T : Storage,IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
