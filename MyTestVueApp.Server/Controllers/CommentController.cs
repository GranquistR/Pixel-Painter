using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Configuration.UserSecrets;
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
        public CommentController(ILogger<CommentController> logger, ICommentAccessService commentAccessService, IOptions<ApplicationConfiguration> appConfig) {
            Logger = logger;
            CommentAccessService = commentAccessService;
            AppConfig = appConfig;
        }

        

        [HttpGet]
        [Route("GetCommentsById")]
        public IEnumerable<Comment> GetCommentsById(int Artid)
        {
            return CommentAccessService.GetCommentsById(Artid);
        }

        [HttpGet]
        [Route("CheckCookietoUser")]
        public IActionResult CheckCookietoUser(string commentUserId)
        {
            if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
            {
                // You can add additional checks here if needed
                return Ok(userId == commentUserId);
            }
            return Ok(false);
        }
        [HttpGet]
        [Route("EditComment")]
        public async Task<IActionResult> EditComment(int commentId, String newMessage)
        {

            // If the user is logged in
            if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
            {
                // You can add additional checks here if needed
                var rowsChanged = await CommentAccessService.EditComment(commentId, newMessage);
                if (rowsChanged > 0) // If the comment has been sucessfuly edited
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Failed to edit comment. User may have already editied this comment.");
                }
            }
            else
            {
                return BadRequest("User is not logged in");
            }

        }


        [HttpGet]
        [Route("DeleteComment")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {

            // If the user is logged in
            if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
            {
                // You can add additional checks here if needed
                var rowsChanged = await CommentAccessService.DeleteComment(commentId);
                if (rowsChanged > 0) // If the comment has been deleted
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
        [Route("postComment")]
        public async Task<IActionResult> PostComment(String comment, int ArtId)
        {
            if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
            {
                await CommentAccessService.createComment(userId, comment, ArtId);

                return Ok();
            }
            return BadRequest("User not logged in");

        }
    }
}
