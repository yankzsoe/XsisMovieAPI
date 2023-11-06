using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XsisMovieAPI.Application.Interfaces;
using XsisMovieAPI.Infrastructure.Persistance;

namespace XsisMovieAPI.Infrastructure {
    public static class ServiceExtension {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<AppDbContext>(opt => {
                opt.UseNpgsql(configuration.GetConnectionString("postgresdb"));
                opt.EnableSensitiveDataLogging(true);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
