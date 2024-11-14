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
            return ArtAccessService.GetAllArt().OrderByDescending(art => art.creationDate);
        }

        [HttpGet]
        [Route("GetCurrentUsersArt")]
        public async Task<IActionResult> GetCurrentUsersArt()
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

                    var result = ArtAccessService.GetAllArt();

                    return Ok(result.Where(art => art.artistId == artist.id).OrderByDescending(art => art.creationDate));
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

        [HttpGet]
        [Route("GetArtById")]
        public async Task<Art> GetArtByIdAsync(int id)
        {
            var art = ArtAccessService.GetArtById(id);

            if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
            {
                var artist = await LoginService.GetUserBySubId(userId);

                    art.currentUserIsOwner = (art.artistId == artist.id);
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

        [HttpGet]
        [Route("IsMyArt")]
        public async Task<bool> IsMyArt(int id)
        {
            var art = ArtAccessService.GetArtById(id);
            bool ismine = false;

            if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
            {
                var artist = await LoginService.GetUserBySubId(userId);

                ismine = (art.artistId == artist.id);
            }
            return ismine;
        }


        [HttpGet]
        [Route("DeleteArt")]
        public async Task<IActionResult> DeleteArt(int ArtId)
        {

            // If the user is logged in
            if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
            {
                // You can add additional checks here if needed
                var rowsChanged = await ArtAccessService.DeleteArt(ArtId);
                if (rowsChanged > 0) // If the art has been deleted
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Failed to delte Comment");
                }
            }
            else
            {
                return BadRequest("User is not logged in");
            }

        }
        [HttpGet]
        [Route("ConfirmDelete")]
        public async Task<bool> ConfirmDelete(int id, string title)
        {
            var art = ArtAccessService.GetArtById(id);
            bool matches = false;
            if(title == art.title)
            {
                matches = true;
            }            
            return matches;
        }

    }
}
