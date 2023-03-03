using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));

            //services.AddDbContext<BaseDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
            //services.AddSingleton<IPostRepository, PostRepository>();

            services.AddScoped<IPostWriteRepository, PostWriteRepository>();
            services.AddScoped<IPostReadRepository, PostReadRepository>(); services.AddScoped<ICommentWriteRepository, CommentWriteRepository>();
              services.AddScoped<ICommentReadRepository, CommentReadRepository>();

            return services;
        }
    }
}
