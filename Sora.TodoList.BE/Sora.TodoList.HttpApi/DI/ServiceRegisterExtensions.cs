using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sora.TodoList.BL.ConstItems;
using Sora.TodoList.BL.TaskItems;
using Sora.TodoList.BL.Users;
using Sora.TodoList.BL.Users.Dtos;
using Sora.TodoList.DL.Commons;
using Sora.TodoList.DL.Data;
using Sora.TodoList.DL.Data.Repositories;

namespace Sora.TodoList.HttpApi.DI
{
    public static class ServiceRegisterExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ContextService>();

            services.AddSingleton<DbContext>();

            services.AddTransient<ITodoListRepositoryBase, TodoListRepositoryBase>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITenantRepository, TenantRepository>();
            services.AddTransient<ITokenRepository, TokenRepository>();
            services.AddTransient<ITaskItemRepository, TaskItemRepository>();
            services.AddTransient<IConstRepository, ConstRepository>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITaskItemService, TaskItemService>();
            services.AddTransient<IConstService, ConstService>();
        }

        public static void RegisterConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOption>(configuration.GetSection(JwtOption.KeyConfig));
        }
    }
}