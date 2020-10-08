using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Taxes.Core.Mappers;
using Taxes.Storage;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Taxes.Api
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
            ConfigureStorage(services);
            ConfigureMappers(services);

            services.AddControllers();
        }

        private void ConfigureStorage(IServiceCollection services)
        {
            var connectionString = Configuration.GetSection("DatabaseConnectionString").Value;

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString), "DatabaseConnectionString not found");

            services.AddDbContext<IDatabaseContext, DatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        private static void ConfigureMappers(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            var mapper = new Mapper(mapperConfig);

            services.AddSingleton<IMapper>(x => mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
