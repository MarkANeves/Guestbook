using System;
using Guestbook.Api.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Guestbook.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var storageProvider = Configuration.GetValue<string>("StorageProvider");
            var guestbookCapacity = Configuration.GetValue<uint>("GuestbookCapacity");

            Console.WriteLine($"Using storage provider {storageProvider} with capacity {guestbookCapacity}");

            if (storageProvider.Equals("Redis", StringComparison.InvariantCultureIgnoreCase))
            {
                var connectionString = Configuration.GetValue<string>("StorageConnectionString");
                var storage = RedisGuestbookStorage.Create(connectionString, guestbookCapacity).Result;

                services.AddSingleton<IGuestbookStorage>(storage);

                Console.WriteLine("Connected and registered Redis storage");
            }
            else
            {
                services.AddSingleton<IGuestbookStorage>(new InMemoryGuestbookStorage(guestbookCapacity));
                Console.WriteLine("Registered InMemory storage");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
