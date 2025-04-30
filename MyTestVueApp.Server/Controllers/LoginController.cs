using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Oauth2.v2;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using MyTestVueApp.Server.Configuration;
using MyTestVueApp.Server.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using MyTestVueApp.Server.ServiceImplementations;
using MyTestVueApp.Server.Entities;
using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyTestVueApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IOptions<ApplicationConfiguration> AppConfig;
        private readonly ILogger<ArtAccessController> Logger;
        private readonly ILoginService LoginService;

        public LoginController(IOptions<ApplicationConfiguration> appConfig, ILogger<ArtAccessController> logger, ILoginService loginService)
        {
            AppConfig = appConfig;
            Logger = logger;
            LoginService = loginService;
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            var returnUrl = AppConfig.Value.HomeUrl + "login/LoginRedirect";
            var url = $"https://accounts.google.com/o/oauth2/v2/auth?client_id={AppConfig.Value.ClientId}&redirect_uri={returnUrl}&scope=email profile&response_type=code&prompt=consent";

            return Redirect(url);
        }

        [HttpGet]
        [Route("LoginRedirect")]
        public async Task<IActionResult> RedirectLogin(string code, string scope, string authuser, string prompt)
        {
            var userInfo = await LoginService.GetUserId(code);

            // Add Id to cookies
            Response.Cookies.Append("GoogleOAuth", userInfo.Id, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            await LoginService.SignupActions(userInfo.Id, userInfo.Email);

            return Redirect(AppConfig.Value.HomeUrl);
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("GoogleOAuth");
            return Ok();
        }

        /// <summary>
        /// Checks if a user is logged in
        /// </summary>
        /// <returns>True if they are logged in, false otherwise</returns>
        [HttpGet]
        [Route("IsLoggedIn")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> IsLoggedIn()
        {
            try
            {
                if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
                {
                    var artist = await LoginService.GetUserBySubId(userId);
                    return Ok(artist != null);
                }
                return Ok(false);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /// <summary>
        /// Get all artists
        /// </summary>
        /// <returns>A list of artists</returns>
        [HttpGet]
        [Route("GetAllArtists")]
        [ProducesResponseType(typeof(List<Artist>), 200)]
        public async Task<IActionResult> GetAllArtists()
        {
            try
            {
                var artist = await LoginService.GetAllArtists();
                return Ok(artist);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /// <summary>
        /// Get the current user's information
        /// </summary>
        /// <returns>A artist object</returns>
        [HttpGet]
        [Route("GetCurrentUser")]
        [ProducesResponseType(typeof(Artist), 200)]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
                {
                    var artist = await LoginService.GetUserBySubId(userId);
                    return Ok(artist);
                }
                throw new AuthenticationException("User is not logged in.");
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /// <summary>
        /// Get an artist by their name
        /// </summary>
        /// <param name="name">Name of the artist</param>
        /// <returns>An artist object</returns>

        [HttpPost]
        [Route("privateSwitch")]
        public async Task<IActionResult> privateSwitch([FromBody, BindRequired]Artist artist) 
        {
            
            try
            {
                var status = await LoginService.privateSwitch(artist);
                Console.WriteLine("Hit controller");
                return Ok(status);
            } catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("privateSwitchChange")]
        public async Task<IActionResult> privateSwitchChange([FromBody, BindRequired]int artistId)
        {

            try
            {
                var status = await LoginService.privateSwitchChange(artistId);
                Console.WriteLine("Hit controller");
                return Ok(status);
        }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
}

        [HttpGet]
        [Route("GetArtistByName")]
        [ProducesResponseType(typeof(Artist), 200)]
        public async Task<IActionResult> GetArtistByName([FromQuery] string name)
        {
            try
            {
                var artist = await LoginService.GetArtistByName(name);
                return Ok(artist);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Check to see if a user is an admin
        /// </summary>
        /// <returns>True if the user is an admin, false otherwise</returns>
        [HttpGet]
        [Route("GetIsAdmin")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> GetIsAdmin()
        {
            try
            {
                // If the user is logged in
                if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
                {
                    var artist = await LoginService.GetUserBySubId(userId);
                    if(artist == null) { return Ok(false); }
                    if (artist.isAdmin)
                    {
                        return Ok(true);
                    }
                    else { return Ok(false); }
                }
                else
                {
                    throw new ArgumentException("User is not logged in");
                }
            } 
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /// <summary>
        /// Updates the current user's username
        /// </summary>
        /// <param name="newUsername">New Username</param>
        /// <returns>True if successful, false otherwise</returns>
        [HttpPut]
        [Route("UpdateUsername")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> UpdateUsername([FromQuery] string newUsername)
        {
            try
            {
                if (Request.Cookies.TryGetValue("GoogleOAuth", out var subId))
                {
                    var success = await LoginService.UpdateUsername(newUsername, subId);
                    return Ok(success);
                }
                else
                {
                    throw new AuthenticationException("User is not logged in");
                }
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /// <summary>
        /// Removes the current user from the database
        /// </summary>
        /// <param name="id">Id of the user to remove</param>
        [HttpDelete]
        [Route("DeleteArtist")]
        public async Task<IActionResult> DeleteArtist([FromQuery] int id)
        {
            try
            {
                // If the user is logged in
                if (Request.Cookies.TryGetValue("GoogleOAuth", out var userId))
                {
                    var artist = await LoginService.GetUserBySubId(userId);
                    if(artist.id == id)
                    {
                        LoginService.DeleteArtist(artist.id);
                        Response.Cookies.Delete("GoogleOAuth");
                        return Ok();
                    }
                    else if (artist.isAdmin)
                    {
                        LoginService.DeleteArtist(artist.id);
                        return Ok();
                    }
                    else {
                        throw new InvalidCredentialException("User is not allowed to preform this action");
                    }
                }
                else
                {
                    throw new AuthenticationException("User is not logged in");
                }
            }
            catch (InvalidCredentialException ex)
            {
                return Forbid(ex.Message);
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
