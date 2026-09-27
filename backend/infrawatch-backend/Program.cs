
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace infrawatch_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: myAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000");
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();


            app.UseAuthorization();
            app.UseRouting();

            app.UseCors(myAllowSpecificOrigins);
            BuildMinimalAPIs(app);
            app.MapControllers();

            app.Lifetime.ApplicationStarted.Register(() =>
            {
                var server = app.Services.GetRequiredService<IServer>();
                var addresses = server.Features.Get<IServerAddressesFeature>();

                foreach (string address in addresses!.Addresses)
                {
                    app.Logger.LogInformation($"Application started and listening on: {address ?? "unknown address"}");
                }
            });


            app.Run();
        }

        private static void BuildMinimalAPIs(WebApplication app)
        {
            // Health check endpoint - critical for distributed systems
            app.MapGet("/health", async () =>
            {
                return new
                {
                    Status = "Healthy",
                    Timestamp = DateTime.UtcNow.ToString("O"), //ISO 8601 format time string
                    Service = "Infrawatch-backend"
                };
            }).WithName("GetHealth");

            // Basic infrastructure status endpoint
            app.MapGet("/api/infrastructure/status", () =>
            {
                var statusServices = new object[]
                {
                    new {
                        Name = "Database",
                        Status = "Running",
                        Uptime = "99.9%"
                    },
                    new {
                        Name = "Cache",
                        Status = "Running",
                        Uptime = "99.8%"
                    },
                    new {
                        Name = "Load Balancer",
                        Status = "Running",
                        Uptime = "100%"
                    }
                };

                return new
                {
                    Services = statusServices,
                    Timestamp = DateTime.UtcNow.ToString("O") //ISO 8601 format time string
                };



            }).WithName("GetInfrastructureStatus");
        }
    }
}
