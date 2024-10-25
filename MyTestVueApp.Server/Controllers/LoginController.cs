using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Oauth2.v2;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using MyTestVueApp.Server.Configuration;
using MyTestVueApp.Server.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace MyTestVueApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private IOptions<ApplicationConfiguration> AppConfig { get; }
        private ILogger<ArtAccessController> Logger { get; }
        private ILoginService LoginService { get; }


        public LoginController(IOptions<ApplicationConfiguration> appConfig, ILogger<ArtAccessController> logger, ILoginService loginService)
        {
            AppConfig = appConfig;
            Logger = logger;
            LoginService = loginService;
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            var returnUrl = AppConfig.Value.HomeUrl + "login/LoginRedirect";
            var url = $"https://accounts.google.com/o/oauth2/v2/auth?client_id={AppConfig.Value.ClientId}&redirect_uri={returnUrl}&scope=email profile&response_type=code&prompt=consent";

            return Redirect(url);
        }

        [HttpGet]
        [Route("LoginRedirect")]
        public async Task<IActionResult> RedirectLogin(string code, string scope, string authuser, string prompt)
        {
            var subId = await LoginService.GetUserId(code);

            // Add Id to cookies
            Response.Cookies.Append("GoogleOAuth", subId, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return Redirect(AppConfig.Value.HomeUrl);
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("GoogleOAuth");
            return Ok();
        }

        [HttpGet]
        [Route("IsLoggedIn")]
        public IActionResult IsLoggedIn()
        {
            if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
            {
                // You can add additional checks here if needed
                return Ok(!string.IsNullOrEmpty(userId));
            }
            return Ok(false);
        }

        [HttpGet]
        [Route("StoreUserSub")]
        public async Task<IActionResult> StoreUserSub()
        {
            if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
            {
                var rowsChanged = await LoginService.SendIdToDatabase(userId);


                if (rowsChanged > 0)
                {
                    return Ok();
                }
                else
                {
                    return Ok(false);
                }
            }
            else
            {
                return Ok(false);
            }
        }
    }
}
