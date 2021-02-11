
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivoPagamentoJudiciais.Services;
using VivoPagamentoJudiciais.Services.Selenium.VivoPagto;
using VivoPagamentoJudiciais.Services.Selenium.Adquira;
using VivoPagamentoJudiciais.Services.Selenium.API;

namespace VivoPagamentoJudiciais.ConsoleApp
{
    //static readonly IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
    class Program
    {
        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }
        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("\r\n");
            Console.WriteLine("*************************MENU*************************************");
            Console.WriteLine("\r\n");
            Console.WriteLine("1) Start Adquira Process");
            Console.WriteLine("2) Start Vivo Pagto Process");
            Console.WriteLine("3) Start API Upload Document");
            Console.WriteLine("4) Sair");
            Console.WriteLine("\r\n");
            Console.WriteLine("******************************************************************");


            Console.WriteLine("Please select one of the options above:");

            switch (Console.ReadLine())
            {
                case "1":
                    {
                        StartAdquira();
                        Console.WriteLine("Please, press any key to continue!");
                        return true;
                    }
                case "2":
                    {
                        StartVivoPagto();
                        Console.WriteLine("Please, press any key to continue!");
                        return true;
                    }
                case "3":
                    {
                        StartAPI();
                        Console.WriteLine("Please, press any key to continue!");
                        return true;
                    }
                case "4":
                    return false;
                default:
                    return true;
            }
        }

        public static void StartAPI()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var services = new ServiceCollection()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.AddNLog("nlog.config");
                })
                .AddSingleton<IConfiguration>(configuration)

                .AddTransient<IAdquiraService, AdquiraService>()
                .AddTransient<IVivoPagamentoService, VivoPagamentoService>()
                .AddTransient<IScheduleService, ScheduleService>()
                .AddTransient<IAPIService, APIService>()
                .BuildServiceProvider();

            var schedule = services.GetService<IScheduleService>();

            schedule.Startup(3);
        }
        public static void StartAdquira()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var services = new ServiceCollection()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.AddNLog("nlog.config");
                })
                .AddSingleton<IConfiguration>(configuration)

                .AddTransient<IAdquiraService, AdquiraService>()
                .AddTransient<IVivoPagamentoService, VivoPagamentoService>()
                .AddTransient<IScheduleService, ScheduleService>()
                .AddTransient<IAPIService, APIService>()
                .BuildServiceProvider();

            var schedule = services.GetService<IScheduleService>();

            schedule.Startup(1);
        }
        public static void StartVivoPagto()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var services = new ServiceCollection()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.AddNLog("nlog.config");
                })
                .AddSingleton<IConfiguration>(configuration)

                .AddTransient<IAdquiraService, AdquiraService>()
                .AddTransient<IVivoPagamentoService, VivoPagamentoService>()
                .AddTransient<IScheduleService, ScheduleService>()
                .AddTransient<IAPIService, APIService>()
                .BuildServiceProvider();

            var schedule = services.GetService<IScheduleService>();

            schedule.Startup(2);
        }

       
    }
}
