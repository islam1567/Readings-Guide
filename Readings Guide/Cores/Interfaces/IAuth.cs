using Readings_Guide.Cores.Dtos;

namespace Readings_Guide.Cores.Interfaces
{
    public interface IAuth
    {
        Task<AuthModel> RegisterAsync(RegisterModelDto model);
        Task<AuthModel> LoginAsync(LoginModelDto model);
        Task<bool> ChangePasswordAsync(string userid, ChangePasswordDto model);
    }
}
