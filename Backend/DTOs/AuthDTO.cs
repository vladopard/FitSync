﻿namespace FitSync.DTOs;

public record AuthRegisterDTO(
    string Email,
    string Password,
    string UserName);

public record AuthLoginDTO(string Identifier, string Password);

public record AuthResultDTO(
    bool IsSuccess,
    string? Token,
    string? UserName,
    string? UserId,
    DateTimeOffset? ExpiresAt,
    IEnumerable<string>? Roles,
    IEnumerable<string>? Errors);
