using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace webapi
{
    public class Program
    {
      
        
      

    
   private static string HostingEnvironment => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    private static bool IsEnvironment(string environmentName) => HostingEnvironment?.ToLower() == environmentName?.ToLower() && null != environmentName;

    private static bool Development => IsEnvironment(EnvironmentName.Development);
    private static bool Production => IsEnvironment(EnvironmentName.Production);
    private static bool Staging => IsEnvironment(EnvironmentName.Staging);
        public static void Main(string[] args)
        {
            var port =(Production? 5004: 5002);
            Debug.Print("port :"+port.ToString());
            CreateWebHostBuilder(args,port).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args,int port) =>
            WebHost.CreateDefaultBuilder(args)
             .UseKestrel(options =>
        {
            options.Listen(System.Net.IPAddress.Loopback, (Production? 5004: 5002));
          /*   options.Listen(System.Net.IPAddress.Loopback, (Production? 5005: 5003), listenOptions =>
            {
                listenOptions.UseHttps("maymaF.pfx","mayma2018");
            }); */
        })
                .UseStartup<Startup>();
    }
}
