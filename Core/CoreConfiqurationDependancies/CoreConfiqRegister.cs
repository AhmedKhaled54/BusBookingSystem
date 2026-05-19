using Core.Behavior;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.CoreConfiqurationDependancies
{
    public static class CoreConfiqRegister
    {
        public static IServiceCollection AddCoreRegisterConfiq(this IServiceCollection services)
        {
            services.AddMediatR(c=>c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));//middlewarwe 
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(TransactionBehavior<,>));
            return services;
        }
    }
}
