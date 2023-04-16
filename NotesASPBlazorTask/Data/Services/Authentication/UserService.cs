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
        /// <summary>
        /// Attempts to sign in a user with the given credentials.
        /// </summary>
        /// <param name="credentionals">The credentials to use for the sign in attempt.</param>
        /// <returns>The user if the sign in attempt was successful, otherwise null.</returns>
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
        /// <summary>
        /// Creates a new user with the given registration information.
        /// </summary>
        /// <param name="regInfo">The registration information for the new user.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
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

        /// <summary>
        /// Deletes a user from the database asynchronously.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        /// <returns>A boolean indicating whether the user was successfully deleted.</returns>
        public async Task<bool> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return true;
            return false;
        }

        /// <summary>
        /// Gets a list of all users asynchronously.
        /// </summary>
        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync() => await _userManager.Users.ToListAsync();

        /// <summary>
        /// Gets a single user by their userId.
        /// </summary>
        /// <param name="userId">The userId of the user to retrieve.</param>
        /// <returns>A Task containing the ApplicationUser object.</returns>
        public Task<ApplicationUser> GetSingleUserAsync(string userId) => _userManager.FindByIdAsync(userId);

        /// <summary>
        /// Updates an ApplicationUser object in the database.
        /// </summary>
        /// <param name="user">The ApplicationUser object to be updated.</param>
        /// <returns>A boolean value indicating whether the update was successful.</returns>
        public async Task<bool> UpdateUserAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Gets the current user asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the user.</param>
        /// <returns>The current user.</returns>
        public Task<ApplicationUser> GetCurrentUserAsync(string id) => _db.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == id);

        /// <summary>
        /// Confirms the email of the specified user with the specified code.
        /// </summary>
        /// <param name="user">The user whose email is to be confirmed.</param>
        /// <param name="code">The code used to confirm the email.</param>
        /// <returns>The result of the confirmation.</returns>
        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string code)
        {
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return result;
        }
    }
}
