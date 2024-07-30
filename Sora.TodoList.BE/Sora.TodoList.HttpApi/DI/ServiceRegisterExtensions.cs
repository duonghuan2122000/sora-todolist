using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sora.TodoList.BL.Users;
using Sora.TodoList.BL.Users.Dtos;
using Sora.TodoList.DL.Data;
using Sora.TodoList.DL.Data.Repositories;

namespace Sora.TodoList.HttpApi.DI
{
    public static class ServiceRegisterExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<DbContext>();

            services.AddTransient<ITodoListRepositoryBase, TodoListRepositoryBase>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IUserService, UserService>();
        }

        public static void RegisterConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOption>(configuration.GetSection(JwtOption.KeyConfig));
        }
    }
}