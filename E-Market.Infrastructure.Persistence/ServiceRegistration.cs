using E_Market.Core.Application.Interface.Repositories;
using E_Market.Infrastructure.Persistence.Context;
using E_Market.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            #region Context
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<E_MarketContext>(options =>
                {
                    options.UseInMemoryDatabase("Market");
                });
            }
            else
            {
                services.AddDbContext<E_MarketContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("Market")
                        , m => m.MigrationsAssembly(typeof(E_MarketContext).Assembly.FullName));
                });
            }
            #endregion

            #region Repositories
            services.AddTransient<ICategoryRepository, CategoriesRepository>();
            services.AddTransient<IAnuncioRepository, AnuncioRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IImagenRepository, ImagenRepository>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            #endregion
        }
    }
}
