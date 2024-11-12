using Microsoft.AspNetCore.Mvc;
using MyTestVueApp.Server.Entities;

namespace MyTestVueApp.Server.Interfaces
{
    public interface ILoginService
    {
        public Task<string> GetUserId(string code);
        public Task SendIdToDatabase(string subId);
        public Task<Artist> GetUserBySubId(string SubId);
    }
}
