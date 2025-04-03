using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using UsgphPlanning;

namespace UsgphPlanning.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        // Champ privé pour stocker l'utilisateur actuel
        private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Vérifier si l'utilisateur est authentifié
            System.Diagnostics.Debug.WriteLine($"Récupération de l'état d'authentification. Est authentifié : {_currentUser.Identity?.IsAuthenticated}");

            return Task.FromResult(new AuthenticationState(_currentUser));
        }

        public async Task MarkUserAsAuthenticated(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.NameIdentifier, user.Id?.ToString() ?? ""),
                new Claim(ClaimTypes.Authentication, "true")
            };

            // Créer une identité authentifiée
            var identity = new ClaimsIdentity(claims, "MauiAuth");
            var principal = new ClaimsPrincipal(identity);

            // Débogage
            System.Diagnostics.Debug.WriteLine($"Utilisateur authentifié : {user.Email}");

            _currentUser = principal;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        // Méthode pour se déconnecter
        public async Task MarkUserAsLoggedOut()
        {
            // Réinitialiser à un utilisateur non authentifié
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}