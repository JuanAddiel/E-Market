using E_Market.Core.Application.Interface.Services;
using E_Market.Core.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            #region Dependecy
            services.AddTransient<IAnuncioServices, AnuncioServices>();
            services.AddTransient<ICategoryServices, CategoryServices>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IImagenServices, ImagenServices>();
            #endregion
        }
    }
}
