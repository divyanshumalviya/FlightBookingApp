//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace FBAPIGateway
//{
//    public class Startup
//    {
//        // This method gets called by the runtime. Use this method to add services to the container.
//        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
//        public void ConfigureServices(IServiceCollection services)
//        {
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }

//            app.Run(async (context) =>
//            {
//                await context.Response.WriteAsync("Hello World!");
//            });
//        }
//    }
//}

using CacheManager.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace FBAPIGateway
{
    public class Startup
    {



        public Startup(IHostingEnvironment env)
        {
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath)
            .AddJsonFile("FBConfiguration.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();




            Configuration = builder.Build();
        }
        public IConfigurationRoot Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Action<ConfigurationBuilderCachePart> settings = (x) =>
            {
                x.WithMicrosoftLogging(log =>
                {
                    log.AddConsole(LogLevel.Debug);



                }).WithDictionaryHandle();
            };
            services.AddOcelot(Configuration, settings);
            services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                });
            });
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());


            await app.UseOcelot();
            

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
           
        }
    }
}