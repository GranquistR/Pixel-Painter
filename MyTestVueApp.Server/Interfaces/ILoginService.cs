using Microsoft.AspNetCore.Mvc;

namespace MyTestVueApp.Server.Interfaces
{
    public interface ILoginService
    {
        public Task<string> GetUserId(string code);
        public Task<int> SendIdToDatabase(string subId);
    }
}
