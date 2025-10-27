using DogsHouseService.Application;
using DogsHouseService.Infrastructure;
using DogsHouseService.Infrastructure.Extensions;
using DogsHouseService.WebAPI.Factories;

namespace DogsHouseService.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<ProblemDetailsFactory>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.ApplyMigrations();

            app.Run();
        }
    }
}
