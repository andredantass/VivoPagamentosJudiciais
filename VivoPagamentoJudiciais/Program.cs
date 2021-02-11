using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using System.IO;
using VivoPagamentoJudiciais.Data.Interfaces;
using VivoPagamentoJudiciais.Data.Repository;
using VivoPagamentoJudiciais.Service;
using VivoPagamentoJudiciais.Service.Core;
using VivoPagamentoJudiciais.Service.DePara;
using VivoPagamentoJudiciais.Service.Selenium.Adquira;
using VivoPagamentoJudiciais.Service.Selenium.Processum;
using VivoPagamentoJudiciais.Services.Selenium.VivoPagto;
using System;

namespace VivoPagamentoJudiciais
{
    class Program
    {
        static readonly IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());

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
            Console.WriteLine("Choose an option:");
            Console.WriteLine("*****");
            Console.WriteLine("*****");
            Console.WriteLine("");
            Console.WriteLine("1) Start Adquira Automation Robot");
            Console.WriteLine("2) Start Vivo Pagto Automation Robot");
            Console.WriteLine("3) Start OCR API Document Upload");
            Console.Write("\r\nSelect an option: ");

            Console.WriteLine("");
            Console.WriteLine("*****");
            Console.WriteLine("*****");

            switch (Console.ReadLine())
            {
                case "1":
                    StartAdquira();
                    return true;
                case "2":
                    StartVivoPagamento();
                    return true;
                case "3":
                    StartAPI();
                    return true;
                default:
                    return true;
            }
        }
        public static void StartVivoPagamento()
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
                .AddSingleton(memoryCache)
                //
                //.AddTransient<IRepository, Repository>()
                .AddTransient<IExcel, Excel>()
                //.AddTransient<IAdquiraService, AdquiraService>()
                .AddTransient<IVivoPagamentoService, VivoPagamentoService>()
                //.AddTransient<IProcessumService, ProcessumService>()
                .AddTransient<IScheduleService, ScheduleService>()
                .BuildServiceProvider();

            var schedule = services.GetService<IScheduleService>();

            schedule.Startup();
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
                .AddSingleton(memoryCache)
                //.AddTransient<IRepository, Repository>()
                .AddTransient<IExcel, Excel>()
                .AddTransient<IAdquiraService, AdquiraService>()
                //.AddTransient<IVivoPagamentoService, VivoPagamentoService>()
                //.AddTransient<IProcessumService, ProcessumService>()
                .AddTransient<IScheduleService, ScheduleService>()
                .BuildServiceProvider();

            var schedule = services.GetService<IScheduleService>();

            schedule.Startup();
        }
        public static void StartAPI()
        {

        }
    }
}
