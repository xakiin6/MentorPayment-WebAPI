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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration,IHostingEnvironment env)
        {
            Configuration = configuration;
             HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }
      

    
    public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)

        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
              services.Configure<IISOptions>(options => 
{
    options.ForwardClientCertificate = false;
});
  services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    }));
    services.AddMvcCore().AddAuthorization().AddJsonFormatters();
    
            services.AddAuthentication("Bearer")
            .AddIdentityServerAuthentication(options =>
            {
                var _authority = "";
                 if (HostingEnvironment.IsDevelopment())
            {

             _authority=   "http://localhost:5000/";
                } else {
                  _authority = "http://ec2-35-178-179-78.eu-west-2.compute.amazonaws.com:81/";
                }
                options.Authority = _authority;
                options.RequireHttpsMetadata = false;

                options.ApiName = "api1";
            });

        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
