using Microsoft.AspNetCore.Mvc;
using MyTestVueApp.Server.Entities;

namespace MyTestVueApp.Server.Interfaces
{
    public interface ILoginService
    {
        public Task<string> GetUserId(string code);
        public Task<Artist> SignupActions(string subId);
        public Task<bool> UpdateUsername(string newUsername, string subId);
        public Task<Artist> GetUserBySubId(string SubId);
    }
}
