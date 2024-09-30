using MyTestVueApp.Server.Entities;
using MyTestVueApp.Server.ServiceImplementations;

namespace MyTestVueApp.Server.Interfaces
{
    public interface IArtServices
    {
        
        public interface IArtServices
        {
            /// <summary>
            /// Get the weather forecast.
            /// </summary>
            /// <returns>The weather forecast.</returns>
            public IEnumerable<ArtPainting> GetArt();
        }
    }

}

