
using PharmacyAPI.DTOs.Auth;

namespace PharmacyAPI.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(AdminLoginDto dto);
    }
}