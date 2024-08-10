using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Web;
using Sora.TodoList.BL.Users.Dtos;
using Sora.TodoList.HttpApi.DI;
using Sora.TodoList.HttpApi.Filters;
using System;
using System.IO;
using System.Text;
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
            var todoListCors = "_myAllowSpecificOrigins";

            try
            {
                logger.Info("app bắt đầu khởi chạy");

                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.

                builder.Services.AddControllers(options =>
                {
                    options.Filters.Add<TodoListActionFilter>();
                })
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    });

                // thêm nlog
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                builder.Services.RegisterConfig(builder.Configuration);
                builder.Services.RegisterServices();

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Services.AddCors(options =>
                {
                    options.AddPolicy(name: todoListCors,
                                      policy =>
                                      {
                                          policy.WithOrigins(builder.Configuration["Cors"].Split(","))
                                            .AllowCredentials()
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                                      });
                });

                var jwtOption = builder.Configuration.GetSection(JwtOption.KeyConfig).Get<JwtOption>();

                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwtOption.Issuer,
                            ValidAudience = jwtOption.Issuer,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Key))
                        };
                    });

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseHttpsRedirection();

                app.UseCors(todoListCors);

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