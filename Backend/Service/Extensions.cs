using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Service
{
    public static class Extensions
    {
        public static void AddBookingServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookingContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Booking")));

            services.AddScoped<IAvailableBusinessService, AvailableBusinessService>();
            //services.Configure<AuthorPublisherConfig>(config =>
            //    configuration.GetSection("AuthorPublisherConfig").Bind(config));
            //services.Configure<BookConsumerConfig>(config =>
            //    configuration.GetSection("BookConsumerConfig").Bind(config));
            //services.AddHostedService<SmartEventProcessingBookService>();
        }
    }

}