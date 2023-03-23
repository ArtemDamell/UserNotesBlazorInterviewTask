using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using NotesASPBlazorTask.Data.Models.Autontication;
using NotesASPBlazorTask.Data.Models;
using System.Text;
using NotesASPBlazorTask.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace NotesASPBlazorTask.Data.Services.Authentication
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly NavigationManager _navManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly SqlDbContext _db;


        public UserService(UserManager<ApplicationUser> userManager, NavigationManager navManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContext, SqlDbContext db)
        {
            _userManager = userManager;
            _navManager = navManager;
            _signInManager = signInManager;
            _db = db;
        }

        // SignIn functionality
        public async Task<ApplicationUser> SignIn(LoginInput credentionals)
        {
            // Try to login
            var user = _userManager.Users.FirstOrDefault(x => x.UserName == credentionals.Email);
            if (user is not null)
            {
                var uselLoginResult = await _signInManager.CheckPasswordSignInAsync(user, credentionals.Password, false);
                if (uselLoginResult.Succeeded)
                    return user;
            }
            return null;
        }

        // Implementing create user functionality
        public async Task CreateNewUser(RegisterInput regInfo)
        {
            var user = new ApplicationUser
            {
                UserName = regInfo.Email,
                Email = regInfo.Email,
                FirstName = regInfo.FirstName,
                LastName = regInfo.LastName,
                PhoneNumber = regInfo.Phone
            };
            var result = await _userManager.CreateAsync(user, regInfo.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var callbackUrl = $"ConfirmEmail/{user.Id}/{code}";

                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    _navManager.NavigateTo($"{callbackUrl}", forceLoad: true);
                else
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _navManager.NavigateTo("/");
                }
            }
        }

        public async Task<bool> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return true;
            return false;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync() => await _userManager.Users.ToListAsync();

        public Task<ApplicationUser> GetSingleUserAsync(string userId) => _userManager.FindByIdAsync(userId);

        public async Task<bool> UpdateUserAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return true;
            else
                return false;
        }

        public Task<ApplicationUser> GetCurrentUserAsync(string id) => _db.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string code)
        {
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return result;
        }
    }
}
