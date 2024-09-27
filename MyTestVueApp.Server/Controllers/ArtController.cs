using Microsoft.AspNetCore.Mvc;
using MyTestVueApp.Server.Entities;
using MyTestVueApp.Server.Interfaces;

namespace MyTestVueApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtController
    {
        private ILogger<ArtController> ArtLog { get; }

        [HttpGet]
        [Route("GetArtById")]

        public string GetArtById(int id)
        {
            
            
            return "Ping " + id;
        }
    }

    
}
