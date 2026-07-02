using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Services.RealTimeServices.NotificationServices;
using Services.Services.Authantication;
using Services.Services.CachingServices;
using Services.Services.DriverApplicationServices;
using Services.Services.EmailServices;
using Services.Services.FileServices;
using Services.Services.OtpService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ConfiqDepedancies
{
    public static class ServicesConfiqRegister
    {
        public static IServiceCollection AddServicesRegister(this IServiceCollection services)
        {

            services.AddTransient<IAuthanticationServices, AuthanticationServices>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ICachServices, CachServices>();
            services.AddTransient<IOtpServices, OtpServices>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IDriverApplicationService, DriverApplicationService>();
            services.AddTransient<INotificaionService, NotificationService>();


            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IUrlHelper>(c =>
            {
                var actioncontext = c.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = c.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actioncontext);

            });
            return services;
        }
    }
}
