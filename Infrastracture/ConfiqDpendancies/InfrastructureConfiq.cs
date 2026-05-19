using Infrastracture.Abstract;
using Infrastracture.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.ConfiqDpendancies
{
    public static  class InfrastructureConfiq
    {
        public static IServiceCollection AddInfrastructureConfiq(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));


            return services;
        }
    }
}
