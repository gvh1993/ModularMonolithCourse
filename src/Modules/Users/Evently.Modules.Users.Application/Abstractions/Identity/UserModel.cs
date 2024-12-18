namespace Evently.Modules.Users.Application.Abstractions.Identity;

public sealed record UserModel(string Email, string Password, string FirstName, string LastName);
