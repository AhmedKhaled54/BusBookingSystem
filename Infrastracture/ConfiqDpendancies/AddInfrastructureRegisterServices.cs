using Data.Helper;
using Data.Identity;
using Infrastracture.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.ConfiqDpendancies
{
    public static class AddInfrastructureRegisterServices
    {
        public static IServiceCollection AddInfrastructureRegister(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("C1");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;

            })
              .AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();



            // Swagger setup
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Social Media  APi ", Version = "v1" });
                //  c.EnableAnnotations();

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });


                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            //Bind JWT settings from configuration
            var Jwt = new JWT();
            config.Bind("JWT", Jwt);
            services.AddSingleton(Jwt);

            //mail Bind 
            var mail = new MailSetting();
            config.Bind("MailSetting", mail);
            services.AddSingleton(mail);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(c =>
            {
                c.RequireHttpsMetadata = false;
                c.SaveToken = true;
                c.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Jwt.Issuer,
                    ValidAudience = Jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwt.Key)),
                    ClockSkew = TimeSpan.Zero,
                    LifetimeValidator = (notBefore, expires, token, parameters) =>
                    {
                        return expires != null && expires > DateTime.UtcNow;
                    }
                };
            });

            services.Configure<IdentityOptions>(c => c.SignIn.RequireConfirmedEmail = true);
            services.Configure<DataProtectionTokenProviderOptions>(c => c.TokenLifespan = TimeSpan.FromHours(30));

            services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
                           .AddEntityFrameworkStores<AppDbContext>()
                           .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);


            return services;
        }
    }
}
