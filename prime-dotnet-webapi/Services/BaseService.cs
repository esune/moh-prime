using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Prime.Services
{
    public abstract class BaseService
    {
        protected readonly ApiDbContext _context;
        protected readonly IHttpContextAccessor _httpContext;

        protected BaseService(IServiceProvider provider)
        {
            _context = provider.GetService<ApiDbContext>();
            _httpContext = provider.GetService<IHttpContextAccessor>();
        }
    }
}
