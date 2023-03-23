using Microsoft.AspNetCore.Identity;
using NotesASPBlazorTask.Data.Models.Autontication;
using NotesASPBlazorTask.Data.Models;

namespace NotesASPBlazorTask.Data.Services.Authentication
{
    public interface IUserService
    {
        Task<ApplicationUser> SignIn(LoginInput credentionals);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetSingleUserAsync(string userId);
        Task<bool> UpdateUserAsync(ApplicationUser user);
        Task<bool> DeleteUserAsync(ApplicationUser user);
        Task CreateNewUser(RegisterInput regInfo);
        Task<ApplicationUser> GetCurrentUserAsync(string id);
        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string code);
    }
}
