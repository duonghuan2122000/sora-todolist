using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Sora.TodoList.DL.Commons
{
    public class ContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContextService(IServiceProvider serviceProvider)
        {
            _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        }

        public string? FindClaimValue(string type)
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == type)?.Value;
        }

        public string TenantId
        {
            get
            {
                return FindClaimValue("tenantid") ?? string.Empty;
            }
        }
    }
}