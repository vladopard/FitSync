namespace FitSync.BusinessServices;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FitSync.BusinessServices.Intefaces;
using FitSync.DTOs;
using FitSync.Entities;
using FitSync.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> _users;
    private readonly SignInManager<User> _signIn;
    private readonly RoleManager<IdentityRole> _roles;
    private readonly JwtOptions _jwt;

    public AuthenticationService(
        UserManager<User> users,
        SignInManager<User> signIn,
        RoleManager<IdentityRole> roles,
        IOptions<JwtOptions> jwt)
    {
        _users = users;
        _signIn = signIn;
        _roles = roles;
        _jwt = jwt.Value;
    }

    public async Task<AuthResultDTO> RegisterAsync(AuthRegisterDTO dto)
    {
        if (await _users.FindByEmailAsync(dto.Email) is not null)
            return Fail("E‑mail already registered.");

        var user = new User
        {
            UserName = dto.UserName,
            Email = dto.Email,
            EmailConfirmed = true
        };

        var res = await _users.CreateAsync(user, dto.Password);
        if (!res.Succeeded) return Fail(res.Errors.Select(e => e.Description));

        await EnsureRoleAsync("User");
        await _users.AddToRoleAsync(user, "User");

        return await Success(user);
    }

    public async Task<AuthResultDTO> LoginAsync(AuthLoginDTO dto)
    {
        var user = await _users.FindByEmailAsync(dto.Identifier);

        if (user is null)
            user = await _users.FindByNameAsync(dto.Identifier);

        if (user is null)
            return Fail("Bad credentials.");

        var ok = await _signIn.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: false);
        if (!ok.Succeeded)
            return Fail("Bad credentials.");

        return await Success(user);
    }

    /* ---------- helpers ---------- */

    private async Task<AuthResultDTO> Success(User user)
    {
        var roles = await _users.GetRolesAsync(user);
        var (token, exp) = GenerateJwt(user, roles);
        return new(true, token, user.UserName, user.Id, exp, roles, null);
    }

    private (string Token, DateTimeOffset Expires) GenerateJwt(User u, IEnumerable<string> roles)
    {
        var creds = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)),
            SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, u.Id),
            new(JwtRegisteredClaimNames.Email, u.Email!)
        };
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var expires = DateTimeOffset.UtcNow.AddMinutes(_jwt.ExpiresMinutes);

        var token = new JwtSecurityToken(
            _jwt.Issuer, _jwt.Audience, claims,
            expires: expires.UtcDateTime,
            signingCredentials: creds);

        return (new JwtSecurityTokenHandler().WriteToken(token), expires);
    }

    private AuthResultDTO Fail(IEnumerable<string> errs)
        => new(false, null, null, null, null, null, errs);

    private AuthResultDTO Fail(string err) => Fail(new[] { err });

    private async Task EnsureRoleAsync(string role)
    {
        if (!await _roles.RoleExistsAsync(role))
            await _roles.CreateAsync(new IdentityRole(role));
    }
}
