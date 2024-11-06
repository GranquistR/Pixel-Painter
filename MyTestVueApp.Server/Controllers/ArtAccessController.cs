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
        public IActionResult SaveArt( Art art)
        {
            if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
            {
                art.artistId = userId;
                art.creationDate = DateTime.UtcNow;



                return Ok(art);
            }
            else
            {
                return BadRequest("User not logged in");
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
