using Microsoft.Extensions.DependencyInjection;
using MovieTestSolution.DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Business.DependencyResolver
{
    public static class ServiceRegistration
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
        }
    }
}
