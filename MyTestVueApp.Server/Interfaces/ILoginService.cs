using Microsoft.AspNetCore.Mvc;

namespace MyTestVueApp.Server.Interfaces
{
    public interface ILoginService
    {
        public Task<string> GetUserId(string code);
        public Task<int> SendIdToDatabase(string subId);
        public Task<string> generateUsername();
        public Task<string> getUsername(string subId);
        public Task<int> updateUsername(string newUsername, string subId);

    }
}
