using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using MyTestVueApp.Server.Configuration;

namespace MyTestVueApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private IOptions<ApplicationConfiguration> AppConfig { get; }
        private ILogger<ArtAccessController> Logger { get; }


        public LoginController(IOptions<ApplicationConfiguration> appConfig, ILogger<ArtAccessController> logger)
        {
            AppConfig = appConfig;
            Logger = logger;
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            var returnUrl = AppConfig.Value.HomeUrl + "login/LoginRedirect";
            var url = $"https://accounts.google.com/o/oauth2/v2/auth?client_id={AppConfig.Value.ClientId}&redirect_uri={returnUrl}&scope=email profile&response_type=code";

            return Redirect(url);
        }

        [HttpGet]
        [Route("LoginRedirect")]
        public IActionResult RedirectLogin(string code, string scope, string authuser, string prompt)
        {
            Logger.LogInformation("code: " + code);
            Logger.LogInformation("scope: " + scope);
            Logger.LogInformation("authuser: " + authuser);
            Logger.LogInformation("prompt: " + prompt);

            return Redirect(AppConfig.Value.HomeUrl);
        }
    }
}
