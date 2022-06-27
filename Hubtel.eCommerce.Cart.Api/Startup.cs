
using Hubtel.eCommerce.Cart.Application.Profiles;
using Hubtel.eCommerce.Cart.Application.Services;
using Hubtel.eCommerce.Cart.Infrastructre.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hubtel.eCommerce.Cart.Infrastructre;
using Hubtel.eCommerce.Cart.Infrastructre.Models;

namespace Hubtel.eCommerce.Cart.Api
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
            services.AddDbContext<ECommerceDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Hubteldb")));
            services.AddScoped<ICartService, CartService>();
            services.AddAutoMapper(typeof(CartModelProfile).Assembly);
            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            RunMigrate(app);
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void RunMigrate(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ECommerceDbContext>();

            if (context.CartModels.Any())
            {
                return;
            }

            context.CartModels.AddRange(
                new CartModel
                {
                    ItemName = "Chicken",
                    Quantity = 1,
                    UnitPrice = 4.5m,
                    Time = DateTime.UtcNow,
                    PhoneNumber = "0594536832"
                },
                new CartModel
                { 
                    ItemName = "Perfume",
                    Quantity = 1,
                    UnitPrice = 4.8m,
                    Time = DateTime.UtcNow,
                    PhoneNumber = "0248999432"
                },
                new CartModel
                {
                    ItemName = "Vanilla Ice Cream",
                    Quantity = 2,
                    UnitPrice = 5.0m,
                    Time = DateTime.UtcNow,
                    PhoneNumber = "0501444901"
                },
                new CartModel
                {
                    ItemName = "Kitchen Table",
                    Quantity = 1,
                    UnitPrice = 6.0m,
                    Time = DateTime.UtcNow,
                    PhoneNumber = "0501444901"
                },
                new CartModel
                {
                    ItemName = "Blanket",
                    Quantity = 1,
                    UnitPrice = 4.5m,
                    Time = DateTime.UtcNow,
                    PhoneNumber = "0207865402"
                },
                new CartModel
                {
                    ItemName = "Electic Stove",
                    Quantity = 1,
                    UnitPrice = 4.5m,
                    Time = DateTime.UtcNow,
                    PhoneNumber = "0555902346"
                });
            context.SaveChanges();
        }
    }
}
