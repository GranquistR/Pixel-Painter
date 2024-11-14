using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using MyTestVueApp.Server.Configuration;
using MyTestVueApp.Server.Entities;
using MyTestVueApp.Server.Interfaces;
using MyTestVueApp.Server.ServiceImplementations;

namespace MyTestVueApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private ILogger<CommentController> Logger { get; }
        private ICommentAccessService CommentAccessService { get; }
        private IOptions<ApplicationConfiguration> AppConfig { get; }
        private ILoginService LoginService { get; }
        public CommentController(ILogger<CommentController> logger, ICommentAccessService commentAccessService, IOptions<ApplicationConfiguration> appConfig, ILoginService loginService)
        {
            Logger = logger;
            CommentAccessService = commentAccessService;
            AppConfig = appConfig;
            LoginService = loginService;
        }

        [HttpGet]
        [Route("GetCommentsByArtId")]
        public async Task<IEnumerable<Comment>> GetCommentsByArtId(int artId)
        {
            var comments = CommentAccessService.GetCommentsById(artId);

            if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
            {
                var artist = await LoginService.GetUserBySubId(userId);
                foreach (var comment in comments)
                {
                    comment.currentUserIsOwner = comment.artistId == artist.id;
                }
            }

            return comments;
        }

        [HttpPost]
        [Route("CreateComment")]
        public async Task<IActionResult> CreateComment(Comment comment)
        {
            try
            {
                if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
                {
                    var artist = await LoginService.GetUserBySubId(userId);
                    if (artist != null)
                    {
                        var result = await CommentAccessService.CreateComment(artist, comment);
                        return Ok(result);
                    }
                }
                return BadRequest("User not logged in");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }
    }
}
