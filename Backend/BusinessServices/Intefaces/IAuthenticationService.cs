using FitSync.DTOs;

namespace FitSync.BusinessServices.Intefaces
{
    public interface IAuthenticationService
    {
        Task<AuthResultDTO> LoginAsync(AuthLoginDTO dto);
        Task<AuthResultDTO> RegisterAsync(AuthRegisterDTO dto);
    }
}