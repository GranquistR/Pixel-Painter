using Microsoft.AspNetCore.Mvc;
using MyTestVueApp.Server.Interfaces;
using MyTestVueApp.Server.Entities;

namespace MyTestVueApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SocketController : ControllerBase
    {
        private ILogger<SocketController> _Logger;
        private IConnectionManager _ConnectionManager;
        
        public SocketController(IConnectionManager connectionManager, ILogger<SocketController> logger)
        {
            this._ConnectionManager = connectionManager;
            this._Logger = logger;
        }

        [HttpGet]
        [Route("GetGroups")]
        public IEnumerable<GroupAdvert> GetGroups()
        {
            IEnumerable<GroupAdvert> groups = _ConnectionManager.GetGroupAdverts();
            return groups;
        }
    }
}
