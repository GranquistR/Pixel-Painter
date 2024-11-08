using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyTestVueApp.Server.Entities;
using MyTestVueApp.Server.Interfaces;

namespace MyTestVueApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtAccessController : ControllerBase
    {
        private ILogger<ArtAccessController> Logger { get; }
        private IArtAccessService ArtAccessService { get; }

        public ArtAccessController(ILogger<ArtAccessController> logger, IArtAccessService artAccessService)
        {
            Logger = logger;
            ArtAccessService = artAccessService;
        }

        [HttpGet]
        [Route("GetAllArt")]
        public IEnumerable<Art> GetAllArt()
        {
            return ArtAccessService.GetAllArt();
        }

        [HttpGet]
        [Route("GetArtById")]
        public Art GetArtById(int id)
        {
            return ArtAccessService.GetArtById(id);
        }

        [HttpPost]
        [Route("SaveArt")]
        public async Task<IActionResult> SaveArt(Art art)
        {
            try
            {
                if (Request.Cookies.TryGetValue("GoogleOAuth", out var userSubId))
                {
                    var result = await ArtAccessService.SaveArt(userSubId, art);

                    return Ok(result);
                }
                else
                {
                    return BadRequest("User not logged in");
                }
            }
            catch (Exception ex)
            {
                return Problem("Failed to save, Check Server logs");
            }
        }

        [HttpGet]
        [Route("GetCommentsById")]
        public IEnumerable<Comment> GetCommentsById(int id)
        {
            return ArtAccessService.GetCommentsById(id);
        }
    }
}
