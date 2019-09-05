using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using papuff.services.Validators.Notifications.Events;
using System;
using System.Net;
using System.Threading.Tasks;

namespace papuff.webapi.Startups {
    public class ApiException {
        private readonly RequestDelegate _next;

        public ApiException(RequestDelegate next) { _next = next; }

        public async Task InvokeAsync(HttpContext httpContext) {
            try { await _next(httpContext); } catch (Exception ex) { await HandleExceptionAsync(httpContext, ex); }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception) {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var _notify = (IEventNotifier)context.RequestServices.GetService(typeof(IEventNotifier));
            _notify.AddException<ApiException>("Ops! Algo deu errado.", exception);

            var result = _notify.GetNotifications();
            _notify.Dispose();

            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}