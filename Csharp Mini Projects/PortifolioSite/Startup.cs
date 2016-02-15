using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace PortifolioSite
{
    public class MyConfig
    {
        public string AppName { get; set; }
    }

    public class Startup
    {
        IConfiguration config;
        /// <summary>
        /// 
        /// </summary>
        public Startup(IApplicationEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.Configuration}.json", true) // a debug or release config - true means its optional.
                .AddEnvironmentVariables();
            config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInstance(new MyConfig()
            {
                AppName = config.GetSection("app:appName").Value
            });

            services.AddMvc();
            //services.AddMvc(options => options.Filters.Add(new AuthFoo()));

            // overlever lifetime
            //services.AddSingleton<IFoo,Foo>();

            // overlever kortere
            //services.AddTransiten<IFoo,Foo>();
            //services.AddScope<IFoo,Foo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run(async (context) =>
            {
                // var foo = context.RequestServices.GetRequiredService<IFoo<();
                var conf = context.RequestServices.GetRequiredService<MyConfig>();
                await context.Response.WriteAsync(conf.AppName);
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
