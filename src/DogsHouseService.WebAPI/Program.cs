using DogsHouseService.Application;
using DogsHouseService.Infrastructure;
using DogsHouseService.Infrastructure.Extensions;
using DogsHouseService.Infrastructure.Extensions.DI;
using DogsHouseService.WebAPI.Extensions;
using DogsHouseService.WebAPI.Factories;
using DogsHouseService.WebAPI.Middleware;
using DogsHouseService.WebAPI.Options;

namespace DogsHouseService.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.ConfigureSwaggerGen();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<ProblemDetailsFactory>();

            builder.Services.ConfigureValidatableOnStartOptions<RateLimitingSettings>();
            builder.Services.ConfigureValidatableOnStartOptions<DefaultRateLimitingPolicySettings>();

            builder.Services.AddRateLimiting(builder.Configuration);

            var app = builder.Build();

            app.UseExceptionHandlingMiddleware();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRateLimiter();

            app.MapControllers();

            app.ApplyMigrations();

            app.Run();
        }
    }
}
