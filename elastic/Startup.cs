using System;
using elastic.Controllers;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using Serilog.Exceptions;


namespace elastic
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // Create Serilog Elasticsearch logger
            var credentials = new BasicAuthenticationCredentials("elastic", "qzKU7qkbpjEJMyzN5gBOIyl0");
            var pool = new CloudConnectionPool("test:dXMtY2VudHJhbDEuZ2NwLmNsb3VkLmVzLmlvJDljNWJkOGNhNWYzZjQ5YjBhOGRkMDRlYTVlYzc0OWRiJDM3NmUxNzlmZWM0NDQyNWQ4N2RlYjBkZGEzODhlNGMz", credentials);
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(
                    new Uri("http://192.168.99.100:9200"))
                {
                    AutoRegisterTemplate = true,
                })
                .CreateLogger();


            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ////
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog(dispose: true);
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });


            services.AddScoped<IElastic, ElasticRepo>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
