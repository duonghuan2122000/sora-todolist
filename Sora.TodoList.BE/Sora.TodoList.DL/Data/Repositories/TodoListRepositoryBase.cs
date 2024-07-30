using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Data.Common;

namespace Sora.TodoList.DL.Data.Repositories
{
    public interface ITodoListRepositoryBase
    {
        DbConnection GetConnection();
    }

    public class TodoListRepositoryBase : ITodoListRepositoryBase
    {
        #region Khởi tạo

        private readonly ILogger<TodoListRepositoryBase> _logger;
        protected readonly DbContext _dbContext;

        public TodoListRepositoryBase(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetService<ILogger<TodoListRepositoryBase>>() ?? NullLogger<TodoListRepositoryBase>.Instance;
            _dbContext = serviceProvider.GetRequiredService<DbContext>();
        }

        #endregion Khởi tạo

        public DbConnection GetConnection()
        {
            return _dbContext.GetConnection();
        }
    }
}