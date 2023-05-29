using Lec11.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Lec11.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> RegisterUser(SignUpUserModel model);
        Task<SignInResult> LoginUser(SigninModel model);
        Task LogoutUser();
    }
}