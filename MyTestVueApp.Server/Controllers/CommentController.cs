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
        public IActionResult CheckCookietoUser(int commentUserId)
        {
            if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
            {
                // You can add additional checks here if needed
                return Ok(userId == commentUserId.ToString());
            }
            return Ok(false);
        }
    }
}
