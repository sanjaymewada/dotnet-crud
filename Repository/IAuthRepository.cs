using System.Threading.Tasks;
using CRUD_Demo.Models;
using Microsoft.AspNetCore.Identity;

namespace CRUD_Demo.Repository
{
    public interface IAuthRepository
    {
        Task<IdentityResult> RegisterAsync(ApplicationUser user, string password);
        Task<ApplicationUser> LoginAsync(string email, string password);   // 👈 ye zaroor hona chahiye
        Task LogoutAsync();

        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);
        Task<ApplicationUser> FindByEmailAsync(string email);
    }
}
