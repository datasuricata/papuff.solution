using Microsoft.AspNetCore.Http;
using papuff.domain.Interfaces.Services.Base;
using System.Threading.Tasks;

namespace papuff.webapi.Startups {
    public class ApiUnitOfWork {
        private readonly RequestDelegate _next;

        public ApiUnitOfWork(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext) {
            await _next(httpContext);
            var core = (IServiceBase)httpContext.RequestServices.GetService(typeof(IServiceBase));
            await core.Commit();
        }
    }
}
