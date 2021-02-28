using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMHW9
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               })
           .ConfigureLogging(logging =>
           {
               logging.ClearProviders();
               logging.SetMinimumLevel(LogLevel.Information);
               logging.AddSerilog(new LoggerConfiguration()
                    .WriteTo.File("webTask.log")
                    .WriteTo.ColoredConsole()
                    .CreateLogger());
           }
           );
    }
}
