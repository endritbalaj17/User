using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace User.Utils.General
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context /*ClientsContext db*/)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex/*, db*/).ConfigureAwait(false);
            }
        }

        private static async Task HandleExceptionMessageAsync(HttpContext context, Exception exception /*ClientsContext db*/)
        {
            //db.ChangeTracker.Clear();
            Log log = new Log
            {
                UserId = context.User.FindFirstValue(ClaimTypes.NameIdentifier),
                Action = context.Request.RouteValues["action"].ToString(),
                Url = context.Request.GetDisplayUrl(),
                Ip = context.Connection.RemoteIpAddress.ToString(),
                InsertedDate = DateTime.Now,
                BError = true,
                Exception = JsonConvert.SerializeObject(exception),
                HostName = context.Connection.RemoteIpAddress.ToString(),
                Controller = context.Request.RouteValues["controller"].ToString(),
                HttpMethod = context.Request.Method
            };
            if (context.Request.HasFormContentType)
            {
                IFormCollection form = await context.Request.ReadFormAsync();
                log.FormContent = JsonConvert.SerializeObject(form);
            }
            //db.Log.Add(log);
            //await db.SaveChangesAsync();

            if (context.Request.Headers["x-requested-with"] == "XMLHttpRequest")
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            else
                context.Response.Redirect("/Home/Error");
            // return await context.Response.WriteAsync(result);
        }
    }
}
