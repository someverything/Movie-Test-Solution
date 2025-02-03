using Microsoft.Extensions.DependencyInjection;
using MovieTestSolution.Business.Aиstract;
using MovieTestSolution.Business.Concrete;
using MovieTestSolution.DataAccess.Abstract;
using MovieTestSolution.DataAccess.Concrete.EntityFramework;
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

            services.AddScoped<IActorDAL, EFActorDAL>();
            services.AddScoped<IActorServices, ActorManager>();
        }
    }
}
