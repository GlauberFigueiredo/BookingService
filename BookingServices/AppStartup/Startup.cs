using BookingService.AppStartup;
using BookingService.Respository.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BookingServices
{
    [ExcludeFromCodeCoverage]
    public class Startup : IStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            AutomapperStartupConfig.AddAutoMapper(services);

            AppServicesRegistration.RegisterServices(services);

            services.AddDbContext<BookingContext>(opt => opt.UseInMemoryDatabase("InMemoryDatabase"));
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookingContext>();
                DatabaseSeeder.PopulateInMemoryDatabase(dbContext);
            }

            app.MapControllers();
        }
    }

    public interface IStartup
    {
        IConfiguration Configuration { get; }
        void Configure(WebApplication app, IWebHostEnvironment environment);
        void ConfigureServices(IServiceCollection services);
    }

    //Extending the startup class in order to remove this responsibility from Program.CS
    public static class StartupExtensions
    {
        public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder webApplicationBuilder) where TStartup : IStartup
        {
            var startup = Activator.CreateInstance(typeof(TStartup), webApplicationBuilder.Configuration) as IStartup;
            if (startup == null) throw new ArgumentException("Startup.cs is invalid!");

            startup.ConfigureServices(webApplicationBuilder.Services);

            var app = webApplicationBuilder.Build();

            startup.Configure(app, app.Environment);

            app.Run();

            return webApplicationBuilder;
        }
    }
}
