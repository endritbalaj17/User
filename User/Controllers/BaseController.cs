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

          

            await next();

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
