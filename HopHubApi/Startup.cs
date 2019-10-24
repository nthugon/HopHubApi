using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HopHubApi.Models;
using HopHubApi.Repositories;
using HopHubApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace HopHubApi
{
    /// <summary>
    /// Configures services and the app's request pipeline.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="environment">Hosting Env</param>
        public Startup(IHostingEnvironment environment)
        {
            HostingEnvironment = environment;
            var environmentName = environment.EnvironmentName;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        /// <summary>
        /// Gets a set of key/value application configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the hosting environment.
        /// </summary>
        protected IHostingEnvironment HostingEnvironment { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

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

            var connectionString = Configuration.GetConnectionString("default");
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<IHopHubDatabase, ApiContext>(options =>
                    options.UseNpgsql(connectionString, o => o.EnableRetryOnFailure(3)));

            services
                .AddTransient<IBeerRepository, BeerRepository>()
                .AddTransient<IBeerService, BeerService>()
                .AddTransient<IReviewService, ReviewService>()
                .AddTransient<IReviewRepository, ReviewRepository>()
                .AddSingleton<Serilog.ILogger>(x => new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger());

            var provider = services.BuildServiceProvider();
            var dbContext = provider.GetRequiredService<IHopHubDatabase>();
            dbContext.ExecuteDatabaseMigration();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Class that defines the mechanisms to configure and application's request pipeline.</param>
        /// <param name="env">Provides information about the web hosting environment an application is running in.</param>
        /// <param name="loggerFactory">Used to configure the logging system.</param>
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
