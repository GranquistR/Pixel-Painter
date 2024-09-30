using Microsoft.AspNetCore.Mvc;
using MyTestVueApp.Server.Entities;
using MyTestVueApp.Server.Interfaces;
using MyTestVueApp.Server.ServiceImplementations;

namespace MyTestVueApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaintingAccessController : ControllerBase
    {
        private ILogger<PaintingAccessController> Logger { get; }
        private IPaintingAccessService PaintingAccessService { get; }

        public PaintingAccessController(ILogger<PaintingAccessController> logger, IPaintingAccessService paintingAccessService)
        {
            Logger = logger;
            PaintingAccessService = paintingAccessService;
        }

        [HttpGet]
        [Route("GetAllPaintings")]
        public IEnumerable<WorkOfArt> GetAllArt()
        {
           return PaintingAccessService.GetAllPaintings();
        }
    }
}
