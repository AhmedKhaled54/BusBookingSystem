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
using Services.Services.PaymentServices.Payment;
using Services.Services.PaymentServices.Paymob;
using Services.Services.RoutesServices;
using Services.Services.SeatsService;
using Services.Services.StationsServices;
using Services.Services.TicketService;
using Services.Services.TripsServices;
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
            services.AddTransient<IBusServices, BusServices>();
            services.AddSingleton<ICachUser, CachUser>();
            services.AddTransient<ITripeServices, TripeServices>();
            services.AddTransient<IBookingServices, BookingServices>();
            services.AddTransient<ISeatServices, SeatServices>();
            services.AddTransient<IPaymobServices, PaymobServices>();
            services.AddTransient<IPaymentServices,PaymentServices>();
            services.AddHttpClient();
            services.AddTransient<ITicketServices, TicketServices>();
            services.AddTransient<IStaitonServices, StaitonServices>();
            services.AddTransient<IRouteServices, RouteServices>();


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
