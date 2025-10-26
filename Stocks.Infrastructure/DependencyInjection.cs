using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Stocks.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<StockDBContext>(options => options.UseSqlServer(connectionString,
                                        sqloptions => sqloptions.UseCompatibilityLevel(120)
                                        ));

            return services;
        }
    }
}
