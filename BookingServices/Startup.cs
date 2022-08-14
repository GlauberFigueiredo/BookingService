using BookingService.Respository.Context;
using Microsoft.EntityFrameworkCore;

namespace BookingServices
{
    public class Startup : IStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookingContext>(opt => opt.UseInMemoryDatabase("InMemoryDatabase"));

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
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

            var context = app.Services.GetService<BookingContext>();
            StartupExtensions.PopulateInMemoryDatabase(context);

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

        public static void PopulateInMemoryDatabase(BookingContext context) 
        {
            var room1 = new BookingService.Model.Entities.Room
            {
                Id = Guid.NewGuid(),
                Floor = 1,
                Number = 1,
                Type = BookingService.Model.Enums.RoomType.DOUBLE,
                Status = BookingService.Model.Enums.RoomStatus.UNAVAILABLE
            };
            var room2 = new BookingService.Model.Entities.Room
            {
                Id = Guid.NewGuid(),
                Floor = 2,
                Number = 2,
                Type = BookingService.Model.Enums.RoomType.SINGLE,
                Status = BookingService.Model.Enums.RoomStatus.AVAILABLE
            };
            var room3 = new BookingService.Model.Entities.Room
            {
                Id = Guid.NewGuid(),
                Floor = 3,
                Number = 3,
                Type = BookingService.Model.Enums.RoomType.SUITE,
                Status = BookingService.Model.Enums.RoomStatus.UNAVAILABLE
            };

            var reservation1 = new BookingService.Model.Entities.Reservation
            {
                Id = Guid.NewGuid(),
                StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5)),
                Room = room2,
                Status = BookingService.Model.Enums.ReservationStatus.ACTIVE
            };
            var reservation2 = new BookingService.Model.Entities.Reservation
            {
                Id = Guid.NewGuid(),
                StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(8)),
                Room = room2,
                Status = BookingService.Model.Enums.ReservationStatus.ACTIVE
            };
            var reservation3 = new BookingService.Model.Entities.Reservation
            {
                Id = Guid.NewGuid(),
                StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(13)),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(16)),
                Room = room2,
                Status = BookingService.Model.Enums.ReservationStatus.ACTIVE
            };
            var reservation4 = new BookingService.Model.Entities.Reservation
            {
                Id = Guid.NewGuid(),
                StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(17)),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(18)),
                Room = room2,
                Status = BookingService.Model.Enums.ReservationStatus.CANCELLED
            };
            var reservation5 = new BookingService.Model.Entities.Reservation
            {
                Id = Guid.NewGuid(),
                StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(23)),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(25)),
                Room = room2,
                Status = BookingService.Model.Enums.ReservationStatus.ACTIVE
            };
            context.Rooms.Add(room1);
            context.SaveChanges();

        }
    }
}
