using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyTestVueApp.Server.Entities;
using MyTestVueApp.Server.Interfaces;
using MyTestVueApp.Server.ServiceImplementations;
using System.Security.Authentication;

namespace MyTestVueApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtAccessController : ControllerBase
    {
        private ILogger<ArtAccessController> Logger { get; }
        private IArtAccessService ArtAccessService { get; }
        private ILoginService LoginService { get; }

        public ArtAccessController(ILogger<ArtAccessController> logger, IArtAccessService artAccessService, ILoginService loginService)
        {
            Logger = logger;
            ArtAccessService = artAccessService;
            LoginService = loginService;
        }

        [HttpGet]
        [Route("GetAllArt")]
        public IEnumerable<Art> GetAllArt()
        {
            return ArtAccessService.GetAllArt();
        }

        [HttpGet]
        [Route("GetArtById")]
        public async Task<Art> GetArtByIdAsync(int id)
        {
            var art = ArtAccessService.GetArtById(id);

            if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
            {
                var artist = await LoginService.GetUserBySubId(userId);

                    art.currentUserIsOwner = (art.artistId == artist.Id);
            }
            return art;
        }

        [HttpPost]
        [Route("SaveArt")]
        public async Task<IActionResult> SaveArt(Art art)
        {
            try
            {
                if (Request.Cookies.TryGetValue("GoogleOAuth", out var userSubId))
                {
                    var artist = await LoginService.GetUserBySubId(userSubId);

                    if (artist == null)
                    {
                        return BadRequest("User not logged in");
                    }

                    var result = await ArtAccessService.SaveArt(artist, art);

                    return Ok(result);
                }
                else
                {
                    return BadRequest("User not logged in");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    
    }
}
