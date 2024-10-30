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
    public class LikeController : ControllerBase
    {
        private IOptions<ApplicationConfiguration> AppConfig { get; }
        private ILogger<LikeController> Logger { get; }
        private ILikeService LikeService { get; }

        public LikeController(IOptions<ApplicationConfiguration> appConfig, ILogger<LikeController> logger, ILikeService likeService)
        {
            AppConfig = appConfig;
            Logger = logger;
            LikeService = likeService;
        }

        [HttpGet]
        [Route("InsertLike")]
        public async Task<IActionResult> InsertLike(int artId)
        {
            // If the user is logged in
            if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
            {
                // You can add additional checks here if needed
                var rowsChanged = await LikeService.InsertLike(artId, userId);
                if (rowsChanged > 0) // If the like has sucessfully been inserted
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
