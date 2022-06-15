using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using User.Models;
using User.Utils;
using User.Utils.General;
using User.Data;
using System.Collections.Generic;

namespace Clients.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected readonly SignInManager<IdentityUser> _signInManager;
        protected readonly UserManager<IdentityUser> _userManager;
        protected ApplicationDbContext _context;
        protected IdentityUser user;
        protected UserModel userModel;

        public BaseController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            user = await _userManager.GetUserAsync(context.HttpContext.User);
            //if (user.PasswordExpires <= DateTime.Now)
            //{
            //    ViewData["Error"] = new ErrorVM { ErrorNumber = ErrorStatus.Info, ErrorDescription = Resource.MustChangePassword };
            //    context.HttpContext.Response.Redirect("/Identity/Account/Manage/ChangePassword");
            //}
            await _signInManager.RefreshSignInAsync(user);

            ViewData["InternalUser"] = user;

            ViewData["User"] = new UserModel
            {
                Email = user.Email,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                Username = user.UserName,

                //Role =User.Claims.FirstOrDefault(t => t.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
                Role = User.Claims.FirstOrDefault(t => t.Subject.RoleClaimType == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value
            };

            //Data.General.Log log = new ();
            //var con = context.ActionDescriptor as ControllerActionDescriptor;
            //DescriptionAttribute description = ((DescriptionAttribute[])con.MethodInfo.GetCustomAttributes(typeof(DescriptionAttribute), inherit: true)).FirstOrDefault();

            //log.Action = context.HttpContext.Request.RouteValues["action"].ToString();
            //log.ActionDescription = context.HttpContext.Request.RouteValues["action"].ToString();
            //log.Berror = false;
            //log.Controller = context.HttpContext.Request.RouteValues["controller"].ToString();
            //log.InsertedDate = DateTime.Now;
            //log.UserId = User.Identity.IsAuthenticated ? user.Id : null;
            //log.Ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
            //log.HostName = context.HttpContext.Connection.RemoteIpAddress.ToString();
            //log.Description = description.Description;
            //log.Developer = description.Delevoper;
            //log.DescriptionTitle = description.Title;
            //log.HttpMethod = context.HttpContext.Request.Method;
            //log.HostName = context.HttpContext.Connection.RemoteIpAddress.ToString(); //Dns.GetHostEntry(context.HttpContext.Connection.RemoteIpAddress.ToString()).HostName;
            //log.Url = context.HttpContext.Request.GetDisplayUrl();

            //if (context.HttpContext.Request.HasFormContentType)
            //{
            //    IFormCollection form = await context.HttpContext.Request.ReadFormAsync();
            //    log.FormContent = JsonConvert.SerializeObject(form);
            //}

            //_context.Logs.Add(log);
            //await _context.SaveChangesAsync();

            await next();

            //ViewData["Error"] = TempData.Get<ErrorVM>("Error");
        }

    
        public IActionResult ChangeLang(string culture, string returnUrl = "/")
        {
            var cultureInfo = new CultureInfo(culture);
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            cultureInfo.NumberFormat.NumberGroupSeparator = ",";
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), HttpOnly = true, Secure = true }
            );
            return LocalRedirect(returnUrl);
        }

        protected LanguageEnum GetLanguage(string culture)
        {
            switch (culture)
            {
                case "sq-AL":
                    return LanguageEnum.Albania;
                case "en-GB":
                    return LanguageEnum.English;
                case "sr-Latn-RS":
                    return LanguageEnum.Serbian;
                default:
                    return LanguageEnum.Albania;
            }
        }

    }
}
