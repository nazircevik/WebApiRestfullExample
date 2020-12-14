    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NazirCevik.WebApiDemo.CustomMiddlewares;
using NazirCevik.WebApiDemo.DataAcces;
using NazirCevik.WebApiDemo.Formatters;

namespace NazirCevik.WebApiDemo
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
            services.AddScoped<IProductDal, EfProductDal>();
            services.AddRouting();
            services.AddMvc(options =>
            {
                options.OutputFormatters.Add(new VcardOutputFormatter());
            }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
          //  app.UseAuthentication();
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthorization();
        
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
        name: "DefaultRoot",
        pattern: "api/{controller=Products}/{action=Get}/{id?}");
            });

        }
    }
}
