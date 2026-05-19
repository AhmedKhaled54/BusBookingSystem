using Core.CoreConfiqurationDependancies;
using Core.MiddleWare;
using Data.Identity;
using Infrastracture.ConfiqDpendancies;
using Infrastracture.RealTime.Hubs;
using Infrastracture.SeedData;
using Microsoft.AspNetCore.Identity;
using Services.ConfiqDepedancies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();

//add configuration dependancies and register services
builder.Services
    .AddInfrastructureRegister(builder.Configuration)
    .AddInfrastructureConfiq()
    .AddServicesConfiquration(builder.Configuration)
    .AddServicesRegister()
    .AddCoreRegisterConfiq();

var app = builder.Build();

// Seed roles and users
using (var scope= app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<Role>>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    await RoleSeeder.SeedRoles(roleManager);
    await UserSeeder.SeedUsers(userManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<ErrorHandleMiddleWare>();
}

app.MapHub<NotificationHub>("/hubs/Notification");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
