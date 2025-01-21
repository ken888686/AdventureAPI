﻿namespace AdventureAPI.Web.Controllers.Auth;

public class RegisterRequest
{
    public const string Route = "/auth/register";

    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}
