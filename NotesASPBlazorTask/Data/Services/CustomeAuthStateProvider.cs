using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using NotesASPBlazorTask.Data.Models.Autontication;
using System.Security.Claims;

namespace NotesASPBlazorTask.Data.Services
{
    public class CustomeAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomeAuthStateProvider(ProtectedSessionStorage storage)
        {
            _sessionStorage = storage;
        }

        /// <summary>
        /// Gets the authentication state of the user.
        /// </summary>
        /// <returns>
        /// The authentication state of the user.
        /// </returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSession = await _sessionStorage.GetAsync<UserSession>("UserSession");
                var session = userSession.Success ? userSession.Value : null;

                if (session is null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));

                var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, session.Id),
                new Claim(ClaimTypes.Name, session.UserName),
                new Claim("FirstName", session.FirstName),
                new Claim("LastName", session.LastName)
            }, "CustomeAuth"));
                return await Task.FromResult(new AuthenticationState(claimPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        /// <summary>
        /// Updates the authentication state of the application.
        /// </summary>
        /// <param name="userSession">The user session.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public async Task UpdateAuthState(UserSession userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession is not null)
            {
                await _sessionStorage.SetAsync("UserSession", userSession);

                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userSession.Id),
                    new Claim(ClaimTypes.Name, userSession.UserName),
                    new Claim("FirstName", userSession.FirstName),
                    new Claim("LastName", userSession.LastName)
                }, "CustomeAuth"));
            }
            else
            {
                await _sessionStorage.DeleteAsync("UserSession");
                claimsPrincipal = _anonymous;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        /// <summary>
        /// Logs out the current user by deleting the user session and notifying the authentication state changed.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public async Task LogOut()
        {
            await _sessionStorage.DeleteAsync("UserSession");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }
    }
}
