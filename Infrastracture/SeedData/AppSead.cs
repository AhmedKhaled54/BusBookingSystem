using Data.Entity;
using Infrastracture.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastracture.SeedData
{
    public static class AppSead
    {



        public static void Seed(AppDbContext context)
        {
            if (!context.Database.CanConnect())
                return;

            SeedStations(context);
            SeedRoutes(context);
            SeedRouteStations(context);
            context.SaveChanges();
        }

        private static void SeedStations(AppDbContext context)
        {
            if (context.Stations.Any()) return;


            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","Infrastracture",
                "SeedData","stations.json"));

            var json = File.ReadAllText(path);

            var stations = JsonSerializer.Deserialize<List<Stations>>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            context.Stations.AddRange(stations!);
        }

        private static void SeedRoutes(AppDbContext context)
        {
            if (context.Routes.Any()) return;

            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "Infrastracture",
               "SeedData", "Routes.json"));

            var json = File.ReadAllText(path);

            var routes = JsonSerializer.Deserialize<List<Routes>>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            // important safety
            routes!.ForEach(r => r.Id = 0);

            context.Routes.AddRange(routes);
        }
        private static void SeedRouteStations(AppDbContext context)
        {
            if (context.RouteStations.Any()) return;

            var routes = context.Routes.ToList();

            foreach (var route in routes)
            {
                context.RouteStations.Add(new RouteStations
                {
                    RouteId = route.Id,
                    StationId = route.StartStationId,
                    Order = 1,
                    ArrivalOffset = TimeSpan.Zero
                });

                context.RouteStations.Add(new RouteStations
                {
                    RouteId = route.Id,
                    StationId = route.EndStationId,
                    Order = 2,
                    ArrivalOffset = TimeSpan.FromHours(route.DisTance / 80)
                });
            }
        }
    }
}

