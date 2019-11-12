using Microsoft.Extensions.DependencyInjection;
using System;
using RunbeckTest.Business.DependencyInjectionExtensions;
using RunbeckTest.Business.Interfaces;
using System.IO;

namespace RunbeckTest.CLI
{
    public class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var provider = services.BuildServiceProvider();
            var delimitedFileService = provider.GetService<IDelimitedFileService>();
            var resultPublishService = provider.GetService<IResultPublishService>();

            try
            {
                var result = delimitedFileService.ProcessDelimitedFile();
                resultPublishService.PublishResult(result);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Could not find the specified file.");
            }
        }

        static void ConfigureServices(IServiceCollection services)
        {
            services.AddDilimitedFileService();
        }
    }
}
