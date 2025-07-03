using FitSync.BusinessServices.Intefaces;
using FitSync.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FitSync.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _svc;
    public AuthController(IAuthenticationService svc) => _svc = svc;

    [HttpPost("register")]
    public async Task<IActionResult> Register(AuthRegisterDTO dto)
        => Created(string.Empty, await _svc.RegisterAsync(dto));  

    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthLoginDTO dto)
    {
        var res = await _svc.LoginAsync(dto);
        return res.IsSuccess ? Ok(res) : Unauthorized(res);
    }
}
