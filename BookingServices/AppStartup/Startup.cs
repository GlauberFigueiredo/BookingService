using BookingService.AppStartup;
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
                StartupExtensions.PopulateInMemoryDatabase(dbContext);
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

        public static void PopulateInMemoryDatabase(BookingContext context) 
        {
            var room1 = new BookingService.Model.Entities.Room
            {
                Id = Guid.Parse("24c7c278-110b-4bc8-932d-739b677cec77"),
                Floor = 1,
                Number = 1,
                Type = BookingService.Model.Enums.RoomType.DOUBLE,
                Status = BookingService.Model.Enums.RoomStatus.UNAVAILABLE
            };
            var room2 = new BookingService.Model.Entities.Room
            {
                Id = Guid.Parse("ccd5f291-25af-4802-bfc3-339929c9b5d9"),
                Floor = 2,
                Number = 2,
                Type = BookingService.Model.Enums.RoomType.SINGLE,
                Status = BookingService.Model.Enums.RoomStatus.AVAILABLE
            };
            var room3 = new BookingService.Model.Entities.Room
            {
                Id = Guid.Parse("37fb6cc7-313b-4b37-867d-2de3562514a9"),
                Floor = 3,
                Number = 3,
                Type = BookingService.Model.Enums.RoomType.SUITE,
                Status = BookingService.Model.Enums.RoomStatus.UNAVAILABLE
            };

            var reservation1 = new BookingService.Model.Entities.Reservation
            {
                Id = Guid.Parse("f1a7c449-323b-492c-b902-b43150ab0b7d"),
                StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5)),
                Room = room2,
                RoomId = room2.Id,
                Status = BookingService.Model.Enums.ReservationStatus.ACTIVE
            };
            var reservation2 = new BookingService.Model.Entities.Reservation
            {
                Id = Guid.Parse("329def49-4d09-4bfb-96ae-68ee5878d313"),
                StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(8)),
                Room = room2,
                RoomId = room2.Id,
                Status = BookingService.Model.Enums.ReservationStatus.ACTIVE
            };
            var reservation3 = new BookingService.Model.Entities.Reservation
            {
                Id = Guid.Parse("e2b76ff4-6bb9-4a11-9524-557a4e2f100d"),
                StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(13)),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(16)),
                Room = room2,
                RoomId = room2.Id,
                Status = BookingService.Model.Enums.ReservationStatus.ACTIVE
            };
            var reservation4 = new BookingService.Model.Entities.Reservation
            {
                Id = Guid.Parse("a8301865-454e-4d92-b60f-1f23bbfe7fd2"),
                StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(17)),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(18)),
                Room = room2,
                RoomId = room2.Id,
                Status = BookingService.Model.Enums.ReservationStatus.CANCELLED
            };
            var reservation5 = new BookingService.Model.Entities.Reservation
            {
                Id = Guid.Parse("656208a2-639a-4816-888a-e5e53f111d24"),
                StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(23)),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(25)),
                Room = room2,
                RoomId = room2.Id,
                Status = BookingService.Model.Enums.ReservationStatus.ACTIVE
            };

            var roomsList = new List<BookingService.Model.Entities.Room> { room1, room2, room3};
            var reservationsList = new List<BookingService.Model.Entities.Reservation> { reservation1, reservation2, reservation3, reservation4, reservation5 };

            context.Rooms.AddRange(roomsList);
            context.Reservations.AddRange(reservationsList);

            context.SaveChanges();

        }
    }
}
