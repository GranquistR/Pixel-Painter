using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyTestVueApp.Server.Configuration;
using MyTestVueApp.Server.Interfaces;
using MyTestVueApp.Server.ServiceImplementations;


namespace MyTestVueApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LikeController : ControllerBase
    {
        private IOptions<ApplicationConfiguration> AppConfig { get; }
        private ILogger<LikeController> Logger { get; }
        private ILikeService LikeService { get; }
        private ILoginService LoginService { get; }

        public LikeController(IOptions<ApplicationConfiguration> appConfig, ILogger<LikeController> logger, ILikeService likeService, ILoginService loginService)
        {
            AppConfig = appConfig;
            Logger = logger;
            LikeService = likeService;
            LoginService = loginService;
        }

        [HttpGet]
        [Route("InsertLike")]
        public async Task<IActionResult> InsertLike(int artId)
        {

            // If the user is logged in
            if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
            {
                var artist = await LoginService.GetUserBySubId(userId);
                if (artist != null)
                {
                    // You can add additional checks here if needed
                    var rowsChanged = await LikeService.InsertLike(artId, artist);
                    if (rowsChanged > 0) // If the like has sucessfully been inserted
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Failed to insert like. User may have already liked this post.");
                    }
                }
                else
                {
                    return BadRequest("User is not logged in!");
                }
            }
            else
            {
                return BadRequest("User is not logged in!");
            }

        }

        [HttpGet]
        [Route("RemoveLike")]
        public async Task<IActionResult> RemoveLike(int artId)
        {

            // If the user is logged in
            if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
            {
                var artist = await LoginService.GetUserBySubId(userId);
                if (artist != null)
                {
                    // You can add additional checks here if needed
                    var rowsChanged = await LikeService.RemoveLike(artId, artist);
                    if (rowsChanged > 0) // If the like has sucessfully been removed
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Did not remove target like. It may not have existed.");
                    }
                }
                else
                {
                    return BadRequest("User is not logged in!");
                }
            }
            else
            {
                return BadRequest("User is not logged in!");
            }

        }

        [HttpGet]
        [Route("IsLiked")]
        public async Task<IActionResult> IsLiked(int artId)
        {
            if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
            {
                var artist = await LoginService.GetUserBySubId(userId);
                if (artist != null)
                {
                    var liked = await LikeService.IsLiked(artId, artist);
                    return Ok(liked);
                }
                else
                {
                    return Ok(false);
                }
            }
            else
            {
                return BadRequest("User is not logged in!");
            }
        }
    }
}
