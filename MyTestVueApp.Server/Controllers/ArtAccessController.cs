using Microsoft.AspNetCore.Mvc;
using MyTestVueApp.Server.Entities;
using MyTestVueApp.Server.Interfaces;
using MyTestVueApp.Server.ServiceImplementations;

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
    }
}
