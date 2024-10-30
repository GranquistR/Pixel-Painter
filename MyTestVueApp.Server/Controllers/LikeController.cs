using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyTestVueApp.Server.Configuration;
using MyTestVueApp.Server.Interfaces;


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
            //Testing
            Console.WriteLine("InsertLike Endpoint Called");
        
            // If the user is logged in
            if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
            {
                // You can add additional checks here if needed
                int userIdInt = int.Parse(userId);
                var rowsChanged = await LikeService.InsertLike(artId, userIdInt);
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