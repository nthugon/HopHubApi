using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HopHubApi.Models;
using Microsoft.EntityFrameworkCore;
using HopHubApi.Services;
using HopHubApi.Repositories;
using Microsoft.Extensions.Logging;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace HopHubApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // SQL SERVER DOCKER CONTAINER

            //var hostname = Environment.GetEnvironmentVariable("SQLSERVER_HOST") ?? "localhost";
            //var password = Environment.GetEnvironmentVariable("SQLSERVER_SA_PASSWORD");
            //var connString = $"Data Source={hostname};Initial Catalog=HopHub;User ID=sa;Password={password};";

            //services.AddDbContext<ApiContext>(options => options.UseSqlServer(connString));


            // SQL SERVER EXPRESS ON MY MACHINE

            var connString = @"Server=.\SQLEXPRESS;Database=HopHubDB;Trusted_Connection=True;";
            services.AddDbContext<ApiContext>(options => options.UseSqlServer(connString))
                    .AddTransient<IBeerRepository, BeerRepository>()
                    .AddTransient<IBeerService, BeerService>()
                    .AddTransient<IReviewService, ReviewService>()
                    .AddTransient<IReviewRepository, ReviewRepository>()
                    .AddSingleton<Serilog.ILogger>(x => new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger());

            var info = new Info()
            {
                Title = "HopHub API",
                Version = "v1",
                Description = "CRUD Api for managing Beers and Reviews for the HopHub web app."
            };
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", info);
                    var internalModelsPath = Path.Combine(AppContext.BaseDirectory, "HopHubApi.xml");
                    options.IncludeXmlComments(internalModelsPath);
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("../swagger/v1/swagger.json", "API");
            });
        }
    }
}
