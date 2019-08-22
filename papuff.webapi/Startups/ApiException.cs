using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using papuff.domain.Notifications;
using System;
using System.Net;
using System.Threading.Tasks;

namespace papuff.webapi.Startups {
	public class ApiException {
		private readonly RequestDelegate _next;

		public ApiException(RequestDelegate next) {
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext) {
			try {
				await _next(httpContext);
			}
			catch (Exception ex) {
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception) {
			context.Response.ContentType = "application/json";

			if (exception?.Source == nameof(FluentValidation))
				context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
			else
				context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

			var ex = exception?.InnerException?.InnerException == null
				? exception?.Message
				: exception.InnerException.InnerException.Message;

			ex += exception?.InnerException == null
				? ""
				: $"{Environment.NewLine} Inner Exception: {Environment.NewLine}{exception.InnerException.Message}";

			var lines = ex.Contains(Environment.NewLine)
				? ex.Split(new[] {Environment.NewLine}, StringSplitOptions.None)
				: new[] {ex};

			return context.Response.WriteAsync(JsonConvert.SerializeObject(new Notification() {
				StatusCode = context.Response.StatusCode,
				Value = "Ops! Algo deu errado.",
				Key = "ApiException",
				Exception = lines,
				Call = new[] {
					$"Endpoint: {context.Request.Path.ToUriComponent()}",
					$"StackTrace: {exception?.InnerException?.StackTrace}"
				},
			}));
		}
	}
}