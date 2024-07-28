using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Web;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Sora.TodoList.HttpApi
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            // thiết lập BaseDirectory
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);

            // thiết lập configuration
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", reloadOnChange: true, optional: true)
                .Build();

            // Early init of NLog to allow startup and exception logging, before host is built
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            try
            {
                logger.Info("app bắt đầu khởi chạy");

                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.

                builder.Services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    });

                // thêm nlog
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseHttpsRedirection();

                app.UseAuthorization();

                app.MapControllers();

                await app.RunAsync();
                return 0;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "App gặp lỗi");
                return 1;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}