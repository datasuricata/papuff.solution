using Microsoft.AspNetCore.Builder;

namespace papuff.webapi.Startups.Kernel {
    public static class Middlewares {
        public static IApplicationBuilder MiddleException(this IApplicationBuilder builder) {
            return builder.UseMiddleware<ApiException>();
        }

        public static IApplicationBuilder UseApiUnitOfWork(this IApplicationBuilder builder) {
            return builder.UseMiddleware<ApiUnitOfWork>();
        }
    }
}