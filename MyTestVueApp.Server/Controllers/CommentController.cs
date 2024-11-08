using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
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
        public CommentController(ILogger<CommentController> logger, ICommentAccessService commentAccessService) {
            Logger = logger;
            CommentAccessService = commentAccessService;
        }

        [HttpGet]
        [Route("IsLoggedIn")]
        public IActionResult IsLoggedIn()
        {
            if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
            {
                // You can add additional checks here if needed
                return Ok(!string.IsNullOrEmpty(userId));
            }
            return Ok(false);
        }

        [HttpGet]
        [Route("GetCommentsById")]
        public IEnumerable<Comment> GetCommentsById(int id)
        {
            return CommentAccessService.GetCommentsById(id);
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
    }
}
