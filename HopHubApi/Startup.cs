using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HopHubApi.Models;
using Microsoft.EntityFrameworkCore;
using HopHubApi.Services;
using HopHubApi.Repositories;

namespace HopHubApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
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
                    .AddTransient<IReviewRepository, ReviewRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
        }
    }
}
